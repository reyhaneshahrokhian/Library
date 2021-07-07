using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WpfProject
{
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
        public long Money;
        public Person(string name, string email, string phone, string pass, string pic)
        {
            if (name == "admin")
            {
                Name = name;
                Password = pass;
            }
            else if (CheckName(name) && CheckEmail(email) && CheckPhoneNumber(phone) && CheckPassword(pass))
            {
                Name = name;
                Email = email;
                PhoneNumber = phone;
                Password = pass;
                PictureURL = pic;
            }
        }
        public static bool CheckName(string name)
        {
            //check name with regex

            string regex = @"^[a-zA-Z]{3,32}$";
            Regex re = new Regex(regex, RegexOptions.IgnoreCase);

            if (re.IsMatch(name))
                return true;

            else
                return false;
        }
        public static bool CheckEmail(string email)
        {
            //check email with regex

            string regex = @"^[a-zA-Z0-9_-]{1,32}@[[a-zA-Z0-9]{1,8}.[a-zA-Z]{1,3}$";
            Regex re = new Regex(regex, RegexOptions.IgnoreCase);

            if (re.IsMatch(email))
                return true;

            else
                return false;
        }
        public static bool CheckPhoneNumber(string phone)
        {
            //check phone number with regex

            string regex = @"^09+[0-9]{9}$";
            Regex re = new Regex(regex, RegexOptions.IgnoreCase);

            if (re.IsMatch(phone))
                return true;

            else
                return false;
        }
        public  static bool CheckPassword(string pass)
        {
            //check password with regex

            string regex = @"^(?=.*[A-Z]).[a-zA-Z]{7,31}$";
            Regex re = new Regex(regex, RegexOptions.IgnoreCase);

            if (re.IsMatch(pass))
                return true;

            else
                return false;
        }
        public List<string> ShowBooks()
        {
            // show all books
            List<string> AllBooks = new List<string>();

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                  G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Book";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                AllBooks.Add(data.Rows[i][0].ToString());
            }

            connection.Close();

            return AllBooks;
        }
    }
    class Admin : Person, Methods
    {
        public Admin(string name, string email, string phone, string pass, string pic)
            : base(name, email, phone, pass, pic)
        {
            // add in SQL

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from adminn";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            if (data.Rows.Count == 0)
            {
                string command = "insert into adminn values('" + name.Trim() + "' ,'" + pass.Trim() + "' , '" + "0" + "')";
                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.ExecuteNonQuery();
            }
            
            connection.Close();
        }
        public List<string> ShowEmployee()
        {
            // show all employee
            List<string> AllEmployee = new List<string>();

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                  G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Employee";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                AllEmployee.Add(data.Rows[i][0].ToString());
            }

            connection.Close();

            return AllEmployee;
        }
        public void AddEmployee(string name, string email, string phone, string pass, string pic)
        {
            //new object of class Employee
            Employee employee = new Employee(name, email, phone, pass, pic);
        }
        public static void DeleteEmployee(string name)
        {
            // delete the employee

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command = "delete from Employee where name ='" + name + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }
        public void PayEmployee(string money)
        {
            //pay all employees a defined money and reduce that amout from admin's wallet

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command = "select * from Employee";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            string command2 = "select * from adminn";
            SqlDataAdapter adapter2 = new SqlDataAdapter(command2, connection);
            DataTable data2 = new DataTable();
            adapter2.Fill(data2);

            long m = long.Parse(money);

            if (long.Parse(data2.Rows[0][2].ToString()) < (data.Rows.Count * m))
            {
                //show that admin doesnt have that amount of money
            }
            else
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string command3 = "update Employee SET wallet = '" +(long.Parse(data.Rows[i][5].ToString()) + m) + "'";
                    SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                    sqlCommand3.ExecuteNonQuery();
                }

                long bank = long.Parse(data2.Rows[0][2].ToString()) - (data.Rows.Count * m);
                string command4 = "update adminn SET bank = '" + bank + "'";
                SqlCommand sqlCommand4 = new SqlCommand(command4, connection);
                sqlCommand4.ExecuteNonQuery();
            }

            connection.Close();
        }
        public void AddBooks(string name, string writer, string genre, int realeseNumber)
        {
            //new book object
            Book book = new Book(name, writer, genre, realeseNumber);
        }
        public static void DeleteBook(string name)
        {
            // delete the Book
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command = "delete from Book where name ='" + name + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }
        public string ShowCredit()
        {
            //show all money
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from adminn";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            string money = data.Rows[0][2].ToString();
            connection.Close();

            return money;
        }
        public void IncreaseMoney(string money)
        {
            //increase money of bank  
            long m = long.Parse(money);

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();


            string command2 = "select * from adminn";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            string command = "update adminn SET Bank = '" + (long.Parse(data.Rows[2][0].ToString()) + m) + "'" +
                                "where name = '" + "admin" + "'";

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

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
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();


            string command2 = "select * from Employee";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if ((string)data.Rows[i][0] == Name)
                {
                    repeated = true;
                }
            }
            if (repeated == false)
            {
                // add in SQL
                string command = "insert into Employee values('" + name.Trim() + "' , '" + phone.Trim() + "' ,'" + pass.Trim() + "' , '" +
                                                             email.Trim() + "' , '" + pic + "' , '" + "0" + "')";

                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.ExecuteNonQuery();
            }
            else
            {
                /***********write reapeted employee****************/
            }
            connection.Close();
        }
        public void DeleteUser(string name)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                                    C:\Users\saba\Desktop\saba folder\project\sql.mdfIntegrated Security=True;Connect Timeout=30");
            connection.Open();

            string command = "Delete from Userr where name = '" + name + "'";

            SqlCommand com = new SqlCommand(command, connection);
            com.ExecuteNonQuery();
            connection.Close();
        }
        public List<string> ShowBorrowedBooks()
        {
            //borrowed books

            List<string> BorrowedBooks = new List<string>();

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();


            string command2 = "select * from Book";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][5].ToString() == "0")
                {
                    BorrowedBooks.Add(data.Rows[i][0].ToString());
                }
            }

            connection.Close();

            return BorrowedBooks;
        }
        public List<string> ShowAvaiableBooks()
        {
            //available books
            List<string> AvailableBooks = new List<string>();

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();


            string command2 = "select * from Book";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][5].ToString() == "1")
                {
                    AvailableBooks.Add(data.Rows[i][0].ToString());
                }
            }

            connection.Close();

            return AvailableBooks;
        }
        public List<string> ShowUser()
        {
            //all users
            List<string> AllUser = new List<string>();

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();


            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                AllUser.Add(data.Rows[0][i].ToString());
            }

            connection.Close();

            return AllUser;
        }
        public List<string> ShowLateReturnUser()
        {
            //late returning users

            List<string> LateReturnUser = new List<string>();
            GregorianCalendar time = new GregorianCalendar();
            DateTime dt = DateTime.Now;
            int year = time.GetYear(dt);
            int month = time.GetMonth(dt);
            int day = time.GetDayOfMonth(dt);

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = 11; j < 16; j++)
                {
                    string[] temp = data.Rows[i][j].ToString().Split('/');
                    if (int.Parse(temp[0]) < year)
                    {
                        LateReturnUser.Add(data.Rows[i][0].ToString());
                        break;
                    }
                    else if (int.Parse(temp[0]) == year && int.Parse(temp[1]) < month)
                    {
                        LateReturnUser.Add(data.Rows[i][0].ToString());
                        break;
                    }
                    else if (int.Parse(temp[0]) == year && int.Parse(temp[1]) == month && int.Parse(temp[2]) < day)
                    {
                        LateReturnUser.Add(data.Rows[i][0].ToString());
                        break;
                    }
                }
            }
            connection.Close();

            return LateReturnUser;
        }
        public List<string> ShowLatePayUser()
        {
            //late pay users
            List<string> LatePayUser = new List<string>();
            GregorianCalendar time = new GregorianCalendar();
            DateTime dt = DateTime.Now;
            int year = time.GetYear(dt);
            int month = time.GetMonth(dt);
            int day = time.GetDayOfMonth(dt);

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][17] == null)
                {
                    string[] temp = data.Rows[i][16].ToString().Split('/');
                    if ((int.Parse(temp[0]) + 1) < year)
                    {
                        LatePayUser.Add(data.Rows[i][0].ToString());
                        break;
                    }
                    else if ((int.Parse(temp[0]) + 1) == year && int.Parse(temp[1]) < month)
                    {
                        LatePayUser.Add(data.Rows[i][0].ToString());
                        break;
                    }
                    else if ((int.Parse(temp[0]) + 1) == year && int.Parse(temp[1]) == month && int.Parse(temp[2]) < day)
                    {
                        LatePayUser.Add(data.Rows[i][0].ToString());
                        break;
                    }
                }
                else
                {
                    string[] temp = data.Rows[i][17].ToString().Split('/');
                    if ((int.Parse(temp[0]) + 1) < year)
                    {
                        LatePayUser.Add(data.Rows[i][0].ToString());
                        break;
                    }
                    else if ((int.Parse(temp[0]) + 1) == year && int.Parse(temp[1]) < month)
                    {
                        LatePayUser.Add(data.Rows[i][0].ToString());
                        break;
                    }
                    else if ((int.Parse(temp[0]) + 1) == year && int.Parse(temp[1]) == month && int.Parse(temp[2]) < day)
                    {
                        LatePayUser.Add(data.Rows[i][0].ToString());
                        break;
                    }
                }
            }
            connection.Close();
            return LatePayUser;
        }
        public List<string> ShowUniqueUser(string name)
        {
            //info : one of the users
            List<string> InfoUniqueUser = new List<string>();

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == name)
                {
                    /*****************************not aure********************/
                    InfoUniqueUser.Add(data.Rows[i][0].ToString());
                    InfoUniqueUser.Add(data.Rows[i][1].ToString());
                    InfoUniqueUser.Add(data.Rows[i][2].ToString());
                    InfoUniqueUser.Add(data.Rows[i][3].ToString());
                    InfoUniqueUser.Add(data.Rows[i][5].ToString());
                    InfoUniqueUser.Add(data.Rows[i][6].ToString());
                    InfoUniqueUser.Add(data.Rows[i][7].ToString());
                    InfoUniqueUser.Add(data.Rows[i][8].ToString());
                    InfoUniqueUser.Add(data.Rows[i][9].ToString());
                    InfoUniqueUser.Add(data.Rows[i][11].ToString());
                    InfoUniqueUser.Add(data.Rows[i][12].ToString());
                    InfoUniqueUser.Add(data.Rows[i][13].ToString());
                    InfoUniqueUser.Add(data.Rows[i][14].ToString());
                    InfoUniqueUser.Add(data.Rows[i][15].ToString());
                    InfoUniqueUser.Add(data.Rows[i][16].ToString());
                    InfoUniqueUser.Add(data.Rows[i][17].ToString());
                    break;
                }
            }
            connection.Close();

            return InfoUniqueUser;
        }
        public string ShowCredit()
        {
            //money in bank of the employee

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");

            string money = "";

            connection.Open();

            string command2 = "select * from Employee";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == this.Name)
                {
                    money = data.Rows[i][0].ToString();
                }
            }

            connection.Close();

            return money;
        }
        public void EditInfo(string email, string phone, string path)
        {
            //Edit Info of employee
            Email = email;
            PhoneNumber = phone;
            PictureURL = path;

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");

            connection.Open();

            string command = "update Employee SET email = '" + email + "' phoneNumber = '" + phone + "' PictureLoc = '" + path + "'" +
                             " where name = '" + this.Name + "'";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
    class User : Person, Methods
    {
        public User(string name, string email, string phone, string pass, string pic)
                    : base(name, email, phone, pass, pic)
        {
            bool repeated = false;

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if ((string)data.Rows[i][0] == name)
                {
                    repeated = true;
                }
            }
            if (repeated == false)
            {
                // add in SQL
                string command = "insert into Userr values('" + name.Trim() + "' , '" + phone.Trim() + "' ,'" + pass.Trim() + "' , '" +
                                                             email.Trim() + "' , '" + pic + "', '" + "0" + "')";

                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
        public void BorrowBook(string name)
        {
            //the Book is borrowed

            GregorianCalendar time = new GregorianCalendar();
            DateTime dt = DateTime.Now;
            int year = time.GetYear(dt);
            int month = time.GetMonth(dt);
            int day = time.GetDayOfMonth(dt);
            string Time = year + "/" + month + "/" + day;

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == Name)
                {
                    for (int j = 6; j < 11; j++)
                    {
                        if (data.Rows[i][j] == null)
                        {
                            if (j == 6)
                            {
                                string command3 = "update Userr SET book1 = '" + name + "' Date1 = '" + Time + "' where name = '" + Name + "' ";

                                SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                                sqlCommand3.ExecuteNonQuery();
                            }
                            else if (j == 7)
                            {
                                string command3 = "update Userr SET book2 = '" + name + "' Date2 = '" + Time + "' where name = '" + Name + "' ";

                                SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                                sqlCommand3.ExecuteNonQuery();
                            }
                            else if (j == 8)
                            {
                                string command3 = "update Userr SET book3 = '" + name + "' Date3 = '" + Time + "' where name = '" + Name + "' ";

                                SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                                sqlCommand3.ExecuteNonQuery();
                            }
                            else if (j == 9)
                            {
                                string command3 = "update Userr SET book4 = '" + name + "' Date4 = '" + Time + "' where name = '" + Name + "' ";

                                SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                                sqlCommand3.ExecuteNonQuery();
                            }
                            else if (j == 10)
                            {
                                string command3 = "update Userr SET book5 = '" + name + "' Date5 = '" + Time + "' where name = '" + Name + "' ";

                                SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                                sqlCommand3.ExecuteNonQuery();
                            }
                            break;
                        }
                    }
                }
            }
            string command = "update Book SET available = '" + false + "'" +
                                "where name = '" + name + "'";

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }
        public void ReturnBook(string name)
        {
            //the book is Returned

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == Name)
                {
                    for (int j = 6; j < 11; j++)
                    {
                        if (data.Rows[i][j].ToString() == name)
                        {
                            if (j == 6)
                            {
                                string command3 = "update Userr SET book1 = '" + null + "' Date1 = '" + null + "' where name = '" + Name + "' ";

                                SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                                sqlCommand3.ExecuteNonQuery();

                            }
                            else if (j == 7)
                            {
                                string command3 = "update Userr SET book2 = '" + null + "' Date2 = '" + null + "' where name = '" + Name + "' ";

                                SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                                sqlCommand3.ExecuteNonQuery();
                            }
                            else if (j == 8)
                            {
                                string command3 = "update Userr SET book3 = '" + null + "' Date3 = '" + null + "' where name = '" + Name + "' ";

                                SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                                sqlCommand3.ExecuteNonQuery();
                            }
                            else if (j == 9)
                            {
                                string command3 = "update Userr SET book4 = '" + null + "' Date4 = '" + null + "' where name = '" + Name + "' ";

                                SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                                sqlCommand3.ExecuteNonQuery();
                            }
                            else if (j == 10)
                            {
                                string command3 = "update Userr SET book5 = '" + null + "' Date5 = '" + null + "' where name = '" + Name + "' ";

                                SqlCommand sqlCommand3 = new SqlCommand(command3, connection);
                                sqlCommand3.ExecuteNonQuery();
                            }
                            break;
                        }
                    }
                }
            }
            string command = "update Book SET available = '" + true + "'" +
                                "where name = '" + name + "'";

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            connection.Close();

        }
        public void PayFine(string money)
        {
            //fine for delay
            /**********how much fine should be payed for each day****************/
            long m = long.Parse(money);

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            string command = "update Userr SET wallet = '" + (long.Parse(data.Columns[5].ToString()) - m) + "'" +
                                "where name = '" + this.Name + "'";

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }
        public void PaySubscription(string money)
        {
            //pay for subscription
            /**********how much money should be payed * ***************/
            long m = long.Parse(money);
            GregorianCalendar time = new GregorianCalendar();
            DateTime dt = DateTime.Now;
            int year = time.GetYear(dt);
            int month = time.GetMonth(dt);
            int day = time.GetDayOfMonth(dt);
            string Time = year + "/" + month + "/" + day;

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            string command = "update Userr SET wallet = '" + (long.Parse(data.Columns[5].ToString()) - m) + "' SubsRenew = '" + Time + "' " +
                " where name = '" + Name + "' ";

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }
        public string ShowCredit()
        {
            //show money
            /*********which user?*******/

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");

            string money = "";

            connection.Open();

            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == this.Name)
                {
                    money = data.Rows[i][5].ToString();
                }
            }

            connection.Close();

            return money;
        }
        public void IncreseCredit(string money)
        {
            // increase money

            long m = long.Parse(money);

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            string command3 = "update Userr SET wallet = '" + (long.Parse(data.Columns[5].ToString()) + m) + "'" +
                        "where name = '" + Name + "'";

            SqlCommand sqlCommand2 = new SqlCommand(command3, connection);
            sqlCommand2.ExecuteNonQuery();

            connection.Close();
        }
        public void EditInfo()
        {
            //edit info of use
        }
        public List<string> ShowBorrowedBooks()
        {
            List<string> BorrowedBooks = new List<string>();

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Userr";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == Name)
                {
                    for (int j = 6; j < 11; j++)
                    {
                        if (data.Rows[i][j] != null)
                        {
                            BorrowedBooks.Add(data.Rows[i][j].ToString());
                        }
                    }
                    break;
                }
            }
            connection.Close();

            return BorrowedBooks;
        }
        public List<string> SearchBookByName(string info)
        {
            List<string> bookInfo = new List<string>();

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Book";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == info)
                {
                    bookInfo.Add(data.Rows[i][0].ToString());
                    bookInfo.Add(data.Rows[i][1].ToString());
                    bookInfo.Add(data.Rows[i][2].ToString());
                    bookInfo.Add(data.Rows[i][3].ToString());
                    bookInfo.Add(data.Rows[i][4].ToString());
                }
            }
            connection.Close();

            return bookInfo;
        }
        public List<string> SearchBookByWriter(string info)
        {
            List<string> bookInfo = new List<string>();

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            string command2 = "select * from Book";
            SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][1].ToString() == info)
                {
                    bookInfo.Add(data.Rows[i][0].ToString());
                    bookInfo.Add(data.Rows[i][1].ToString());
                    bookInfo.Add(data.Rows[i][2].ToString());
                    bookInfo.Add(data.Rows[i][3].ToString());
                    bookInfo.Add(data.Rows[i][4].ToString());
                }
            }
            connection.Close();

            return bookInfo;
        }
    }
    class Pay
    {
        public static bool CheckCVV(string CVV)
        {
            //check CVV with regex

            string regex = @"^[0-9]{3,4}$";
            Regex re = new Regex(regex, RegexOptions.IgnoreCase);

            if (re.IsMatch(CVV))
                return true;

            else
                return false;
        }
        public static bool CheckCreditNumber(string CreditNumber)
        {
            //check credit number with Luhn algorithm

            int sum = 0, a;
            for (int i = 0; i < CreditNumber.Length; i++)
            {
                a = Convert.ToInt32(CreditNumber.Substring(i, 1));
                if (i % 2 == 0)
                {
                    a *= 2;

                    if (a > 9)
                        a -= 9;
                }
                sum += a;
            }

            if (sum % 10 == 0)
                return true;

            else
                return false;
        }
        public static bool CheckExpireDate(int Year,int Month)
        {
            //check expire date

            GregorianCalendar time = new GregorianCalendar();
            DateTime dt = DateTime.Now;
            int year = time.GetYear(dt);
            int month = time.GetMonth(dt);

            if (Year < year)
                return false;

            else if (Year > year)
                return true;

            else if (Month - 3 < month)
                return false;

            else
                return true;
        }
    }
    class Book
    {
        string Name;
        string Writer;
        string Genre;
        int RealeseNumber;

        public Book(string name, string writer, string genre, int realeseNumber)
        {
            Name = name;
            Writer = writer;
            Genre = genre;
            RealeseNumber = realeseNumber;
            int count = 1;

            //add the book to SQL
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
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
                                                    Genre.Trim() + "' , '" + RealeseNumber + "' , '" + count + "','" + true + "'  )";
           
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
    public partial class App : Application
    {
    }
}
