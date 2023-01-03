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
	public partial class AdminUserOverviewPage : ContentPage
	{
		User currentUser;
		public AdminUserOverviewPage(User currentUser)
		{
			InitializeComponent();
			this.currentUser = currentUser;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<User>();
				var user = conn.Query<User>("SELECT * FROM User WHERE isAdmin=false").ToList();
				userListView.ItemsSource = user;
			}
		}

		private void logoutToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new MainPage());
		}

		private async void userListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (((ListView)sender).SelectedItem == null)
				return;
			var selectedUser = userListView.SelectedItem as User;
			var answerAction = await DisplayActionSheet("Wat wil je doen het antwoord: " + selectedUser.name, "Annuleer", null, "Bekijk resultaten"); ;
				
			if (answerAction == "Bekijk resultaten")
			{
				Navigation.PushAsync(new ResultatenPage(selectedUser, currentUser));
			}
		}
		private void Home_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminHomePage(currentUser));
		}
	}
}