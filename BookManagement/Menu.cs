using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BookManagement
{ 
    public partial class Menu : Form
    {
        //event
        public delegate void CreatePage(string username, string password);
        public CreatePage Sender;
        //connection
        string connection = "Data Source=HIEUDINHLAPTOP\\SQLEXPRESS;" +
                            " Initial Catalog=BookManagement;" +
                            " Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;


        public Menu()
        {
            InitializeComponent();
            Sender = new CreatePage(GetName);
        }
        public void GetName(string name,string passowrd)
        { 
            conn = new SqlConnection(connection);
            conn.Open();

            string cc = ("select NAME,TYPEACCOUNT,IDsame,CLASS,AGE ")+
                        ("from Account ")+
                        ("inner join AccountInformation on AccountInformation.IDsame = Account.ID ")+
                        ("Where NAME = '")+name+("'");
            SqlCommand queryAcc = new SqlCommand(cc, conn);
            SqlDataReader accquestion = queryAcc.ExecuteReader();

            while( accquestion.Read())
            {
                loginbox.Text = accquestion.GetString(0);
                typebox.Text = accquestion.GetString(1);
                idbox.Text = accquestion.GetString(2);
                cssbox.Text = accquestion.GetString(3);
                agebox.Text = accquestion.GetString(4);
            }
            accquestion.Close();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            ShowMenuPage();
        }
        private void ShowMenuPage()
        {
            if ( conn == null )
            {
               SqlConnection conn = new SqlConnection(connection);
            }
            

                string CallBookList = "select * from BOOKLIST";
                adapter = new SqlDataAdapter(CallBookList,conn);
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);

                ds = new DataSet();
                adapter.Fill(ds,"BookList");
                BookListTable.DataSource = ds.Tables["BookList"];
        }
        //position
        int index = -1;


        private void BookListTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            index = e.RowIndex;
                
            if (index == -1 )
            {
                MessageBox.Show("Your are clicking wrongfield");
                return;
            }
            else
            {
                DataRow row = ds.Tables["BookList"].Rows[index];
                tx1.Text = row["IDbook"].ToString();
                tx2.Text = row["IDbookOwner"].ToString();
                tx3.Text = row["BookName"].ToString();
                tx4.Text = row["BookBorrowTime"].ToString();
                tx5.Text = row["BookReturnTime"].ToString();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            DataRow row = ds.Tables["BookList"].Rows[index];
            row.BeginEdit();

            row["IDbook"]           = tx1.Text.Trim();
            row["IDbookOwner"]      = tx2.Text.Trim();
            row["BookName"]         = tx3.Text.Trim();
            row["BookBorrowTime"]   = tx4.Text.Trim();
            row["BookReturnTime"]   = tx5.Text.Trim();

            row.EndEdit();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["BookList"].NewRow();

            row["IDbook"] = tx1.Text.Trim();
            row["IDbookOwner"] = tx2.Text.Trim();
            row["BookName"] = tx3.Text.Trim();
            row["BookBorrowTime"] = tx4.Text.Trim();
            row["BookReturnTime"] = tx5.Text.Trim();

            ds.Tables["BookList"].Rows.Add(row);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult ketqua = MessageBox.Show("Are you sure about deleting data?","WARNNING",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if( ketqua == DialogResult.Yes )
            {
                DataDelete();
            }
        }

        private void DataDelete()
        {
            if (index == -1)
            {
                MessageBox.Show("You are not selecting any items yet?");
                return;
            }
            else
            {
                DataRow row = ds.Tables["BookList"].Rows[index];
                row.BeginEdit();
                row.Delete();
                row.EndEdit();

            }
        }
    }
}
