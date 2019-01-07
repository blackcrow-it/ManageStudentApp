using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentApp.Entity
{
    class TokenResponse
    {
        private string _accessToken;

        public string AccessToken { get => _accessToken; set => _accessToken = value; }
    }
}
