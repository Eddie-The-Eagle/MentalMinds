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
	public partial class AdminQuestionManagePage : ContentPage
	{
		User currentUser;
		Question selectedQuestion;
		public AdminQuestionManagePage(User currentUser, Question selectedQuestion)
		{
			InitializeComponent();

			this.currentUser = currentUser;
			this.selectedQuestion = selectedQuestion;
			questionLabel.Text = selectedQuestion.question;

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Answer>();
				var answer = conn.Query<Answer>("SELECT * FROM Answer WHERE questionId = ?", selectedQuestion.id).ToList();
				absLayout.HeightRequest = answer.Count() * 45;
				answerListView.ItemsSource = answer;
			}
		}

		private void homeToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminHomePage(currentUser));
		}

		private async void deleteToolbarItem_Clicked(object sender, EventArgs e)
		{
			bool question = await DisplayAlert("Verwijder", "Weet je het zeker dat je deze vraag wilt verwijderen?", "Ja", "Nee");
			if (question == true)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Question>();
					int rows = conn.Delete(selectedQuestion);
					if (rows > 0)
					{
						DisplayAlert("Succes", "De vraag is verwijderd", "Ok");
						Navigation.PushAsync(new AdminHomePage(currentUser));
					}
					else
						DisplayAlert("Mislukking", "De vraag is niet verwijderd", "Ok");
				}
			}
		}

		private void logoutToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new MainPage());
		}

		private async void questionEditButton_Clicked(object sender, EventArgs e)
		{
			var newQuestionQuestion = await DisplayPromptAsync("Level Naam", "Wijzig de vraag: " + selectedQuestion.question, initialValue: selectedQuestion.question);
			if (newQuestionQuestion != null)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<User>();
					Level level = conn.Query<Level>("SELECT * FROM Question WHERE id = ?", selectedQuestion.id).FirstOrDefault();
					if (level != null)
					{
						selectedQuestion.question = newQuestionQuestion;
						conn.Update(selectedQuestion);
						Navigation.PushAsync(new AdminQuestionManagePage(currentUser, selectedQuestion));
					}
				}
			}
		}

		private async void answerListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (((ListView)sender).SelectedItem == null)
				return;
			var selectedAnswer = answerListView.SelectedItem as Answer;
			var answerAction = await DisplayActionSheet("Wat wil je doen het antwoord: " + selectedAnswer.answer + "\n Score: " + selectedAnswer.score, "Annuleer", null, "Wijzig Antwoord", "Wijzig Score"); ;
			if (answerAction == "Wijzig Antwoord")
			{
				var newAnswerAnswer = await DisplayPromptAsync("Antwoord", selectedAnswer.answer, initialValue: selectedAnswer.answer);
				if (newAnswerAnswer != null)
				{
					using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
					{
						conn.CreateTable<Answer>();
						Answer answer = conn.Query<Answer>("SELECT * FROM Answer WHERE id = ?", selectedAnswer.id).FirstOrDefault();
						if (answer != null)
						{
							selectedAnswer.answer = newAnswerAnswer;
							conn.Update(selectedAnswer);
							Navigation.PushAsync(new AdminQuestionManagePage(currentUser, selectedQuestion));
						}
					}
				}
			}
			else if (answerAction == "Wijzig Score")
			{
				var newAnswerScore = await DisplayPromptAsync("Score", selectedAnswer.score.ToString(), initialValue: selectedAnswer.score.ToString());
				if (newAnswerScore != null)
				{
					using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
					{
						conn.CreateTable<Answer>();
						Answer answer = conn.Query<Answer>("SELECT * FROM Answer WHERE id = ?", selectedAnswer.id).FirstOrDefault();
						if (answer != null)
						{
							selectedAnswer.score = Int16.Parse(newAnswerScore);
							conn.Update(selectedAnswer);
							Navigation.PushAsync(new AdminQuestionManagePage(currentUser, selectedQuestion));
						}
					}
				}
			}

			((ListView)sender).SelectedItem = null;
		}
	}
}