﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ManageStudentApp.Entity
{
    public class Courses
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string avarta { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
