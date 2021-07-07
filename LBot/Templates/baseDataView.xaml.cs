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
        public baseDataView() {
            InitializeComponent();

            App.libBase lib = (Application.Current as App).libInfo;
            App.pageBase page = (Application.Current as App).pageInfo;

            if (lib.currentLibrary =="jnr") {
                LibraryTitle.Text="Junior Library";
                AlertText.Text="Junior Library Closed for stocktake";
            } else {
                LibraryTitle.Text="Senior Library";
                AlertText.Text="Year 7s detected in the senior library";
            }

            if (page.currentPage=="home") {
                TodaysPrediction.IsVisible = false;
                FuturePredictions.IsVisible = false;
            }



            var entries = new[] {
                new ChartEntry(70) {
                    Label = "Before School",
                     ValueLabel = "70",
                },
                new ChartEntry(40) {
                    Label = "Period 1",
                    ValueLabel = "40",
                },
                new ChartEntry(90) {
                    Label = "Period 2",
                    ValueLabel = "90",
                
                },
            };
            BarChart chart = new BarChart { Entries = entries, ValueLabelOrientation=Orientation.Horizontal, MaxValue=110, LabelOrientation=Orientation.Horizontal};
            Chart.Chart = chart;
        }
    }
}