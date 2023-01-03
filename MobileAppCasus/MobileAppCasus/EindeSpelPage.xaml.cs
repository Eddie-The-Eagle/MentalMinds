using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;


namespace MobileAppCasus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EindeSpelPage : ContentPage
    {
        Level currentLevel;
        User currentUser;
        Result result;
        public EindeSpelPage(User currentUser, Level currenLevel, int userScore)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            this.currentLevel = currenLevel;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Result>();
                var result = conn.Query<Result>("Select * FROM Result WHERE ? BETWEEN minScore AND maxScore AND levelId=?", userScore,currenLevel.id).FirstOrDefault();
                Feedback.Text = result.name;
                UserResult userResult = new UserResult() { levelName = currenLevel.name,levelId = currentLevel.id, userId = currentUser.id, score = userScore };
                conn.CreateTable<UserResult>();
                conn.Insert(userResult);
                this.result = result;
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

        private void Confirm_Clicked(object sender, EventArgs e)
        {
            if (GeenHulp.IsChecked){
                Navigation.PushAsync(new FeedbackPage(currentUser, result));
            }
            else if (AanradenHulp.IsChecked)
            {
                Navigation.PushAsync(new ContactPage(currentUser,result));
            }
            else if (HulpKrijgen.IsChecked)
            {
                DisplayAlert("", "Er wordt zo spoedig mogelijk contact met u opgenomen", "Sluit");
                Navigation.PushAsync(new FeedbackPage(currentUser, result));
            }
            else
            {
                Error.Text = "Kies een optie";
            }
        }
    }
}