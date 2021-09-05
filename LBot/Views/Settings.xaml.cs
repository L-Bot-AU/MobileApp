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
            int home = Preferences.Get("home_page_choice", 1);
            if (home==1) {
                homeSwitch.IsToggled=true;
            } else {
                homeSwitch.IsToggled=false;
            }
            homeSwitch.Toggled+=ToggleHomeChoice;
            resetButtons();

            string defPage = Preferences.Get("defaultHomePage", "home");
            if (defPage == "home") {
                resetButtons();
                homebutton.TextColor=Color.White;
                homebutton.FontAttributes=FontAttributes.Bold;
            } else if (defPage =="jnr") {
                resetButtons();
                jnrbutton.TextColor=Color.White;
                jnrbutton.FontAttributes=FontAttributes.Bold;
            } else {
                resetButtons();
                snrbutton.TextColor=Color.White;
                snrbutton.FontAttributes=FontAttributes.Bold;
            }
        }

        private void ToggleHomeChoice(object sender, ToggledEventArgs e) {
            int homePage = Preferences.Get("home_page_choice", 1);
            if(homePage == 1) {
                homePage = 0;
                Preferences.Set("home_page_choice", 0);
            } else {
                homePage = 1;
                Preferences.Set("home_page_choice", 1);
            }
            MessagingCenter.Send<object,int>(this, "Home", homePage);
        }

        private void resetButtons() {
            homebutton.TextColor=Color.Black;
            jnrbutton.TextColor=Color.Black;
            snrbutton.TextColor=Color.Black;

            homebutton.FontAttributes=FontAttributes.None;
            jnrbutton.FontAttributes=FontAttributes.None;
            snrbutton.FontAttributes=FontAttributes.None;
        }

        private void HomeClicked(object sender, EventArgs e) {
            Preferences.Set("defaultHomePage", "home");
            resetButtons();
            homebutton.TextColor=Color.White;
            homebutton.FontAttributes=FontAttributes.Bold;
        }

        private void JnrClicked(object sender, EventArgs e) {
            Preferences.Set("defaultHomePage", "jnr");
            resetButtons();
            jnrbutton.TextColor=Color.White;
            jnrbutton.FontAttributes=FontAttributes.Bold;
        }

        private void SnrClicked(object sender, EventArgs e) {
            Preferences.Set("defaultHomePage", "snr");
            resetButtons();
            snrbutton.TextColor=Color.White;
            snrbutton.FontAttributes=FontAttributes.Bold;
        }
    }
}