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
    public partial class EditHoliday : Form
    {
        private Form1 main;
        public EditHoliday()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditHoliday_Load(object sender, EventArgs e)
        {
            main = this.Owner as Form1;
            if (main != null)
            {
                var person = main._repositoryProvider.GetRepository<Employee>().GetAll().ToList();
                dateTimePicker1.Value = Convert.ToDateTime(main.Holiday.StartDate.ToShortDateString());
                dateTimePicker2.Value = Convert.ToDateTime(main.Holiday.EndDate.ToShortDateString());
                comboBox1.DataSource = person;
                comboBox1.ValueMember = "Id";
                comboBox1.DisplayMember = "FIO";
                if (main.Holiday.Employee != null)
                    comboBox1.SelectedValue = main.Holiday.Employee.Id;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                main.Holiday.Employee = (Employee) comboBox1.SelectedItem;
                main.Holiday.StartDate = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                main.Holiday.EndDate = Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString());
                main._repositoryProvider.SaveChanges();
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Что-то пошло не так!");
            }
        }
    }
}
