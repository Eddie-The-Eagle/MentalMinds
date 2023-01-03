using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppCasus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultatenPage : ContentPage
    {
        User currentUser;
        User adminUser;
        public ResultatenPage(User currentUser, User adminUser = null)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            this.adminUser = adminUser;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<UserResult>();
                var question = conn.Query<UserResult>("SELECT * FROM UserResult WHERE UserId = ?", currentUser.id).ToList();
                UserResultListView.ItemsSource = question;
            }
        }
        private void LogUit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        private void Home_Clicked(object sender, EventArgs e)
        {
            if (adminUser != null)
            {
                Navigation.PushAsync(new AdminHomePage(adminUser));
            }
            else
            {
                Navigation.PushAsync(new HomePage(currentUser));
            }
        }
    }
}