using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LBot.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home:ContentPage {
        public Home() {
            (Application.Current as App).pageInfo.currentPage = "home";
            
            InitializeComponent();
        }
    }
}