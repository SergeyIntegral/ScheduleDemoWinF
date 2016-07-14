using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule;
//using Schedule.DAL.Models;
using ScheduleNahui.DAL.Projection;
//using Schedule1 = Schedule.DAL.Models.Schedule;


namespace ScheduleNahui
{
    public partial class CreateHand : Form
    {
        private Form1 main;
        public CreateHand()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan dds = TimeSpan.Parse(textBox1.Text);
            TimeSpan dde = TimeSpan.Parse(textBox2.Text);
            
            Schedule.Schedule ww = new Schedule.Schedule();

            
            ww.Employee = (Employee) comboBox1.SelectedItem;
            ww.Date = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            if (ww.Employee.PositionId == 5)
            {
                ww.StartTime = dds;
                ww.EndTime = dde;
                ww.SumTime = dde - dds;
                ww.Summary = dde.Hours - dds.Hours;

            }else if (ww.Employee.PositionId == 1 && ww.Date.Value.DayOfWeek == DayOfWeek.Saturday ||
                      ww.Employee.PositionId == 4 && ww.Date.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                ww.StartTime = dds;
                ww.EndTime = dde;
                ww.SumTime = dde - dds;
                ww.Summary = (dde.Hours - dds.Hours) - 1;
            }
            else
            {
                ww.StartTime = dds;
                ww.EndTime = dde;
                ww.SumTime = dde - dds;
                ww.Summary = (dde.Hours - dds.Hours) - 1;
            }
            
            //ww.StartTime = TimeSpan.Parse(textBox1.Text);
            //ww.EndTime = TimeSpan.Parse(textBox2.Text);
            //ww.SumTime = TimeSpan.Parse(textBox2.Text) - TimeSpan.Parse(textBox1.Text);
            main._repositoryProvider.GetRepository<Schedule.Schedule>().Add(ww);
            main._repositoryProvider.SaveChanges();

            var schdg =
                main._repositoryProvider.GetRepository<Schedule.Schedule>()
                    .GetAll()
                    .Select(x => new ScheduleProjectionForDg()
                    {
                        Id = x.Id,
                        EmployeeId = x.EmployeeId,
                        FIO = x.Employee.Name + " " + x.Employee.LastName + " " + x.Employee.MiddleName,
                        //Datee = x.Date.Value.ToString("d") + "\n" + x.StartTime.Value.ToString(@"hh\:mm") + " - " + x.EndTime.Value.ToString(@"hh\:mm")
                        Date = x.Date.Value,
                        StartTime = x.StartTime.Value,
                        EndTime = x.EndTime.Value,
                        Dolj = x.Employee.Position.Title
                    }).Where(x=>x.EmployeeId== ww.Employee.Id).Where(x=>x.Date.Month.ToString().Contains(ww.Date.Value.Month.ToString()))
                    .ToList();
            dataGridView1.DataSource = schdg;
            dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false; //emp
            dataGridView1.Columns[7].Visible = false; //id
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



            //MessageBox.Show("Добавлено!");

        }

        private void CreateHand_Load(object sender, EventArgs e)
        {
            main = this.Owner as Form1;
            if (main != null)
            {
                var emp = main._repositoryProvider.GetRepository<Employee>().GetAll().ToList();
                comboBox1.DataSource = emp;
                comboBox1.ValueMember = "Id";
                comboBox1.DisplayMember = "FIO";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
