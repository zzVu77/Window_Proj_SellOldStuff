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
    /// Interaction logic for WinDeltailItem.xaml
    /// </summary>
    public partial class WinDeltailItem : Window
    {
        private bool checkWishList=false;
        public Item info;
        private Seller seller;
        private int Id_user;
        private ImgPath_DAO ImgPath_DAO = new ImgPath_DAO();
        private WishList_DAO wishListDAO = new WishList_DAO();
        public WinDeltailItem()
        {
            InitializeComponent();
        }
        public WinDeltailItem(Item item, Seller seller,int id_user)
        {
            this.info = item;
            this.seller = seller;
            Id_user = id_user;
            //var user_dao = new user_DAO();
            //users = user_dao.Load();
            InitializeComponent();
            SetDefaultValue();
            List<ImgPath> imgPaths = ImgPath_DAO.GetListImagePathByItemID(this.info.Item_Id);
            dplImageSlide.Children.Clear();
            foreach (var i in imgPaths)
            {
                UC_ImageSlide imgs = new UC_ImageSlide(i.Path);
                imgs.ImageClicked += OnImageSlideClicked;
                dplImageSlide.Children.Add(imgs);
            }


        }

        private void OnImageSlideClicked(object sender, RoutedEventArgs e)
        {
            if(sender is UC_ImageSlide img)
            {
                string imgpath = img.imgPath;
                var resourceUri = new Uri(imgpath, UriKind.RelativeOrAbsolute);
                imgItem.Source = new BitmapImage(resourceUri);
            }
        }
        
        private void SetDefaultValue()
        {
            if (wishListDAO.CheckAddedItemInWishList(info.Item_Id, Id_user) == true)
            {
                spAddToWishList.Visibility = Visibility.Collapsed;
                spRemoveFromWishList.Visibility = Visibility.Visible;
                btnAddToWishList.Background = Brushes.Black;
                checkWishList = true;
            }
            txblItemName.Text = info.name;
            txbOriginalPrice.Text = info.original_price.ToString()+" $";
            DateTime dateinfo = (DateTime)info.bought_date;
            txbBughtDate.Text= dateinfo.ToShortDateString();
            txbType.Text = info.type.ToString();
            txbConditon.Text = info.condition.ToString()+" %";
            txtItemPrice.Text = info.price.ToString() + " $";
            txbSellerName.Text = seller.ShopName;
            txbSellerContact.Text = seller.phone;
            txbSellerAddress.Text = seller.Ward +", "+ seller.District + ", " + seller.City;

            
            var resourceUri = new Uri(info.image_path, UriKind.RelativeOrAbsolute);
            imgItem.Source = new BitmapImage(resourceUri);
            // Tạo một FlowDocument
            FlowDocument flowDoc = new FlowDocument();
            // Thêm một Paragraph chứa văn bản mặc định vào FlowDocument
            Paragraph paragraph = new Paragraph(new Run(info.detail_description));
            flowDoc.Blocks.Add(paragraph);
            // Gán FlowDocument cho RichTextBox
            rtbDetailDescription.Document = flowDoc;

            // Tạo một FlowDocument
            FlowDocument flowDoc1 = new FlowDocument();
            // Thêm một Paragraph chứa văn bản mặc định vào FlowDocument
            Paragraph paragraph1 = new Paragraph(new Run(info.condition_description));
            flowDoc1.Blocks.Add(paragraph1);
            // Gán FlowDocument cho RichTextBox
            rtbConditonDescription.Document = flowDoc1;


        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //private void btnBuyNow_Click(object sender, RoutedEventArgs e)
        //{
        //    WinBuyingInterface winBuyingInterface = new WinBuyingInterface(this.info, Id_user);
        //    this.Hide();
        //    winBuyingInterface.ShowDialog();
        //    this.ShowDialog();
        //}

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Item_DAO item_DAO = new Item_DAO();
            WinShopView winShopView = new WinShopView(seller, item_DAO.GetItemsBySellerID(seller.SellerID));
            winShopView.ShowDialog();
            this.Show();
            this.BringIntoView();
        }

        private void txbSellerName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse_MouseLeftButtonDown(sender, e);
        }
        private void iconClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void iconClose_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(checkWishList==false)
            {
                DateTime now = DateTime.Now;
                Wishlist wishlistItem = new Wishlist
                {
                    Id_user = this.Id_user,
                    Item_Id = info.Item_Id,
                    AddedDate = now
                };
                wishListDAO.Add(wishlistItem);
                checkWishList = true;
                spAddToWishList.Visibility = Visibility.Collapsed;
                spRemoveFromWishList.Visibility = Visibility.Visible;
                btnAddToWishList.Background = Brushes.Black;
            }    
            else
            {                
                wishListDAO.RemoveItem(info.Item_Id,this.Id_user);
                btnAddToWishList.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E56B6F"));
                checkWishList = false;
                spAddToWishList.Visibility = Visibility.Visible;
                spRemoveFromWishList.Visibility = Visibility.Collapsed;
            }    

        }
    }
}
