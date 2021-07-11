using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections.ObjectModel;

namespace WpfProject
{
    public partial class EmployeePanel : Window
    {
        Employee employee;
        public ObservableCollection<string> Books { get; set; }
        public ObservableCollection<string> Users { get; set; }
        public EmployeePanel (string name, string email, string phone, string pass, string pic)
        {
            employee = new Employee(name, email, phone, pass, pic);
            Books = new ObservableCollection<string>();
            Users = new ObservableCollection<string>();

            InitializeComponent();

            DataContext = this;
        }
        private void Book_Click(object sender, RoutedEventArgs e)
        {
            if (Books != null && Books.Count > 0)
            {
                Books.Clear();
            }

            foreach (var item in employee.ShowBorrowedBooks())
            {
                Books.Add(item);
            }

            tab.SelectedIndex = 1;
        }
        private void User_Click(object sender, RoutedEventArgs e)
        {
            if (Users != null && Users.Count > 0)
            {
                Users.Clear();
            }

            foreach (var item in employee.ShowLateReturnUser())
            {
                Users.Add(item);
            }
            tab.SelectedIndex = 2;
        }
        private void Wallet_Click(object sender, RoutedEventArgs e)
        {
            //show how much money the employee have
            Show_Money.Text = employee.ShowCredit();
            tab.SelectedIndex = 3;
        }
        private void EditInfo_Click(object sender, RoutedEventArgs e)
        {
            passwordContinueBox.Password = "";
            tab.SelectedIndex = 4;
        }
        private void AllBooks_Click(object sender, RoutedEventArgs e)
        {
            //show all books in list
            if (Books != null && Books.Count > 0)
            {
                Books.Clear();
            }

            foreach (var item in Employee.ShowBooks())
            {
                Books.Add(item);
            }
        }
        private void BorrowedBooks_Click(object sender, RoutedEventArgs e)
        {
            //show borrowed books in list
            if (Books != null && Books.Count > 0)
            {
                Books.Clear();
            }

            foreach (var item in employee.ShowBorrowedBooks())
            {
                Books.Add(item);
            }
        }
        private void AvailableBooks_Click(object sender, RoutedEventArgs e)
        {
            //show available books in list
            if (Books != null && Books.Count > 0)
            {
                Books.Clear();
            }

            foreach (var item in employee.ShowAvailableBooks())
            {
                Books.Add(item);
            }
        }
        private void AllMembers_Click(object sender, RoutedEventArgs e)
        {
            //show all users in list
            if (Users != null && Users.Count > 0)
            {
                Users.Clear();
            }

            foreach (var item in employee.ShowUsers())
            {
                Users.Add(item);
            }

        }
        private void LatePayment_Click(object sender, RoutedEventArgs e)
        {
            //show users which are late in payment in list
            if (Users != null && Users.Count > 0)
            {
                Users.Clear();
            }

            foreach (var item in employee.ShowLatePayUser())
            {
                Users.Add(item);
            }
        }
        private void LateReturning_Click(object sender, RoutedEventArgs e)
        {
            //show users which are late in returning books in list
            if (Users != null && Users.Count > 0)
            {
                Users.Clear();
            }

            foreach (var item in employee.ShowLateReturnUser())
            {
                Users.Add(item);
            }
        }
        private void SHOWuser_Click(object sender, RoutedEventArgs e)
        {
            //info of special user

            try
            {
                var button = sender as Button;
                List<string> infos = new List<string>();
                infos = employee.ShowUniqueUser(button.Tag.ToString());

                NameUserBlock.Text = infos[0];
                PhoneUserBlock.Text = infos[1];
                EmailUserBlock.Text = infos[3];
                ImageUser.Source = new BitmapImage(new Uri(infos[4]));
                SignInDateBlock.Text = infos[16];

                GregorianCalendar time = new GregorianCalendar();
                DateTime Today = DateTime.Now;
                int days = 0;

                if (infos[17] == "")
                {
                    RenewDateBlock.Text = "No renew yet!";

                    string[] date = infos[16].Split('/');
                    DateTime dateValue = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                    days = 365 - (Today.Date - dateValue.Date).Days;
                }
                else
                {
                    RenewDateBlock.Text = infos[17];

                    string[] date = infos[17].Split('/');
                    DateTime dateValue = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                    days = 365 - (Today.Date - dateValue.Date).Days;
                }

                if (days >= 0)
                {
                    SubscriptionDaysBlock.Text = days.ToString();
                    SubscriptionDaysBlock.Foreground = Brushes.Green;
                }
                else
                {
                    SubscriptionDaysBlock.Text = (-1 * days).ToString();
                    SubscriptionDaysBlock.Foreground = Brushes.Red;
                }

                if (infos[6] != "")
                {
                    string[] date = infos[11].Split('/');
                    DateTime dateValue = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                    int year = time.GetYear(dateValue);
                    int month = time.GetMonth(dateValue);
                    int day = time.GetDayOfMonth(dateValue);
                    string Time = year + "/" + month + "/" + day;

                    int delays = 7 - (Today.Date - dateValue.Date).Days;
                    BookName1Table.Text = infos[6];
                    BookReturnDate1Table.Text = Time;

                    if (delays >= 0)
                    {
                        BookDelay1Table.Text = delays.ToString();
                        BookDelay1Table.Foreground = Brushes.DarkGreen;
                    }
                    else
                    {
                        BookDelay1Table.Text = (-1 * days).ToString();
                        BookDelay1Table.Foreground = Brushes.Red;
                    }
                }
                if (infos[7] != "")
                {
                    string[] date = infos[12].Split('/');
                    DateTime dateValue = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                    int year = time.GetYear(dateValue);
                    int month = time.GetMonth(dateValue);
                    int day = time.GetDayOfMonth(dateValue);
                    string Time = year + "/" + month + "/" + day;

                    int delays = 7 - (Today.Date - dateValue.Date).Days;
                    BookName2Table.Text = infos[7];
                    BookReturnDate2Table.Text = Time;

                    if (delays >= 0)
                    {
                        BookDelay2Table.Text = delays.ToString();
                        BookDelay2Table.Foreground = Brushes.DarkGreen;
                    }
                    else
                    {
                        BookDelay2Table.Text = (-1 * days).ToString();
                        BookDelay2Table.Foreground = Brushes.Red;
                    }
                }
                if (infos[8] != "")
                {
                    string[] date = infos[13].Split('/');
                    DateTime dateValue = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                    int year = time.GetYear(dateValue);
                    int month = time.GetMonth(dateValue);
                    int day = time.GetDayOfMonth(dateValue);
                    string Time = year + "/" + month + "/" + day;

                    int delays = 7 - (Today.Date - dateValue.Date).Days;
                    BookName3Table.Text = infos[8];
                    BookReturnDate3Table.Text = Time;

                    if (delays >= 0)
                    {
                        BookDelay3Table.Text = delays.ToString();
                        BookDelay3Table.Foreground = Brushes.DarkGreen;
                    }
                    else
                    {
                        BookDelay3Table.Text = (-1 * days).ToString();
                        BookDelay3Table.Foreground = Brushes.Red;
                    }

                }
                if (infos[9] != "")
                {
                    string[] date = infos[11].Split('/');
                    DateTime dateValue = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                    int year = time.GetYear(dateValue);
                    int month = time.GetMonth(dateValue);
                    int day = time.GetDayOfMonth(dateValue);
                    string Time = year + "/" + month + "/" + day;

                    int delays = 7 - (Today.Date - dateValue.Date).Days;
                    BookName4Table.Text = infos[9];
                    BookReturnDate4Table.Text = Time;

                    if (delays >= 0)
                    {
                        BookDelay4Table.Text = delays.ToString();
                        BookDelay4Table.Foreground = Brushes.DarkGreen;
                    }
                    else
                    {
                        BookDelay4Table.Text = (-1 * days).ToString();
                        BookDelay4Table.Foreground = Brushes.Red;
                    }
                }
                if (infos[10] != "")
                {
                    string[] date = infos[11].Split('/');
                    DateTime dateValue = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                    int year = time.GetYear(dateValue);
                    int month = time.GetMonth(dateValue);
                    int day = time.GetDayOfMonth(dateValue);
                    string Time = year + "/" + month + "/" + day;

                    int delays = 7 - (Today.Date - dateValue.Date).Days;
                    BookName5Table.Text = infos[10];
                    BookReturnDate5Table.Text = Time;

                    if (delays >= 0)
                    {
                        BookDelay5Table.Text = delays.ToString();
                        BookDelay5Table.Foreground = Brushes.DarkGreen;
                    }
                    else
                    {
                        BookDelay5Table.Text = (-1 * days).ToString();
                        BookDelay5Table.Foreground = Brushes.Red;
                    }
                }
            }
            catch { }

            tab.SelectedIndex = 5;
        }
        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            //delete the user

