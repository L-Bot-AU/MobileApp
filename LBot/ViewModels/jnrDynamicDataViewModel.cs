using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using LBot.Models;

namespace LBot.ViewModels {
    class jnrDynamicDataViewModel:INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int remaining,fullnessbar;
        private string fullness;
        private string alert = "Alert Loading...";
        private List<string> periodnames;
        private List<string> periodvalues;
        private Trends trend;

        public int Remaining { get => remaining; set { remaining = value; NotifyPropertyChanged(); } }
        public string Fullness { get => fullness; set { fullness=value+"% full"; NotifyPropertyChanged(); } }
        public int FullnessBar { get => fullnessbar; set { fullnessbar = (400*(value/100))-50; } }
        public string Alert { get => alert; set { alert=value; NotifyPropertyChanged(); } }
        public List<string> PeriodNames { get => periodnames; set { periodnames=value; NotifyPropertyChanged(); } }
        public List<string> PeriodValues { get => periodvalues; set { periodvalues=value; NotifyPropertyChanged(); } }
        public Trends Trend { get => trend; set { trend=value; NotifyPropertyChanged(); } }

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

            MessagingCenter.Subscribe<object, List<List<string>>>(this, "jnrPeriods", (sender, value) => {
                List<string> pnames = new List<string>();
                List<string> pvals = new List<string>();
                for(int i = 0; i<value.Count; i++) {
                    pvals.Add(value[i][1]);
                    if(value[i][0].Length==1) {
                        pnames.Add("Period "+value[i][0]);
                    } else {
                        pnames.Add(value[i][0]);
                    }
                }
                PeriodNames = pnames;
                PeriodValues = pvals;
            });

            MessagingCenter.Subscribe<object, Trends>(this, "jnrTrends", (sender, value) => {
                Trend = value;
            });

        }

    }
}
