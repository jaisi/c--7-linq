using System;
using System.Linq;
using System.Collections.Generic;

public class Customer
{
    public string Name { get; set; }
    public double Balance { get; set; }
    public string Bank { get; set; }

}

// Define a bank
public class Bank
{
    public string Symbol { get; set; }
    public string Name { get; set; }
}

public class Program
{

    public static void Main() {

        // Find the words in the collection that start with the letter 'L'
        List<string> fruits = new List<string>() 
        {
            "Lemon", "Apple", "Orange", "Lime", "Watermelon", "Loganberry"
        };

         IEnumerable<string> Lfruits = from fruit in fruits
                where fruit.StartsWith("L")  
                select fruit;

        foreach(string f in Lfruits)
        {
                Console.WriteLine($"{f}");
        }

        // Which of the following numbers are multiples of 4 or 6
        List<int> num = new List<int>()
        {
            15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
        };

        IEnumerable<int> fourSixMultiples = num.Where(number => number % 6 == 0 || number % 4 == 0);
        
        foreach (int number in fourSixMultiples)
        {
            Console.WriteLine(number);
        }

        // Order these student names alphabetically, in descending order (Z to A)
        List<string> names = new List<string>()
        {
            "Heather", "James", "Xavier", "Michelle", "Brian", "Nina",
            "Kathleen", "Sophia", "Amir", "Douglas", "Zarley", "Beatrice",
            "Theodora", "William", "Svetlana", "Charisse", "Yolanda",
            "Gregorio", "Jean-Paul", "Evangelina", "Viktor", "Jacqueline",
            "Francisco", "Tre" 
        };
        IEnumerable<string> sortedNames = from name in names
            orderby name descending
            select name;
        
        foreach (string n in sortedNames)
        {
            Console.WriteLine($"{n}");
        }

        // Build a collection of these numbers sorted in ascending order
        List<int> numbers = new List<int>()
        {
            15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
        };

        IEnumerable<int> sortedNumbers = from number in numbers
            orderby number ascending
            select number;
        
        foreach(int n in sortedNumbers)
        {
            Console.WriteLine($"{n}");
        }

        // Output how many numbers are in this list
        List<int> numbersAgain = new List<int>()
        {
            15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
        };
       int countAgain = (from n in numbersAgain select n).Count();

       
            Console.WriteLine($"Count is {countAgain}");
        
        // How much money have we made?
        List<double> purchases = new List<double>()
        {
            2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
        };
        IEnumerable<double> money = from p in purchases
            select p;
        double total = money.Sum();
        Console.WriteLine($"We have made {total:C}");

        // What is our most expensive product?
        List<double> prices = new List<double>()
        {
            879.45, 9442.85, 2454.63, 45.65, 2340.29, 34.03, 4786.45, 745.31, 21.76
        };
        IEnumerable<double> expensive = from p in prices
            select p;
        Console.WriteLine($"Most expensive product is {expensive.Max():C}");

        /*
        Store each number in the following List until a perfect square
        is detected.

        Ref: https://msdn.microsoft.com/en-us/library/system.math.sqrt(v=vs.110).aspx
        */
        List<int> wheresSquaredo = new List<int>()
        {
            66, 12, 8, 27, 82, 34, 7, 50, 19, 46, 81, 23, 30, 4, 68, 14
        };


        IEnumerable<int> query =
            wheresSquaredo.TakeWhile((x) => Math.Sqrt(x)%1!=0);

        foreach(int x in query)
        {
            //Console.WriteLine("i am here");
            Console.WriteLine(x);
        }

        // Create some banks and store in a List
        List<Bank> banks = new List<Bank>() {
            new Bank(){ Name="First Tennessee", Symbol="FTB"},
            new Bank(){ Name="Wells Fargo", Symbol="WF"},
            new Bank(){ Name="Bank of America", Symbol="BOA"},
            new Bank(){ Name="Citibank", Symbol="CITI"},
        };

        List<Customer> customers = new List<Customer>() {
            new Customer(){ Name="Bob Lesman", Balance=80345.66, Bank="FTB"},
            new Customer(){ Name="Joe Landy", Balance=9284756.21, Bank="WF"},
            new Customer(){ Name="Meg Ford", Balance=487233.01, Bank="BOA"},
            new Customer(){ Name="Peg Vale", Balance=7001449.92, Bank="BOA"},
            new Customer(){ Name="Mike Johnson", Balance=790872.12, Bank="WF"},
            new Customer(){ Name="Les Paul", Balance=8374892.54, Bank="WF"},
            new Customer(){ Name="Sid Crosby", Balance=957436.39, Bank="FTB"},
            new Customer(){ Name="Sarah Ng", Balance=56562389.85, Bank="FTB"},
            new Customer(){ Name="Tina Fey", Balance=1000000.00, Bank="CITI"},
            new Customer(){ Name="Sid Brown", Balance=49582.68, Bank="CITI"}
        };

        // Build a collection of customers who are millionaires
            var millionaires = from c in customers
            where c.Balance >= 1000000
            select c;

            foreach(var c in millionaires)
            {
                Console.WriteLine($"{c.Name} ${c.Balance}") ;
            }

        
        
        
        /* 
            Given the same customer set, display how many millionaires per bank.
            Ref: https://code.msdn.microsoft.com/LINQ-to-DataSets-Grouping-c62703ea

            Example Output:
            WF 2
            BOA 1
            FTB 1
            CITI 1
        */
         var groupedMillionaires = from c in customers
            where c.Balance >=1000000
            join bank in banks on c.Bank equals bank.Symbol
            group c by c.Bank into q //grouping by Bank
            select new {Bank = q.Key, Count = q.Count(), Customers = q};//q returns keys and values

        foreach(var q  in groupedMillionaires)
        {
            Console.WriteLine($"{q.Bank} -  {q.Count}") ;
            
        }

        /* alternate way
        var groupedMillionaires = customers.Where(c => c.Balance>=1000000)
                                            .GroupBy(d => d.Bank);//groupby now only works from what is returned from where

        foreach(var m in groupedMillionaires)
        {
            Console.WriteLine($"{m.Key} {m.Count()}");
            foreach(var cust in m)
            {
                Console.WriteLine($"{cust.Name} {cust.Balance}");
            }
        }*/

        /*
            TASK:
            As in the previous exercise, you're going to output the millionaires,
            but you will also display the full name of the bank. You also need
            to sort the millionaires' names, ascending by their LAST name.

            Example output:
                Tina Fey at Citibank
                Joe Landy at Wells Fargo
                Sarah Ng at First Tennessee
                Les Paul at Wells Fargo
                Peg Vale at Bank of America
        */

        var millionaireReport =
            from customer in customers
            where customer.Balance >=1000000
            join bank in banks on customer.Bank equals bank.Symbol
            orderby customer.Name.Substring(customer.Name.IndexOf(" ") + 1)//to sort by lastname
            select new {CustomerName = customer.Name, Bank = bank.Name };

            foreach (var customer in millionaireReport)
            {
                Console.WriteLine($"{customer.CustomerName} at {customer.Bank}");
            }
           
        
        
    }
}