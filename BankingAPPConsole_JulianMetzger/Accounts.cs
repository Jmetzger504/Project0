using System; 

class Accounts {

    #region Properties

    public int number { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public double balance { get; set; }
    public bool isActive { get; set; }
    public string email { get; set; }

    #endregion

    #region Account Methods

        #region withdrawl

        public void withdraw(double amount) {
            amount = Math.Round(amount,2);
            this.balance -= amount;
            Console.WriteLine("Your new balance after the withdrawl is: " + this.balance);
            
        }

        #endregion withdrawl

        #region deposit

        public void deposit(double amount) {
            amount = Math.Round(amount,2);
            this.balance += amount;
            Console.WriteLine("Your new balance after the deposit is: " + this.balance);
        }

        #endregion deposit

        #region AccountDetails

            public void getAccountDetails() {
                Console.WriteLine("Account No: " + number);
                Console.WriteLine("Name: " + name);
                Console.WriteLine("Type: " + type);
                Console.WriteLine("Balance: " + balance);
                Console.WriteLine("Email: " + email);
            }

        #endregion AccountDetails
            
        #region checkBalance
            public void CheckBalance() {
                Console.WriteLine("Your current account balance: $" + balance);
            }

        #endregion checkBalance

    #endregion AccountMethods

}