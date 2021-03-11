using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Entities
{
    public class UserInfo
    {
        public int id = 1;
        private string firstName = "Paulo";
        private string lastName = "Silva";
        private string userName = "test";
        private string password = "test";


        public int Id
        {
            get { return id; }
        }
        public string FirstName
        {
            get { return firstName; }
        }
        public string LastName
        {
            get { return lastName; }
        }
        public string UserName
        {
            get { return userName; }
        }
        public string Password
        {
            get { return password; }
        }
    }
}
