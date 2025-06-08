public class DepositTransaction : Transaction
{
    // Fields for the transaction
    private Account _account; 
    private bool _success = false; 

// Constructor to initialize transaction with account and amount
    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }
    
    //property indicating transaction success status
    public override bool Success 
    {
        get 
        {
            return _success; 
        }
    }

    // Method to execute the deposit transaction
    public override void Execute() 
    {
        base.Execute();
        _success = _account.Deposit(_amount); 
    }

     // Method to rollback(undo) the transaction
    public override void Rollback() 
    {
        base.Rollback();

            if (_success)
        {
            // If the transaction was successful, withdraw the amount back from the account
            _account.Withdraw(_amount);
            _reversed = true;
        }

        if (_account.Withdraw(_amount)) // Reverse the deposit by withdrawing the deposited amount
        {
            _reversed = true;
            _success = false;
            _executed = false;
        }
        else
        {
            _reversed = false;
            _success = true;
            _executed = true;
        }
    }

    // Method to print transaction details
    public override void Print()
{
    if (_success)
    {   
        Console.WriteLine($"At {base.DateStamp}, {_amount}$ was successfully deposited into {_account.Name}'s account.");
        Console.WriteLine($"Success status: {_success}");
        Console.WriteLine($"Reverse status: {_reversed}");
    }
    else
    {   
        Console.WriteLine($"At {base.DateStamp}, deposit of {_amount}$ into {_account.Name}'s account failed.");
        Console.WriteLine($"Success status: {_success}");
        Console.WriteLine($"Reverse status: {_reversed}");
    }
}


}
