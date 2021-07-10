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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace WpfProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            //register butten click she bere too safheye Register 
            Register r = new Register();
            r.Show();
            this.Close();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            NameBlock.Text = "";
            passwordBlock.Text = "";

            //check admin
            if (NameBox.Text == "admin")
            {
                if (passwordBox.Password == "AdminLib123")
                {
                    AdminPanel admin = new AdminPanel();
                    admin.Show();
                    this.Close();
                }
                else
                {
                    passwordBlock.Text = "InCorrect password!";
                }
            }
            else
            {
                SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =
                                G:\c#\project\newSQL.mdf; Integrated Security = True; Connect Timeout = 30");
                connection.Open();

                string command2 = "select * from Userr";
                SqlDataAdapter adapter = new SqlDataAdapter(command2, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);

                bool checkName = false;

                //check user
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][0].ToString() == NameBox.Text)
                    {  
                        checkName = true;
                        if (data.Rows[i][2].ToString() == passwordBox.Password)
                        {
                            UserPanel user = new UserPanel(data.Rows[i][0].ToString(), data.Rows[i][3].ToString(), data.Rows[i][1].ToString(), data.Rows[i][2].ToString(), data.Rows[i][4].ToString());
                            user.Show();
                            this.Close();
                        }
                        else
                        {
                            passwordBlock.Text = "InCorrect password!";
                            break;
                        }
                    }
                }
                string command3 = "select * from Employee";
                SqlDataAdapter adapter1 = new SqlDataAdapter(command3, connection);
                DataTable data1 = new DataTable();
                adapter1.Fill(data1);

                //check employee
                for (int i = 0; i < data1.Rows.Count; i++)
                {
                    if (data1.Rows[i][0].ToString() == NameBox.Text)
                    {
                        checkName = true;
                        if (data1.Rows[i][2].ToString() == passwordBox.Password)
                        {
                            EmployeePanel employee = new EmployeePanel(data1.Rows[i][0].ToString(), data1.Rows[i][3].ToString(), data1.Rows[i][1].ToString(), data1.Rows[i][2].ToString(), data1.Rows[i][4].ToString());
                            employee.Show();
                            this.Close();
                        }
                        else
                        {
                            passwordBlock.Text = "InCorrect password!";
                            break;
                        }
                    }
                }
                if (!checkName)
                {
                    NameBlock.Text = "there is no member with this name";
                }
                connection.Close();
            }
        }
    }
}

