using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileAppCasus
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
         private void Registreer_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistreerPage());
        }
        private void WachtwoordVergeten_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WachtwoordVergetenPage());
        }
        private void LogIn_Clicked(object sender, EventArgs e)
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(Username.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(Password.Text);

            if (isUsernameEmpty || isPasswordEmpty)
            {
                Error.Text = "Vul een email en een wachtwoord in!";
            }
            else
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<User>();
                    User currentUser = conn.Query<User>("SELECT * FROM User WHERE name = ? AND password = ?", Username.Text, Password.Text).FirstOrDefault();
                    if (currentUser != null)
                    {
                        if (currentUser.isAdmin)
						{
                            Navigation.PushAsync(new AdminHomePage(currentUser));
                        }
						else
						{
                            Navigation.PushAsync(new HomePage(currentUser));
                        }
                    }
                    else
                    {
                        Error.Text = "Vul een geldig email en een wachtwoord in!";
                    }

                }
            }

        }
    }
}
