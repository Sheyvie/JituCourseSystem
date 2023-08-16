using System;
using System.Text;
using System.IO;

namespace JituUssd;


public class UserModule
{
    static string UsersFilePath = @"C:\Users\USER\Desktop\JITUUSSDPROJECT\JituUssd\JituUssd\Data\users.txt";

    static Dictionary<string, int> userBalances = new Dictionary<string, int>();
    private static Dictionary<string, string> users = new Dictionary<string, string>();
    static UserModule()

    {
        // Automatically register the admin user
        users["Admin"] = "AdminPassword,Admin";
    }
    public static void RegisterUser(string username, string password)
    {
        if (!File.Exists(UsersFilePath))
        {
            // CREATE FILE IF IT DOESN'T EXIST
            using (FileStream fileStream = File.Create(UsersFilePath))
            {
                Console.WriteLine("creating a text file");
            }
        }

        string userData = $"{username},User,{password}";
        File.AppendAllText(UsersFilePath, userData + Environment.NewLine);
    }

    public static bool LoginUser(string username, string password)
    {
        string[] lines = File.ReadAllLines(UsersFilePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 3 && parts[0] == username && parts[2] == password)
            {
                return true;
            }
        }
        return false;
    }
    public static string GetUserRole(string username)
    {
        string[] lines = File.ReadAllLines(UsersFilePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 3 && parts[0] == username)
            {
                return parts[1]; // Assuming user role is in the second position
            }
        }
        return ""; // User not found or role not available
    }

    public static void InitializeUserBalance(string username, int balance)
    {
        userBalances[username] = balance;
    }

    public static int GetUserBalance(string username)
    {

        if (userBalances.ContainsKey(username))
        {
            return userBalances[username];
        }
        else
        {
            return 0; // Default balance for new users
        }
    }

    public static void UpdateUserBalance(string username, int newBalance)
    {
        userBalances[username] = newBalance;
    }
}



