using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentApp.Entity
{
    class Student
    {
        private string _firstName;
        private string _lastName;
        private string _password;
        private string _email;
        private string _phone;
        private string _avatar;
        private string _introduction;
        private string _address;
        private int _gender;
        private string _birthday;
        private string _createAt;
        private string _updateAt;
        private string _status;

        public string firstName { get => _firstName; set => _firstName = value; }
        public string lastName { get => _lastName; set => _lastName = value; }
        public string password { get => _password; set => _password = value; }
        public string email { get => _email; set => _email = value; }
        public string phone { get => _phone; set => _phone = value; }
        public string avatar { get => _avatar; set => _avatar = value; }
        public string introduction { get => _introduction; set => _introduction = value; }
        public string address { get => _address; set => _address = value; }
        public int gender { get => _gender; set => _gender = value; }
        public string birthday { get => _birthday; set => _birthday = value; }
        public string createAt { get => _createAt; set => _createAt = value; }
        public string updateAt { get => _updateAt; set => _updateAt = value; }
        public string status { get => _status; set => _status = value; }
    }
}
   
