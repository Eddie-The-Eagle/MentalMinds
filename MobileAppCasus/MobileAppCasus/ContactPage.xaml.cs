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
    public partial class ContactPage : ContentPage
    {
        User currentUser;
        Result result;
        public ContactPage(User currentUser,Result result=null)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            this.result = result;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (result != null)
            {
                Navigation.PushAsync(new FeedbackPage(currentUser, result));
            }
            else
            {
                Navigation.PushAsync(new HomePage(currentUser));
            }
        }
    }
}