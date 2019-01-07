using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentApp.Entity
{
    class StudentWithRollnumber
    {
        private string _rollNumber;
        private Student _informations;

        public string rollNumber { get => _rollNumber; set => _rollNumber = value; }
        public Student informations { get => _informations; set => _informations = value; }
    }
}
