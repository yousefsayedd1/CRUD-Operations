using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    internal class User
    {
        public string Name { get; set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; set; }
        public User(string name, string email, string phoneNumber)
        {
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        public bool SetEmail(string newEmail)
        {
            if (IsValidEmail(newEmail))
            {
                this.Email = newEmail;
                return true;
            }
            else
            {

                return false;
            }
            // Local Methods

        }
        public static bool IsValidEmail(string newEmail)
        {
            try
            {

                MailAddress e = new System.Net.Mail.MailAddress(newEmail);
                return e.Address == newEmail;
            }
            catch { return false; }
        }
        public void Print()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
        }






    }
}
