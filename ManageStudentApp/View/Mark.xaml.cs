using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ManageStudentApp.Entity;
using ManageStudentApp.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ManageStudentApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Mark : Page
    {
        public ObservableCollection<Marks> ListMarks { get; set; }
        public Mark()
        {
            this.ListMarks = new ObservableCollection<Marks>();
            this.InitializeComponent();
            LoadMark();
        }

        public async void LoadMark()
        {
            ListMarks = await APIHandle.GetMarks();
            if (ListMarks != null)
            {
                foreach (var item in ListMarks)
                {
                    if (item.type == 1)
                    {
                        addValue(item.status, item.value, txt_theory, txt_status_theory);
                    }
                    else if (item.type == 2)
                    {
                        addValue(item.status, item.value, txt_paratice, txt_status_paratice);
                    }
                    else
                    {
                        addValue(item.status, item.value, txt_asm, txt_status_asm);
                    }
                }
            }

            if (txt_theory.Text == "FAIL" || txt_paratice.Text == "FAIL" || txt_asm.Text == "FAIL")
            {
                txt_status.Text = "FAIL";
                txt_status.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                if (txt_theory.Text == "Chưa có điểm ..." || txt_paratice.Text == "Chưa có điểm ..." || txt_asm.Text == "Chưa có điểm ...")
                {
                    txt_status.Text = "Chưa có xếp loại";
                    txt_status.Foreground = new SolidColorBrush(Colors.Gray);
                }
                else
                {
                    txt_status.Text = "PASS";
                    txt_status.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
        }

        public void addValue(int status, int value, TextBlock textValue, TextBlock textStatus)
        {
            textValue.Text = value.ToString();
            textValue.Foreground = new SolidColorBrush(Colors.Black);
            if (status == 1)
            {
                textStatus.Text = "PASS";
                textStatus.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                textStatus.Text = "FAIL";
                textStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void BackToList(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(View.Subjects));
        }
    }
}
