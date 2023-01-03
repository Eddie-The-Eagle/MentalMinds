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
    public partial class LevelPage : ContentPage
    {
        Level currentLevel;
		User currentUser;
        int userScore;
        int answerOneScore;
        int answerTwoScore;
        List<Question> questionList;
        int questionNumber;
        public LevelPage(User currentUser, Level currentLevel, List<Question> questionList, int questionNumber = 0, int userScore = 0)
        {
            InitializeComponent();
            //NavigationPage.SetHasBackButton(this, false);
            this.currentUser = currentUser;
            this.currentLevel = currentLevel;
            this.userScore = userScore;
            this.questionList = questionList;
            this.questionNumber = questionNumber;
            Scenario1.Text = questionList[questionNumber].question;
            Banner.Text = questionNumber+1 + "/" + questionList.Count();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Answer>();
                var answer = conn.Query<Answer>("Select * From Answer WHERE questionId = ?", questionList[questionNumber].id).ToList();
                Optie1.Text = answer[0].answer;
                this.answerOneScore = answer[0].score;
                Optie2.Text = answer[1].answer;
                this.answerTwoScore = answer[1].score;
            }
        }
        private void Optie1_Clicked(object sender, EventArgs e)
        {
            userScore += answerOneScore;
            if (questionList.Count() == questionNumber+1)
			{

                Navigation.PushAsync(new EindeSpelPage(currentUser,currentLevel,userScore));
            }
			else
			{
                Navigation.PushAsync(new LevelPage(currentUser, currentLevel, questionList, questionNumber + 1, userScore));
            }
        }

        private void Optie2_Clicked(object sender, EventArgs e)
        {
            userScore += answerTwoScore;
            if (questionList.Count() == questionNumber+1)
            {
                Navigation.PushAsync(new EindeSpelPage(currentUser,currentLevel, userScore));
            }
            else
            {
                Navigation.PushAsync(new LevelPage(currentUser, currentLevel, questionList, questionNumber + 1, userScore));
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