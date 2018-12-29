using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ManageStudentApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        public Home()
        {
            this.InitializeComponent();
        }
        //private void Nav_Menu_Loaded(object sender, RoutedEventArgs e)
        //{
        //    foreach (NavigationViewItemBase item in Nav_Menu.MenuItems)
        //    {
        //        if (item is NavigationViewItem && item.Tag.ToString() == "Home")
        //        {
        //            Nav_Menu.SelectedItem = item;
        //            break;
        //        }
        //    }
        //    Content_Header.Text = "Home";
        //    contentFrame.Navigate(typeof(View.Home));
        //}

        private void Nav_Menu_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock ItemContent = args.InvokedItem as TextBlock;
            Debug.WriteLine(ItemContent.Tag);
            if (ItemContent != null)
            {
                switch (ItemContent.Tag)
                {
                    case "Nav_Home":
                        Content_Header.Text = "Home";
                        contentFrame.Navigate(typeof(View.Login));
                        break;

                    case "Nav_Login":
                        Content_Header.Text = "Login";
                        contentFrame.Navigate(typeof(View.Login));
                        break;

                    case "Nav_Infor":
                        Content_Header.Text = "Information";
                        contentFrame.Navigate(typeof(MainPage));
                        break;
                }
            }
        }
    }
}