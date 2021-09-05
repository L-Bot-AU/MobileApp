using Xamarin.Forms;
using Xamarin.Essentials;
using SocketIOClient;
using LBot.Worker;
using System.Threading.Tasks;
using LBot.Models;
using System.Collections.Generic;

namespace LBot {
    public partial class App:Application {

        public class pageBase {
            public string currentPage;
        }

        public class libBase {
            public string currentLibrary;
        }

        public List<Event> eventslist = new List<Event>();
        public pageBase pageInfo = new pageBase();
        public libBase libInfo = new libBase();
        public App() {
            InitializeComponent();
            MainPage = new AppShell();
            startSocketIO();
        }

        private void startSocketIO() {
            SocketIOWorker socketIOWorker = new SocketIOWorker();
            socketIOWorker.Start();
        }

        protected override void OnStart() {
            int homePage = Preferences.Get("home_page_choice", 1);
            string defPage = Preferences.Get("defaultHomePage", "home");
            MessagingCenter.Send<object, int>(this, "Home", homePage);
            if (defPage == "home") {
                if (homePage==0) {
                    Shell.Current.GoToAsync($"//home0");
                } else {
                    Shell.Current.GoToAsync($"//home1");
                }
            } else if (defPage =="jnr") {
                Shell.Current.GoToAsync($"//jnrlib");
            } else {
                Shell.Current.GoToAsync($"//snrlib");
            }
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}