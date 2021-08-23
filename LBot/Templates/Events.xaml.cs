using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SocketIOClient;
using System.Text.Json;

namespace LBot.Templates {
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public class Event {
        public string text { get; set; }
        public int severity { get; set; }
        public string library { get; set; }
    }
    
    public class Fetcher {
        public List<Event> eventList;
        SocketIO client = (Application.Current as App).client;

        private async void connect() {
            await client.ConnectAsync();
        }

        public List<Event> GetEvents(bool jnrFilter, bool snrFilter) {
            List<Event> initialList;
            initialList = new List<Event>();
            client.On("events", response => {
                string text = response.GetValue<string>();
                initialList = JsonSerializer.Deserialize<List<Event>>(text);
            });
            connect();
            
            
            initialList.Add(new Event { severity = 0, text="Jnr: Zad Zad Zad", library="jnr"});
            initialList.Add(new Event { severity = 1, text="Snr: Foo Foo Foo", library="snr" });
            initialList.Add(new Event { severity = 1, text="Jnr: Baz Baz Baz", library="jnr" });
            initialList.Add(new Event { severity = 2, text="Snr: Bar Bar Bar", library="snr" });
            
            eventList = new List<Event>();
            for (int i = 0; i < initialList.Count(); i++) {
                if(initialList[i].library=="jnr" && jnrFilter == false) {
                    eventList.Add(initialList[i]);
                } else if(initialList[i].library=="snr" && snrFilter == false) {
                    eventList.Add(initialList[i]);
                }  
            }

            return eventList;
        }
        
    }


    public partial class Events:ContentView {


        bool JnrFilter = false;
        bool SnrFilter = false;
        private List<Event> eventList;
        Fetcher fetcher = new Fetcher();

        public void generateEventGrid(bool jnrFilter = false, bool snrFilter = false) {
            eventList = new List<Event>();
            eventList = fetcher.GetEvents(jnrFilter, snrFilter);

            int count = eventList.Count;

            eventsGrid.Children.Clear();
            eventsGrid.ColumnDefinitions.Clear();
            eventsGrid.RowDefinitions.Clear();
            
            
            

            for (int i = 0; i<count; i++) {
                eventsGrid.RowDefinitions.Add(new RowDefinition());
            }

            eventsGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width=20 });
            eventsGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i<count; i++) {
                Label EventText = new Label {
                    Text=eventList[i].text,
                    TextColor = Color.Black
                };

                Color severityColour = Color.Black;

                switch (eventList[i].severity) {
                    case 0:
                        severityColour = Color.Red;
                        break;
                    case 1:
                        severityColour = Color.Yellow;
                        break;
                    case 2:
                        severityColour = Color.Green;
                        break;
                    default:
                        break;

                }

                Label basic = new Label {
                    Text = "",
                    BackgroundColor = severityColour

                };
                eventsGrid.Children.Add(basic, 0, i);
                eventsGrid.Children.Add(EventText, 1, i);
            }
        }

        public void filterJnr(object sender, EventArgs args) {
            if (JnrFilter) {
                eventsGrid.Children.Clear();
                JnrFilter = false;
                jnrCheckBox.IsChecked = false;
                generateEventGrid(JnrFilter,SnrFilter);
                
            } else {
                jnrCheckBox.IsChecked = true;
                JnrFilter = true;
                generateEventGrid(JnrFilter, SnrFilter);
                
            }
        }

        public void filterSnr(object sender, EventArgs args) {
            if (SnrFilter) {
                eventsGrid.Children.Clear();
                snrCheckBox.IsChecked = false;
                SnrFilter = false;
                generateEventGrid(JnrFilter, SnrFilter);

            } else {
                snrCheckBox.IsChecked = true;
                SnrFilter = true;
                generateEventGrid(JnrFilter, SnrFilter);

            }
        }

        public Events() {
            App.pageBase page = (Application.Current as App).pageInfo;

            InitializeComponent();
            if (page.currentPage=="jnr") {
                generateEventGrid(false,true);
            } else if(page.currentPage=="snr") {
                generateEventGrid(true, false);
            } else {
                generateEventGrid();
            }
            if(page.currentPage=="jnr"  || page.currentPage=="snr") {
                jnrFilterButton.IsVisible = false;
                snrFilterButton.IsVisible = false;
            }
            
        }
    }
}