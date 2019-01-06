using ManageStudentApp.Entity;
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
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ManageStudentApp.Dialog
{
    public sealed partial class EditInformationDialog : ContentDialog
    {
        private string currentUploadUrl;
        private Student currentStudent;
        private StorageFile photo;
        private string contents;
        public EditInformationDialog()
        {
            this.currentStudent = new Student();
            this.InitializeComponent();
        }

        private async void SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("credential.txt");
            var content = await FileIO.ReadTextAsync(file);
            TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);

            HttpClient httpClient = new HttpClient();

            this.currentStudent.email = this.Email.Text;
            this.currentStudent.phone = this.Phone.Text;
            this.currentStudent.avatar = this.AvatarUrl.Text;
            this.currentStudent.address = this.Address.Text;
            string jsonUser = JsonConvert.SerializeObject(currentStudent);

            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + tokenResponse.AccessToken);
            StringContent stringContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            Debug.WriteLine("Thong tin thay doi " + jsonUser);
            var response = httpClient.PostAsync(Service.APIUrl.CHANGE_INFORMATION, stringContent);
            var responseText = await response.Result.Content.ReadAsStringAsync();
            if (response.Result.StatusCode == HttpStatusCode.OK)
            {
                MessageDialog messageDialog = new MessageDialog("Thay đổi thông tin thành công");
                messageDialog.ShowAsync();
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(View.Information));
                Debug.WriteLine("Change success!!!");
            }
            else
            {
                Debug.WriteLine("Change fail " + response.Result.StatusCode);
            }

        }

        private void ResetButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Phone.Text = string.Empty;
            this.Email.Text = string.Empty;
            this.AvatarUrl.Text = string.Empty;
            this.Address.Text = string.Empty;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            this.currentStudent.gender = Int32.Parse(radio.Tag.ToString());
        }
        
        private async void Choose_Image(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (this.photo == null)
            {
                return;
            }
            HttpClient httpClient = new HttpClient();
            currentUploadUrl = await httpClient.GetStringAsync(Service.APIUrl.MEMBER_INFORMATION);   //cần link upload th4ng tin
            Debug.WriteLine("Upload url:" + currentUploadUrl);
            HttpUploadFile(currentUploadUrl, "myFile", "image/png");
        }

        public async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await this.photo.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                string imageUrl = reader2.ReadToEnd();
                Avatar.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                AvatarUrl.Text = imageUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }
    }
}
