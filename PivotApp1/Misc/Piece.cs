﻿using Microsoft.Phone.Controls;
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
    public class Piece
    {
        public int Id { get; set; }
        public String Name { get; set; }
        //public Image Picture { get; set; }
        public String Option1 { get; set; }
        public String Option2 { get; set; }

        public async void Insert()
        {
            await App.MobileService.GetTable<Piece>().InsertAsync(this);
        }

        public async void Update()
        {
            await App.MobileService.GetTable<Piece>().UpdateAsync(this);
        }

        public async void Delete()
        {
            await App.MobileService.GetTable<Piece>().DeleteAsync(this);
        }
    }
}
