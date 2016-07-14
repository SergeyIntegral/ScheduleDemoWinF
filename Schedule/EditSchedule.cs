using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule;

namespace ScheduleNahui
{
    public partial class EditSchedule : Form
    {
        private Form1 main;

        public EditSchedule()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditSchedule_Load(object sender, EventArgs e)
        {
            main = this.Owner as Form1;
            if (main != null)
            {
                dateTimePicker1.Value = Convert.ToDateTime(main.Schedule.Date.Value.ToShortDateString());
                textBox1.Text = main.Schedule.Employee.Name + " " + main.Schedule.Employee.LastName + " " +
                                main.Schedule.Employee.MiddleName;
                textBox2.Text = main.Schedule.StartTime.Value.ToString().Substring(0,5);
                textBox3.Text = main.Schedule.EndTime.Value.ToString().Substring(0, 5);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            main.Schedule.Date = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            main.Schedule.StartTime = TimeSpan.Parse(textBox2.Text);
            main.Schedule.EndTime = TimeSpan.Parse(textBox3.Text);
            main.Schedule.SumTime = TimeSpan.Parse(textBox3.Text) - TimeSpan.Parse(textBox2.Text);
            main._repositoryProvider.SaveChanges();
            this.Close();

        }
    }
}
