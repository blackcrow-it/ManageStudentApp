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
using ManageStudentApp.Entity;
using ManageStudentApp.Service;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ManageStudentApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListStudent : Page
    {
        public ObservableCollection<Student> ListStudentInClass { get; set; }
        public ListStudent()
        {
            this.ListStudentInClass = new ObservableCollection<Student>();
            this.InitializeComponent();
            LoadStudents();

        }
        public async void LoadStudents()
        {
            var listStudentInClass = await APIHandle.GetListStudent();
            foreach (var item in listStudentInClass)
            {
                ListStudentInClass.Add(item);
                Debug.WriteLine(item.email);
            }
        }
        private void BackToList(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(View.Subjects));
        }
    }
}
