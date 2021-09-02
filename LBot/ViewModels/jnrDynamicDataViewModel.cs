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


        public jnrDynamicDataViewModel() {
            MessagingCenter.Subscribe<object, int>(this, "jnrRemaining", (sender, value) => {
                Remaining = value;
            });



        }

    }
}
