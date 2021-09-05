using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using LBot.ViewModels;
using LBot.Models;
using SkiaSharp;

namespace LBot.Templates {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class baseDataView:ContentView {
        public string selectedDay = "Mon";

        public ChartEntry[] getGraphData(string lib) {

            int[] nums;
            if (lib=="jnr") {
                nums = new int[] { 70, 40, 90 };
            } else {
                nums = new int[] { 40, 50, 100 };
            }

            ChartEntry[] entries = new[] {
                new ChartEntry(nums[0]) {
                    Label = "Before School",
                    ValueLabel = nums[0].ToString(),
                },
                new ChartEntry(nums[1]) {
                    Label = "Break 1",
                    ValueLabel = nums[1].ToString(),
                },
                new ChartEntry(nums[2]) {
                    Label = "Break 2",
                    ValueLabel = nums[2].ToString(),
                },
            };
            return entries;
        }

        public void closeButton(object sender, EventArgs args) {
            Alert.IsVisible = false;
        }

        public void resetButtons() {
            MondayButton.FontAttributes = FontAttributes.None;
            TuesdayButton.FontAttributes = FontAttributes.None;
            WednesdayButton.FontAttributes = FontAttributes.None;
            ThursdayButton.FontAttributes = FontAttributes.None;
            FridayButton.FontAttributes = FontAttributes.None;
            MondayButton.TextColor =Color.Black;
            TuesdayButton.TextColor =Color.Black;
            WednesdayButton.TextColor =Color.Black;
            ThursdayButton.TextColor =Color.Black;
            FridayButton.TextColor =Color.Black;
        }

        public void updateChart() {
            //List<int> data = new List<int>();
            int index = 0;
            switch (selectedDay) {
                case "Mon":
                    index = 0;
                    break;
                case "Tue":
                    index = 1;
                    break;
                case "Wed":
                    index=2;
                    break;
                case "Thu":
                    index=3;
                    break;
                case "Fri":
                    index=4;
                    break;
                default:
                    break;
            }
            ChartEntry[] entries = new[] {
                new ChartEntry(Trend.data[index][0]) {
                    Label = Trend.labels[0],
                    ValueLabel = Trend.data[index][0].ToString(),
                    Color = SKColor.Parse("#1E90FF")
                },
                new ChartEntry(Trend.data[index][1]) {
                    Label = Trend.labels[1],
                    ValueLabel = Trend.data[index][1].ToString(),
                    Color = SKColor.Parse("#1E90FF")
                },
                new ChartEntry(Trend.data[index][2]) {
                    Label = Trend.labels[2],
                    ValueLabel = Trend.data[index][2].ToString(),
                    Color = SKColor.Parse("#1E90FF")
                }
            };
            int max = 108;
            App.libBase lib = (Application.Current as App).libInfo;
            if (lib.currentLibrary=="snr") {
                max=84;
            }
            BarChart chart = new BarChart {
                Entries = entries,
                ValueLabelOrientation=Orientation.Horizontal,
                MaxValue=max,
                LabelOrientation=Orientation.Horizontal,
                AnimationDuration= TimeSpan.Zero,
                BackgroundColor=SKColor.Parse("#FAFAFA")
            };
            Chart.Chart = chart;
        }

        public void MonBtn(object sender, EventArgs args) {
            resetButtons();
            selectedDay = "Mon";
            MondayButton.FontAttributes = FontAttributes.Bold;
            MondayButton.TextColor = Color.White;
            updateChart();
        }

        public void TueBtn(object sender, EventArgs args) {
            resetButtons();
            selectedDay = "Tue";
            TuesdayButton.FontAttributes = FontAttributes.Bold;
            TuesdayButton.TextColor = Color.White; updateChart();
            updateChart();
        }

        public void WedBtn(object sender, EventArgs args) {
            resetButtons();
            selectedDay = "Wed";
            WednesdayButton.FontAttributes = FontAttributes.Bold;
            WednesdayButton.TextColor = Color.White;
            updateChart();
        }

        public void ThuBtn(object sender, EventArgs args) {
            resetButtons();
            selectedDay = "Thu";
            ThursdayButton.FontAttributes = FontAttributes.Bold;
            ThursdayButton.TextColor = Color.White;
            updateChart();
        }

        public void FriBtn(object sender, EventArgs args) {
            resetButtons();
            selectedDay = "Fri";
            FridayButton.FontAttributes = FontAttributes.Bold;
            FridayButton.TextColor = Color.White;
            updateChart();
        }


        public Trends Trend = new Trends();

        public baseDataView() {
            InitializeComponent();
            resetButtons();
            App.libBase lib = (Application.Current as App).libInfo;
            App.pageBase page = (Application.Current as App).pageInfo;
            string libraryName = lib.currentLibrary;

            if (lib.currentLibrary =="jnr") {
                LibraryTitle.Text="Junior Library";
                BindingContext = new jnrDynamicDataViewModel();
                MessagingCenter.Subscribe<object, Trends>(this, "jnrTrends", (sender, value) => {
                    Trend = value;
                    updateChart();
                });
            } else {
                LibraryTitle.Text="Senior Library";
                BindingContext = new snrDynamicDataViewModel();
                MessagingCenter.Subscribe<object, Trends>(this, "snrTrends", (sender, value) => {
                    Trend = value;
                    updateChart();
                });
            }

            if (page.currentPage=="home") {
                TodaysPrediction.IsVisible = false;
                FuturePredictions.IsVisible = false;
            }

            
            selectedDay = "Mon";
            MondayButton.FontAttributes = FontAttributes.Bold;
            MondayButton.TextColor = Color.White;
        }
    }
}