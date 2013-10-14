using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
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

        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public String Surname { get; set; }

        [JsonProperty(PropertyName = "mail")]
        public String Mail { get; set; }

        [JsonProperty(PropertyName = "pass")]
        public String Pass { get; set; }

        public async void Insert()
        {
            await App.MobileService.GetTable<User>().InsertAsync(this);
        }
    }

}
