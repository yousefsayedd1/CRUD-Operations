using System.Net.Mail;

namespace CRUD
{
  
    internal class Program
    {

        static void Main(string[] args)
        {
            bool run = true;
            Dictionary<string, User> Users = new Dictionary<string, User>();
            while (run)
            {
                int chosenOption = DisplayMenu();
                switch ((Option)chosenOption)
                {
                    case Option.AddUser:
                        User newUser = AddNewUser(Users);
                        // display user data
                        Console.WriteLine("===========================User Info=====================");
                        newUser.Print();
                        // Store the object in hashmap
                        if (!IsUserExist(newUser.PhoneNumber,Users)) Users.Add(newUser.PhoneNumber, newUser);
                        
                        break;
                    case Option.UpdateUser:
                        UpdateUser(Users);
                        break;
                    case Option.DeleteUser:
                        Console.Write("Enter Phone Number: ");
                        string phoneNumber = Console.ReadLine();
                        if (IsUserExist(phoneNumber, Users))
                        {
                            Users.Remove(phoneNumber);
                        }
                        else Console.WriteLine("No user found with that phone number.");
                        break;
                    case Option.PrintUsers:
                        foreach (var u in Users)
                        {
                            u.Value.Print();
                            
                        }
                        break;
                }


                Console.WriteLine("Do you want to try anything else ?");
                Console.WriteLine(" Y for yes || N for no");
                string WannaContinue = Console.ReadLine().Trim().ToUpper();
                if (WannaContinue != "Y")
                {
                    run = false;
                }
            }   

        }
        static private int DisplayMenu()
        {
            Console.WriteLine("1-Add User");
            Console.WriteLine("2-Update Data of User");
            Console.WriteLine("3-Delete User");
            Console.WriteLine("4-Print All Users");
            Console.Write("Enter Option Number: ");
            int option = Convert.ToInt32(Console.ReadLine());
            while (option < 1 || option > 4)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                Console.Write("Enter Option Number: ");
                option = Convert.ToInt32(Console.ReadLine());
            }
            return option;

        }
        enum Option
        {
            AddUser = 1,
            UpdateUser,
            DeleteUser,
            PrintUsers,
        }
        enum UpdateOption
        {
            EditName = 1,
            EditEmail,
            EditPhoneNumber
        }
        static string PromptForValidEmail()
        {
            Console.Write("Enter the Email: ");
            string email = Console.ReadLine();
            //check email validation
            while (!User.IsValidEmail(email))
            {
                Console.WriteLine("invalid Email");
                Console.Write("Enter The Email:");
                email = Console.ReadLine();
            }
            return email;

        }
        static public User AddNewUser(Dictionary<string, User> Users)
        {
            Console.WriteLine("===========================Add User=====================");
            Console.Write("Enter the Name: ");
            string name = Console.ReadLine();

            string email = PromptForValidEmail();

            Console.Write("Enter the Phone Number: ");
            string phoneNumber = Console.ReadLine();
            while (IsUserExist(phoneNumber, Users))
            {
                Console.WriteLine("Phone Number takend please try again");
                phoneNumber = Console.ReadLine();
            }
            User newUser = new User(name, email, phoneNumber);
            return newUser;
        }
        static User findUser(Dictionary<string, User> Users)
        {
            Console.WriteLine("Enter User phone Number");
            string phoneNumber = Console.ReadLine();
            if (Users.ContainsKey(phoneNumber)) return Users[phoneNumber];
            return null;
        }
        static UpdateOption displayUpdateMenu()
        {
            Console.WriteLine("1- Eidt Name");
            Console.WriteLine("2- Eidt Email");
            Console.WriteLine("3- Eidt PhoneNumber");
            Console.Write("Please Enter Option Number: ");
            UpdateOption option = (UpdateOption)int.Parse(Console.ReadLine());
            return option;
        }
        static bool IsUserExist(string phoneNumber, Dictionary<string, User> Users)
        {
            if (Users.ContainsKey(phoneNumber)) return true;
            return false;
        }
        static public void UpdateUser(Dictionary<string, User> Users)
        {
            // find the User
            User user = findUser(Users);
            if (user == null)
            {
                Console.WriteLine("No user found with that phone number.");
                return;
            }
            UpdateOption option = displayUpdateMenu();
            if (option == UpdateOption.EditName)
            {
                string newName = Console.ReadLine();
                user.Name = newName;
                Console.WriteLine("Name has be updated");
            }
            else if (option == UpdateOption.EditEmail)
            {
                Console.Write("Enter new Email: ");
                string newEmail = PromptForValidEmail();
                user.SetEmail(newEmail);
                Console.WriteLine("Email has be updated");
            }
            else
            {
                Console.Write("Enter new Phone Number: ");
                string newPhoneNumber = Console.ReadLine();
                while (IsUserExist(newPhoneNumber, Users))
                {
                    Console.WriteLine("this Phone Number is taken ");
                    Console.Write("Enter new Phone Number: ");
                    newPhoneNumber = Console.ReadLine();

                }
                Users.Remove(user.PhoneNumber);
                user.PhoneNumber = newPhoneNumber;
                Users.Add(user.PhoneNumber, user);

                Console.WriteLine("Phone Number has be updated");
            }
        }
    }
 
}
