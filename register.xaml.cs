using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Win32;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;


namespace WpfProject
{
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            //if the infos are ok,go to payment page  

            if (!Person.CheckName(NameBox.Text))
            {
                NameBlock.Text = "The name is invalid";
                NameBlock.Foreground = Brushes.Red;
            }
            else
            {
                NameBlock.Text = "Contain 8-32 English characters";
                NameBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF397F65"));
            }

            if (!Person.CheckEmail(EmailBox.Text))
            {
                EmailBlock.Text = "The email is invalid";
                EmailBlock.Foreground = Brushes.Red;
            }
            else
            {
                EmailBlock.Text = "Characters or numbers or - or _";
                EmailBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF397F65"));
            }

            if (!Person.CheckPhoneNumber(PhoneNumberBox.Text))
            {
                PhoneNumberBlock.Text = "The phone number is invalid";
                PhoneNumberBlock.Foreground = Brushes.Red;
            }
            else
            {
                PhoneNumberBlock.Text = "Contain 11number.start with 09";
                PhoneNumberBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF397F65"));
            }

            if (!Person.CheckPassword(PasswordBox.Password))
            {
                PasswordBlock.Text = "The Password is invalid";
                PasswordBlock.Foreground = Brushes.Red;
            }
            else
            {
                PasswordBlock.Text = "At least 1 upper character(8-32)";
                PasswordBlock.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF397F65"));
            }

            if (Person.CheckName(NameBox.Text) && Person.CheckEmail(EmailBox.Text) && Person.CheckPhoneNumber(PhoneNumberBox.Text) && Person.CheckPassword(PasswordBox.Password))
            {
                //cheking if the user is repeated or no then added

                bool repeated = false;
                SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
                connection.Open();

                string command = "select * from Userr";
                SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if ((string)data.Rows[i][0] == NameBox.Text)
                    {
                        repeated = true;
                    }
                }

                if (repeated == false)
                {
                    payment P = new payment(NameBox.Text, EmailBox.Text, PhoneNumberBox.Text, PasswordBox.Password, personImage.Source.ToString());
                    P.Show();
                    this.Close();
                }
                else
                {
                    RepeatBlock.Text = "There is already an employee with";
                    Repeat2Block.Text = "this name!!";
                }
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //go to login page
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
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
    }
}
