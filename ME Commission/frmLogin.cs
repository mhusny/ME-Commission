using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ME_Commission
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool logged = false;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=DELLWORK;Initial Catalog=RC;User ID=sa;Password=Vx@7190";
                conn.Open();
                // Create the command
                SqlCommand command = new SqlCommand("select USER_ID, USER_PASSWORD from USER_MASTER ", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader[0].ToString().ToUpper() == txtUserName.Text.ToUpper() && reader[1].ToString().ToUpper() == txtPassword.Text.ToUpper())
                        {
                            this.Hide();
                            logged = true;
                        }
                    }

                    if (logged == false)
                    {
                        MessageBox.Show("Username or Password Incorrect", "Error", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
