using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SocketIOClient;

namespace LBot.Templates {
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public class Event {
        public int severity { get; set; }
        public string Text { get; set; }
        public string library {get; set;}
    }
    
    public class Fetcher {
        public List<Event> eventList;
        


        public List<Event> GetEvents(bool jnrFilter, bool snrFilter) {
            List<Event> inititialList;
            inititialList = new List<Event>();
            eventList = new List<Event>();
            inititialList.Add(new Event { severity = 0, Text="Jnr: Zad Zad Zad", library="jnr"});
            inititialList.Add(new Event { severity = 1, Text="Snr: Foo Foo Foo", library="snr" });
            inititialList.Add(new Event { severity = 1, Text="Jnr: Baz Baz Baz", library="jnr" });
            inititialList.Add(new Event { severity = 2, Text="Snr: Bar Bar Bar", library="snr" });
            for(int i = 0; i < inititialList.Count(); i++) {
                if(inititialList[i].library=="jnr" && jnrFilter == false) {
                    eventList.Add(inititialList[i]);
                } else if(inititialList[i].library=="snr"&& snrFilter == false) {
                    eventList.Add(inititialList[i]);
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
                    Text=eventList[i].Text,
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
                generateEventGrid(JnrFilter,SnrFilter);
                
            } else {
                JnrFilter = true;
                generateEventGrid(JnrFilter, SnrFilter);
                
            }
        }

        public void filterSnr(object sender, EventArgs args) {
            if (SnrFilter) {
                eventsGrid.Children.Clear();
                SnrFilter = false;
                generateEventGrid(JnrFilter, SnrFilter);

            } else {
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