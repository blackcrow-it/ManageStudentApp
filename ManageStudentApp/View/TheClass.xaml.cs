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
    public sealed partial class TheClass : Page
    {
        public ObservableCollection<Grades> ListGrades { get; set; }
        public TheClass()
        {
            this.ListGrades = new ObservableCollection<Grades>();
            this.InitializeComponent();
            LoadGrades();
        }
        public async void LoadGrades()
        {
            var listGrades = await APIHandle.GetGrades();
            foreach (var item in listGrades)
            {
                ListGrades.Add(item);
                Debug.WriteLine(item.name);
            }
        }
        private async void Btn_Stu(object sender, RoutedEventArgs e)
        {
            
            this.MyFrame.Navigate(typeof(View.ListStudent));
        }

        private async void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            var grade = grid.Tag as Grades;
            var gradeId = grade.id;
            Debug.WriteLine(gradeId);
            await Handle.WriteFile("gradeId.txt", gradeId.ToString());
            this.MyFrame.Navigate(typeof(View.ListStudent));
        }
    }
}
