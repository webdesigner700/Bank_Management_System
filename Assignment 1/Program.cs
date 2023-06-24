using System;
using System.Collections;
using System.IO;

namespace Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\vinay\OneDrive\Documents\Visual Studio 2019\UTS Code Files\Assignment 1\Assignment 1\login.txt");
            // ADD FILENOTFOUND EXCEPTION FOR THIS 

            ArrayList usernames = new ArrayList(); //stores usernames

            ArrayList passwords = new ArrayList(); //stores passwords

            int menuCount = 0;
            Boolean menuChecker = true;

            string inputUsername;
            string inputPassword;

            Boolean usernameChecker = false;
            Boolean passwordChecker = false;

            int usernameCount = 0;
            int passwordCount = 0;

            int usernameError = 0;
            int passwordError = 0;

            foreach (string line in lines)
            {

                string[] words = line.Split('|');

                usernames.Add(words[0]);
                passwords.Add(words[1]);
                
            }

            Console.WriteLine(" WELCOME TO SIMPLE BANKING SYSTEM " + "\n");

            Console.WriteLine(" LOGIN TO START ");

            // For login username and password are to be crosschecked with the
            // valid credentials in login.txt


            while(usernameChecker == false)
            {

                if (usernameError > 0)
                {
                    Console.WriteLine("You enterned an invalid username");
                }

                Console.Write(" User Name: ");

                inputUsername = Console.ReadLine();

                foreach (string username in usernames)
                {
                    if (inputUsername == username)
                    {
                        usernameCount = usernames.IndexOf(username);
                        usernameChecker = true;
                    }
                }

                usernameError++;
            }

            while (passwordChecker == false)
            {

                if (passwordError > 0)
                {
                    Console.WriteLine("You enterned an invalid password");
                }

                Console.Write(" Password: ");

                inputPassword = ReadPassword();

                foreach (string password in passwords)
                {

                    passwordCount = passwords.IndexOf(password);
                    if (passwordCount == usernameCount && inputPassword == password)
                    {
                        passwordChecker = true;
                    }
                }

                passwordError++;
            }

            while(menuChecker == true)
            {

                if(menuCount > 0)
                {
                    Console.WriteLine("Press any key to continue" + "\n");
                    Console.ReadKey();
                }

                Console.WriteLine("\n" + "WELCOME TO SIMPLE BANKING SYSTEM" + "\n");
                Console.WriteLine("1. Create a new account");
                Console.WriteLine("2. Search for an account");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. A/C statement");
                Console.WriteLine("6. Delete account");
                Console.WriteLine("7. Exit" + "\n");

                Console.WriteLine("Enter your choice (1 - 7): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        createAccount();
                        break;

                    case "2":
                        searchAccount();
                        break;

                    case "3":
                        deposit();
                        break;

                    case "4":
                        withdraw();
                        break;

                    case "5":
                        showStatement();
                        break;

                    case "6":
                        deleteAccount();
                        break;

                    case "7":
                        menuChecker = false;
                        return;
                }

                menuCount++;
            }





        }

        public static void createAccount()
        {


            string yesNo;
            Boolean informationCheck = false;
            int informationCount = 0;

            int phoneCount = 0;
            Boolean phoneChecker = false;
            int phone = 0;

            int emailCount = 0;
            Boolean emailChecker = false;
            string email = "";

            string firstName = "";
            string lastName = "";
            string address = "";

            int balance = 0;



            Console.WriteLine("\n" + "CREATE A NEW ACCOUNT" + "\n");
            Console.WriteLine("ENTER THE DETAILS" + "\n");

            while (informationCheck == false)
            {

                if (informationCount > 0)
                {
                    Console.WriteLine("Enter your account information again" + "\n");
                    phoneChecker = false;
                    emailChecker = false;
                }

                Console.Write("First Name: ");
                firstName = Console.ReadLine();
                Console.Write("Last Name: ");
                lastName = Console.ReadLine();
                Console.Write("Address: ");
                address = Console.ReadLine();


                while (phoneChecker == false)
                {

                    if (phoneCount > 0)
                    {
                        Console.WriteLine("The phone number should not be more than 10 characters");
                    }

                    Console.Write("Phone: ");
                    string phoneNumber = Console.ReadLine();

                    if (phoneNumber.Length <= 10)
                    {
                        phoneChecker = true;
                    }

                    if (phoneChecker == true)
                    {
                        phone = Convert.ToInt32(phoneNumber);
                    }

                    phoneCount++;
                }


                while (emailChecker == false)
                {

                    if (emailCount > 0)
                    {
                        Console.WriteLine("The entered string should have an @ signature to be a valid email ");
                    }

                    Console.Write("Email: ");
                    email = Console.ReadLine();

                    if (email.Contains('@'))
                    {
                        emailChecker = true;
                    }

                    emailCount++;
                }

                Console.Write("Balance: ");
                balance = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Is the information correct (y/n)? ");
                yesNo = Console.ReadLine();

                if (yesNo == "Y" || yesNo == "y")
                {
                    informationCheck = true;
                }

                informationCount++;
            }

            Account account = new Account(firstName, lastName, address, phone, email, balance);
            account.createAccount();



        }

        public static void searchAccount()
        {

            string accountPath = "";

            Console.WriteLine("SEARCH FOR AN ACCOUNT" + "\n");
            Console.WriteLine("ENTER THE DETAILS" + "\n");

            int accountNumber = 0;
            Boolean accountChecker = true;

            while (accountChecker)
            {

                Console.Write("Account Number: ");

                try
                {
                    accountNumber = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    return;
                }


                accountPath = Account.searchAccount(accountNumber);

                if (accountPath != "wrong")
                {
                    accountChecker = false;
                }
                else
                {
                    Console.WriteLine("The account number entered was wrong. Enter the account number again!");                }
            }



            Console.WriteLine("ACCOUNT DETAILS" + "\n");

            string[] accountInformation = System.IO.File.ReadAllLines(accountPath);

            foreach (string accountInfo in accountInformation)
            {
                Console.WriteLine(accountInfo);
            }

            Console.WriteLine("\n");

        }

        public static void deposit()
        {

            string accountPath = "";

            Console.WriteLine("DEPOSIT" + "\n");
            Console.WriteLine("ENTER THE DETAILS" + "\n");


            int accountNumber = 0;
            int depositAmount = 0;
            Boolean accountChecker = true;
            Boolean amountChecker = true;

            while (accountChecker)
            {

                Console.Write("Account Number: ");

                try
                {
                    accountNumber = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    return;
                }

                accountPath = Account.searchAccount(accountNumber);

                if (accountPath != "wrong")
                {
                    accountChecker = false;
                }
                else
                {
                    Console.WriteLine("The account number entered was wrong. Enter the account number again!");
                }
            }

            while (amountChecker)
            {
                Console.Write("Amount: ");

                try
                {
                    depositAmount = Convert.ToInt32(Console.ReadLine());
                    amountChecker = false;
                }
                catch
                {
                    Console.WriteLine("The deposit amount provided is not in the right format");
                }
            }

            string[] accountInformation = System.IO.File.ReadAllLines(accountPath);

            Account.deposit(accountPath, accountInformation, accountNumber, depositAmount);

        }

        public static void withdraw()
        {

            string accountPath = "";

            Console.WriteLine("WITHDRAW" + "\n");
            Console.WriteLine("ENTER THE DETAILS" + "\n");

            int accountNumber = 0;
            int withdrawAmount = 0;
            Boolean accountChecker = true;
            Boolean amountChecker = true;

            while (accountChecker)
            {

                Console.Write("Account Number: ");

                try
                {
                    accountNumber = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    return;
                }

                accountPath = Account.searchAccount(accountNumber);

                if (accountPath != "wrong")
                {
                    accountChecker = false;
                }
                else
                {
                    Console.WriteLine("The account number entered was wrong. Enter the account number again!");
                }
            }

            while (amountChecker)
            {
                Console.Write("Amount: ");

                try
                {
                    withdrawAmount = Convert.ToInt32(Console.ReadLine());
                    amountChecker = false;
                }
                catch
                {
                    Console.WriteLine("The deposit amount provided is not in the right format");
                }
            }

            string[] accountInformation = System.IO.File.ReadAllLines(accountPath);

            Account.withdraw(accountPath, accountInformation, accountNumber, withdrawAmount);
        }

        public static void showStatement()
        {
            string accountPath = "";

            Console.WriteLine("STATEMENT" + "\n");
            Console.WriteLine("ENTER THE DETAILS" + "\n");

            int accountNumber = 0;
            Boolean accountChecker = true;

            while (accountChecker)
            {

                Console.Write("Account Number: ");

                try
                {
                    accountNumber = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    return;
                }

                accountPath = Account.searchAccount(accountNumber);

                if (accountPath != "wrong")
                {
                    accountChecker = false;
                }
                else
                {
                    Console.WriteLine("The account number entered was wrong. Enter the account number again!");
                }
            }

            // what if i search the account here and then call the method

            Account.showStatement(accountNumber);
        }

        public static void deleteAccount()
        {
            string accountPath = "";

            Console.WriteLine("DELETE AN ACCOUNT" + "\n");
            Console.WriteLine("ENTER THE DETAILS" + "\n");

            int accountNumber = 0;
            Boolean accountChecker = true;

            while (accountChecker)
            {

                Console.Write("Account Number: ");

                try
                {
                    accountNumber = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    return;
                }

                accountPath = Account.searchAccount(accountNumber);

                if (accountPath != "wrong")
                {
                    accountChecker = false;
                }
                else
                {
                    Console.WriteLine("The account number entered was wrong. Enter the account number again!");
                }
            }

            Console.WriteLine("\n");

            Account.deleteAccount(accountNumber, accountPath);
        }

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo letter = Console.ReadKey(true); 
            while (letter.Key != ConsoleKey.Enter)
            {
                Console.Write("*");
                password = password + letter.KeyChar;
                letter = Console.ReadKey(true);
            }

            return password;
        }
    }
}
