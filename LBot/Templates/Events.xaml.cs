using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SocketIOClient;
using System.Text.Json;
using LBot.Models;

namespace LBot.Templates {
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Events:ContentView {
        bool JnrFilter = false;
        bool SnrFilter = false;
        
        public void generateEventGrid(bool jnrFilter = false, bool snrFilter = false) {
            List<Event> eventList = (Application.Current as App).eventslist;
            int count = eventList.Count;

            eventsGrid.Children.Clear();
            eventsGrid.ColumnDefinitions.Clear();
            eventsGrid.RowDefinitions.Clear();

            eventsGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width=20 });
            eventsGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i<count; i++) {
                if(eventList[i].library=="jnr" && jnrFilter || eventList[i].library=="snr" && snrFilter) {
                    continue;
                }
                eventsGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i<count; i++) {
                if (eventList[i].library=="jnr" && jnrFilter || eventList[i].library=="snr" && snrFilter) {
                    continue;
                }

                Label EventText = new Label {
                    Text=eventList[i].text,
                    TextColor = Color.Black
                };

                Color severityColour = Color.Black;

                switch (eventList[i].impact) {
                    case "high":
                        severityColour = Color.Red;
                        break;
                    case "moderate":
                        severityColour = Color.Yellow;
                        break;
                    case "low":
                        severityColour = Color.Green;
                        break;
                    default:
                        break;
                }

                BoxView test = new BoxView {
                    BackgroundColor = severityColour,
                    HeightRequest = 20,
                    WidthRequest = 20
                };

                eventsGrid.Children.Add(test, 0, i);
                eventsGrid.Children.Add(EventText, 1, i);
            }
        }
        
        public void filterJnr(object sender, EventArgs args) {
            if (JnrFilter) {
                JnrFilter = false;
            } else {
                JnrFilter = true;
            }
            generateEventGrid(JnrFilter, SnrFilter);
        }

        public void filterSnr(object sender, EventArgs args) {
            if (SnrFilter) {
                SnrFilter = false;
            } else {
                SnrFilter = true;
            }
            generateEventGrid(JnrFilter, SnrFilter);
        }

        public Events() {
            App.pageBase page = (Application.Current as App).pageInfo;
            
            InitializeComponent();
            MessagingCenter.Subscribe<object>(this, "Event", sender => {
                if (page.currentPage=="jnr") {
                    generateEventGrid(false, true);
                } else if (page.currentPage=="snr") {
                    generateEventGrid(true, false);
                } else {
                    generateEventGrid();
                }
            });

            if (page.currentPage=="jnr") {
                generateEventGrid(false, true);
            } else if (page.currentPage=="snr") {
                generateEventGrid(true, false);
            } else {
                generateEventGrid();
            }

            if (page.currentPage=="jnr"  || page.currentPage=="snr") {
                jnrFilterButton.IsVisible = false;
                snrFilterButton.IsVisible = false;
                jnrCheckBox.IsVisible = false;
                snrCheckBox.IsVisible = false;
            }
        }
    }
}