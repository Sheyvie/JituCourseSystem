using System;
using System.Text;
using System.IO;


namespace JituUssd
{
    public class AdminModule
    {
        private const string UsersFilePath = @"C:\Users\USER\Desktop\JITUUSSDPROJECT\JituUssd\JituUssd\Data\users.txt";
        private const string CoursesFilePath = @"C:\Users\USER\Desktop\JITUUSSDPROJECT\JituUssd\JituUssd\Data\courses.txt";
        private const string AnalyticsFilePath = @"C:\Users\USER\Desktop\JITUUSSDPROJECT\JituUssd\JituUssd\Data\analytics.txt";


        public static void UpdateToAdmin(string username)
        {
            string[] lines = File.ReadAllLines(UsersFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 3 && parts[0] == username)
                {
                    lines[i] = $"{parts[0]},Admin,{parts[2]}";
                    break;
                }
            }
            File.WriteAllLines(UsersFilePath, lines);
        }

        public static void AddCourse(string courseName, int coursePrice)
        {
            if (!File.Exists(CoursesFilePath))
            {
                //CREATE FILE IF IT DOESN'T EXIST
                using (FileStream fileStream = File.Create(CoursesFilePath))
                {
                    Console.WriteLine("creating a text file");
                }
            }

            string courseData = $"{courseName}@{coursePrice}";
            File.AppendAllText(CoursesFilePath, courseData + Environment.NewLine);
        }




        public static void DisplayCourses()
        {
            string[] lines = File.ReadAllLines(CoursesFilePath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        public static void DeleteCourse(string courseName)
        {
            List<string> courses = new List<string>(File.ReadAllLines(CoursesFilePath));
            int index = courses.FindIndex(course => course.StartsWith(courseName + "@"));
            if (index >= 0)
            {
                courses.RemoveAt(index);
                File.WriteAllLines(CoursesFilePath, courses);
                Console.WriteLine($"Course '{courseName}' has been deleted.");
            }
            else
            {
                Console.WriteLine($"Course '{courseName}' not found.");
            }
        }

        public static void UpdateCourse(string courseName, int newPrice)
        {
            List<string> courses = new List<string>(File.ReadAllLines(CoursesFilePath));
            int index = courses.FindIndex(course => course.StartsWith(courseName + "@"));
            if (index >= 0)
            {
                string[] parts = courses[index].Split('@');
                string updatedCourse = $"{courseName}@{newPrice}";
                courses[index] = updatedCourse;
                File.WriteAllLines(CoursesFilePath, courses);
                Console.WriteLine($"Course '{courseName}' has been updated with new price: {newPrice}");
            }
            else
            {
                Console.WriteLine($"Course '{courseName}' not found.");
            }
        }


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
