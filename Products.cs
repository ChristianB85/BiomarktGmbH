using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiomarktGmbH
{
    public partial class Products : Form
    {
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source =.\SQLEXPRESS; AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BioMarktDB.mdf;Integrated Security = True; Connect Timeout = 30");
        public Products()
        {
            InitializeComponent();

            //Establish database connection
            databaseConnection.Open();

            string query = "select * from Products";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            ProductsDGV.DataSource = dataSet.Tables[0];
            ProductsDGV.Columns[0].Visible = false;

            databaseConnection.Close();
        }

        private void btnProductSave_Click(object sender, EventArgs e)
        {

        }

        private void btnProductEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnProductClear_Click(object sender, EventArgs e)
        {

        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
