using System; 
using System.Data.SqlClient;

class Accounts {

    public static int AccNo;

    #region Properties
    public int number { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public double balance { get; set; }
    public bool isActive { get; set; }
    public string email { get; set; }

    #endregion

    #region Account Methods

        #region Get initialized fields

            public void getInitializedDetails() {
                if(email != "")
                    Console.WriteLine("Email: " + email);
                if(name != null)
                    Console.WriteLine("Account Name: " + name);
                if(number != 0)
                    Console.WriteLine("Account Number: " + number.ToString("D8"));
                if(type != null) {
                    if(type.ToLower() == "c")
                        Console.WriteLine("Account Type: Checking");
                    else Console.WriteLine("Account Type: Savings");
                }
                if(balance != 0) 
                    Console.WriteLine("Balance: " + balance.ToString("C2"));

            }

        #endregion Get initialized fields

        #region withdrawl

        public void withdraw(double amount) {
            amount = Math.Round(amount,2);
            this.balance -= amount;
        }

        #endregion withdrawl

        #region deposit

        public void deposit(double amount) {
            amount = Math.Round(amount,2);
            this.balance += amount;
        }

        #endregion deposit

        #region AccountDetails

            public void getAccountDetails() {
                Console.WriteLine("Account Holder: " + name);
                Console.WriteLine("Email: " + email);
                Console.WriteLine("Account Number: " + number.ToString("D8"));
                if(type.ToLower() == "c")
                    Console.WriteLine("Account Type: Checking");
                else Console.WriteLine("Account Type: Savings");
                    Console.WriteLine("Balance: " + balance);
                if(isActive)
                    Console.WriteLine("Account Status: Active" );
                else Console.WriteLine("Account Status: Inactive");
                Console.Write("Press enter to return to the main menu.");
                Console.ReadLine();
            }

        #endregion AccountDetails
            
        #region checkBalance
            public void checkBalance() {
                Console.WriteLine("Account balance: " + balance.ToString("C2"));
            }

        #endregion checkBalance

    #endregion AccountMethods

   
    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-VE9QC92\TRAINERINSTANCE;Initial Catalog=bankingApp;Integrated Security=True");  
    #region Database Methods

    public virtual bool successfulLogin() {

        SqlCommand login = new SqlCommand("select * from accountDetails where Email = @email",con);
        login.Parameters.AddWithValue("@email",email);
        SqlDataReader read;
        try {
            con.Open();
            read = login.ExecuteReader();

            if(read.Read()) 
                return false;
            
        }
        catch(System.Exception es) {
            Console.WriteLine(es.Message);
            Console.ReadLine();
        }
        finally{
            con.Close();
        }
        return true;
    }

    public bool Login(string email, string password) {

        SqlCommand login = new SqlCommand("select * from accountDetails where Email = @email and Password = @password",con);
        login.Parameters.AddWithValue("@email",email);
        login.Parameters.AddWithValue("@password",password);
        SqlDataReader read;
        try {
            con.Open();
            read = login.ExecuteReader();

            if(read.Read()) {
                this.number = Convert.ToInt32(read[0]);
                this.name = Convert.ToString(read[1]);
                this.type = Convert.ToString(read[2]);
                this.balance = Convert.ToDouble(read[3]);
                this.isActive = Convert.ToBoolean(read[4]);
                this.email = Convert.ToString(read[5]);
                return true;
            }

        }
        catch(System.Exception es) {
            Console.WriteLine(es.Message);
        }
        finally{
            con.Close();
        }
        
        return false;
    }


    #endregion Database Methods

}