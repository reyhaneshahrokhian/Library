using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;


namespace WpfProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    ///    
    interface Methods
    {
        public string ShowCredit();
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
    class Admin : Person, Methods
    {
        //baraye admin ke email nadarim !!!!!!!!!!!!!!!!!!!!!!!!!
        public Admin(string name, string email, string phone, string pass, string pic)
            : base(name, email, phone, pass, pic)
        {
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                C: \Users\saba\Desktop\saba folder\project\sql.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            /*******************************      we only have one name and one pass / is difult in the bank ziro?    *****************************/
            // add in SQL
            string command = "insert into Admin values('" + Name.Trim() + "' ,'" + pass.Trim() + "' , '" + "0" + "')";

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.BeginExecuteNonQuery();

            connection.Close();
        }
        public void AddEmployee(string name, string email, string phone, string pass, string pic)
        {
            //new object of class Employee
            Employee employee = new Employee(name, email, phone, pass, pic);
        }
        public void DeleteEmployee(Employee emp)
        {
            /********* show admin a message "are sure you want to delete? "********/
            // delete the employee
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                C: \Users\saba\Desktop\saba folder\project\sql.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            /*************     we use email because it's unique for each person      *************/

            string command = "delete from Employee where name ='" + emp.Name + "' " +
                ", email = '" + emp.Email + "' phoneNumber = '" + emp.PhoneNumber + "', pass = '" + emp.Password + "', PictureLoc ";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.BeginExecuteNonQuery();

            connection.Close();
        }
        public void PayEmployee()
        {
            /************** how much money? ***********/
            //pay all employees a defined money

            int money = 0;

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                C: \Users\saba\Desktop\saba folder\project\sql.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            DataTable data = new DataTable();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                /*********************************  i don't know what to do ********************************/

                string command = "update Employee SET wallet = wallet + '" + money + "'";
                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.BeginExecuteNonQuery();
            }
            connection.Close();
        }
        public void AddBooks(string name, string writer, string genre, string realeseNumber)
        {
            //new book object
            Book book = new Book(name, writer, genre, realeseNumber);
        }
        public string ShowCredit()
        {
            //show all money
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                C: \Users\saba\Desktop\saba folder\project\sql.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            DataTable data = new DataTable();
            string money = data.Rows[2][0].ToString();
            connection.Close();

            return money;
        }
        public void IncreaseMoney()
        {
            /********* how much money ? ********/
            //increase money of bank  
            string money = "";

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                C: \Users\saba\Desktop\saba folder\project\sql.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            DataTable data = new DataTable();
            string command = "update Admin SET Bank = '" + (int.Parse(data.Rows[2][0].ToString()) + int.Parse(money)).ToString() + "'" +
                                "where name = '" + "admin" + "'";

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.BeginExecuteNonQuery();

            connection.Close();
        }
    }

    class Employee : Person, Methods
    {
        public Employee(string name, string email, string phone, string pass, string pic)
            : base(name, email, phone, pass, pic)
        {
            bool repeated = false;

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                C: \Users\saba\Desktop\saba folder\project\sql.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            DataTable data = new DataTable();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if ((string)data.Rows[i][0] == Name)
                {
                    repeated = true;
                }
            }

            if (repeated == false)
            {
                /*******************************      we have name and lastname in SQL but there is only one field in the class      *****************************/

                // add in SQL
                string command = "insert into Employee values('" + Name.Trim() + "' , '" + phone.Trim() + "' ,'" + pass.Trim() + "' , '" +
                                                             email.Trim() + "' , '" + pic + "' , '" + "0" + "')";

                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.BeginExecuteNonQuery();
            }
            else
            {
                //write "reapeted employee"
            }
            connection.Close();
        }
        public List<string> ShowBorrowedBooks()
        {
            //borrowed books

            List<string> BorrowedBooks = new List<string>();

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                C: \Users\saba\Desktop\saba folder\project\sql.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            DataTable data = new DataTable();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                /********** not sure ********/
                if (data.Rows[i][5].ToString() == "0")
                {
                    BorrowedBooks.Add(data.Rows[i][0].ToString());
                    BorrowedBooks.Add(data.Rows[i][1].ToString());
                    BorrowedBooks.Add(data.Rows[i][2].ToString());
                    BorrowedBooks.Add(data.Rows[i][3].ToString());
                    BorrowedBooks.Add(data.Rows[i][4].ToString());
                }
            }

            connection.Close();

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
            bool repeated = false;

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                C: \Users\saba\Desktop\saba folder\project\sql.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            DataTable data = new DataTable();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if ((string)data.Rows[i][0] == Name)
                {
                    repeated = true;
                }
            }

            /*******************************      we have name and lastname in SQL but there is only one field in the class      *****************************/

            if (repeated == false)
            {
                // add in SQL
                string command = "insert into Userr values('" + Name.Trim() + "' , '" + phone.Trim() + "' ,'" + pass.Trim() + "' , '" +
                                                             email.Trim() + "' , '" + pic + "', '" + "0" + "')";

                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.BeginExecuteNonQuery();
            }
            connection.Close();
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
            int count = 1;

            //add the book to SQL
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                C: \Users\saba\Desktop\saba folder\project\sql.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            DataTable data = new DataTable();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if ((string)data.Rows[i][0] == Name)
                {
                    count++;
                }
            }
            string command = "insert into Book values('" + Name.Trim() + "' , '" + Writer.Trim() + "' ,'" +
                                                    Genre.Trim() + "' , '" + RealeseNumber.Trim() + "' , '" + count + "','" + true + "'  )";

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.BeginExecuteNonQuery();

            connection.Close();
        }
    }

    public partial class App : Application
    {
    }
}
