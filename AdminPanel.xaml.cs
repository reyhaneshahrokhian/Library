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
using System.Media;

namespace WpfProject
{
    public partial class AdminPanel : Window
    {
        string IndexOfTab;
        Admin admin;
        public List<string> AllBooks { get; set; }
        public List<string> AllEmployee { get; set; }
        public AdminPanel()
        {
            admin = new Admin("admin", "", "", "AdminLib123", "");
            AllBooks = admin.ShowBooks();
            AllEmployee = admin.ShowEmployee();

            InitializeComponent();

            DataContext = this;
        }
        private void Book_Click(object sender, RoutedEventArgs e)
        {
            AllBooks = admin.ShowBooks();
            tab.SelectedIndex = 1;
            IndexOfTab = "Book";
        }
        private void bank_Click(object sender, RoutedEventArgs e)
        {
            Show_Credit.Text = admin.ShowCredit();
            tab.SelectedIndex = 2;
            IndexOfTab = "Bank";
        }
        private void employee_Click(object sender, RoutedEventArgs e)
        {
            AllEmployee = admin.ShowEmployee();
            tab.SelectedIndex = 3;
            IndexOfTab = "Emp";
        }
        private void AddNewBook_Click(object sender, RoutedEventArgs e)
        {
            //go to submit page
            tab.SelectedIndex = 4;
        }
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            //go to pay tab
            tab.SelectedIndex = 5;
        }
        private void EmpAddDone_Click(object sender, RoutedEventArgs e)
        {
            //new employee add

            if (!Person.CheckName(rnameBox.Text))
            {
                rnameBlock.Text = "The name is invalid";
                rnameBlock.Foreground = Brushes.Red;
            }
            else
            {
                rnameBlock.Text = "Contain 8-32 English characters";
                rnameBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF397F65"));
            }

            if (!Person.CheckEmail(remailBox.Text))
            {
                remailBlock.Text = "The email is invalid";
                remailBlock.Foreground = Brushes.Red;
            }
            else
            {
                remailBlock.Text = "Characters or numbers or - or _";
                remailBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF397F65"));
            }

            if (!Person.CheckPhoneNumber(rphoneBox.Text))
            {
                rphoneBlock.Text = "The phone number is invalid";
                rphoneBlock.Foreground = Brushes.Red;
            }
            else
            {
                rphoneBlock.Text = "Contain 11number.start with 09";
                rphoneBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF397F65"));
            }

            if (!Person.CheckPassword(rpassBox.Password))
            {
                rpassBlock.Text = "The Password is invalid";
                rpassBlock.Foreground = Brushes.Red;
            }
            else
            {
                rpassBlock.Text = "At least 1 upper character(8-32)";
                rpassBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF397F65"));
            }

            if (Person.CheckName(rnameBox.Text) && Person.CheckEmail(remailBox.Text) && Person.CheckPhoneNumber(rphoneBox.Text) && Person.CheckPassword(rpassBox.Password))
            {
                admin.AddEmployee(rnameBox.Text, remailBox.Text, rphoneBox.Text, rpassBox.Password, personImage.Source.ToString());
                AllEmployee.Add(rnameBox.Text);
                tab.SelectedIndex = 3;
            }
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //book info has to be added to sql
            try
            {
                releasenumberBlock.Text = " ";
                admin.AddBooks(nameBox.Text, writerBox.Text, genreBox.Text, int.Parse(releasenumberBox.Text));
                AllBooks.Add(nameBox.Text);
                tab.SelectedIndex = 1;
            }
            catch
            {
                releasenumberBlock.Text = "Just enter number";
            }
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            //go to register tab
            tab.SelectedIndex = 6;
        }

        private void PayEnd_Click(object sender, RoutedEventArgs e)
        {
            //payment for charging and paying employees money

            string CreditNum = Account1.Text + Account2.Text + Account3.Text + Account4.Text;
            if (!Pay.CheckCreditNumber(CreditNum))
                AccountNumberBlock.Text = "The account number is invalid";

            else
                AccountNumberBlock.Text = " ";

            if (!Pay.CheckCVV(CvvBox.Text))
                cvvBlock.Text = "The CVV is invalid";

            else
                cvvBlock.Text = " ";

            if (!Pay.CheckExpireDate(int.Parse(YearBox.Text), int.Parse(MonthBox.Text)))
                DateBlock.Text = "The expire date has pass over";

            else
                DateBlock.Text = " ";


            if (Pay.CheckCreditNumber(CreditNum) && Pay.CheckCVV(CvvBox.Text) && Pay.CheckExpireDate(int.Parse(YearBox.Text), int.Parse(MonthBox.Text)))
            {
                if (IndexOfTab == "Bank")
                {
                    PaidAmount.Text = ChargeBox.Text;
                    admin.IncreaseMoney(ChargeBox.Text);
                }
                else if (IndexOfTab == "Emp")
                {
                    admin.PayEmployee(PaidAmount.Text);
                }
                tab.SelectedIndex = 2;
            }
        }
        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            //picture choosen from their labtop and added to sql

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
        private void DELETEemployee_Click(object sender, RoutedEventArgs e)
        {
            /********** delte eployee from list *****/
        }
        private void DELETEbook_Click(object sender, RoutedEventArgs e)
        {
            /********** delte book from list *****/
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 3;
        }
    }
}
