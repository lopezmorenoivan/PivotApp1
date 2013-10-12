﻿using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;

namespace PivotApp1
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            User user = new User { Mail = "Awesome user2", Pass = "prueba" };
            user.Insert();
           
        }



        public static Object GetImage(string filename)
        {
            string imgLocation = Application.Current.Resources["ImagesLocation"].ToString();
            StreamResourceInfo imageResource = Application.GetResourceStream(new Uri(imgLocation + filename, UriKind.Relative));
            BitmapImage image = new BitmapImage();
            image.SetSource(imageResource.Stream);
            return (Object) image;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // navigate
            this.NavigationService.Navigate(new Uri("/Views/Insertion.xaml", UriKind.Relative));
        }


        private void Clothes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // navigate
            this.NavigationService.Navigate(new Uri("/Views/Description.xaml", UriKind.Relative));
        }
        

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}