using System;
using System.Data.SqlClient;

namespace BankingDBPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Accounts account = new Accounts();
            if(account.Login("Jmetzger6@gmail.com","Password"))
                account.getInitializedDetails();
        }
    }
}
