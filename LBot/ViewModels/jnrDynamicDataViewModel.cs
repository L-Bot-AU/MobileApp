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

        private int remaining,fullnessbar;
        private string fullness;
        private string alert = "Alert Loading...";

        public int Remaining { get => remaining; set { remaining = value; NotifyPropertyChanged(); } }
        public string Fullness { get => fullness; set { fullness=value+"% full"; NotifyPropertyChanged(); } }
        public int FullnessBar { get => fullnessbar; set { fullnessbar = (400*(value/100))-50; } }
        public string Alert { get => alert; set { alert=value; NotifyPropertyChanged(); } }


        public jnrDynamicDataViewModel() {
            MessagingCenter.Subscribe<object, int>(this, "jnrRemaining", (sender, value) => {
                Remaining = value;
            });

            MessagingCenter.Subscribe<object, string>(this, "jnrAlert", (sender, value) => {
                Alert = value;
            });

            MessagingCenter.Subscribe<object, string>(this, "jnrFullness", (sender, value) => {
                Fullness = value;
                FullnessBar = int.Parse(value);
            });

        }

    }
}
