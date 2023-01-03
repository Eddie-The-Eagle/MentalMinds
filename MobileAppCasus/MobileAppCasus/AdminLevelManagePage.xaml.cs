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
	public partial class AdminLevelManagePage : ContentPage
	{
		User currentUser;
		Level selectedLevel;
		public AdminLevelManagePage(User currentUser, Level selectedLevel)
		{
			InitializeComponent();
			this.currentUser = currentUser;
			this.selectedLevel = selectedLevel;
			levelNameLabel.Text = selectedLevel.name;
			levelNumberLabel.Text = selectedLevel.number.ToString();
			levelDescriptionLabel.Text = selectedLevel.description;

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Question>();
				var question = conn.Query<Question>("SELECT * FROM Question WHERE levelId = ?", selectedLevel.id).ToList();
				absLayout.HeightRequest = question.Count() * 45;
				questionListView.ItemsSource = question;

				conn.CreateTable<Result>();
				var result = conn.Query<Result>("SELECT * FROM Result WHERE levelId = ?", selectedLevel.id).ToList();
				absLayout1.HeightRequest = result.Count() * 45;
				resultListView.ItemsSource = result;
			}
		}

		private async void deleteToolbarItem_Clicked(object sender, EventArgs e)
		{
			bool answer = await DisplayAlert("Verwijder", "Weet je het zeker dat je dit level wilt verwijderen?", "Yes", "No");
			if (answer == true)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Level>();
					int rows = conn.Delete(selectedLevel);
					if (rows > 0)
					{
						DisplayAlert("Succes", "Het level is verwijderd", "Ok");
						Navigation.PushAsync(new AdminHomePage(currentUser));
					}
					else
						DisplayAlert("Mislukking", "Het level is niet verwijderd", "Ok");
				}
			}
		}

		private void homeToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminHomePage(currentUser));
		}

		private async void levelEditNameButton_Clicked(object sender, EventArgs e)
		{
			var newLevelName = await DisplayPromptAsync("Level Naam", "Wijzig de naam van het level: " + selectedLevel.name, initialValue: selectedLevel.name);
			if (newLevelName != null)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Level>();
					Level level = conn.Query<Level>("SELECT * FROM Level WHERE id = ?", selectedLevel.id).FirstOrDefault();
					if (level != null)
					{
						selectedLevel.name = newLevelName;
						conn.Update(selectedLevel);
						Navigation.PushAsync(new AdminLevelManagePage(currentUser, selectedLevel));
					}
				}
			}
		}

		private async void levelEditDescriptionButton_Clicked(object sender, EventArgs e)
		{
			var newLevelDescription = await DisplayPromptAsync("Level Naam", "Wijzig de beschrijving van het level: " + selectedLevel.description, initialValue: selectedLevel.description);
			if (newLevelDescription != null)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<User>();
					Level level = conn.Query<Level>("SELECT * FROM Level WHERE id = ?", selectedLevel.id).FirstOrDefault();
					if (level != null)
					{
						selectedLevel.description = newLevelDescription;
						conn.Update(selectedLevel);
						Navigation.PushAsync(new AdminLevelManagePage(currentUser, selectedLevel));
					}
				}
			}
		}

		private void questionAddButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminQuestionAddPage(currentUser, selectedLevel));
		}

		private void questionListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var selectedQuestion = questionListView.SelectedItem as Question;
			if (selectedQuestion != null)
			{
				((ListView)sender).SelectedItem = null;
				Navigation.PushAsync(new AdminQuestionManagePage(currentUser, selectedQuestion));
			}
		}

		private void resultListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var selectedResult = resultListView.SelectedItem as Result;
			if (selectedResult != null)
			{
				((ListView)sender).SelectedItem = null;
				Navigation.PushAsync(new AdminResultManagePage(currentUser, selectedResult));
			}
		}

		private void resultAddButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminResultAddPage(currentUser, selectedLevel));
		}

		private void logoutToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new MainPage());
		}

		private async void levelEditNumberButton_Clicked(object sender, EventArgs e)
		{
			var newLevelNumber = await DisplayPromptAsync("Level Naam", "Wijzig het nummer van het level: " + selectedLevel.number, initialValue: selectedLevel.number.ToString());
			if (newLevelNumber != null)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<User>();
					Level level = conn.Query<Level>("SELECT * FROM Level WHERE id = ?", selectedLevel.id).FirstOrDefault();
					if (level != null)
					{
						selectedLevel.number = Int16.Parse(newLevelNumber);
						conn.Update(selectedLevel);
						Navigation.PushAsync(new AdminLevelManagePage(currentUser, selectedLevel));
					}
				}
			}
		}
	}
}