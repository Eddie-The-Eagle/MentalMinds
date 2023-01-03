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
	public partial class AdminQuestionAddPage : ContentPage
	{
		User currentUser;
		Level selectedLevel;
		public AdminQuestionAddPage(User currentUser, Level selectedLevel)
		{
			InitializeComponent();
			this.currentUser = currentUser;
			this.selectedLevel = selectedLevel;
		}

		private void homeToolbarItem_Clicked(object sender, EventArgs e)
		{

		}

		private void logoutToolbarItem_Clicked(object sender, EventArgs e)
		{

		}

		private void questionAddButton_Clicked(object sender, EventArgs e)
		{
			bool isQuestionEmpty= string.IsNullOrEmpty(questionEntry.Text);
			bool isAnswerOneEmpty = string.IsNullOrEmpty(answerOneEntry.Text);
			bool isAnswerOneNumberEmpty = string.IsNullOrEmpty(answerOneScoreEntry.Text);
			bool isAnswerTwoEmpty = string.IsNullOrEmpty(answerTwoEntry.Text);
			bool isAnswerTwoNumberEmpty = string.IsNullOrEmpty(answerTwoScoreEntry.Text);

			if (isQuestionEmpty || isAnswerOneEmpty || isAnswerOneNumberEmpty || isAnswerTwoEmpty || isAnswerTwoNumberEmpty)
			{
				labelHobbyError.Text = "Vul een naam en een beschrijving in!";
			}
			else
			{
				Question question = new Question() { question = questionEntry.Text, levelId = selectedLevel.id};
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Question>();
					int rows = conn.Insert(question);
					Answer answerOne = new Answer() { answer = answerOneEntry.Text, score = Int16.Parse(answerOneScoreEntry.Text), questionId = question.id };
					Answer answerTwo = new Answer() { answer = answerTwoEntry.Text, score = Int16.Parse(answerTwoScoreEntry.Text), questionId = question.id };
					conn.CreateTable<Answer>();
					conn.Insert(answerOne);
					conn.Insert(answerTwo);
					if (rows > 0)
					{
						DisplayAlert("Succes", questionEntry.Text + " is aangemaakt", "Ok");
						Navigation.PushAsync(new AdminLevelManagePage(currentUser, selectedLevel));
					}
					else
					{
						DisplayAlert("Fout", questionEntry.Text + " is niet aangemaakt", "Ok");
					}
				}
			}
		}
	}
}