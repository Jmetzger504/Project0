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
    *Double check region bounds.
SQL Database Implementation:
    Check email/password via count executescalar
    *Connect to local server.
    *Login/New Account
    *Get accNo from database.
    *Reflect Withdrawl in database table.
    *Reflect Deposit in database table.
    *Query accounts
    *Deactivate account
    *Delete Account 
    *Deactivated account withdraw/deposit check.
    *Email/Password to load each account.
Optional: difficulty impossible(?) -> easy.
    Switch accounts/Logout
    Admin account that can view all accounts.
    *Cancel button in submenu control flow.
    *Currency formatting
    *Check for proper withdrawl,deposit truncation
    Email regex verification
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

                }//New account chosen. Logged in after.
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
                    

                    //Acquire account email. Make sure it's unique!
                    bool uniqueEmail = false;
                    while(!uniqueEmail) {
                        menu();
                        Console.Write("Please input a valid, unique email for this account:");
                        account.email = Console.ReadLine();
                        if(account.checkEmail())
                            uniqueEmail = true;
                    }
                    
                    //Acquire account password. No validation (yet!).
                    menu();
                    account.getInitializedDetails();
                    
                    Console.Write("Please input a password of your choosing: ");
                    account.password = Console.ReadLine();

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

                    //Save valid account to database
                    account.saveNewAccount();
                   


                    //Finalize and return to main menu.
                    menu();
                    account.isActive = true;
                    account.getAccountDetails();
                    Console.ReadLine();
                    newAccount = false;
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
                                Console.WriteLine("Login unsuccesful. Your credentials were invalid.");
                                Console.WriteLine("Press enter to try again.");
                                Console.ReadLine();
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
                    Console.WriteLine("3. Account Details");
                    Console.WriteLine("4. Account Balance");
                    Console.WriteLine("5: Account Activation Status");
                    Console.WriteLine("6: Exit");
                    Console.WriteLine("42: Delete Account");

                    //Receive and validate user input
                    bool validInput = false;
                    int validatedInput = 0;
                    double amount;

                    do {
                        string input = Console.ReadLine();
                        //Validate integer in [1,5].
                        if(Int32.TryParse(input,out validatedInput) && validatedInput <= 6 && validatedInput >= 1 || validatedInput == 42)
                            validInput = true;
                        else {
                            menu();
                            Console.WriteLine("We're sorry, you seem to have given an unexecutable command.");
                            Console.WriteLine("1. Withdraw");
                            Console.WriteLine("2. Deposit");
                            Console.WriteLine("3. Account details");
                            Console.WriteLine("4. Account balance");
                            Console.WriteLine("5: Account Activation Status");
                            Console.WriteLine("6: Exit");
                            Console.WriteLine("42: Delete Account");
                        }
                    }while(!validInput);

                    //Control flow based on validated input.
                    
                    //Withdrawl
                    if(validatedInput == 1) {
                        #region Withdrawl
                            
                            if(account.isActive) {

                                menu();
                                Console.WriteLine("Please input how much you would like to withdraw: ('c' to cancel) ");
                                account.checkBalance();

                                validInput = false;
                                validatedInput = 0;
                                amount = 0;
                                //Validate input.
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
                                    Console.WriteLine(amount.ToString("C2") + " withdrawn.");
                                    account.checkBalance();

                                    //Prompt to return to main menu
                                    Console.WriteLine("Please press enter to return to the main menu.");
                                    Console.ReadLine();
                                } 
                            }
                            else {
                                menu();
                                Console.WriteLine("Sorry, your account is currently deactivated. Reactivate your account to perform transactions.");
                                Console.WriteLine("Please press enter to return to the main menu.");
                                Console.ReadLine();
                            }
                            //Else Return to main menu.
                        #endregion Withdrawl
                    }
                    //Deposit
                    else if(validatedInput == 2) {
                        #region Deposit
                            if(account.isActive) {
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

                                //Check for valid withdrawl
                                if(amount > 0) {
                                    //Transaction
                                    amount = Math.Round(amount,2,MidpointRounding.ToZero);
                                    account.deposit(amount);
                                    //Confirmation
                                    menu();
                                    Console.WriteLine(amount.ToString("C2") + " deposited.");
                                    account.checkBalance();

                                    //Prompt to return to main menu
                                    Console.WriteLine("Please press enter to return to the main menu.");
                                    Console.ReadLine();  
                                }
                            }
                            else {
                                menu();
                                Console.WriteLine("Sorry, your account is currently deactivated. Reactivate your account to perform transactions.");
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
                    //Toggle Account Activation Status.
                    else if (validatedInput == 5) {
                        #region Account Status
                            bool loop = false;
                            do {
                                if(account.isActive) {
                                    menu();
                                    Console.WriteLine("Account Status: Active");
                                    Console.WriteLine("Would you like to deactivate your account? (y/n))");
                                    Console.WriteLine("WARNING: Deactivating your account will freeze all transactions until the account is reactivated.");
                                }
                                else {
                                    menu();
                                    Console.WriteLine("Account Status: Inactive");
                                    Console.WriteLine("Would you like to reactivate your account? (y/n)");
                                    Console.WriteLine("NOTE: Reactivating your account will open your account to new transactions.");
                                }

                                string response = Console.ReadLine();

                                if(response.ToLower() == "y") {
                                    
                                    loop = false;
                                    bool unconfirmed = true;
                                    while(unconfirmed) {
                                        Console.Write("Please input your password to confirm: ('c') to cancel ");
                                        string password = Console.ReadLine();
                                        if(password == account.password) {
                                            account.isActive = !account.isActive;
                                            unconfirmed = false;
                                            account.updateAccountStatus();
                                        }
                                        else if(password.ToLower() == "c")
                                            unconfirmed = false;

                                    }
                                    menu();
                                    if(account.isActive)
                                        Console.WriteLine("Account Status: Active");
                                    else 
                                        Console.WriteLine("Account Status: Inactive");
                                    Console.WriteLine("Press enter to return to the main menu.");
                                    Console.ReadLine();
                                }
                                else if(response.ToLower() == "n") {
                                    loop = false;
                                }
                                else loop = true;
                               
                                

                            }while(loop);

                        #endregion Account Status
                    }
                    //Exit 
                    else if(validatedInput == 6) {
                        #region Exit Routine
                            servicingCustomer = false;
                            menu();
                            Console.WriteLine("Thank you for banking with us today!");
                            Console.Write("Press enter to exit.");
                            Console.ReadLine();
                            Console.Clear();

                        #endregion Exit Routine
                    }
                    //Delete Account
                    else if (validatedInput == 42) {
                        bool delete = true;
                        while(delete) {
                            menu();
                            Console.WriteLine("Would you like to delete your account? (y/n)");
                            Console.WriteLine("WARNING: THIS PROCESS CANNOT BE UNDONE ONCE PERFORMED.");
                            Console.WriteLine("YOU WILL BE ASKED TO SUPPLY A ROUTING NUMBER TO TRANSFER ANY FUNDS IN YOUR ACCOUNT");
                            Console.WriteLine("ALL NON-TRANSFERRED FUNDS MUST BE WITHDRAWN BY THE END OF THE NEXT WORK WEEK OR THEY WILL BE LOST.");
                            Console.WriteLine("YOU WILL BE ASKED FOR YOUR PASSWORD TO CONFIRM.");
                            string response = Console.ReadLine();

                            if(response.ToLower() == "n")
                                delete = false;
                            else if(response.ToLower() == "y") {
                                menu();
                                Console.WriteLine("WARNING: THIS PROCESS CANNOT BE UNDONE ONCE PERFORMED.");
                                Console.WriteLine("ALL NON-TRANSFERRED FUNDS MUST BE WITHDRAWN BY THE END OF THE NEXT WORK WEEK OR THEY WILL BE LOST.");
                                Console.Write("Please input your password to confirm you want to delete your account. Press Enter to cancel. ");
                                string password = Console.ReadLine();
                                if(password == account.password) {
                                    menu();
                                    Console.WriteLine("WARNING: THIS PROCESS CANNOT BE UNDONE ONCE PERFORMED.");
                                    Console.WriteLine("ALL NON-TRANSFERRED FUNDS MUST BE WITHDRAWN BY THE END OF THE NEXT WORK WEEK OR THEY WILL BE LOST.");
                                    Console.Write("Please type your password again to confirm. Press Enter to cancel. ");
                                    password = Console.ReadLine();
                                    if(password == account.password) {
                                        account.deleteAccount();
                                        delete = false;
                                        servicingCustomer = false;
                                        menu();
                                        Console.WriteLine("Account successfully deleted. Application Closing.");
                                        Console.ReadLine();
                                    }
                                    else {
                                        delete = false;
                                    }
                                }
                                else {
                                    delete = false;
                                }
                            }
                        }
                    }
                }
            #endregion Customer Service
        }
    }
}
