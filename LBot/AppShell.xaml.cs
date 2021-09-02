using Xamarin.Forms;
using SocketIOClient;
using System;

namespace LBot {
    public partial class AppShell:Shell {
        public AppShell() {
            InitializeComponent();
            MessagingCenter.Subscribe<object, int>(this, "Home", (sender, homePage) => {
                if (homePage == 0) {
                    Home0.IsVisible=true;
                    Home1.IsVisible=false;
                } else {
                    Home0.IsVisible=false;
                    Home1.IsVisible=true;
                }
            });
        }

    }
}