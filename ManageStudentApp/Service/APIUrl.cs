using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentApp.Service
{
    class APIUrl
    {

        public static string MEMBER_INFORMATION = "https://serverproject20190106113433.azurewebsites.net/api/HandleApi/information-student";
        public static string API_LOGIN = "https://serverproject20190106113433.azurewebsites.net/api/AuthenticationApi/login";
        public static string CHANGE_INFORMATION = "https://serverproject20190106113433.azurewebsites.net/api/HandleApi/change-information";
        public static string CHANGE_PASSWORD = "https://serverproject20190106113433.azurewebsites.net/api/HandleApi/change-password";
        public static string COURSES_FOR_STUDENT = "https://serverproject20190106113433.azurewebsites.net/api/HandleApi/list-courses?rollNumber=";
        public static string MARKS_FOR_STUDENT = "https://serverproject20190106113433.azurewebsites.net/api/ApiMark";
        public static string GRADES_FOR_STUDENT = "https://serverproject20190106113433.azurewebsites.net/api/StudentGradeApi/list-class?rollNumber=";
        public static string LIST_STUDENT = "https://serverproject20190106113433.azurewebsites.net/api/StudentGradeApi/list-student?gradeId=";
    }
}