            if (PassDeleteBox.Password == employee.Password)
            {
                PassDeleteBlock.Text = " ";
                employee.DeleteUser(NameUserBlock.Text);
                tab.SelectedIndex = 2;
            }
            else
            {
                PassDeleteBlock.Text = "Incorrect pass";
            }
        }
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            //check correction of password

            if (passwordContinueBox.Password == employee.Password)
            {
                passwordContinueBlock.Text = " ";
                NameEditInfo.Text = employee.Name;
                EmailEditInfoBox.Text = employee.Email;
                PhoneEditInfoBox.Text = employee.PhoneNumber;
                personImage.Source = new BitmapImage(new Uri(employee.PictureURL));
                tab.SelectedIndex = 6;
            }
            else
            {
                passwordContinueBlock.Text = "InCorrect password !";
            }
        }
        private void ChangePicture_Click(object sender, RoutedEventArgs e)
        {
            //picture choosen from their labtop and added to sql

            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    personImage.Source = new BitmapImage(new Uri(op.FileName));
                }
            }
            catch { }
        }
        private void DoneChange_Click(object sender, RoutedEventArgs e)
        {
            //checking correction of email and phone number and change info of employee

            try
            {
                if (!Person.CheckEmail(EmailEditInfoBox.Text))
                {
                    EmailEditBlock.Text = "The email is invalid";
                    EmailEditBlock.Foreground = Brushes.Red;
                }
                else
                {
                    EmailEditBlock.Text = "Characters or numbers or - or _";
                    EmailEditBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF397F65"));
                }

                if (!Person.CheckPhoneNumber(PhoneEditInfoBox.Text))
                {
                    PhoneEditBlock.Text = "The phone number is invalid";
                    PhoneEditBlock.Foreground = Brushes.Red;
                }
                else
                {
                    PhoneEditBlock.Text = "Contain 11number.start with 09";
                    PhoneEditBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF397F65"));
                }

                if (Person.CheckEmail(EmailEditInfoBox.Text) && Person.CheckPhoneNumber(PhoneEditInfoBox.Text))
                {
                    employee.EditInfo(EmailEditInfoBox.Text, PhoneEditInfoBox.Text, personImage.Source.ToString());
                    tab.SelectedIndex = 0;
                }
            }
            catch { }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
