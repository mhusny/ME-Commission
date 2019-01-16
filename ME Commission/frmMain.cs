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
using System.Data.SqlClient;

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
                OleDbCommand oconn = new OleDbCommand("Select mecode, me, conno, schno, st_schmon, tinv_no, invno, duedate, SUM(payab_amt)AS payab_amt, '"+ DateTime.Today.ToString("dd/MM/yyyy") + "' From[" + name + "$] " +
                    " WHERE payab_amt > 0 GROUP BY mecode, me, conno, schno, st_schmon, tinv_no,  invno, duedate, '" + DateTime.Today.ToString("dd/MM/yyyy") + "' ", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    DataTable data = new DataTable();
                sda.Fill(data);
                con.Close();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy("Data Source = DELLWORK; Initial Catalog = RC; User ID = sa; Password = Vx@7190"))
                {
                    bulkCopy.DestinationTableName =
                        "dbo.RECOVERY_PAYMENT";

                    try
                    {
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                //foreach (DataRow dr in data.Rows)
                //{
                //    if (Convert.ToDouble(dr[3])>0)
                //    {
                //        var ddd = 0;

                //    }
                //}


                //grid_items.DataSource = data;
            }

        }

        private void tblImportMri_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select Excell File...";
            openFileDialog1.Filter = "Excell files (*.xls)|*.xls|All files (*.*)|*.*";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var sr = openFileDialog1.FileName;

                String name = openFileDialog1.SafeFileName.Substring(0, openFileDialog1.SafeFileName.IndexOf('.'));
                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            sr +
                            ";Extended Properties='Excel 8.0;HDR=YES;';";

                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand oconn = new OleDbCommand("Select conno, invno, invdate, SUM(inv_amt) AS inv_amt, SUM(not_invamt) AS not_invamt, '" + DateTime.Today.ToString("dd/MM/yyyy") + "' AS IMPORT_DATE From[" + name + "$] " +
                    "  GROUP BY conno, invno, invdate, '" + DateTime.Today.ToString("dd/MM/yyyy") + "' ", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);
                con.Close();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy("Data Source = DELLWORK; Initial Catalog = RC; User ID = sa; Password = Vx@7190"))
                {
                    //bulkCopy.DestinationTableName =
                    //    "dbo.RECOVERY_PAYMENT";

                    //try
                    //{
                    //    // Write from the source to the destination.
                    //    bulkCopy.WriteToServer(data);
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.Message);
                    //}
                }

                //foreach (DataRow dr in data.Rows)
                //{
                //    if (Convert.ToDouble(dr[3])>0)
                //    {
                //        var ddd = 0;

                //    }
                //}


                //grid_items.DataSource = data;
            }
        }
    }
}
