public class TransferTransaction : Transaction
{   
    // Fields for the transaction
    private Account _toAccount; 
    private Account _fromAccount; 
    private DepositTransaction _theDeposit; 
    private WithdrawTransaction _theWithdraw; 
    private bool _success = false; 

    //property indicating transaction success status
    public override bool Success 
    {
        get 
        {
            return _success; //return the value of _success, which indicates whether transfer operation is successful
        }
    }
    
    // Constructor to initialize transaction with accounts and amount
    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _theDeposit = new DepositTransaction(_toAccount, _amount);
        _theWithdraw = new WithdrawTransaction(_fromAccount, _amount);
    }

    // Method to execute the transfer transaction
    public override void Execute() 
    {
        base.Execute();

        _theWithdraw.Execute(); // executes the withdrawal transaction, attempting to withdraw funds from Jake account.
        if(_theWithdraw.Success) //checks if the withdrawal transaction was successful. If it was, it proceeds with the deposit transaction; otherwise, it skips the deposit and proceeds to rollback the withdrawal.
        {
            _theDeposit.Execute(); //If the withdrawal was successful,executes the deposit transaction, attempting to deposit funds into KahYan account.
            if (_theDeposit.Success) // Check if the deposit was successful
            {
                _executed = true; // If both withdrawal and deposit were successful, mark the transfer as executed
                _success = true; // Mark the transfer as successful
            }
            else
            {
                _theWithdraw.Rollback(); //if deposit unsuccesful, rollback the withdrawal transaction
            }
        }
    }

    // Method to rollback the transaction
    public override void Rollback()
    {
        base.Rollback();

        _theWithdraw.Rollback();//rollback the withdrawal transaction, attempting to reverse the transfer by withdrawing the transferred amount from the KahYan account.
        if(_theWithdraw.Success)//if the withdrawal transaction success, it proceeds with executing the deposit transaction to reverse the transfer.
        {
            _theDeposit.Execute();//If the withdrawal was successful, this line executes the deposit transaction to reverse the transfer by depositing the transferred amount back into the Jake account.
            if (_theDeposit.Success)//checks if the deposit transaction was successful.
            {
                _executed = false;
                _success = false;
                _reversed = true;
            }
            else
            {
                _theWithdraw.Rollback();//If the deposit reversal fails, it rolls back the withdrawal operation,
                                        //updates the transaction status to indicate that it has not been reversed
                _executed = true;
                _success = true;
                _reversed = false;
            }
        }
    }

    // Method to print transaction details
    public override void Print() 
{
    if (_success)
    {   
        Console.WriteLine($"At {base.DateStamp}, transfer of {_amount}$ from {_fromAccount.Name}'s account to {_toAccount.Name}'s account was successful.");
        Console.WriteLine($"Success status: {_success}");
        Console.WriteLine($"Reverse status: {_reversed}");
    }
    else
    {   
        Console.WriteLine($"At {base.DateStamp}, transfer of {_amount}$ from {_fromAccount.Name}'s account to {_toAccount.Name}'s account failed due to insufficient funds in {_fromAccount.Name}'s account.");
        Console.WriteLine($"Success status: {_success}");
        Console.WriteLine($"Reverse status: {_reversed}");
    }
}
}