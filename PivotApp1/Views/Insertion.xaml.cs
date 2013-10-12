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
using Microsoft.Phone;
using System.Windows.Media.Imaging;

namespace PivotApp1.Contents
{
    public partial class Insertion : PhoneApplicationPage
    {
        public Insertion()
        {
            InitializeComponent();
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Piece piece = new Piece { Name = this.Name.Text, Option1 = this.Option1.Text, Option2 = this.Option2.Text };
            piece.Insert();
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}