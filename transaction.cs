public abstract class Transaction
{
protected decimal _amount; //transaction amount
protected bool _executed; //transaction execution status
protected bool _reversed; //transaction reversal status
private DateTime _dateStamp; //date and time of transaction

public Transaction(decimal amount)
{
    _amount = amount;
}

//property indicating transaction execution status
public bool Executed 
    {
        get 
        {
            return _executed; 
        }
    }

//abstract dont need to implement it, just need to define it
public abstract bool Success 
    {
        get;
    }

//property indicating transaction reversal status
public bool Reversed
    {
        get 
        {
            return _reversed; 
        }
    }

//property to access the date and time stamp of the transaction
public DateTime DateStamp
    {
        get 
        {
            return _dateStamp; 
        }
    }

//abstract method to print the transaction
public abstract void Print();


//virtual method to execute the transaction
public virtual void Execute()
{
    if ( _executed ) 
        {
            throw new Exception("Cannot execute this transaction as it has already been executed ");
        }

        _executed = true;
        _dateStamp = DateTime.Now;
}

//virtual method to rollback the transaction
public virtual void Rollback()
{
    //throw an exception if the transaction has not been executed 
    if (!_executed)
        {
            throw new Exception("Cannot rollback this transaction as it has not been executed");
        }

        //Throw an exception if the transaction has been reversed
        if (_reversed)
        {
            throw new Exception("Cannot rollback this transaction as it has already been reversed.");
        }
}
}
