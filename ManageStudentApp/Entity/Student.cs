using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentApp.Entity
{
    public class Student
    {
        private string _accountId;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _avatar;
        private string _address;
        private int _gender;
        private DateTime _birthday;
        private DateTime _createAt;
        private DateTime _updateAt;

        public string firstName { get => _firstName; set => _firstName = value; }
        public string middleName { get => _middleName; set => _middleName = value; }
        public string lastName { get => _lastName; set => _lastName = value; }
        public int gender { get => _gender; set => _gender = value; }
        public DateTime birthday { get => _birthday; set => _birthday = value; }
        public string email { get => _email; set => _email = value; }
        public string address { get => _address; set => _address = value; }
        public string phone { get => _phone; set => _phone = value; }
        public string avatar { get => _avatar; set => _avatar = value; }
        public DateTime createAt { get => _createAt; set => _createAt = value; }
        public DateTime updateAt { get => _updateAt; set => _updateAt = value; }
        public string accountId { get => _accountId; set => _accountId = value; }
    }
}
   
