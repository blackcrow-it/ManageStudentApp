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
      
        Student student = new Student();
        public Information()
        {
            this.InitializeComponent();
            this.InitializeComponent();
            this.GetInfoUser();

        }

        private async void Edit_Information(object sender, RoutedEventArgs e)
        {
            var edit = new EditInformationDialog();
            await edit.ShowAsync();
           // this.Myframe.Navigate(typeof(MainPage));
        }

      
       
        public async void GetInfoUser()
        {
            string content = await Handle.ReadFile("credential.txt");
            TokenResponse member_token = JsonConvert.DeserializeObject<TokenResponse>(content);
            
            StudentWithRollnumber responseJsonMember = await APIHandle.GetInformation(member_token.AccessToken);
            this.txt_fullname.Text = responseJsonMember.informations.firstName + " " + responseJsonMember.informations.middleName + " " + responseJsonMember.informations.lastName;
            this.rollNumber.Text = responseJsonMember.rollNumber;
            if (responseJsonMember.informations.phone != null)
            {
                this.txt_phone.Text = responseJsonMember.informations.phone;
            }

            if (responseJsonMember.informations.email != null)
            {
                this.txt_email.Text = responseJsonMember.informations.email;
            }

            if (responseJsonMember.informations.address != null)
            {
                this.txt_address.Text = responseJsonMember.informations.address;
            }
            try
            {
                this.img_avatar.ProfilePicture = new BitmapImage(new Uri(responseJsonMember.informations.avatar, UriKind.Absolute));
            }
            catch
            {

            }
            this.txt_birthday.Text = responseJsonMember.informations.birthday.ToString("dd/MM/yyyy");
            
        }
    }
}
