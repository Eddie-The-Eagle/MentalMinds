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
	public partial class AdminResultManagePage : ContentPage
	{
		User currentUser;
		Result selectedResult;
		public AdminResultManagePage(User currentUser, Result selectedResult)
		{
			InitializeComponent();
			this.currentUser = currentUser;
			this.selectedResult = selectedResult;
			resultNameLabel.Text = selectedResult.name;
			resultDescriptionLabel.Text = selectedResult.description;
			resultMinScoreLabel.Text = selectedResult.minScore.ToString();
			resultMaxScoreLabel.Text = selectedResult.maxScore.ToString();
		}

		private async void deleteToolbarItem_Clicked(object sender, EventArgs e)
		{
			bool answer = await DisplayAlert("Verwijder", "Weet je het zeker dat je dit resultaat wilt verwijderen?", "Ja", "Nee");
			if (answer == true)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Result>();
					int rows = conn.Delete(selectedResult);
					if (rows > 0)
					{
						DisplayAlert("Succes", "Het resultaat is verwijderd", "Ok");
						Navigation.PushAsync(new AdminHomePage(currentUser));
					}
					else
						DisplayAlert("Mislukking", "Het resultaat is niet verwijderd", "Ok");
				}
			}
		}

		private void homeToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminHomePage(currentUser));
		}

		private void logoutToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new MainPage());
		}

		private async void resultNameEditButton_Clicked(object sender, EventArgs e)
		{
			var newResultName = await DisplayPromptAsync("Level Naam", "Wijzig de naam van het resultaat: " + selectedResult.name, initialValue: selectedResult.name); ;
			if (newResultName != null)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Result>();
					Result result = conn.Query<Result>("SELECT * FROM Result WHERE id = ?", selectedResult.id).FirstOrDefault();
					if (result != null)
					{
						selectedResult.name = newResultName;
						conn.Update(selectedResult);
						Navigation.PushAsync(new AdminResultManagePage(currentUser, selectedResult));
					}
				}
			}
		}

		private async void resultDescriptionEditButton_Clicked(object sender, EventArgs e)
		{
			var newResultDescription = await DisplayPromptAsync("Level Naam", "Wijzig de beschrijving van het resultaat: " + selectedResult.description, initialValue: selectedResult.description); ;
			if (newResultDescription != null)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Result>();
					Result result = conn.Query<Result>("SELECT * FROM Result WHERE id = ?", selectedResult.id).FirstOrDefault();
					if (result != null)
					{
						selectedResult.description = newResultDescription;
						conn.Update(selectedResult);
						Navigation.PushAsync(new AdminResultManagePage(currentUser, selectedResult));
					}
				}
			}
		}

		private async void resultMinScoreEditButton_Clicked(object sender, EventArgs e)
		{
			var newResultMinScore = await DisplayPromptAsync("Resultaat Min Score", "Wijzig de minimale score van het resultaat: " + selectedResult.minScore, initialValue: selectedResult.minScore.ToString()); ;
			if (newResultMinScore != null)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Result>();
					Result result = conn.Query<Result>("SELECT * FROM Result WHERE id = ?", selectedResult.id).FirstOrDefault();
					if (result != null)
					{
						selectedResult.minScore = Int16.Parse(newResultMinScore);
						conn.Update(selectedResult);
						Navigation.PushAsync(new AdminResultManagePage(currentUser, selectedResult));
					}
				}
			}
		}

		private async void resultMaxScoreEditButton_Clicked(object sender, EventArgs e)
		{
			var newResultMaxScore = await DisplayPromptAsync("Resultaat Max Score", "Wijzig de maximale score van het resultaat: " + selectedResult.maxScore, initialValue: selectedResult.maxScore.ToString()); ;
			if (newResultMaxScore != null)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Result>();
					Result result = conn.Query<Result>("SELECT * FROM Result WHERE id = ?", selectedResult.id).FirstOrDefault();
					if (result != null)
					{
						selectedResult.maxScore = Int16.Parse(newResultMaxScore);
						conn.Update(selectedResult);
						Navigation.PushAsync(new AdminResultManagePage(currentUser, selectedResult));
					}
				}
			}
		}
	}
}