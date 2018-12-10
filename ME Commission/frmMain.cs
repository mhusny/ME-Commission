using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Xml.Linq;

namespace ME_Commission
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.ShowDialog();
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select Excell File...";
            openFileDialog1.Filter = "Excell files (*.xls)|*.xls|All files (*.*)|*.*";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var sr = openFileDialog1.FileName;

                String name = "Items";
                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            sr +
                            ";Extended Properties='Excel 8.0;HDR=YES;';";

                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    DataTable data = new DataTable();
                sda.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    if (Convert.ToDouble(dr[3])>0)
                    {
                        var ddd = 0;

                    }
                }


                //grid_items.DataSource = data;
            }

        }
    }
}
