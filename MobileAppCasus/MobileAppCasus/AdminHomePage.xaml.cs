using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppCasus
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdminHomePage : ContentPage
	{
		User currentUser;
		public AdminHomePage(User currentUser)
		{
			InitializeComponent();
			this.currentUser = currentUser;
		}

		private void ManageLevelsButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminLevelOverviewPage(currentUser));
		}

		private void ManageUserButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AdminUserOverviewPage(currentUser));
		}
		private void LogUit_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new MainPage());
		}
	}
}