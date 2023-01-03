using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace MobileAppCasus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelSelectorPage : ContentPage
    {
        User currentUser;
        public LevelSelectorPage(User currentUser)
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
                var levels = conn.Query<Level>("Select * From Level").ToList();
                LevelListView.ItemsSource = levels;
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

        private void LevelListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var currentLevel = LevelListView.SelectedItem as Level;
            Navigation.PushAsync(new LevelOmschrijvingPage(currentUser, currentLevel));
        }
    }
}