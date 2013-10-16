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
        private static int weather = 2;
        private BitmapImage right;
        private BitmapImage wrong;

        // Constructor
        public Menu()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            right = Load_Image("right.png");
            wrong = Load_Image("wrong.png");
        }

        private async void InitializeList()
        {
            piecesList.all = await piecesTable.Where(Piece => Piece.User == user.Mail).
                ToCollectionAsync();

            Clothes.ItemsSource = piecesList.all;
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
                if (piece != null)
                    TrousersW.Text = piece.Name;


                piece = piecesList.Selector(1, weather);

                if (piece != null)
                    TShirtW.Text = piece.Name;

                piece = piecesList.Selector(3, weather);

                if (piece != null)
                    ShoesW.Text = piece.Name;

                piece = piecesList.Selector(2, weather);

                if (piece != null)
                    CoatW.Text = piece.Name;
            }
            catch (Exception e)
            {
                //do nothing
            }
        }

        private static BitmapImage Load_Image(string filename)
        {
            StreamResourceInfo imageResource = Application.GetResourceStream(new Uri(filename, UriKind.Relative));
            BitmapImage image = new BitmapImage();
            image.SetSource(imageResource.Stream);
            image.DecodePixelHeight = 10;
            image.DecodePixelWidth = 10;
            return image;
        }

        private void Load_Buyer()
        {
            try
            {
                TrousersB.Source = (piecesList.Buyer(0)) ? right : wrong;
                TShirtB.Source = (piecesList.Buyer(1)) ? right : wrong;
                CoatB.Source = (piecesList.Buyer(2)) ? right : wrong;
                ShoesB.Source = (piecesList.Buyer(3)) ? right : wrong;
            }
            catch (Exception e)
            {
                //do nothing
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

        private void RefreshB_Click(object sender, RoutedEventArgs e)
        {
            Load_Buyer();
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