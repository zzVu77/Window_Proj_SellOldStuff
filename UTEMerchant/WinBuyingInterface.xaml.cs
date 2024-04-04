﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinBuyingInterface.xaml
    /// </summary>
    public partial class WinBuyingInterface : Window
    {
        public event EventHandler ItemClicked;
        public Item info;
        List<User> users = new List<User>();
        user_DAO user_dao = new user_DAO();
        public WinBuyingInterface()
        {
            InitializeComponent();
        }
        public WinBuyingInterface(Item item)
        {
            info = item;
            var user_dao = new user_DAO();
            users = user_dao.Load();
            InitializeComponent();
            SetDefault();
        }
        private void SetDefault()
        {
            txblOrderItemName.Text = info.Name;
            txblOrderOriginalPrice.Text = info.OriginalPrice.ToString()+"$";
            txblOrderPrice.Text = info.Price.ToString() + "$";
            txblConditon.Text = info.Status.ToString()+"%";
            txblTotalValue.Text = info.Price.ToString() + "$";
            var resourceUri = new Uri(info.ImagePath, UriKind.RelativeOrAbsolute);
            imgOrderItem.Source = new BitmapImage(resourceUri);

        }
    }
}