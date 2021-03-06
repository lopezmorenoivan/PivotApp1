﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace PivotApp1
{
    public class Piece
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public String Picture { get; set; }

        [JsonProperty(PropertyName = "option1")]
        public int Option1 { get; set; }

        [JsonProperty(PropertyName = "option2")]
        public int Option2 { get; set; }

        [JsonProperty(PropertyName = "user")]
        public String User { get; set; }

        public async void Insert()
        {
            await App.MobileService.GetTable<Piece>().InsertAsync(this);
        }

        public async void Delete()
        {
            await App.MobileService.GetTable<Piece>().DeleteAsync(this);
        }

    }
}
