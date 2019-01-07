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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ManageStudentApp.Dialog
{
    public sealed partial class ChangePasswordDialog : ContentDialog
    {
        public ChangePasswordDialog()
        {
            this.InitializeComponent();
        }

        private async void SubmitPasswordClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Dictionary<String, String> changePassword = new Dictionary<string, string>();
            var password = Password.Password;
            var newPassword = NewPassword.Password;
            changePassword.Add("password", password);
            changePassword.Add("newPassword", newPassword);
            string token = await Handle.GetToken();
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(changePassword), System.Text.Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = client.PostAsync(APIUrl.CHANGE_PASSWORD, content);
            Debug.WriteLine(changePassword);
            Debug.WriteLine(token);
            Debug.WriteLine(response.Result.StatusCode);
            if (response.Result.StatusCode == HttpStatusCode.OK)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(View.Home));
                Debug.WriteLine("Thay đổi mật khẩu thành công");
            } else
            {
                Debug.WriteLine("Lỗi");
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
