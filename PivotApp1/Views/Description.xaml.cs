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
            piece.Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            piece.Delete();

        }
    }
}