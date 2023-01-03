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
	public partial class AdminLevelAddPage : ContentPage
	{
		User currentUser;
		public AdminLevelAddPage(User currentUser)
		{
			InitializeComponent();
			this.currentUser = currentUser;
		}

		private void homeToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminHomePage(currentUser));
		}

		private void logoutToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new MainPage());
		}

		private void levelAddButton_Clicked(object sender, EventArgs e)
		{
			bool isLevelNameEmpty = string.IsNullOrEmpty(levelNameEntry.Text);
			bool isLevelDescriptionEmpty = string.IsNullOrEmpty(levelDescriptionEntry.Text);
			bool isLevelNumberEmpty = string.IsNullOrEmpty(levelNumberEntry.Text);

			if (isLevelNameEmpty || isLevelDescriptionEmpty || isLevelNumberEmpty)
			{
				labelHobbyError.Text = "Vul een naam en een beschrijving in!";
			}
			else
			{
				Level level = new Level() { name = levelNameEntry.Text, description = levelDescriptionEntry.Text, number = Int16.Parse(levelNumberEntry.Text) };
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Level>();
					int rows = conn.Insert(level);
					if (rows > 0)
					{
						DisplayAlert("Succes", levelNameEntry.Text + " is aangemaakt", "Ok");
						Navigation.PushAsync(new AdminLevelManagePage(currentUser, level));
					}
					else
					{
						DisplayAlert("Fout", levelNameEntry.Text + " is niet aangemaakt", "Ok");
					}
				}
			}
		}
	}
}