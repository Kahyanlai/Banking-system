using System;
using SplashKitSDK;

namespace BankApp
{
    // Class representing the program logic
    public class Program
    {   
        // Main method of the program
        public static void Main()
        {   
            Bank bank = new Bank(); // Create a new Bank instance

            MenuOption userSelection;

            // Main program loop
            do
            {
                // Read user option from the menu
                userSelection = ReadUserOption();

                // Switch statement to handle user selections
                switch (userSelection)
                {
                    case MenuOption.AddAccount:
                        AddNewAccount(bank);
                        break;

                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;

                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;
                    
                    case MenuOption.Transfer:
                        DoTransfer(bank);
                        break;

                    case MenuOption.Print:
                        DoPrint(bank);
                        break;
                    
                    case MenuOption.History:
                        bank.PrintTrasactionHistory();
                        break;

                    case MenuOption.Quit:
                        Console.WriteLine("You have quit the program.");
                        break;
                }
            } while (userSelection != MenuOption.Quit);
        }

        // Method to add a new account
        public static void AddNewAccount(Bank bank)
        {
            string name;

            while (true)
            {
                try
                {
                    Console.Write("Enter account name: ");
                    name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Account name cannot be empty. Please enter a valid name.");
                        continue;
                    }

                    foreach (char characters in name)
                    {
                        if (!char.IsLetter(characters))
                        {
                            throw new FormatException("Account name should only contain alphabetic characters.");
                        }
                    }

                    break;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            decimal startingBalance;

            // Validate user input for starting balance
            while (true)
            {
                try
                {   
                    Console.Write("Enter starting balance: ");
                    startingBalance = decimal.Parse(Console.ReadLine());
                    
                    if (startingBalance <= 0)
                    {
                        Console.WriteLine("Please insert a positive number or non-zero value.");
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number or value!");
                }
            }

            // Create a new account and add it to the bank
            Account newAccount = new Account(name, startingBalance);
            bank.AddAccount(newAccount);
            Console.WriteLine($"New account '{name}' with balance {startingBalance} created.");
        }

        // Method to find an account by its name
        public static Account FindAccount(Bank fromBank)
        {
            Console.Write("Enter account name: ");
            string name = Console.ReadLine();
            Account result = fromBank.GetAccount(name);
            if (result == null)
            {
                Console.WriteLine($"No account found with name {name}");
            }
            return result;
        }

        // Method to perform a deposit in an account
        // Method to perform a deposit in an account
public static void DoDeposit(Bank toBank)
{
    Account toAccount = FindAccount(toBank);
    if (toAccount == null) return;

    decimal amount;
    while (true)
    {
        try
        {
            Console.Write("Enter the amount you want to deposit: ");
            amount = decimal.Parse(Console.ReadLine());
            if (amount <= 0)
            {
                Console.WriteLine("Please insert a positive number or non-zero value.");
                continue;
            }
            break;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number or value!");
        }
    }

    // Create a deposit transaction and execute it
    DepositTransaction depositTransaction = new DepositTransaction(toAccount, amount);
    toBank.ExecuteTransaction(depositTransaction);
    depositTransaction.Print();

    // Print remaining balance
    Console.WriteLine($"Remaining balance in {toAccount.Name}'s account: {toAccount.Balance}$");
}

// Method to perform a withdrawal from an account
public static void DoWithdraw(Bank toBank)
{
    Account toAccount = FindAccount(toBank);
    if (toAccount == null) return;

    decimal amount;
    while (true)
    {
        try
        {
            Console.Write("Enter the amount you want to withdraw: ");
            amount = decimal.Parse(Console.ReadLine());
            if (amount <= 0)
            {
                Console.WriteLine("Please insert a positive number or non-zero value.");
                continue;
            }
            break;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number or value!");
        }
    }

    // Create a withdraw transaction and execute it
    WithdrawTransaction withdrawTransaction = new WithdrawTransaction(toAccount, amount);
    toBank.ExecuteTransaction(withdrawTransaction);
    withdrawTransaction.Print();

    // Print remaining balance
    Console.WriteLine($"Remaining balance in {toAccount.Name}'s account: {toAccount.Balance}$");
}

// Method to perform a transfer between two accounts
public static void DoTransfer(Bank toBank)
{
    Console.Write("Enter the account name to transfer from: ");
    string fromAccountName = Console.ReadLine();
    Account fromAccount = toBank.GetAccount(fromAccountName);
    if (fromAccount == null)
    {
        Console.WriteLine($"No account found with name {fromAccountName}");
        return;
    }

    Console.Write("Enter the account name to transfer to: ");
    string toAccountName = Console.ReadLine();
    Account toAccount = toBank.GetAccount(toAccountName);
    if (toAccount == null)
    {
        Console.WriteLine($"No account found with name {toAccountName}");
        return;
    }

    decimal amount;
    while (true)
    {
        try
        {
            Console.Write($"Enter the amount you want to transfer from {fromAccount.Name} to {toAccount.Name}: ");
            amount = decimal.Parse(Console.ReadLine());
            if (amount <= 0)
            {
                Console.WriteLine("Please insert a positive number or non-zero value.");
                continue;
            }
            break;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number or value!");
        }
    }

    // Create a transfer transaction and execute it
    TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, amount);
    toBank.ExecuteTransaction(transferTransaction);
    transferTransaction.Print();

    // Print remaining balance for both accounts
    Console.WriteLine($"Remaining balance in {fromAccount.Name}'s account: {fromAccount.Balance}$");
    Console.WriteLine($"Remaining balance in {toAccount.Name}'s account: {toAccount.Balance}$");
}


        // Method to print account details
        public static void DoPrint(Bank toBank)
        {
            Account toAccount = FindAccount(toBank);  
            if (toAccount == null) return;

            // If the account is found, print its details
            toAccount.Print();
        }


        // Method to read user option from the menu
        public static MenuOption ReadUserOption()
        {
            int option;
            string inputNumber;

            // Loop until a valid option is chosen
            do
            {
                Console.WriteLine(" --------------------------------");
                Console.WriteLine("|        Bank Menu               |");
                Console.WriteLine("|--------------------------------|");
                Console.WriteLine("| 1. Withdraw Money              |");
                Console.WriteLine("| 2. Deposit Money               |");
                Console.WriteLine("| 3. Transfer Money              |");
                Console.WriteLine("| 4. Print Bank Account Details  |");
                Console.WriteLine("| 5. Add Account                 |");
                Console.WriteLine("| 6. Quit                        |");
                Console.WriteLine("| 7. Print Transaction History   |");
                Console.WriteLine("---------------------------------");


                Console.Write("Choose an option[1-7]: ");
                inputNumber = Console.ReadLine();

                // Validate user input
                try
                {
                    option = Convert.ToInt32(inputNumber);
                }
                catch (FormatException)
                {
                    Console.WriteLine("That was not a number, please enter a valid number within 1-7!");
                    option = -1;
                }
            } while (option < 1 || option > 7);

            return (MenuOption)(option - 1);
        }
    }

    // Enumeration for different Menu options for bank program
    public enum MenuOption
    {
        Withdraw,
        Deposit,
        Transfer,
        Print,
        AddAccount,
        Quit,
        History,
    }
}