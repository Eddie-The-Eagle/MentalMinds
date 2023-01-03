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
    public partial class LevelOmschrijvingPage : ContentPage
    {
        User currentUser;
        Level currentLevel;
        public LevelOmschrijvingPage(User currentUser,Level currentLevel)
        {
            InitializeComponent();
            LevelName.Text = currentLevel.name;
            LevelOmschrijving.Text = currentLevel.description;

            this.currentLevel = currentLevel;
            this.currentUser = currentUser;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Question>();
                var questionList = conn.Query<Question>("Select * From Question WHERE levelId = ?", currentLevel.id).ToList();
                Navigation.PushAsync(new LevelPage(currentUser, currentLevel, questionList));
            }
        }
        private void LogUit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage(currentUser));
        }
    }
}