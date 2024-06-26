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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for UC_ItemView.xaml
    /// </summary>
    public partial class UC_ItemView : UserControl
    {
        public event EventHandler<RoutedEventArgs> ItemClicked;

        public Item info;
        Seller_DAO seller_DAO = new Seller_DAO();
        
        public UC_ItemView()
        {
            InitializeComponent();
            this.MouseUp += UC_ItemView_MouseUp;
            
        }
        public UC_ItemView(Item item) : this()
        {            
            info = item;
            SetDefaultValue();
        }

        private void UC_ItemView_MouseUp(object sender, MouseButtonEventArgs e)
        {            
            ItemClicked?.Invoke(this, EventArgs.Empty as RoutedEventArgs);
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            ItemClicked?.Invoke(this, EventArgs.Empty as RoutedEventArgs);
        }

        public void SetDefaultValue()
        {
            if(info.Sale_Status)
            {
                imgSoldStamp.Visibility=Visibility.Visible;
                txblPostedDate.Visibility=Visibility.Hidden;
                txblLocation.Visibility=Visibility.Hidden;
            }   
            txblItemName.Text = info.Name;
            txblPrice.Text = "$"+info.Price.ToString();
            txblOldPrice.Text= "$"+info.Original_Price.ToString();
            txblCondition.Text = info.Condition.ToString() + "%";
            var resourceUri = new Uri(info.Image_Path, UriKind.RelativeOrAbsolute);
            imgItemPic.Source = new BitmapImage(resourceUri);
            DateTime today = DateTime.Now;
            TimeSpan duration = today - info.PostedDate;
            txblPostedDate.Text = duration.Days.ToString() + " days ago";
            txblLocation.Text = seller_DAO.GetSeller(this.info.SellerID).City.ToString();


        }
    }

}

