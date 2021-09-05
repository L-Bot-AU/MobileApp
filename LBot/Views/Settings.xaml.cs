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
    public partial class Settings:ContentPage {
        public Settings() {
            InitializeComponent();
        }

        private void ToggleHomeChoice(object sender, ToggledEventArgs e) {
            int home_page = Preferences.Get("home_page_choice", 1);
            if(home_page == 1) {
                home_page = 0;
                Preferences.Set("home_page_choice", 0);
            } else {
                home_page = 1;
                Preferences.Set("home_page_choice", 1);
            }
            MessagingCenter.Send<object,int>(this, "Home", home_page);
        }
    }
}