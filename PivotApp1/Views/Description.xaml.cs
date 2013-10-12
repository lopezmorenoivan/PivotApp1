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

namespace PivotApp1.Contents
{
    public partial class Description : PhoneApplicationPage
    {
        Piece piece = new Piece();

        public Description()
        {
            InitializeComponent();

            //LoadPiece();
        }

        public void LoadPiece()
        {
            //Image.Source = piece.Image();
            Name.Text = piece.Name;
            Option1.Text = piece.Option1;
            Option2.Text = piece.Option2;
        }

        private void NameUpdate(object sender, TextChangedEventArgs e)
        {
            piece.Name = Name.Text;
            //piece.Picture = Image;
            piece.Option1 = Option1.Text;
            piece.Option2 = Option2.Text;
            piece.Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            piece.Delete();
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
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
            }
        }
    }
}