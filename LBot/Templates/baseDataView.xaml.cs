using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        }
    }
}