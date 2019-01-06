using ManageStudentApp.Entity;
using ManageStudentApp.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class Login : Page
    {
        private static Student currentLogin;
        public Login()
        {
            this.InitializeComponent();
          
        }

        private async void Button_submit(object sender, RoutedEventArgs e)
        {
            var tb_username = UserName.Text;
            var tb_password = Password.Password;
            var httpResponseMessage = APIHandle.Sign_In(tb_username, tb_password);
            if (httpResponseMessage.Result.StatusCode == HttpStatusCode.OK)
            {
                await Handle.Login(tb_username, tb_password);
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(View.Home));
                Debug.WriteLine(httpResponseMessage.Result.StatusCode);
            }
            else
            {
                //var errorJson = await httpResponseMessage.Result.Content.ReadAsStringAsync();
                //ErrorResponse errResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorJson);
                //foreach (var errorField in errResponse.error.Keys)
                //{
                //    if (this.FindName(errorField) is TextBlock textBlock)
                //    {
                //        textBlock.Text = "*" + errResponse.error[errorField];
                //        //Debug.WriteLine("'" + errorField + "' : '" + errResponse.error[errorField] + "'");
                //        //textBlock.Visibility = Visibility.Visible;
                //        //textBlock.Foreground = new SolidColorBrush(Colors.Red);
                //        //textBlock.FontSize = 10;
                //        //textBlock.FontStyle = FontStyle.Italic;
                //    }
                //}
                Debug.WriteLine(httpResponseMessage.Result.StatusCode);
            }
            if (rememberCheck.IsChecked == true)
            {
                await Handle.WriteFile("remember.txt", "true");
            }
            else
            {
                await Handle.WriteFile("remember.txt", "");
            }
        }
        public static async void DoLogin()
        {
            var token = await Handle.GetToken();
            if (token != "")
            {
                var httpResponseMessage = APIHandle.GetData(APIUrl.MEMBER_INFORMATION, "Basic", token);
                Debug.WriteLine(httpResponseMessage.Result.StatusCode);
                if (httpResponseMessage.Result.StatusCode == HttpStatusCode.OK)
                {
                    var rootFrame = Window.Current.Content as Frame;
                    rootFrame.Navigate(typeof(View.Home));
                }
                else
                {
                    Debug.WriteLine("Chưa có token");
                }
            }
            // Auto login nếu tồn tại file token 
            //currentLogin = new Student();
            //StorageFolder folder = ApplicationData.Current.LocalFolder;
            //if (await folder.TryGetItemAsync("credential.txt") != null)
            //{
            //    StorageFile file = await folder.GetFileAsync("credential.txt");
            //    var tokenContent = await FileIO.ReadTextAsync(file);
            //    var rootFrame = Window.Current.Content as Frame;
            //    rootFrame.Navigate(typeof(View.Home));
            //    Debug.WriteLine("Đã đăng nhập thành công");
            //}
            //else
            //{
            //    Debug.WriteLine("Vui lòng đăng nhập lại");
            //}
        }

        private void Sigin_Loaded(object sender, RoutedEventArgs e)
        {
            DoLogin();
        }

    }
}
