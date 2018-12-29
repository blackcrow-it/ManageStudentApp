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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ManageStudentApp
{ 
    public sealed partial class MainPage : Page
    {
        //private static string API_GET_UPLOAD_URL;
        private string currentUploadUrl;
        private Student currentStudent;
        private StorageFile photo;
        private string contents;
        public MainPage()
        {
            this.currentStudent = new Student();
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
       }
        private async void doSubmit_Click(object sender, RoutedEventArgs e)
        {

            this.currentStudent.firstName = this.FirstName.Text;
            this.currentStudent.lastName = this.LastName.Text;
            this.currentStudent.email = this.Email.Text;
            this.currentStudent.phone = this.Phone.Text;
            this.currentStudent.password = this.PassWord.Password;
            this.currentStudent.introduction = this.Introduction.Text;
            this.currentStudent.avatar = this.AvatarUrl.Text;
            this.currentStudent.address = this.Address.Text;

            string jsonStudent = JsonConvert.SerializeObject(this.currentStudent);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(jsonStudent, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(Service.APIUrl.MEMBER_INFORMATION, content);
            var contents = await response.Result.Content.ReadAsStringAsync();
            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                //success
            }
            else
            {
                ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(contents);
                if (errorResponse.error.Count > 0)
                {
                    foreach (var key in errorResponse.error.Keys)
                    {
                        var objectBykey = this.FindName(key);
                        var value = errorResponse.error[key];
                        if (objectBykey != null)
                        {
                            TextBlock textblock = objectBykey as TextBlock;
                            textblock.Text = "* " + value;
                            textblock.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            this.currentStudent.gender = Int32.Parse(radio.Tag.ToString());
        }

        private void BirthdayPicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            this.currentStudent.birthday = BirthdayPicker.Date.Value.ToString("yyyy-MM-dd");
        }

        private void doReset_Click(object sender, RoutedEventArgs e)
        {
            this.FirstName.Text = string.Empty;
            this.LastName.Text = string.Empty;
            this.Phone.Text = string.Empty;
            this.Email.Text = string.Empty;
            this.PassWord.Password = string.Empty;
            this.AvatarUrl.Text = string.Empty;
            this.Address.Text = string.Empty;
            this.Introduction.Text = string.Empty;
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

        private static CameraCaptureUIMode GetPhoto()
        {
            return CameraCaptureUIMode.Photo;
        }
    }
}
