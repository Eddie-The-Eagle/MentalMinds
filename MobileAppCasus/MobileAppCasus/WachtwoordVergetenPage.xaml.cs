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
    public partial class WachtwoordVergetenPage : ContentPage
    {
        public WachtwoordVergetenPage()
        {
            InitializeComponent();
        }
		private void ResetWachtwoord_Clicked(object sender, EventArgs e)
		{
            bool isUsernameEmpty = string.IsNullOrEmpty(Username.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(Password.Text);
            bool isPasswordConfirmEmpty = string.IsNullOrEmpty(PasswordConfirm.Text);
            bool isTwoFACodeEmpty = string.IsNullOrEmpty(TwoFactorCode.Text);

            if (isUsernameEmpty || isPasswordEmpty || isPasswordConfirmEmpty || isTwoFACodeEmpty)
            {
                Error.Text = "Vul een email, wachtwoord en 2FA code in!";
            }
            else if (Password.Text != PasswordConfirm.Text)
            {
                Error.Text = "Wachtwoorden komen niet overeen!";
            }
            else
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<User>();
                    User currentUser = conn.Query<User>("SELECT * FROM User WHERE name = ?", Username.Text).FirstOrDefault();
                    if (currentUser != null)
                    {
                        currentUser.password = Password.Text;
                        conn.Update(currentUser);
                        Navigation.PushAsync(new HomePage(currentUser));
                    }
                    else
                    {
                        Error.Text = "Vul een geldige gebruikersnaam in!";
                    }
                }
            }
        }
	}
}