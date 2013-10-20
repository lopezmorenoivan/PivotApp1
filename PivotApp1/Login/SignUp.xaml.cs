using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PivotApp1.Login
{
    public partial class SignUp : PhoneApplicationPage
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = User.CreateObject();

            user.Name = Name.Text;
            user.Surname = Surname.Text;
            user.Mail = Mail.Text;
            user.Pass = Password.Password;

            if (user.Name.Length > 0 && user.Surname.Length > 0 && user.Mail.Length > 0 && user.Pass.Length > 0)
            {
                user.Insert();

                this.NavigationService.Navigate(new Uri("/Menu.xaml", UriKind.Relative));
            }
            else
                MessageBox.Show("Check your data");
        }

    }
}