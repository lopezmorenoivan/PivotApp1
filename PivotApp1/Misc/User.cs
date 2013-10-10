using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PivotApp1
{
    
    public class User
    {
        public int Id { get; set; }
        public String Mail { get; set; }
        public String Pass { get; set; }

        public async void Insert()
        {
            await App.MobileService.GetTable<User>().InsertAsync(this);
        }
    }

}
