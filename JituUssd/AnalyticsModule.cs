using System;
using System.IO;

namespace JituUssd
{
   public  class AnalyticsModule
    {
        private const string AnalyticsFilePath = "data/analytics.txt";

        public static void DisplayAnalytics()
        {
            Dictionary<string, int> coursePurchases = new Dictionary<string, int>();
            string[] lines = File.ReadAllLines(AnalyticsFilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    string course = parts[1];
                    if (coursePurchases.ContainsKey(course))
                    {
                        coursePurchases[course]++;
                    }
                    else
                    {
                        coursePurchases.Add(course, 1);
                    }
                }
            }

            foreach (var entry in coursePurchases)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value} purchases");
            }
        }
    }

}
