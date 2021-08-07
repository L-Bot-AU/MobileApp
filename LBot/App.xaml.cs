using Xamarin.Forms;
using Xamarin.Essentials;

namespace LBot {
    public partial class App:Application {

        public class pageBase {
            public string currentPage;
        }

        public class libBase {
            public string currentLibrary;
        }

        public pageBase pageInfo = new pageBase();
        public libBase libInfo = new libBase();
        public App() {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart() {
            int home_page = Preferences.Get("home_page_choice", 0);

        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}