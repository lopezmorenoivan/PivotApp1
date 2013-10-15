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
using FacebookUtils;

namespace PivotApp1
{
    public partial class Menu : PhoneApplicationPage
    {
        private Pieces piecesList = Pieces.CreateObject();
        private User user = User.CreateObject();
        private IMobileServiceTable<Piece> piecesTable = App.MobileService.GetTable<Piece>();
        private static int weather = 0;

        // Constructor
        public Menu()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void InitializeList()
        {
            piecesList.all = await piecesTable.Where(Piece => Piece.User == user.Mail).
                ToCollectionAsync();

            Clothes.ItemsSource = piecesList.all;
        }

        public static Object GetImage(string filename)
        {
            string imgLocation = Application.Current.Resources["ImagesLocation"].ToString();
            StreamResourceInfo imageResource = Application.GetResourceStream(new Uri(imgLocation + filename, UriKind.Relative));
            BitmapImage image = new BitmapImage();
            image.SetSource(imageResource.Stream);
            return (Object)image;
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
            piecesList.current = (Piece)Clothes.SelectedItem;
            this.NavigationService.Navigate(new Uri("/Views/Description.xaml", UriKind.Relative));
        }

        private void Cloathes_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeList();
        }

        private void Clothes_Selection(object sender, System.Windows.Input.GestureEventArgs e)
        {
            piecesList.current = (Piece)Clothes.SelectedItem;
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            // Disconnect from Facebook
            FacebookClient.Instance.AccessToken = "";

            User user = User.CreateObject();
            user.Name = "";
            user.Mail = "";
            user.Pass = "";
            user.Surname = "";
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Load_Selector();
        }

        private void Load_Selector()
        {
            try
            {
                Piece piece = piecesList.Selector(0, weather);
                TrousersW.Text = piece.Name;

                piece = piecesList.Selector(1, weather);
                TShirtW.Text = piece.Name;

                piece = piecesList.Selector(2, weather);
                CoatW.Text = piece.Name;

                piece = piecesList.Selector(3, weather);
                ShoesW.Text = piece.Name;
            }
            catch (Exception e)
            {
                //Do nothing
            }
        }

        private void Type_Selection(object sender, System.Windows.Input.GestureEventArgs e)
        {
            weather = Weather.SelectedIndex;
        }

        private void WTW_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Selector();
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