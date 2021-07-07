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

namespace WpfProject
{
    public partial class EmployeePanel : Window
    {

        private string IndexOfTab;
        Employee employee;
        public List<string> Books { get; set; }
        public List<string> Users { get; set; }
        public EmployeePanel (string name, string email, string phone, string pass, string pic)
        {
            employee = new Employee(name, email, phone, pass, pic);
            Books = employee.ShowBorrowedBooks();
            Users = employee.ShowLateReturnUser();

            InitializeComponent();

            DataContext = this;
        }
        private void Book_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 1;
            IndexOfTab = "Book";
        }
        private void User_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 2;
            IndexOfTab = "User";
        }
        private void Wallet_Click(object sender, RoutedEventArgs e)
        {
            Show_Money.Text = employee.ShowCredit();
            tab.SelectedIndex = 3;
            IndexOfTab = "Wallet";
        }
        private void EditInfo_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 4;
            IndexOfTab = "EditInfo";
        }
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (passwordContinueBox.Password == employee.Password)
            {
                tab.SelectedIndex = 5;
            }
            else
            {
                // show wrong passworrd
            }
        }
        private void ChangePicture_Click(object sender, RoutedEventArgs e)
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
        private void AllBooks_Click(object sender, RoutedEventArgs e)
        {
            //show all books in list
            Books = employee.ShowBooks();
        }
        private void BorrowedBooks_Click(object sender, RoutedEventArgs e)
        {
            //show borrowed books in list
            Books = employee.ShowBorrowedBooks();
        }
        private void AvailableBooks_Click(object sender, RoutedEventArgs e)
        {
            //show available books in list
            Books = employee.ShowAvaiableBooks();
        }
        private void AllMembers_Click(object sender, RoutedEventArgs e)
        {
            //show all users in list
            Users = employee.ShowUser();
        }
        private void LatePayment_Click(object sender, RoutedEventArgs e)
        {
            //show users which are late in payment in list
            Users = employee.ShowLatePayUser();
        }
        private void LateReturning_Click(object sender, RoutedEventArgs e)
        {
            //show users which are late in returning books in list
            Users = employee.ShowLateReturnUser();
        }
        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            //delete kone usero
            /***** method ? :) *************/
            /*******which pass ?********/
            string name = "";
            employee.DeleteUser(name);
        }
    }
}
