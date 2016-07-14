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
    public partial class CreateHoliday : Form
    {
        private Form1 main;
        public CreateHoliday()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateHoliday_Load(object sender, EventArgs e)
        {
            main = this.Owner as Form1;
            if (main != null)
            {
                var person = main._repositoryProvider.GetRepository<Employee>().GetAll().ToList();
                comboBox1.DataSource = person;
                comboBox1.ValueMember = "Id";
                comboBox1.DisplayMember = "FIO";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Holiday hd = new Holiday
            {
             Employee   = (Employee)comboBox1.SelectedItem,
             StartDate = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString()),
             EndDate = Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString())
            };
            main._repositoryProvider.GetRepository<Holiday>().Add(hd);
            main._repositoryProvider.SaveChanges();
            this.Close();
        }
    }
}
