using System;

/*TODO: 
Email verification
Implement:
    ***Initialize "dummy" account.
    Create account as option.
    withdraw
    deposit
    getAccountDetails
    CheckBalance
    Exit
*/
namespace BankingAPPConsole_JulianMetzger
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Account Initalize/Greeting
                Accounts account = new Accounts();
                //Greeting
                Console.WriteLine("~------Metzger Banking------~");
                Console.WriteLine("Welcome to Metzger Banking. What type of account will you be making today? ('c' for checking or 's' for savings)");
            
                //Get account type
                bool verified = true;
                //Account type verification.
                do {
                    string type = Console.ReadLine();
                    if(type.ToLower() == "c")
                        account.AccountType = "checking";
                    else if (type.ToLower() == "s")
                        account.AccountType = "savings";
                    else {
                        Console.WriteLine("We're sorry, that input was invalid. Please input 'c' for a checking account or 's' for a savings account.");
                        verified = false;
                    }
                }while(!verified);

                //Generate account number.
                Random rand = new Random();
                account.AccountNumber = rand.Next(1,99999999);  
                Console.WriteLine(" your account number is: " + account.AccountNumber.ToString("D8"));  //Always display account number with 8 digits!
            #endregion

            //Set up the account.
            #region Account Setup
                //Get account name.
                System.Console.WriteLine("In order to best serve you, we are going to need a few more details.");
                System.Console.WriteLine("Please input your name:");
                account.AccountName = Console.ReadLine();

                //Get initial account balance.
                System.Console.WriteLine("Thanks, now please input your initial deposit: ");
                verified = true;
                double deposit = 0;
                //Initial deposit verification.
                do {
                string initialDeposit = Console.ReadLine();
                if(double.TryParse(initialDeposit,out deposit) == false) {
                    Console.WriteLine("Sorry, your initial deposit has to be a number!");
                    Console.WriteLine("Please input your initial deposit: ");
                    verified = false;
                }
                else if(deposit <= 0) {
                    Console.WriteLine("Sorry, your initial deposit has to be greater than zero.");
                    Console.WriteLine("Please input your initial deposit: ");
                    verified = false;
                }
                else {
                    verified = true;
                    account.AccountBalance = deposit;
                    }
                }while(!verified);

                Console.WriteLine("Fantastic! We just need one more thing: your email to be associated with this account.");
                Console.WriteLine("Please input your email address: ");

            #endregion
        }
    }
}
