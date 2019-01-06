using ManageStudentApp.Dialog;
using ManageStudentApp.Entity;
using ManageStudentApp.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ManageStudentApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Information : Page
    {
        private Student _currentStudent;
        Student student = new Student();
        public Information()
        {
            this.InitializeComponent();
            this.InitializeComponent();
            this.GetInfoUser();

        }

        private async void Edit_Information(object sender, RoutedEventArgs e)
        {
            //this.Myframe.Navigate(typeof(MainPage));
            var edit = new EditInformationDialog();
            await edit.ShowAsync();
        }

      
       
        public async void GetInfoUser()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync("credential.txt");
            string content = await FileIO.ReadTextAsync(file);
            TokenResponse member_token = JsonConvert.DeserializeObject<TokenResponse>(content);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + member_token.AccessToken);
            var response = client.GetAsync(APIUrl.MEMBER_INFORMATION);
            Debug.WriteLine(response.Result.StatusCode);
            var result = await response.Result.Content.ReadAsStringAsync();
            Student responseJsonMember = JsonConvert.DeserializeObject<Student>(result);
            this.txt_fullname.Text = responseJsonMember.firstName + " " + responseJsonMember.lastName;

            //this.txt_phone.Text = responseJsonMember.phone;
            this.txt_birthday.DataContext = responseJsonMember.birthday;
            //this.txt_email.Text = responseJsonMember.email;
            //this.txt_address.Text = responseJsonMember.address;
        }
    }
}
