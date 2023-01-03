using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppCasus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        User currentUser;
        public HomePage(User currentUser)
        {
            InitializeComponent();

            this.currentUser = currentUser;
        }

        

       

        private void SpeelSpel_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LevelSelectorPage(currentUser));
        }
        private void LogUit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void Resultaten_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResultatenPage(currentUser));
        }

        private void Contact_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContactPage(currentUser));
        }
    }
}