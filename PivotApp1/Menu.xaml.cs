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
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Tools;

namespace PivotApp1
{
    public partial class Menu : PhoneApplicationPage
    {
        private Pieces piecesList = Pieces.CreateObject();
        private User user = User.CreateObject();
        private IMobileServiceTable<Piece> piecesTable = App.MobileService.GetTable<Piece>();
        private int weather = 0;
        private BitmapImage right;
        private BitmapImage wrong;
        private const String TextToPost = "Hey, Thanks to Nil, Dani and Ivan there is a smart closet for WP8 ;)";

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


        private void Cloathes_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeList();
        }

        private void Clothes_Selection(object sender, System.Windows.Input.GestureEventArgs e)
        {
            piecesList.current = (Piece)Clothes.SelectedItem;
            this.NavigationService.Navigate(new Uri("/Views/Description.xaml", UriKind.Relative));
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
                Piece piece = piecesList.Selector(0, this.weather);
                if (piece != null)
                    TrousersW.Text = piece.Name;
                else
                    TrousersW.Text = "";

                piece = piecesList.Selector(1, this.weather);

                if (piece != null)
                    TShirtW.Text = piece.Name;
                else
                    TShirtW.Text = "";

                piece = piecesList.Selector(3, this.weather);

                if (piece != null)
                    ShoesW.Text = piece.Name;
                else
                    ShoesW.Text = "";

                piece = piecesList.Selector(2, this.weather);

                if (piece != null)
                    CoatW.Text = piece.Name;
                else
                    CoatW.Text = "";
            }
            catch (Exception e)
            {
                //do nothing
            }
        }

        private BitmapImage Load_Image(string filename)
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

        private void WTW_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Selector();
        }

        private void RefreshB_Click(object sender, RoutedEventArgs e)
        {
            Load_Buyer();
        }

        private void Weather_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.weather = Weather.SelectedIndex;
        }

        private void Post_Click(object sender, RoutedEventArgs e)
        {
            FacebookClient.Instance.PostMessageOnWall(TextToPost, new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
        }

        void PostMessageOnWallCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;
            if (e.Error != null)
            {
                MessageBox.Show("Error Occurred: " + e.Error.Message);
                return;
            }

            System.Diagnostics.Debug.WriteLine(e.Result);

            string result = e.Result;
            byte[] data = Encoding.UTF8.GetBytes(result);
            MemoryStream memStream = new MemoryStream(data);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ResponseData));
            ResponseData responseData = (ResponseData)serializer.ReadObject(memStream);

            if (responseData.id != null && !responseData.id.Equals(""))
            {
                // Success
                MessageBox.Show("Message Posted!");
            }
            else if (responseData.error != null && responseData.error.code == 190)
            {
                if (responseData.error.error_subcode == 463)
                {
                    // Access Token Expired, need to get new token
                    FacebookClient.Instance.ExchangeAccessToken(new UploadStringCompletedEventHandler(ExchangeAccessTokenCompleted));
                }
                else
                {
                    // Another Error with Access Token, need to clear the Access Token
                    FacebookClient.Instance.AccessToken = "";
                }
            }
            else
            {
                // Error
            }
        }

        void ExchangeAccessTokenCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            // Acquire access_token and expires timestamp
            IEnumerable<KeyValuePair<string, string>> pairs = UriToolKits.ParseQueryString(e.Result);
            string accessToken = KeyValuePairUtils.GetValue(pairs, "access_token");

            if (accessToken != null && !accessToken.Equals(""))
            {
                MessageBox.Show("Access Token Exchange Failed");
                return;
            }

            // Save access_token
            FacebookClient.Instance.AccessToken = accessToken;
            FacebookClient.Instance.PostMessageOnWall(TextToPost, new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
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