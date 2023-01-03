﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppCasus
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
