using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JituUssd
{
   
    public class CoursesModule
    {
        private const string CoursesFilePath = @"C:\Users\USER\Desktop\JITUUSSDPROJECT\JituUssd\JituUssd\Data\courses.txt";
        private const string AnalyticsFilePath = @"C:\Users\USER\Desktop\JITUUSSDPROJECT\JituUssd\JituUssd\Data\analytics.txt";

        public static void DisplayCourses()
        {
            string[] lines = File.ReadAllLines(CoursesFilePath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        public static void PurchaseCourse(string username, string courseName)
        {
            string courseData = FindCourseData(courseName);
            if (!string.IsNullOrEmpty(courseData))
            {
                int coursePrice = ExtractCoursePrice(courseData);

                // Simulate STK push and purchase logic here
                bool purchaseSuccess = SimulateSTKPush(coursePrice);

                if (purchaseSuccess)
                {
                    string purchaseData = $"{username},{courseName}";
                    File.AppendAllText(AnalyticsFilePath, purchaseData + Environment.NewLine);
                    Console.WriteLine("Congratulations! You've successfully purchased the course.");
                }
                else
                {
                    Console.WriteLine("Insufficient funds. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Course not found.");
            }
        }

        public static string FindCourseData(string courseName)
        {
            string[] lines = File.ReadAllLines(CoursesFilePath);
            foreach (string line in lines)
            {
                if (line.StartsWith(courseName + "@"))
                {
                    return line;
                }
            }
            return null;
        }

        public static int ExtractCoursePrice(string courseData)
        {
            string[] parts = courseData.Split('@');
            if (parts.Length >= 2 && int.TryParse(parts[1], out int price))
            {
                return price;
            }
            return 0;
        }
        public static void DisplayPurchasedCourses(string username)
        {
            string[] lines = File.ReadAllLines(AnalyticsFilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 2 && parts[0] == username)
                {
                    Console.WriteLine(parts[1]); // Display the purchased course
                }
            }
        }
        private static bool SimulateSTKPush(int amount)
        {
            Console.WriteLine($"Simulating STK push for amount: {amount}");

            string username = Console.ReadLine();
           
            // Check if user has sufficient funds in their account
            int userBalance = GetUserBalance(username); // Implement a method to retrieve user's account balance
            if (userBalance >= amount)
            {
                // Deduct the amount from the user's balance (for simulation purposes)
                userBalance -= amount;

                // Update the user's balance (for simulation purposes)
                UserModule.UpdateUserBalance(username, 100 ); // Implement a method to update user's account balance

                Console.WriteLine("STK push successful. Purchase complete!");
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds. STK push failed.");
                return false;
            }
        }
        public static int GetUserBalance(string username)
        {
            // In a real-world scenario, you might retrieve the user's balance from a database or an external service
            // For now, let's assume a dictionary for demonstration purposes

            Dictionary<string, int> userBalances = new Dictionary<string, int>
        {
            { "user1", 100000 }, // Sample user with balance of 100,000
            { "user2", 50000 }   // Sample user with balance of 50,000
        };

            if (userBalances.ContainsKey(username))
            {
                return userBalances[username];
            }
            else
            {
                return 0; // User not found or balance not available
            }
        }

        public static void UpdateUserBalance(string username, int newBalance)
        {
            // In a real-world scenario, you might update the user's balance in a database or an external service
            // For now, let's assume a dictionary for demonstration purposes
           
            Dictionary<string, int> userBalances = new Dictionary<string, int>
        {
            { "user1", 100000 }, // Sample user with balance of 100,000
            { "user2", 50000 }   // Sample user with balance of 50,000
        };
            UserModule.UpdateUserBalance(username, newBalance);

            if (userBalances.ContainsKey(username))
            {
                userBalances[username] = newBalance;
                Console.WriteLine($"User '{username}' balance updated to: {newBalance}");
            }
            else
            {
                Console.WriteLine($"User '{username}' not found. Balance not updated.");
            }
        }
    }
}
    






 
