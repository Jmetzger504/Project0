using System; 

class Accounts {

    #region Properties

        int accNo;
        string accName;
        string accType;
        double accBalance;
        bool accIsActive = true;
        string accEmail;

    #endregion

    #region Gets/Sets

        public int AccountNumber {

            get{return accNo;}
            set {accNo = value;}

        }

        public string AccountName {

            get{return accName;}
            set{accName = value;}

        }

        public string AccountType {

            get{return accType;}
            set{accType = value;}

        }

        public double AccountBalance {

            get{return accBalance;}
            set {accBalance = Math.Round(value,2);}

        }

        public bool isAccountActive {

            get{return accIsActive;}
            set{accIsActive = value;}

        }

        public string AccountEmail {

            get{return accEmail;}
            set{accEmail = value;}

        }

    #endregion

    #region Account Methods

        #region withdrawl
        public void withdraw(double amount) {
            amount = Math.Round(amount,2);
            AccountBalance -= amount;
            Console.WriteLine("Your new balance after the withdrawl is: " + AccountBalance);
        }
        #endregion

        #region deposit
        public void deposit(double amount) {
            amount = Math.Round(amount,2);
            AccountBalance += amount;
            Console.WriteLine("Your new balance after the deposit is: " + AccountBalance);
        }
        #endregion
        #region AccountDetails
        public void getAccountDetails() {
            Console.WriteLine("Account No: " + AccountNumber);
            Console.WriteLine("Name: " + AccountName);
            Console.WriteLine("Type: " + AccountType);
            Console.WriteLine("Balance: " + AccountBalance);
            Console.WriteLine("Email: " + AccountEmail);
        }
        #endregion
        
        #region checkBalance
        public void CheckBalance() {
            Console.WriteLine("Your current account balance: $" + AccountBalance);
        }
        #endregion

    #endregion

}