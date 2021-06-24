using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    interface Methods
    {
        string ShowCredit();
    }
    class Person
    {
        public string Name;
        public string Email;
        public string PhoneNumber;
        public string Password;
        public string PictureURL;
        public Person(string name, string email, string phone, string pass, string pic)
        {
            Name = name;
            Email = email;
            PhoneNumber = phone;
            Password = pass;
            PictureURL = pic;
        }
        public List<string> ShowBooks()
        {
            // show all books
            List<string> AllBooks = new List<string>();
            return AllBooks;
        }
    }
    class Employee : Person, Methods
    {
        public Employee(string name, string email, string phone, string pass, string pic)
            : base(name, email, phone, pass, pic)
        {

        }
        public List<string> ShowBorrowedBooks()
        {
            //borrowed books
            List<string> BorrowedBooks = new List<string>();
            return BorrowedBooks;
        }
        public List<string> ShowAvaiableBooks()
        {
            //available books
            List<string> AvailableBooks = new List<string>();
            return AvailableBooks;
        }
        public List<string> ShowUser()
        {
            //all users
            List<string> AllUser = new List<string>();
            return AllUser;
        }
        public List<string> ShowLateReturnUser()
        {
            //late returning users
            List<string> LateReturnUser = new List<string>();
            return LateReturnUser;
        }
        public List<string> ShowLatePayUser()
        {
            //late pay users
            List<string> LatePayUser = new List<string>();
            return LatePayUser;
        }
        public List<string> ShowUniqueUsers(string name)
        {
            //info : one of the users
            List<string> InfoUniqueUser = new List<string>();
            return InfoUniqueUser;
        }
        public string ShowCredit()
        {
            //money in bank of the employee
            string money = "";
            return money;
        }
        public void EditInfo()
        {
            //Edit Info of employee

        }
    }
    class User : Person, Methods
    {
        public User(string name, string email, string phone, string pass, string pic)
                    : base(name, email, phone, pass, pic)
        {

        }
        public void BorrowBook(string name)
        {
            //the Book is borrowed
        }
        public void ReturnBook(string name)
        {
            //the book is Returned
        }
        public void PayFine()
        {
            //fine for delay
        }
        public void PaySubscription()
        {
            //pay for subscription
        }
        public string ShowCredit()
        {
            //show money
            string money = "";
            return money;
        }
        public void IncreseCredit()
        {
            // increase money
        }
        public void EditInfo()
        {
            //edit info of use
        }
    }
    class Admin : Person, Methods
    {
        public Admin(string name, string email, string phone, string pass, string pic)
            : base(name, email, phone, pass, pic)
        {

        }
        public void AddEmployee(string name, string email, string phone, string pass, string pic)
        {
            //new object of class Employee
        }
        public void DeleteEmployee(Employee emp)
        {
            // delete the employee
        }
        public void PayEmployee()
        {
            //pay all employees a defined money
        }
        public void AddBooks(string name, string writer, string genre, string realeseNumber)
        {
            //new book object
        }
        public string ShowCredit()
        {
            //show all money
            string money = "";
            return money;
        }
        public void IncreaseMoney()
        {
            //increase money of bank   
        }
    }
    class Book
    {
        string Name;
        string Writer;
        string Genre;
        string RealeseNumber;

        public Book(string name, string writer, string genre, string realeseNumber)
        {
            Name = name;
            Writer = writer;
            Genre = genre;
            RealeseNumber = realeseNumber;
        }
    }
    class Pay
    {
        string CreditNumber;
        string CVV;
        int Year;
        int Month;
        public Pay(string CreditNumber, string CVV, int Year, int Month)
        {
            this.CreditNumber = CreditNumber;
            this.CVV = CVV;
            this.Year = Year;
            this.Month = Month;
        }

        public bool CheckCVV()
        {
            //check CVV with regex
            return true;
        }
        public bool CheckCreditNumber()
        {
            //check credit number with regex
            return true;
        }
        public bool CheckExpireDate()
        {
            //check expire date
            return true;
        }
    }
    public partial class App : Application
    {
    }
}
