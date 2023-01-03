using SQLite;
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
    public partial class RegistreerPage : ContentPage
    {
        public RegistreerPage()
        {
            InitializeComponent();
        }
        private void Registreer_Clicked(object sender, EventArgs e)
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(Username.Text);
            bool isEmailEmpty = string.IsNullOrEmpty(Email.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(Password.Text);
            bool isPasswordConfirmEmpty = string.IsNullOrEmpty(PasswordConfirm.Text);
            if (isUsernameEmpty || isPasswordEmpty || isPasswordConfirmEmpty || isEmailEmpty)
            {
                Error.Text = "Vul een email en een wachtwoord in!";
            }
            else if (Password.Text != PasswordConfirm.Text)
            {
                Error.Text = "Wachtwoorden komen niet overeen!";
            }
            else
            {
                bool adminCheck = false;
                if (Username.Text == "Admin")
                {
                    adminCheck = true;
                }
                User user = new User() { name = Username.Text, password = Password.Text, isAdmin = adminCheck };
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<User>();
                    User currentUser = conn.Query<User>("SELECT * FROM User WHERE name = ?", Username.Text).FirstOrDefault();
                    if (currentUser == null) {
                    
                        conn.CreateTable<User>();
                        int rows = conn.Insert(user);
                        if (rows > 0)
                        {
                            DisplayAlert("Succes", Username.Text + " gebruiker is aangemaakt. Welkom!", "Ok");
                            Navigation.PushAsync(new HomePage(currentUser));
                        }
                    }
                    else
                    {
                        Error.Text = "Deze gebruiker bestaat al!";
                    }
                }
            }
        }
    }
}