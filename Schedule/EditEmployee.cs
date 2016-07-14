using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule
{
    public partial class EditEmployee : Form
    {
        private Form1 main;

        public EditEmployee()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditEmployee_Load(object sender, EventArgs e)
        {
            main = this.Owner as Form1;
            var position = main._repositoryProvider.GetRepository<Position>().GetAll().ToList();
            if (main != null)
            {
                textBox1.Text = main.editEmpl.Name;
                textBox2.Text = main.editEmpl.LastName;
                textBox3.Text = main.editEmpl.MiddleName;
                comboBox1.DataSource = position;
                comboBox1.ValueMember = "Id";
                comboBox1.DisplayMember = "Title";
                if (main.editEmpl.Position != null)
                    comboBox1.SelectedValue = main.editEmpl.Position.Id;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            main.editEmpl.Name = textBox1.Text;
            main.editEmpl.LastName = textBox2.Text;
            main.editEmpl.MiddleName = textBox3.Text;
            main.editEmpl.Position = (Position)comboBox1.SelectedItem;
            main._repositoryProvider.SaveChanges();
            this.Close();
        }
    }
}
