using System;
public class Account
    {
        private decimal _balance; //field(name,balance)
        private string _name;

        //Constructor initialize account with name and starting balance
        public Account(string name, decimal startingBalance)
        {
            _name = name;
            _balance = startingBalance;

        }
        //deposit money to account
        public bool Deposit(decimal amountToAdd)
        {
            if (amountToAdd > 0)
            {
                _balance = _balance + amountToAdd;
                return true;
            }
            return false;
        }


        //withdraw money from account
        public bool Withdraw(decimal amountToWithdraw)
        {
            if (_balance >= amountToWithdraw)
            {
                _balance = _balance - amountToWithdraw;
                return true;
            }
            return false;
        }

        // Method to read the account name
        public string Name
        {
            get { return _name; }
        }

        // Method to read the current account balance
        public decimal Balance
        {
            get { return _balance; }
        }

        //print bank acount details
        public void Print()
        {
            Console.WriteLine($"Name: {_name}");
            Console.WriteLine($"Balance: {_balance}$");
        }



    }