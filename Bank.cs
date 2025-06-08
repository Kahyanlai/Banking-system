// Class representing a bank
    public class Bank
    {
        // List of accounts managed by the bank
        private List<Account> _accounts;
        private List<Transaction> _transaction;

        // Constructor to initialize the bank with an empty list of accounts
        public Bank()
        {
            _accounts = new List<Account>();
            _transaction = new List<Transaction>();
        }

        // Method to add an account to the bank
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        // Method to find an account by its name
        public Account GetAccount(string name)
        {
            foreach (Account account in _accounts)
            {
                if (account.Name.ToLower().Trim() == name.ToLower().Trim())
                {
                    return account;
                }
            }
            return null; // When account is not found
        }

        public void PrintTrasactionHistory()
        {
            foreach(Transaction transaction in _transaction)
            {
                transaction.Print();
            }
        }

        // Method to execute a transaction
        public void ExecuteTransaction(Transaction transaction)
        {
            _transaction.Add(transaction);
            transaction.Execute();
        }
    }
