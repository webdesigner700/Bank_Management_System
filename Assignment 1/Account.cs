using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Assignment_1
{
    class Account
    {
        private int accountNumber;
        private string firstName;
        private string lastName;
        private string address;
        private int phone;
        private string email;
        private int balance;
        private string[] accountInformation = new string[7];
        Random number = new Random();

        //public string accountPath = @"c:\Users\vinay\OneDrive\Documents\Visual Studio 2019\UTS Code Files\Assignment 1\Assignment 1";
        // created the path for the file to be stored in

        public Account(string firstName, string lastName, string address, int phone, string email, int balance)
        {

            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.balance = balance;
        }

        public void createAccount()
        {

            this.accountNumber = number.Next(100000, 10000000);

            string accountFile = this.accountNumber + ".txt";
            //created the file name 

            string accountPath = @"c:\Users\vinay\OneDrive\Documents\Visual Studio 2019\UTS Code Files\Assignment 1\Assignment 1";

            accountPath = System.IO.Path.Combine(accountPath, accountFile);
            // created a file with name <accountnumber>.txt

            
            accountInformation[0] = "AccountNo|" + this.accountNumber;
            accountInformation[1] = "First Name|" + this.firstName;
            accountInformation[2] = "Last Name|" + this.lastName;
            accountInformation[3] = "address|" + this.address;
            accountInformation[4] = "Phone|" + this.phone;
            accountInformation[5] = "Email|" + this.email;
            accountInformation[6] = "Balance| " + this.balance;

            Console.WriteLine(accountInformation[6]);

            System.IO.File.WriteAllLines(accountPath, accountInformation);

            //saved all the information with proper format       
      

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("banking.system@gmail.com");

            try
            {
                mail.To.Add(this.email);
            }
            catch (System.FormatException fe)
            {
                Console.WriteLine("The email format is wrong", fe.Source);
            }


            mail.Subject = "Account details";

            Attachment accountData = new Attachment(accountPath);
            mail.Attachments.Add(accountData);

            SmtpClient client = new SmtpClient("smtp.live.com", 587);
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            Console.WriteLine("Account Created! details will be provided via email." + "\n");

            try
            {
                client.Send(mail);
            }
            catch
            {
                Console.WriteLine("The email was not sent" + "\n");
            }

            Console.WriteLine("Account number is: " + this.accountNumber + "\n");
        }

        public static string searchAccount(int accountNumber)
        {

            Boolean accountChecker = false;

            string accountPath = @"c:\Users\vinay\OneDrive\Documents\Visual Studio 2019\UTS Code Files\Assignment 1\Assignment 1";

            DirectoryInfo di = new DirectoryInfo(@"c:\Users\vinay\OneDrive\Documents\Visual Studio 2019\UTS Code Files\Assignment 1\Assignment 1");
            FileInfo[] files = di.GetFiles("*.txt");

            ArrayList accountFiles = new ArrayList();

            string[] accountNos;

            ArrayList accountNumbers = new ArrayList();

            foreach (FileInfo file in files)
            {
                accountFiles.Add(file.Name);
            }

            foreach (string accountFile in accountFiles)
            {
                accountNos = accountFile.Split(".");

                accountNumbers.Add(accountNos[0]);
            }

            foreach (string accountNo in accountNumbers)
            {

                int account = Convert.ToInt32(accountNo);
                if (account == accountNumber)
                {
                    accountChecker = true;
                }
            }

            if (accountChecker == true)
            {
                Console.WriteLine("Account found!" + "\n");
                string accountFile = accountNumber + ".txt";
                accountPath = System.IO.Path.Combine(accountPath, accountFile);
            }
            else
            {
                accountPath = "wrong";
            }

            return accountPath;
        }

        public static void deposit(string accountPath, string[] accountInformation, int accountNumber, int depositAmount)
        {

            string[] balanceInfo;

            balanceInfo = accountInformation[accountInformation.Length - 1].Split("|");

            int originalBalance = Convert.ToInt32(balanceInfo[1]);

            int newBalance = originalBalance + depositAmount;

            DateTime dt = DateTime.Now;

            string[] lines = new string[2];
            
            try
            {
                StreamWriter sw = System.IO.File.AppendText(accountPath);

                sw.WriteLine(dt.ToString() + "|Deposit|" + originalBalance + "|" + newBalance);

                sw.WriteLine("New Balance|" + newBalance);

                sw.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Close the application and deposit the money" +
                " again into this account. The method is not working when the" +
                " user creates an account and runs the deposit method is run" +
                " right after it.", ex.Source);
                return;
            }

            Console.WriteLine("Deposit Successful" + "\n");
        }

        public static void withdraw(string accountPath, string[] accountInformation, int accountNumber, int withdrawAmount)
        {
            string[] balanceInfo;

            balanceInfo = accountInformation[accountInformation.Length - 1].Split("|");

            int originalBalance = Convert.ToInt32(balanceInfo[1]);

            int newBalance = originalBalance - withdrawAmount;

            DateTime dt = DateTime.Now;

            try
            {
                StreamWriter sw = System.IO.File.AppendText(accountPath);

                sw.WriteLine(dt.ToString() + "|Withdraw|" + originalBalance + "|" + newBalance);

                sw.WriteLine("New Balance|" + newBalance);

                sw.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Close the application and withdraw the money" +
                " again into this account. The method is not working when the" +
                " user creates an account and runs the deposit method is run" +
                " right after it.", ex.Source);
                return;
            }

            Console.WriteLine("Withdraw Successful" + "\n");
        }

        public static void showStatement(int accountNumber)
        {
            string accountPath = Account.searchAccount(accountNumber);

            string[] accountInformation = System.IO.File.ReadAllLines(accountPath);

            Console.WriteLine("ACCOUNT STATEMENT" + "\n");

            string[] singleInformation;

            foreach (string accountInfo in accountInformation)
            {
                singleInformation = accountInfo.Split("|");
                Console.WriteLine(singleInformation[0] + ": " + singleInformation[1]);
            }

            Console.WriteLine("\n");
        }

        public static void deleteAccount(int accountNumber, string accountPath)
        {

            Console.WriteLine("The details are displayed below" + "\n");

            string[] accountInformation = System.IO.File.ReadAllLines(accountPath);

            string[] singleInformation;

            foreach (string accountInfo in accountInformation)
            {
                singleInformation = accountInfo.Split("|");
                Console.WriteLine(singleInformation[0] + ": " + singleInformation[1]);
            }

            Console.Write("deleteAccount(y / n)? ");

            string yesNo = Console.ReadLine();

            if (yesNo == "y" || yesNo == "Y")
            {

               try
                {
                    File.Delete(accountPath);
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Close the application and delete the account." +
                    " The method is not working when the user creates an account and" +
                    " deletes the account right after", ex.Source);
                    return;
                }
                Console.WriteLine("\n" + "Account Deleted!" + "\n");
            }
            else
            {
                Console.WriteLine("Account is not deleted");
                return;
            }


        }
    }
}
