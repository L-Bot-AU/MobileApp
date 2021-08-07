using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;

namespace LBot.Templates {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class baseDataView:ContentView {


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


        public baseDataView() {
            InitializeComponent();
            App.libBase lib = (Application.Current as App).libInfo;
            App.pageBase page = (Application.Current as App).pageInfo;
            string libraryName = lib.currentLibrary;

            if (lib.currentLibrary =="jnr") {
                LibraryTitle.Text="Junior Library";
                AlertText.Text="Junior library closed for stocktake";
            } else {
                LibraryTitle.Text="Senior Library";
                AlertText.Text="Year 7s detected in the senior library";
            }

            if (page.currentPage=="home") {
                TodaysPrediction.IsVisible = false;
                FuturePredictions.IsVisible = false;
            }

           
            ChartEntry[] entries =  getGraphData(libraryName);
            BarChart chart = new BarChart { Entries = entries, ValueLabelOrientation=Orientation.Horizontal, MaxValue=110, LabelOrientation=Orientation.Horizontal};
            Chart.Chart = chart;
        }

        public void closeButton(object sender, EventArgs args) {
            Alert.IsVisible = false;
        }
        
    }
}