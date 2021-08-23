using Xamarin.Forms;
using Xamarin.Essentials;
using SocketIOClient;

namespace LBot {
    public partial class App:Application {

        public class pageBase {
            public string currentPage;
        }

        public class libBase {
            public string currentLibrary;
        }

        public SocketIO client = new SocketIO("http://192.168.137.1:2910/", new SocketIOOptions {
            EIO = 3
        });

        public pageBase pageInfo = new pageBase();
        public libBase libInfo = new libBase();
        public App() {
            InitializeComponent();
            MainPage = new AppShell();
            int home_page = Preferences.Get("home_page_choice", 1);
            MessagingCenter.Send<object, int>(this, "Home", home_page);
        }

        protected override void OnStart() {

        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}