using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManagement
{
   
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            string a = Convert.ToString(NameBox.Text);
            string b = Convert.ToString(Password.Text);
            account acc = new account();
            
            if( acc.CheckinIn4(a,b) == true)
            {
                Menu firstpage = new Menu();

                firstpage.Sender(a, b);

                firstpage.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Your passowrd or account is incorrect!!!");
            }

        }
    }




    class account
    {
        private string _id { get; set; }
        private string _name { get; set; }
        private string _password { get; set; }

        public account() { }

        public account(string  id,string name, string password)
        {
            _id = id;
            _name = name;
            _password = password;
        }

        string connection = "Data Source=HIEUDINHLAPTOP\\SQLEXPRESS;" +
                            " Initial Catalog=BookManagement;" +
                            " Integrated Security=True";
        SqlConnection conn = null;


        public bool login(string username, string password)
        {
            conn = new SqlConnection(connection);
            conn.Open();

            string query = "Select * from Account Where Name = '" + username + "' AND  PASSWORD = '" + password + "' ";



            SqlDataAdapter VirtualDatabase = new SqlDataAdapter(query, conn);

            DataTable result = new DataTable();

            
            VirtualDatabase.Fill(result);

            conn.Close();

            return result.Rows.Count > 0;
        }

        public bool CheckinIn4(string name, string password)
        {
            if (login(name, password) == true)
            {
                return true;
            }
            return false;

        }
     }




    class MyNotification : ApplicationException
    {
        public MyNotification(string message) : base(message) { }
    }
}
