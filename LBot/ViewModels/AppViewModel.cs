using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace LBot.ViewModels {

    public class AppViewModel : BaseViewModel {

        private bool Home0Visible;
        public bool home0Visible { get => Home0Visible; set => Home0Visible = value;}

        private bool Home1Visible;
        public bool home1Visible { get => Home1Visible; set => Home1Visible = value; }

        public AppViewModel() {
            MessagingCenter.Subscribe<object, int>(this, "Home", (sender, homePage) => {
                
                if (homePage==0) {
                    Home0Visible=true;
                    Home1Visible=false;
                } else {
                    Home0Visible=false;
                    Home1Visible=true;

                }
            });
        }
    }
}
