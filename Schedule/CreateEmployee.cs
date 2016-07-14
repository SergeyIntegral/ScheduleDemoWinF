using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Schedule.DAL.Models;

namespace Schedule
{
    public partial class CreateEmployee : Form
    {
        private Form1 main;
        public CreateEmployee()
        {
            InitializeComponent();
        }

        private void CreateEmployee_Load(object sender, EventArgs e)
        {
            main = this.Owner as Form1;
            if (main != null)
            {
                var position = main._repositoryProvider.GetRepository<Position>().GetAll().ToList();
                comboBox1.DataSource = position;
                comboBox1.ValueMember = "Id";
                comboBox1.DisplayMember = "Title";

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();

            employee.Name = textBox1.Text;
            employee.LastName = textBox2.Text;
            employee.MiddleName = textBox3.Text;
            employee.Position = (Position)comboBox1.SelectedItem;
            main._repositoryProvider.GetRepository<Employee>().Add(employee);
            main._repositoryProvider.SaveChanges();
            this.Close();
        }
    }
}
