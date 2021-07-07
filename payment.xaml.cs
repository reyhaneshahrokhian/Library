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
using System.Data;
using System.Data.SqlClient;

namespace WpfProject
{
    public partial class payment : Window
    {
        private string name;
        private string email;
        private string phone;
        private string password;
        string picture;
        public payment(string name, string email, string phone, string password, string picture)
        {
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.password = password;
            this.picture = picture;
            PaidAmount.Text = "10000";
            InitializeComponent();
        }
        private void PayEnd_Click(object sender, RoutedEventArgs e)
        {
            //payment for registering

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
                User user = new User(name, email, phone, password, picture);
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
        }
    }
}
