using ManageStudentApp.Dialog;
using ManageStudentApp.Service;
using System;
using System.Collections.Generic;
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
            // string appName = Windows.ApplicationModel.Package.Current.DisplayName;
            // AppTitle.Text = appName;

        }
        private void Blog_Home(object sender, RoutedEventArgs e)
        {
            // set the initial SelectedItem
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Blog Home")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }
            ContentFrame.Navigate(typeof(View.BlogHome));
        }
        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(View.Home));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                switch (item.Tag.ToString())
                {
                  
                    case "Information":
                        ContentFrame.Navigate(typeof(View.Information));
                        break;
                    case "This_Class":
                        ContentFrame.Navigate(typeof(View.TheClass));
                        break;
                    case "Subject":
                        ContentFrame.Navigate(typeof(View.Subjects));
                        break;
                    
                }
            }
        }
        private async void Do_Password(object sender, RoutedEventArgs e)
        {
            var changePassword = new ChangePasswordDialog();
            await changePassword.ShowAsync();
        }

        private async void Do_Logout(object sender, RoutedEventArgs e)
        {
            await Handle.WriteFile("credential.txt", "");
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(View.Login));
        }
    }
}
