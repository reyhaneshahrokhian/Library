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

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for UserPanel.xaml
    /// </summary>
    /// 

    public partial class UserPanel : Window
    {
        private string IndexOfTab;
        User user;
        public UserPanel(string name, string email, string phone, string pass, string pic)
        {
            user = new User(name, email, phone, pass, pic);
            InitializeComponent();
        }
        private void Sub_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 3;
            IndexOfTab = "Sub";
        }
        private void Wallet_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 4;
            IndexOfTab = "Wallet";
        }
        private void EditInfo_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 5;
            IndexOfTab = "EditInfo";
        }
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            //go to pay tab
            tab.SelectedIndex = 6;
        }
        private void MyBook_Click(object sender, RoutedEventArgs e)
        {
            //show all the borrowed books
            tab.SelectedIndex = 1;
            IndexOfTab = "MyBook";

            /*********** method nadare ke :||||| ***************/
            /******** show in table ***********/
            user.ShowBorrowedBooks();
        }
        private void AllBook_Click(object sender, RoutedEventArgs e)
        {
            //show all the books available in the library
            tab.SelectedIndex = 2;
            IndexOfTab = "AllBook";

            /******** show in table ***********/
            user.ShowBooks();
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            //dar sql dorost she
            /********** choose in combo box then save the name and send it to method ***********/
            string info = "";
            user.ReturnBook(info);
        }
        private void SearchByname_Click(object sender, RoutedEventArgs e)
        {
            //show info of book 
            /***dar jadval dorost she***/
            /******** method nadare *********/
            user.SearchBookByName(serachbynameBox.Text);
        }
        private void SearchByWriter_Click(object sender, RoutedEventArgs e)
        {
            //show info of book 
            user.SearchBookByWriter(serachbywriterBox.Text);
        }
        private void ExtendSub_Click(object sender, RoutedEventArgs e)
        {
            //check she agar pool kafi be sub ezafe she age nabood khataneshoon bede ke pool kafi nist
            /****** ba che algorythmi??? ***********/
            /***** chera field baraye pool nadarim? :) ********/
            int defiedMoneyForExtendSub = 100;

            if (user.Money >= defiedMoneyForExtendSub)
            {
                //bere too safhe pay ?

            }
            else
            {
                //show not enough money
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
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            // unlock the other half of tab
            /****** if the info is correct *********/
            if (passwordBox.Password == user.Password)
            {
                tab.SelectedIndex = 7;
            }
            else
            {
                //show wrong password
            }
        }
        private void PayEnd_Click(object sender, RoutedEventArgs e)
        {
            //dar sql pool kam va ezafe she
            /****** pay  money *****/

            if (IndexOfTab == "Wallet")
            {
                tab.SelectedIndex = 4;
            }
        }
    }
}
