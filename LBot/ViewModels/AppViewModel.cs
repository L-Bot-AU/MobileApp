using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;

namespace LBot.ViewModels {
    class AppViewModel :INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool home0visible;
        public bool Home0Visible { get => home0visible; set { home0visible = value; NotifyPropertyChanged(); } }

        private bool home1visible;
        public bool Home1Visible { get => home1visible; set { home1visible = value; NotifyPropertyChanged(); } }

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
