using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileUtilityZero
{
    public partial class FrmViewer : Form
    {
        public FrmViewer()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmViewer_Load(object sender, EventArgs e)
        {
            string filePath = FrmMain.FUZDirectory + @"\files.csv";
            DataTable dataTable = ReadCsvIntoDataTable(filePath);
            dataGridView1.DataSource = dataTable;
        }

        public static DataTable ReadCsvIntoDataTable(string filePath)
        {
            DataTable dataTable = new DataTable();
            bool isFirstRowHeader = true;
            string[] headers = null;
            string[] rows = File.ReadAllLines(filePath);

            foreach (string row in rows)
            {
                string[] cells = row.Split(',');
                if (isFirstRowHeader)
                {
                    isFirstRowHeader = false;
                    headers = cells;
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }
                }
                else
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dataRow[i] = cells.Length > i ? cells[i] : null;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
    }
}
