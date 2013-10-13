using Microsoft.Phone.Controls;
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
using PivotApp1.Misc;

namespace PivotApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Pieces piecesList = Pieces.CreateObject();
        private IMobileServiceTable<Piece> piecesTable = App.MobileService.GetTable<Piece>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void InitializeList()
        {
            piecesList.all = await piecesTable.ToCollectionAsync();
            
            Clothes.ItemsSource = piecesList.all;   
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

        private void AddButton_Click_1(object sender, RoutedEventArgs e)
        {
            // navigate
            this.NavigationService.Navigate(new Uri("/Views/Insertion.xaml", UriKind.Relative));
        }


        private void Clothes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // navigate
            piecesList.current = (Piece) Clothes.SelectedItem;
            this.NavigationService.Navigate(new Uri("/Views/Description.xaml", UriKind.Relative));
        }

        private void Cloathes_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeList();
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