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

namespace PivotApp1.Contents
{
    public partial class Description : PhoneApplicationPage
    {
        private Pieces piecesList = Pieces.CreateObject();

        public Description()
        {
            InitializeComponent();

            LoadPiece();
        }

        public void LoadPiece()
        {
            //Image.Source = piece.Image();
            Name.Text = piecesList.current.Name;
            Option1.Text = piecesList.current.Option1;
            Option2.Text = piecesList.current.Option2;
        }

        private void NameUpdate(object sender, TextChangedEventArgs e)
        {
            piecesList.current.Name = Name.Text;
            //piece.Picture = Image;
            piecesList.current.Option1 = Option1.Text;
            piecesList.current.Option2 = Option2.Text;
            piecesList.current.Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            piecesList.current.Delete();
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