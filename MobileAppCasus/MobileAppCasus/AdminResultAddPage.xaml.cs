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
	public partial class AdminResultAddPage : ContentPage
	{
		User currentUser;
		Level selectedLevel;
		public AdminResultAddPage(User currentUser, Level selectedLevel)
		{
			InitializeComponent();
			this.currentUser = currentUser;
			this.selectedLevel = selectedLevel;
		}

		private void resultAddButton_Clicked(object sender, EventArgs e)
		{
			bool isResultNameEmpty = string.IsNullOrEmpty(resultNameEntry.Text);
			bool isResultDescriptionEmpty = string.IsNullOrEmpty(resultDescriptionEntry	.Text);
			bool isResultMinScoreEmpty = string.IsNullOrEmpty(resultMinScoreEntry.Text);
			bool isResultMaxScoreEmpty = string.IsNullOrEmpty(resultMaxScoreEntry.Text);

			if (isResultNameEmpty || isResultDescriptionEmpty || isResultMinScoreEmpty || isResultMaxScoreEmpty)
			{
				labelHobbyError.Text = "Vul een naam en een beschrijving in!";
			}
			else
			{
				Result result = new Result() { name = resultNameEntry.Text, description = resultDescriptionEntry.Text,  minScore = Int16.Parse(resultMinScoreEntry.Text), maxScore = Int16.Parse(resultMaxScoreEntry.Text), levelId = selectedLevel.id};
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Result>();
					int rows = conn.Insert(result);
					if (rows > 0)
					{
						DisplayAlert("Succes", resultNameEntry.Text + " is aangemaakt", "Ok");
						Navigation.PushAsync(new AdminLevelManagePage(currentUser, selectedLevel));
					}
					else
					{
						DisplayAlert("Fout", resultNameEntry.Text + " is niet aangemaakt", "Ok");
					}
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
	}
}