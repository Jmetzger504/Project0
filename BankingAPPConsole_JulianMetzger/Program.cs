using System;
using System.Data.SqlClient;
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
    *Double check region bounds.
SQL Database Implementation:
    *Connect to local server.
    Login/New Account
    Fix accNo
    Reflect Deposit/Withdrawl in database table.
    *Query accounts
    Switch accounts
    Delete account
    Deactivate account
    Deactivated account widthdraw/deposit check.
    *Email/Password to load each account.
Optional: difficulty impossible(?) -> easy.
    *Cancel button in submenu control flow.
    *Currency formatting
    Check for proper withdrawl,deposit truncation
    Email verification
*/

namespace BankingAPPConsole_JulianMetzger {
    class Program {
        public static void menu() {
            Console.Clear();
            Console.WriteLine("~------Metzger Banking------~");
        }

        static void Main(string[] args) {

            #region Greeting Login/New Account
                //Greeting
                string firstStep = "";
                bool loggingIn;
                bool newAccount;
                bool servicingCustomer;

                while(firstStep.ToLower() != "c" && firstStep != "1" && firstStep != "2") {
                    menu();
                    Console.WriteLine("Welcome to Metzger Banking. How can we help you today?");
                    Console.WriteLine("Press 1 to login or 2 to create a new account. ('c' to cancel)");
                    firstStep = Console.ReadLine();
                }
                //Login chosen
                if(firstStep == "1") {

                    loggingIn = true;
                    newAccount = false;
                    servicingCustomer = true;

                }//New account chosen
                else if(firstStep == "2") {
                    loggingIn = false;
                    newAccount = true;
                    servicingCustomer = true;
                }
                //Cancel chosen
                else {
                    menu();
                    Console.WriteLine("Press enter to exit.");
                    Console.ReadLine();
                    loggingIn = false;
                    newAccount = false;
                    servicingCustomer = false;
                }

                //Initialize account
                Accounts account = new Accounts();
                
                //New account selected
                while(newAccount) {
                    

                    //Acquire account email.
                    bool uniqueEmail = false;
                    while(!uniqueEmail) {
                        menu();
                        Console.Write("Please input a valid, unique email for this account:");
                        account.email = Console.ReadLine();
                        if(account.successfulLogin())
                            uniqueEmail = true;
                    }
                    
                    //Acquire account password. No validation (yet!).
                    menu();
                    account.getInitializedDetails();
                    
                    Console.Write("Please input a password of your choosing: ");
                    string password = Console.ReadLine();

                    //Acquire account name.
                    menu();
                    account.getInitializedDetails();
                    Console.Write("Please input the name associated with this account: ");
                    account.name = Console.ReadLine();

                    //Account type
                    menu();
                    account.getInitializedDetails();
                    Console.WriteLine("What type of account would you like to make today?");
                    Console.WriteLine("'c' for checking, 's' for savings");

                    bool validInput = false;
                    string input;
                    //Validate account type
                    do {
                        input = Console.ReadLine();
                        //Validate for an integer > 0 or cancel command.
                        if(input.ToLower() == "c" || input.ToLower() == "s")
                            validInput = true;
                        else {
                            menu();
                            account.getInitializedDetails();
                            Console.WriteLine("We're sorry, you seem to have given an unexecutable command.");
                            Console.WriteLine("Please input what account you would like to make: ");
                            Console.WriteLine("'c' for checking, 's' for savings");
                            
                        }
                    }while(!validInput);

                    if(input.ToLower() == "c" ) {
                        account.type = "c";
                    }
                    else account.type = "s";

                    //Validate initial deposit.
                    menu();
                    account.getInitializedDetails();
                    Console.Write("Please input your initial deposit: $");
                    validInput = false;
                    double amount;
                    do {
                        input = Console.ReadLine();
                        //Validate for an integer > 0 or cancel command.
                        if(Double.TryParse(input,out amount) && amount > 0)
                        
                            validInput = true;
                        else {
                            menu();
                            account.getInitializedDetails();
                            Console.WriteLine("We're sorry, you seem to have given an unexecutable command.");
                            Console.Write("Please input your initial deposit: $");
                            
                        }
                    }while(!validInput);
                    amount = Math.Round(amount,2,MidpointRounding.ToZero);
                    account.balance = amount;


                    //Finalize and return to main menu.

                    //**FIX ME
                    // Accounts.accNo++;
                    // account.number = Accounts.accNo;
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
                }
                //Login selected
                while(loggingIn){
                    menu();
                    Console.WriteLine("Please enter your credentials to continue: ('c') to cancel");
                    Console.Write("Email: ");
                   
                    string email = Console.ReadLine();
                    if(email.ToLower() == "c") {
                        servicingCustomer = false;
                        loggingIn = false;
                    }
                    else {
                        menu();
                        Console.WriteLine("Please enter your credentials to continue: ('c') to cancel");
                        Console.WriteLine("Username: " + email);
                        Console.Write("Password: ");

                        string password = Console.ReadLine();
                        if(password.ToLower() == "c") {
                            servicingCustomer = false;
                            loggingIn = false;
                        }
                        else {
                            //Finally we can try to log in!
                            bool loginSuccess = account.Login(email,password);
                            if(loginSuccess){
                                loggingIn = false;
                                menu();
                                Console.WriteLine("Login succesful! Press enter to continue.");
                                Console.ReadLine();
                            }
                            else {
                            
                            }
                        }
                        
                    }
                    
                }
            #endregion

            #region Customer Service
                
                while(servicingCustomer) {
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
                            account.checkBalance();

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
                                    account.checkBalance();
                                }
                                if(amount > account.balance) {
                                    validInput = false;
                                    menu();
                                    Console.WriteLine("We're sorry, you can't overdraw your account.");
                                    Console.WriteLine("Please input how much you would like to withdraw: (press 'c' to cancel):");
                                    account.checkBalance();
                                }
                            }while(!validInput);

                            //Check for valid withdawl (input isn't 'c')
                            if(amount > 0) {
                                //Transaction
                                amount = Math.Round(amount,2,MidpointRounding.ToZero);
                                account.withdraw(amount);
                                //Confirmation
                                menu();
                                Console.WriteLine(amount + " withdrawn.");
                                account.checkBalance();

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
                                    account.checkBalance();
                                }
                            }while(!validInput);

                             //Check for valid withdawl
                            if(amount > 0) {
                                //Transaction
                                amount = Math.Round(amount,2,MidpointRounding.ToZero);
                                account.deposit(amount);
                                //Confirmation
                                menu();
                                Console.WriteLine(amount + " deposited.");
                                account.checkBalance();

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
                            account.getInitializedDetails();
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
                    //Deactivate Account
                    else if (validatedInput == 5) {
                        #region Deactivate Account
                            bool deactivating = false;
                            do {

                                //Implement me!!
                                deactivating = true;

                            }while(deactivating);

                        #endregion Deactivate Account
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
                }
            #endregion Customer Service
        }
    }
}
