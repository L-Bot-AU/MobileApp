using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;

namespace LBot.ViewModels {
    class jnrDynamicDataViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int remaining = 25;
        public int Remaining { get => remaining; set { remaining = value; NotifyPropertyChanged(); } }

        private string alert = "Alert Loading...";
        public string Alert { get => alert; set { alert=value; NotifyPropertyChanged(); } }

        public jnrDynamicDataViewModel() {
            MessagingCenter.Subscribe<object, int>(this, "jnrRemaining", (sender, value) => {
                Remaining = value;
            });

            MessagingCenter.Subscribe<object, string>(this, "jnrAlert", (sender, value) => {
                Alert = value;
            });

        }

    }
}
