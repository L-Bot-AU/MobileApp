using Xamarin.Forms;

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
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}