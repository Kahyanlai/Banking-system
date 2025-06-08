public class WithdrawTransaction : Transaction
{
    // Fields for the transaction
    private Account _account; 
    private bool _success = false; 

// Constructor to initialize transaction with account and amount
    public WithdrawTransaction(Account account, decimal amount) : base(amount)
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

    //method to execute withdraw
    public override void Execute() 
    {
        base.Execute();
        _success = _account.Withdraw(_amount); 
        
    }

    //method to rollback(undo) the transaction
    public override void Rollback() 
    {
        base.Rollback();

        if (_success)
        {
            // If the transaction was successful, deposit the amount back from the account
            _account.Deposit(_amount);
            _reversed = true;
        }

        if (_account.Deposit(_amount))// Reversing the withdrawal by depositing the amount back to the account
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
        Console.WriteLine($"At {base.DateStamp}, {_amount}$ was successfully withdrawn from {_account.Name}'s account.");
        Console.WriteLine($"Success status: {_success}");
        Console.WriteLine($"Reverse status: {_reversed}");
    }
    else
    {   
        Console.WriteLine($"At {base.DateStamp}, withdrawal of {_amount}$ from {_account.Name}'s account failed due to insufficient funds.");
        Console.WriteLine($"Success status: {_success}");
        Console.WriteLine($"Reverse status: {_reversed}");
    }
}
}
