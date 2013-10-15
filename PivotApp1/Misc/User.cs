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
    
    public sealed class User
    {
        private bool validated = false;

        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public String Surname { get; set; }

        [JsonProperty(PropertyName = "mail")]
        public String Mail { get; set; }

        [JsonProperty(PropertyName = "pass")]
        public String Pass { get; set; }

        private static User user;
        User() { }

        public static User CreateObject()
        {
            // If the object is null for first time instantiate it once.  
            if (user == null)
            {
                user = new User();
            }

            // Return the emp object, when user request for create an instance.  
            return user;
        }

        public async void Insert()
        {
            await App.MobileService.GetTable<User>().InsertAsync(this);
        }

        public async void Check()
        {
            MobileServiceCollection<User, User> users = await App.MobileService.GetTable<User>().Where(User => user.Name == this.Name).ToCollectionAsync();

            validated = (users.Count > 0);
        }

        public bool isValid ()
        {
            return this.validated;
        }
    }

}
