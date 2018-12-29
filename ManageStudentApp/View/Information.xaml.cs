using ManageStudentApp.Entity;
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
        Student student = new Student();
        public Information()
        {
            this.InitializeComponent();
            LoadStudentInformation();
        }
        private async void LoadStudentInformation()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "lMyHzmDDg4vQik4XAmaOap9fy9VsDbH1HP6TMdJUCh3NHexd4rib2ASn05rCPpPG");
            var response = httpClient.GetAsync("");        // cần api
            var content = await response.Result.Content.ReadAsStringAsync();
            Debug.WriteLine(content);
            student = JsonConvert.DeserializeObject<Student>(content);
            Debug.WriteLine(student.email);
            this.txt_fullname.Text = student.firstName + " " + student.lastName;
            this.txt_phone.Text = student.phone;
            this.txt_email.Text = student.email;
            this.txt_birthday.Text = student.birthday;
            this.txt_address.Text = student.address;
            this.img_avatar.Source = new BitmapImage(new Uri(student.avatar, UriKind.Absolute));
        }
        private void Go_Rigister(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
