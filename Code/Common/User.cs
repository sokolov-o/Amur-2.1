using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsIntegritySecurity { get; set; }

        public User(string Name, string Password, bool IsIntegritySecurity = false)
        {
            this.Name = Name;
            this.Password = Password;
            this.IsIntegritySecurity = IsIntegritySecurity;
        }
        static public User Parse(string userNamePwd, char splitter = ';')
        {
            string name = string.Empty, pwd = string.Empty;
            if (!string.IsNullOrEmpty(userNamePwd))
            {
                string[] s = userNamePwd.Split(splitter);
                name = s[0];
                if (s.Length > 1)
                    pwd = s[1];
            }
            return new User(name, pwd);
        }

        public static bool IsEqual(User user1, User user2)
        {
            if 
            (
                user1.Name == user2.Name
                && user1.Password == user2.Password
            )
            return true;
            return false;
        }
    }
}
