using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.Phone;
using PivotApp1.Misc;
using System.Windows.Resources;
using System.IO;
 
namespace PivotApp1.Contents
{
    public partial class Description : PhoneApplicationPage
    {
        private Pieces piecesList = Pieces.CreateObject();

        public Description()
        {
            InitializeComponent();
        }

        public void LoadPiece()
        {
            piecesList = Pieces.CreateObject();
            Name.Text = piecesList.current.Name;
            Option1.SelectedIndex = piecesList.current.Option1;
            Option2.SelectedIndex = piecesList.current.Option2;
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(piecesList.current.Picture));
            BitmapImage image = new BitmapImage();
            image.SetSource(ms);
            image.DecodePixelHeight = 10;
            image.DecodePixelWidth = 10;
            Image.Source = image;
        }

        private void NameUpdate(object sender, TextChangedEventArgs e)
        {
            piecesList.current.Name = Name.Text;
            piecesList.Update();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            piecesList.current.Delete();
            this.NavigationService.Navigate(new Uri("/Menu.xaml", UriKind.Relative));
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhotoChooserTask taskToChoosePhoto = new PhotoChooserTask();
            taskToChoosePhoto.PixelHeight = 400;
            taskToChoosePhoto.PixelWidth = 400;
            taskToChoosePhoto.Show();
            taskToChoosePhoto.Completed += new EventHandler<PhotoResult>(taskToChoosePhoto_Completed);
        }

        void taskToChoosePhoto_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                string fileName = e.OriginalFileName;
                WriteableBitmap selectedPhoto = PictureDecoder.DecodeJpeg(e.ChosenPhoto);
                Image.Source = selectedPhoto;
                piecesList.Update();
            }
        }

        private void Type_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            piecesList.current.Option2 = Option2.SelectedIndex;
            piecesList.Update();
        }

        private void Clothes_Load(object sender, RoutedEventArgs e)
        {
            LoadPiece();
        }

        private void Kind_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            piecesList.current.Option1 = Option1.SelectedIndex;
            piecesList.Update();
        }
    }
}