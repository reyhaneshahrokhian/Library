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
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace WpfProject
{

    public partial class UserPanel : Window
    {
        User user;
        public ObservableCollection<string> Books { get; set; }

        public UserPanel(string name, string email, string phone, string pass, string pic)
        {
            //make new user object with (the user that log in)
            user = new User(name, email, phone, pass, pic);

            Books = new ObservableCollection<string>();

            InitializeComponent();

            DataContext = this;
        }
        private void MyBook_Click(object sender, RoutedEventArgs e)
        {
            //show all the borrowed books

            try
            {
                returnM.Text = "";
                if (Books != null && Books.Count > 0)
                {
                    Books.Clear();
                }
                foreach (var item in user.ShowBorrowedBooks())
                {
                    Books.Add(item);
                }
                if (Books != null && Books.Count > 0)
                {
                    ComboBox.ItemsSource = Books;
                }
            }
            catch { }

            tab.SelectedIndex = 1;
        }
        private void AllBook_Click(object sender, RoutedEventArgs e)
        {
            //show all the books in the library
            try
            {
                searchbookBox.Text = "";
                if (Books != null && Books.Count > 0)
                {
                    Books.Clear();
                }

                foreach (var item in user.ShowAvailableBooks())
                {
                    Books.Add(item);
                }
            }
            catch { }
            tab.SelectedIndex = 2;
        }
        private void Sub_Click(object sender, RoutedEventArgs e)
        {
            // go to subcription page

            try
            {
                money.Text = user.ShowCredit();

                if (user.LeftSubscriptionDays() < 0)
                {
                    Days.Text = (-1 * user.LeftSubscriptionDays()).ToString();
                    Days.Foreground = Brushes.Green;
                }
                else
                {
                    Days.Text = user.LeftSubscriptionDays().ToString();
                    Days.Foreground = Brushes.Red;
                }
            }
            catch { }
            tab.SelectedIndex = 3;
        }
        private void Wallet_Click(object sender, RoutedEventArgs e)
        {
            //go to wallet page
            PaidAmount.Text = "";
            Money.Text = user.ShowCredit();
            tab.SelectedIndex = 4;
        }
        private void EditInfo_Click(object sender, RoutedEventArgs e)
        {
            //go to edit information page
            passwordContinueBox.Password = "";
            tab.SelectedIndex = 5;
            NameEditInfo.Text = user.Name;
        }
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            //go to pay tab

            PaidAmount.Text = amountMoney.Text;
            Account1.Text = "";
            Account2.Text = "";
            Account3.Text = "";
            Account4.Text = "";
            CvvBox.Text = "";
            YearBox.Text = "";
            MonthBox.Text = "";
            tab.SelectedIndex = 6;
        }
        private void ChangePicture_Click(object sender, RoutedEventArgs e)
        {
            //change the picture in info tab
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
        /****************************************************************************************************************************/
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            // choose in combo box then save the name and send it to method 
            string selected = ComboBox.SelectedItem.ToString();
            string ans = user.ReturnBook(selected);
            returnM.Text = ans;

            if (Books != null && Books.Count > 0)
            {
                Books.Clear();
            }
            foreach (var item in user.ShowBorrowedBooks())
            {
                Books.Add(item);
            }
            if (Books != null && Books.Count > 0)
            {
                ComboBox.ItemsSource = Books;
            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //show the book info by using the writer or name of the book

            if (SearchByNameCheck.IsChecked == true)
            {
                if (Books != null && Books.Count > 0)
                {
                    Books.Clear();
                }
                Books.Add(user.SearchBookByName(searchbookBox.Text));             
            }
            else if (SearchByWriterCheck.IsChecked == true)
            {
                if (Books != null && Books.Count > 0)
                {
                    Books.Clear();
                }
                Books.Add(user.SearchBookByWriter(searchbookBox.Text));    
            }

            searchbookBox.Text = "";
        }
        private void SearchByname_Check(object sender, RoutedEventArgs e)
        {
            SearchByNameCheck.IsChecked = true;
        }
        private void SearchBywriter_Check(object sender, RoutedEventArgs e)
        {
            SearchByWriterCheck.IsChecked = true;
        }
        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            user.BorrowBook(button.Tag.ToString());

            if (Books != null && Books.Count > 0)
            {
                Books.Clear();
            }

            foreach (var item in user.ShowAvailableBooks())
            {
                Books.Add(item);
            }
        }
        private void ExtendSub_Click(object sender, RoutedEventArgs e)
        {
            int defiedMoneyForExtendSub = 100;

            try
            {
                if (long.Parse(user.ShowCredit()) >= defiedMoneyForExtendSub)
                {
                    user.PaySubscription("100");
                }
                else
                {
                    //show not enough money(write money needed)
                    ExtendsubM.Text = "deficit : " + (long.Parse(user.ShowCredit()) - defiedMoneyForExtendSub).ToString();
                }
            }
            catch { }
        }
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            // unlock the other half of tab
            //if the info is correct
            try
            {
                if (passwordContinueBox.Password == user.Password)
                {
                    passwordContinueBlock.Text = " ";
                    NameEditInfo.Text = user.Name;
                    EmailEditInfoBox.Text = user.Email;
                    PhoneEditInfoBox.Text = user.PhoneNumber;
                    personImage.Source = new BitmapImage(new Uri(user.PictureURL));
                    tab.SelectedIndex = 7;
                }
                else
                {
                    //show wrong password
                    passwordContinueBlock.Text = "InCorrect password !";
                }
            }
            catch { }
        }
        private void PayEnd_Click(object sender, RoutedEventArgs e)
        {
            //increse money in the wallet 

            string CreditNum = Account1.Text + Account2.Text + Account3.Text + Account4.Text;
            try
            {
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
                    user.IncreseCredit(amountMoney.Text);
                }
            }
            catch  { }

            PaidAmount.Text = "";
            Money.Text = user.ShowCredit();
            tab.SelectedIndex = 4;
        }
        private void DoneChange_Click(object sender, RoutedEventArgs e)
        {
            //checking correction of email and phone number and change info of user
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
                    user.EditInfo(EmailEditInfoBox.Text, PhoneEditInfoBox.Text, personImage.Source.ToString());
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

