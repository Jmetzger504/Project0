using System;

/*TODO: 
Implement:
    *Initialize default account.
    *Main Menu
    *Create account as option.
    *Withdraw
    *Deposit
    *getAccountDetails
    *checkBalance
    *Exit
    *Database accType shorthand -> Full string for UI.
    Double check region bounds.
SQL Database Implementation:
    Connect to server.
    Load Jeff Bezos Object
    Query accounts
    Sort accounts
    Switch accounts
    Email/Password to load each account.
Optional: difficulty impossible(?) -> easy.
    Switching accounts via OOP.
    *Cancel button in submenu control flow.
    *Currency formatting
    Email verification
*/

namespace BankingAPPConsole_JulianMetzger {
    class Program {
        public static void menu() {
            Console.Clear();
            Console.WriteLine("~------Metzger Banking------~");
        }

        static void Main(string[] args) {

            #region Account Initalize/Greeting
                //Greeting
                menu();
                Console.WriteLine("Welcome to Metzger Banking. How can we help you today?");
                Console.WriteLine("Press enter to continue");

                //Initialize default account
                Accounts account = new Accounts();
                account.number = 1;
                account.name = "Jeff Bezos";
                account.type = "Checking";
                account.balance = Double.MaxValue;
                account.isActive = true;
                account.email = "Jeff@gmail.com";

                Console.ReadLine();
            #endregion

            #region Customer Service
                bool servicingCustomer = true;
                do {
                    //Initialize Main Menu/User Interface
                    menu();
                    Console.WriteLine("1. Withdraw");
                    Console.WriteLine("2. Deposit");
                    Console.WriteLine("3. Account Details.");
                    Console.WriteLine("4. Account Balance.");
                    Console.WriteLine("5: Create a New Account");
                    Console.WriteLine("6: Exit");

                    //Receive and validate user input
                    bool validInput = false;
                    int validatedInput = 0;
                    double amount;

                    do {
                        string input = Console.ReadLine();
                        //Validate integer in [1,5].
                        if(Int32.TryParse(input,out validatedInput) && validatedInput <= 6 && validatedInput >= 1)
                            validInput = true;
                        else {
                            menu();
                            Console.WriteLine("We're sorry, you seem to have given an unexecutable command.");
                            Console.WriteLine("1. Withdraw");
                            Console.WriteLine("2. Deposit");
                            Console.WriteLine("3. Account details.");
                            Console.WriteLine("4. Account balance.");
                            Console.WriteLine("5: Create a New Account");
                            Console.WriteLine("6: Exit");
                        }
                    }while(!validInput);

                    //Control flow based on validated input.
                    
                    //Withdrawl
                    if(validatedInput == 1) {
                        #region Withdrawl
                            //Menu
                            menu();
                            Console.WriteLine("Please input how much you would like to withdraw: ('c' to cancel) ");
                            Console.WriteLine("Account Balance: " + account.balance);

                            //Validate input.
                            validInput = false;
                            validatedInput = 0;
                            amount = 0;
                            do {
                                string input = Console.ReadLine();
                                //Validate for an integer > 0 or cancel command.
                                if(Double.TryParse(input,out amount) && amount > 0 || input.ToLower() == "c")
                                    validInput = true;
                                else {
                                    menu();
                                    Console.WriteLine("We're sorry, you seem to have given an unexecutable command.");
                                    Console.WriteLine("Please input how much you would like to withdraw: (press 'c' to cancel):");
                                    Console.WriteLine("Account Balance: " + account.balance);
                                }
                                if(amount > account.balance) {
                                    validInput = false;
                                    menu();
                                    Console.WriteLine("We're sorry, you can't overdraw your account.");
                                    Console.WriteLine("Please input how much you would like to withdraw: (press 'c' to cancel):");
                                    Console.WriteLine("Account Balance: " + account.balance);
                                }
                            }while(!validInput);

                            //Check for valid withdawl
                            if(amount > 0) {
                                //Transaction
                                account.withdraw(amount);
                                //Confirmation
                                menu();
                                Console.WriteLine(amount + " withdrawn.");
                                Console.WriteLine("Current Balance: " + account.balance + "\n");

                                //Prompt to return to main menu
                                Console.WriteLine("Please press enter to continue.");
                                Console.ReadLine();
                                
                            }
                            //Else Return to main menu.
                        #endregion Withdrawl
                    }
                    //Deposit
                    else if(validatedInput == 2) {
                        #region Deposit
                            //Menu
                            menu();
                            Console.WriteLine("Please input how much you would like to deposit: ('c' to cancel) ");

                            //Validate input.
                            validInput = false;
                            validatedInput = 0;
                            amount = 0;

                            
                            do {
                                string input = Console.ReadLine();
                                //Validate for an integer > 0 or cancel command.
                                if(Double.TryParse(input,out amount) && amount > 0 || input.ToLower() == "c")
                                    validInput = true;
                                else {
                                    menu();
                                    Console.WriteLine("We're sorry, you seem to have given an unexecutable command.");
                                    Console.WriteLine("Please input how much you would like to deposit: (press 'c' to cancel):\n");
                                    Console.WriteLine("Account Balance: " + account.balance);
                                }
                            }while(!validInput);

                             //Check for valid withdawl
                            if(amount > 0) {
                                //Transaction
                                account.deposit(amount);
                                //Confirmation
                                menu();
                                Console.WriteLine(amount + " deposited.");
                                Console.WriteLine("Current Balance: " + account.balance + "\n");

                                //Prompt to return to main menu
                                Console.WriteLine("Please press enter to return to the main menu.");
                                Console.ReadLine();  
                            }
                            //Else return to main menu.

                        #endregion Deposit
                    }
                    //Account Details
                    else if(validatedInput == 3) {
                        #region Account Details
                            menu();
                            Console.WriteLine("Account Holder: " + account.name);
                            Console.WriteLine("Email: " + account.email);
                            Console.WriteLine("Account Number: " + account.number.ToString("D8"));
                            Console.WriteLine("Account Type: " + account.type);
                            Console.WriteLine("Balance: " + account.balance);
                            if(account.isActive)
                                Console.WriteLine("Account Status: Active" );
                            else Console.WriteLine("Account Status: Inactive");
                            Console.Write("Press enter to return to the main menu.");
                            Console.ReadLine();

                        #endregion Account Details

                    }
                    //Account Balance
                    else if(validatedInput == 4) {
                        #region Account Balance
                            menu();
                            account.checkBalance();
                            Console.Write("Press enter to return to the main menu.");
                            Console.ReadLine();

                        #endregion Account Balance
                    }
                    //Create new account
                    else if (validatedInput == 5) {
                        #region Create Account
                            menu();
                            Console.Write("Account Name: ");
                            account.name = Console.ReadLine();

                            menu();
                            Console.WriteLine("What type of account would you like to make today?");
                            Console.WriteLine("'c' for checking, 's' for savings");

                            validInput = false;
                            string input;
                            //Validate account type
                            do {
                                input = Console.ReadLine();
                                //Validate for an integer > 0 or cancel command.
                                if(input.ToLower() == "c" || input.ToLower() == "s")
                                    validInput = true;
                                else {
                                    menu();
                                    Console.WriteLine("Account Name: " + account.name);
                                    Console.WriteLine("We're sorry, you seem to have given an unexecutable command.");
                                    Console.WriteLine("Please input what account you would like to make: ");
                                    Console.WriteLine("'c' for checking, 's' for savings");
                                    
                                }
                            }while(!validInput);

                            if(input == "c") {
                                account.type = "Checking";
                            }
                            else account.type = "Savings";

                            //Validate initial deposit.
                            menu();
                            Console.WriteLine("Account Name: " + account.name);
                            Console.WriteLine("Account Type: " + account.type);
                            Console.Write("Please input your initial deposit: $");
                            validInput = false;

                            do {
                                input = Console.ReadLine();
                                //Validate for an integer > 0 or cancel command.
                                if(Double.TryParse(input,out amount) && amount > 0)
                                    validInput = true;
                                else {
                                    menu();
                                    Console.WriteLine("Account Name: " + account.name);
                                    Console.WriteLine("Account Type: " + account.type);
                                    Console.WriteLine("We're sorry, you seem to have given an unexecutable command.");
                                    Console.Write("Please input your initial deposit: $");
                                    
                                }
                            }while(!validInput);

                            account.balance = amount;

                            //Acquire account email. No validation (yet!).
                            menu();
                            Console.WriteLine("Account Name: " + account.name);
                            Console.WriteLine("Account Type: " + account.type);
                            Console.WriteLine("Initial Deposit: " + account.balance);
                            Console.Write("Please input a valid email for this account:");
                            account.email = Console.ReadLine();

                            //Finalize and return to main menu.
                            Accounts.accNo++;
                            account.number = Accounts.accNo;
                            account.isActive = true;
                            menu();
                            Console.WriteLine("Account Name: " + account.name);
                            Console.WriteLine("Email: " + account.email);
                            Console.WriteLine("Account No: " + account.number.ToString("D8"));
                            Console.WriteLine("Account Type: " + account.type);
                            Console.WriteLine("Initial Deposit: " + account.balance.ToString("C2"));
                            Console.WriteLine("Activation status: Active");
                            Console.WriteLine("Thank you for configuring an account with us!");
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();

                        #endregion Create Account
                    }
                    //Exit 
                    else if(validatedInput == 6) {
                        #region Exit Routine
                            servicingCustomer = false;
                            Console.Clear();
                            Console.WriteLine("~------Metzger Banking------~");
                            Console.WriteLine("Thank you for banking with us today!");
                            Console.Write("Press enter to exit.");
                            Console.ReadLine();
                            Console.Clear();

                        #endregion Exit Routine
                    }
                }while(servicingCustomer);
            #endregion Customer Service
                


                //Generate account number.
                // Random rand = new Random();
                // account.number = rand.Next(1,99999999);  
                // Console.WriteLine(" your account number is: " + account.number.ToString("D8"));  //Always display account number with 8 digits!
            

            // Set up new account.
            // #region Account Setup
            //     Get account name.
            //     System.Console.WriteLine("In order to best serve you, we are going to need a few more details.");
            //     System.Console.WriteLine("Please input your name:");
            //     account.name = Console.ReadLine();

            //     Get initial account balance.
            //     System.Console.WriteLine("Thanks, now please input your initial deposit: ");
            //     bool verified = true;
            //     verified = true;
            //     double deposit = 0;
            //     Initial deposit verification.
            //     do {
            //     string initialDeposit = Console.ReadLine();
            //     if(double.TryParse(initialDeposit,out deposit) == false) {
            //         Console.WriteLine("Sorry, your initial deposit has to be a number!");
            //         Console.WriteLine("Please input your initial deposit: ");
            //         verified = false;
            //     }
            //     else if(deposit <= 0) {
            //         Console.WriteLine("Sorry, your initial deposit has to be greater than zero.");
            //         Console.WriteLine("Please input your initial deposit: ");
            //         verified = false;
            //     }
            //     else {
            //         verified = true;
            //         account.balance = deposit;
            //         }
            //     }while(!verified);

            //     Console.WriteLine("Fantastic! We just need one more thing: your email to be associated with this account.");
            //     Console.WriteLine("Please input your email address: ");

            // #endregion
        }
    }
}
