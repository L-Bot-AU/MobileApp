using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LBot.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About:ContentPage {
        public About() {
            InitializeComponent();
        }
        protected async void gotoIssues(object sender, EventArgs e) {
            await Launcher.OpenAsync("https://github.com/L-Bot-SBHS/MobileApp/issues");
        }

        protected async void gotoWebsite(object sender, EventArgs e) {
            await Launcher.OpenAsync("https://github.com/L-Bot-SBHS");
        }
    }
}