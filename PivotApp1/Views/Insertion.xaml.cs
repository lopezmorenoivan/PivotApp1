using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PivotApp1.Contents
{
    public partial class Insertion : PhoneApplicationPage
    {
        public Insertion()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Piece piece = new Piece { Name = this.Name.Text, Option1 = this.Option1.Text, Option2 = this.Option2.Text };
            piece.Insert();
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/Insertion.xaml", UriKind.Relative));
        }

        
    }
}