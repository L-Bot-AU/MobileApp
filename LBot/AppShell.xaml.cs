using Xamarin.Forms;
using Xamarin.Essentials;

namespace LBot {
    public partial class AppShell:Shell {
        public AppShell() {
            InitializeComponent();
        }

        public void homePageChoice(int home_page_selection) {
            if(home_page_selection == 0) {
                Home0.IsVisible=true;
                Home1.IsVisible=false;
            } else {
                Home0.IsVisible=false;
                Home1.IsVisible=true;
            }
        }
    }
}