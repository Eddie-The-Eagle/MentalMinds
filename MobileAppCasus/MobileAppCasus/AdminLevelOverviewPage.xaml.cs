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
	public partial class AdminLevelOverviewPage : ContentPage
	{
		User currentUser;
		public AdminLevelOverviewPage(User currentUser)
		{
			InitializeComponent();
			this.currentUser = currentUser;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Level>();
				var level = conn.Query<Level>("SELECT * FROM Level").ToList();
				levelListview.ItemsSource = level;
			}
		}

		private void levelAddToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminLevelAddPage(currentUser));
		}

		private void logoutToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new MainPage());
		}

		private void levelListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var selectedLevel = levelListview.SelectedItem as Level;
			if (selectedLevel != null)
			{
				Navigation.PushAsync(new AdminLevelManagePage(currentUser, selectedLevel));
			}
		}
		private void Home_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminHomePage(currentUser));
		}
	}
}