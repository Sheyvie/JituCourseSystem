
namespace JituUssd
{

    class PurchaseModule
    {

        public static int amount = 50000;

        private const string AnalyticsFilePath = @"C:\Users\USER\Desktop\JITUUSSDPROJECT\JituUssd\JituUssd\Data\analytics.txt";

        public static void PurchaseCourse(string username, string courseName)


        {

            if (!File.Exists(AnalyticsFilePath))
            {
                // CREATE FILE IF IT DOESN'T EXIST
                using (FileStream fileStream = File.Create(AnalyticsFilePath))
                {
                    Console.WriteLine("creating analytics file");
                }
            }
            string courseData = CoursesModule.FindCourseData(courseName);
            if (!string.IsNullOrEmpty(courseData))
            {
                int coursePrice = CoursesModule.ExtractCoursePrice(courseData);

                int userBalance = UserModule.GetUserBalance(username);

                Console.WriteLine($"Course Price: {coursePrice}");
                Console.WriteLine($"Your Balance: {userBalance}");


                if (userBalance < coursePrice)
                {
                    Console.Write("Insufficient balance. Do you want to top-up? (y/n): ");
                    string topUpChoice = Console.ReadLine();
                    if (topUpChoice.ToLower() == "y")
                    {
                        Console.Write("Enter the amount to top-up: ");
                        if (int.TryParse(Console.ReadLine(), out int topUpAmount))
                        {
                            UserModule.UpdateUserBalance(username, topUpAmount);
                            Console.WriteLine($"Successfully topped up by {topUpAmount}. Current balance: {UserModule.GetUserBalance(username)}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Top-up canceled.");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Top-up canceled. Purchase canceled.");
                        return;
                    }
                }

                bool purchaseSuccess = SimulateSTKPush(username, coursePrice);

                if (purchaseSuccess)
                {
                    string purchaseData = $"{username},{courseName}";
                    File.AppendAllText(AnalyticsFilePath, purchaseData + Environment.NewLine);
                    Console.WriteLine("Congratulations! You've successfully purchased the course.");
                }
                else
                {
                    Console.WriteLine("Purchase failed. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Course not found.");
            }
        }

        private static bool SimulateSTKPush(string username, int amount)
        {
            
            Console.WriteLine($"Simulating STK push for user '{username}' and amount: {amount}");

            // Check if user has sufficient balance (you can implement GetUserBalance here)
            int userBalance = UserModule.GetUserBalance(username);
            if (userBalance >= amount)
            {
                // Deduct the amount from the user's balance (you can implement UpdateUserBalance here)
                int newBalance = userBalance - amount;
                UserModule.UpdateUserBalance(username, newBalance);

                Console.WriteLine("STK push successful. Purchase complete!");
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds. STK push failed.");
                return false;
            }
        }
    }

}
