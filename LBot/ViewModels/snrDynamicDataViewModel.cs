using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LBot.ViewModels {
    class snrDynamicDataViewModel : BaseViewModel {

        public int remaining;


        public snrDynamicDataViewModel() {
            MessagingCenter.Subscribe<object, int>(this, "jnrRemaining", (sender, value) => {
                remaining = value;
            });



        }

    }
}
