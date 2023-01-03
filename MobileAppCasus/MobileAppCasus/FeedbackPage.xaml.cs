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
    public partial class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            InitializeComponent();
        }
        User currentUser;
        Result result;
        public FeedbackPage(User currentUser, Result result)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            this.result = result;
            ResultNameLabel.Text = result.name;
            ResultDescriptionLabel.Text = result.description;
        }
        private void LogUit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage(currentUser));
        }
        private void Confirm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage(currentUser));
        }
    }
}