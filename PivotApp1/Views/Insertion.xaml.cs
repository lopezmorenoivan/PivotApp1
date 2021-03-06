﻿using System;
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
using System.Windows.Resources;
using System.IO;

namespace PivotApp1.Contents
{
    public partial class Insertion : PhoneApplicationPage
    {
        private User user = User.CreateObject();

        //temp
        private BitmapImage image = new BitmapImage();
        private String fileName = "SplashScreenImage.jpg";

        public Insertion()
        {
            InitializeComponent();

            InitializeImage();
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
                fileName = e.OriginalFileName;
                WriteableBitmap selectedPhoto = PictureDecoder.DecodeJpeg(e.ChosenPhoto);
                Image.Source = selectedPhoto;
            }
        }

        public void InitializeImage ()
        {
            StreamResourceInfo imageResource = Application.GetResourceStream(new Uri(fileName, UriKind.Relative));
            image.SetSource(imageResource.Stream);
            image.DecodePixelHeight = 10;
            image.DecodePixelWidth = 10;
            Image.Source = image;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Piece piece = new Piece { Name = this.Name.Text, Picture = imageTo64Base(Image),
                Option1 = this.Option1.SelectedIndex, Option2 = this.Option2.SelectedIndex, User = user.Mail };
            piece.Insert();
            this.NavigationService.Navigate(new Uri("/Menu.xaml", UriKind.Relative));
        }

        private String imageTo64Base (Image image)
        {
            Stream imageResource = Application.GetResourceStream(new Uri(fileName, UriKind.Relative)).Stream;
            MemoryStream memStream = new MemoryStream();
            imageResource.CopyTo(memStream);
            // Convert byte[] to Base64 String
            String base64String = Convert.ToBase64String(memStream.ToArray());
            return base64String;
        }

    }
}