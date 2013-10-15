using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PivotApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Login/SignUp.xaml", UriKind.Relative));
        }

        private void SignUpFacebook_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Login/SignUpFacebook.xaml", UriKind.Relative));
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            User user = User.CreateObject();
            user.Mail = Mail.Text;
            user.Pass = Pass.Password;

            user.Check();

            if (user.isValid())
                this.NavigationService.Navigate(new Uri("/Menu.xaml", UriKind.Relative));
            else
                MessageBox.Show("Mail or Pass are wrong!", "Wrong Login", MessageBoxButton.OK);
        }
    }
}