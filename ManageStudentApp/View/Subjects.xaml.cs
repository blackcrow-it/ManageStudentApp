using ManageStudentApp.Entity;
using ManageStudentApp.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class Subjects : Page
    {
        public ObservableCollection<Courses> ListCourses { get; set; }
        public Subjects()
        {
            this.ListCourses = new ObservableCollection<Courses>();
            this.InitializeComponent();
            LoadCourses();
        }
        public async void LoadCourses()
        {
            var token = await Handle.GetToken();
            var responseJsonStudent = await APIHandle.GetInformation(token);
            var httpResponseMessage = APIHandle.GetCourses(responseJsonStudent.rollNumber);
            var responseJsonCourses = await httpResponseMessage.Result.Content.ReadAsStringAsync();
            Debug.WriteLine(responseJsonStudent.rollNumber);
            Debug.WriteLine(responseJsonCourses);
            var listCourses = JsonConvert.DeserializeObject<ObservableCollection<Courses>>(responseJsonCourses);
            foreach(var item in listCourses)
            {
                ListCourses.Add(item);
            }
        }
        private async void Courses_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            var course = grid.Tag as Courses;
            var courseId = course.id;
            Debug.WriteLine(courseId);
            Handle.WriteFile("courseId.txt", courseId.ToString());
            this.MyFrame.Navigate(typeof(View.Mark));
        }
        private async void Btn_Subject(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(View.Mark));
        }
    }
}
