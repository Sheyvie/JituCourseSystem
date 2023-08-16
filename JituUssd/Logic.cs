namespace JituUssd
{
    public class Logic
    {
        private static  string CurrentUserRole = ""; // Store the current user's role
        public static void LoginMenu()
        {

            Console.WriteLine("Login");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            string role = UserModule.GetUserRole(username); // Retrieve the user's role

            if (UserModule.LoginUser(username, password))
            {
                Console.WriteLine("Login successful!");

                CurrentUserRole = role;

                if (role == "Admin")
                {

                    AdminMenu(username);
                }
                else if (role == "User")
                {

                    UserMenu(username);
                }

                else

                {

                    Console.WriteLine("Login failed. Wrong credentials.");
                }
            }
        }
        public static void UserMenu(string username)
        {
            while (true)
            {
                Console.WriteLine("User Dashboard");
                Console.WriteLine("1. View All Courses");
                Console.WriteLine("2. Purchase a Course");
                Console.WriteLine("3. View Purchased Courses");
                Console.WriteLine("4. Logout");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {

                        case 1:
                        case 2:
                            Console.WriteLine("Available Courses:");
                            CoursesModule.DisplayCourses();
                            Console.Write("Enter the name of the course you want to purchase: ");
                            string courseToPurchase = Console.ReadLine();
                            PurchaseModule.PurchaseCourse(username, courseToPurchase);
                            break;

                        case 3:
                            Console.WriteLine("Purchased Courses:");
                            CoursesModule.DisplayPurchasedCourses(username);
                            break;

                        case 4:
                            Console.WriteLine("Logged out.");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

       
        public static  void AdminMenu( string username)
        {
                while (true)
                {
                    Console.WriteLine("Admin Dashboard");
                    Console.WriteLine("1. Add a New Course");
                    Console.WriteLine("2. View All Courses");
                    Console.WriteLine("3. Delete a Course");
                    Console.WriteLine("4. Update a Course");
                    Console.WriteLine("5. View Analytics");
                    Console.WriteLine("6. Logout");

                    int choice;
                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Enter course name: ");
                                string courseName = Console.ReadLine();
                                Console.Write("Enter course price: ");
                                if (int.TryParse(Console.ReadLine(), out int coursePrice))
                                {
                                    AdminModule.AddCourse(courseName, coursePrice);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Course price must be a number.");
                                }
                                break;
                            case 2:
                                AdminModule.DisplayCourses();
                                break;
                            case 3:
                                Console.Write("Enter the name of the course to delete: ");

                                string courseToDelete = Console.ReadLine();

                                AdminModule.DeleteCourse(courseToDelete);

                                break;
                            case 4:
                                Console.Write("Enter the name of the course to update: ");

                                string courseToUpdate = Console.ReadLine();

                                int newPrice;

                                Console.Write("Enter the new price: ");

                                if (int.TryParse(Console.ReadLine(), out newPrice))
                                {
                                    AdminModule.UpdateCourse(courseToUpdate, newPrice);
                                }
                                break;
                            case 5:
                                AdminModule.DisplayAnalytics();
                                break;
                            case 6:
                                Console.WriteLine("Logged out.");
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Please select a valid option.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }
        }

        public static void RegisterMenu()
        {
                Console.WriteLine("Register");
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                UserModule.RegisterUser(username, password);
                Console.WriteLine("Registration successful!");
        }

       

        public static void Init()
        {
                while (true)
                {
                    Console.WriteLine("Welcome to the System!");
                    Console.WriteLine("1. Login");
                    Console.WriteLine("2. Register");
                    Console.WriteLine("3. Exit");

                    int choice;
                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                LoginMenu();
                                break;
                            case 2:
                                RegisterMenu();
                                break;
                            case 3:
                                Console.WriteLine("Goodbye!");
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Please select a valid option.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }
        }
        

    }

    
}


