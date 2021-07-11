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

namespace WpfProject
{
    public partial class AreYouSure : Window
    {
        string name;
        string who;
        public AreYouSure(string name,string who)
        {
            this.name = name;
            this.who = who;
            InitializeComponent();
        }

        private void Sure_Click(object sender, RoutedEventArgs e)
        {
            if (who == "employee")
            {
                Admin.DeleteEmployee(name);


                if (AdminPanel.AllEmployee != null && AdminPanel.AllEmployee.Count > 0)
                {
                    AdminPanel.AllEmployee.Clear();
                }

                foreach (var item in Admin.ShowEmployee())
                {
                    AdminPanel.AllEmployee.Add(item);
                }

            }
            else if (who == "book")
            {
                Admin.DeleteBook(name);

                if (AdminPanel.AllBooks != null && AdminPanel.AllBooks.Count > 0)
                {
                    AdminPanel.AllBooks.Clear();
                }

                foreach (var item in Admin.ShowBooks())
                {
                    AdminPanel.AllBooks.Add(item);
                }
            }
            this.Close();
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

