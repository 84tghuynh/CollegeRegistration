using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BITCollege_GiangHuynh.Models;


namespace WindowsApplication
{
    public partial class frmBatch : Form
    {
        // Database connection
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        /// <summary>
        /// FormBatch instructor
        /// </summary>
        public frmBatch()
        {
            InitializeComponent();
            IQueryable<BITCollege_GiangHuynh.Models.Program> query = db.Programs;

            programBindingSource.DataSource = query.ToList();

        }

        /// <summary>
        /// Click "Process Batch" button on FORMBATCH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtKey.Text == "" || txtKey.Text.Length != 8)
            {
                MessageBox.Show("A 64-bit Key must be entered", "Error");
            }
            else
            {

                if (this.radAll.Checked)
                {
                    GradeAndRegisterAllPrograms();
                }

                if (this.radSelect.Checked)
                {
                    GradeAndRegisterChosenProgram();
                }
            }
        }
        /// <summary>
        /// Process all files in Bin/Debug folder
        /// </summary>
        private void GradeAndRegisterAllPrograms()
        {

            IQueryable<BITCollege_GiangHuynh.Models.Program> query = db.Programs;

            Batch batch = new Batch();

            foreach (BITCollege_GiangHuynh.Models.Program program in query)
            {
                batch.processTranmission(program.ProgramAcronym, txtKey.Text);
                richTextBox1.Text += batch.writeLogData();
            }


        }
        /// <summary>
        /// Process one file in Bin/Debug folder. 
        /// This file has Program Acronym as choosen in Combobox.
        /// </summary>
        private void GradeAndRegisterChosenProgram()
        {
            Batch batch = new Batch();
            batch.processTranmission(descriptionComboBox.SelectedValue.ToString(), txtKey.Text);
            richTextBox1.Text = batch.writeLogData();

        }

        /// <summary>
        /// Loading FormBatch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBatch_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
