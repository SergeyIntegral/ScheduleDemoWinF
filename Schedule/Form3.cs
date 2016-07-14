using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule;
//using Schedule.DAL.Models;
using ScheduleNahui.DAL.Projection;
//using Schedule = Schedule.Schedule;

namespace ScheduleNahui
{
    public partial class Form3 : Form
    {
        private Form1 main;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            main = this.Owner as Form1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            List<int> sortListOnPosition = new List<int>()
            {
                4,
                3,
                1,
                5,
                6,
                2
            };


            
            int Year = Convert.ToInt32(textBox1.Text);

            int mounth = 5;
            #region if
            if (comboBox1.SelectedIndex == 0)
            {
                mounth = 1;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                mounth = 2;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                mounth = 3;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                mounth = 4;
            }
            if (comboBox1.SelectedIndex == 4)
            {
                mounth = 5;
            }
            if (comboBox1.SelectedIndex == 5)
            {
                mounth = 6;
            }
            if (comboBox1.SelectedIndex == 6)
            {
                mounth = 7;
            }
            if (comboBox1.SelectedIndex == 7)
            {
                mounth = 8;
            }
            if (comboBox1.SelectedIndex == 8)
            {
                mounth = 9;
            }
            if (comboBox1.SelectedIndex == 9)
            {
                mounth = 10;
            }
            if (comboBox1.SelectedIndex == 10)
            {
                mounth = 11;
            }
            if (comboBox1.SelectedIndex == 11)
            {
                mounth = 12;
            }
            #endregion
            List<DateTime> masDayOfmonth = new List<DateTime>() { new DateTime(Year, mounth, 1) };

            for (int i = 0; i < System.DateTime.DaysInMonth(Year, mounth) - 1; i++)
            {
                masDayOfmonth.Add(masDayOfmonth.Last().AddDays(1));
            }

            //var peoples = main._repositoryProvider.GetRepository<Employee>().GetAll().ToList();
            var peoplesNonSort = main._repositoryProvider.GetRepository<Employee>().GetAll().ToList();
            List<Employee> peoples = new List<Employee>();

            //сортировка по sortListOnPosition

            foreach (var position in sortListOnPosition)
            {
                var listPeopleWithCurrentPosition = peoplesNonSort.Where(x => x.PositionId == position).ToList();

                foreach (var el in listPeopleWithCurrentPosition)
                {
                    peoples.Add(el);
                }
            }

            List<ProjForExcelDg> Raspisanie = new List<ProjForExcelDg>();

            for (int i = 0; i < peoples.Count; i++)
            {
                Raspisanie.Add(new ProjForExcelDg()
                {
                    FIO = peoples[i].Name + " " + peoples[i].LastName + " " + peoples[i].MiddleName,
                    Dolj = peoples[i].Position.Title
                });
                var _employeeId = peoples[i].Id;
                var workdays = main._repositoryProvider.GetRepository<Schedule.Schedule>().GetAll().
                    Where(x => x.EmployeeId == _employeeId)
                    .Where(x => x.Date.Value.Year == Year && x.Date.Value.Month == mounth)
                    .ToList();

                for (int j = 0; j < workdays.Count; j++)
                {

                    //string start = "0";
                    //string end = "1";

                    string start = workdays[j].StartTime.ToString().Substring(0, 5);
                    string end = workdays[j].EndTime.ToString().Substring(0, 5);
                    string sum = workdays[j].SumTime.ToString().Substring(0, 5);

                    Raspisanie.Last().ScheduleTable.Add(workdays[j].Date.Value, start + " - " + end);
                }
            }
            dataGridView1.DataSource = Raspisanie;

            for (int i = 0; i < masDayOfmonth.Count; i++)
            {
                DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = masDayOfmonth[i].ToString().Split(' ')[0];
                newColumn.Name = "ID" + Convert.ToString(i);

                dataGridView1.Columns.Add(newColumn);

                int indexColumn = dataGridView1.Columns.Count - 1;
                //int indexColumn = i;

                for (int indexRow = 0; indexRow < dataGridView1.Rows.Count; indexRow++)
                {
                    if (indexRow == 2 && masDayOfmonth[i].Day == 4)
                    {
                        int ssssll = 2;
                    }
                    bool qwer = Raspisanie[indexRow].ScheduleTable.ContainsKey(masDayOfmonth[i]);

                    if (Raspisanie[indexRow].ScheduleTable.ContainsKey(masDayOfmonth[i]))
                    {
                        dataGridView1.Rows[indexRow].Cells[indexColumn].Value = Raspisanie[indexRow].ScheduleTable[masDayOfmonth[i]];
                    }
                    else
                    {
                        dataGridView1.Rows[indexRow].Cells[indexColumn].Value = "";
                    }

                }
            }
            DataGridViewTextBoxColumn newColumn1 = new DataGridViewTextBoxColumn();
            newColumn1.HeaderText = "Норма Часы";
            //newColumn.Name = "ID" + Convert.ToString(i);

            dataGridView1.Columns.Add(newColumn1);
            for (int i = 0; i < peoples.Count; i++)
            {
                int id = peoples[i].Id;
                var date = main._repositoryProvider.GetRepository<Schedule.Schedule>().GetAll()
                    .Where(x => x.EmployeeId == id)
                    .Where(x=>x.Date.Value.Year==Year&& x.Date.Value.Month== mounth).ToList();
                //TimeSpan summSpan = new TimeSpan(0,0,0);

                //foreach (var el in date)
                //{
                //    summSpan += el.SumTime.Value;
                //}
                //dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = summSpan.TotalHours.ToString();
                int? SummaryTime = 0;
                foreach (var vatt in date)
                {
                    SummaryTime += vatt.Summary;

                }
                dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = SummaryTime.ToString();
            }

        }
        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        private void ExcelImport(DataGridView dgv)
        {
            //saveFileDialog1.InitialDirectory = "C:";
            saveFileDialog1.InitialDirectory = "C:/Users/Integral/Documents";
            saveFileDialog1.Title = "Сохранить как Excel";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Excel Files(2007)|*.xlsx";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);

                int id;
                GetWindowThreadProcessId(excelApp.Hwnd, out id);
                Process excelProcess = Process.GetProcessById(id);
                excelApp.Columns[1].ColumnWidth = 30;
                excelApp.Columns[2].ColumnWidth = 15;
                excelApp.Columns[3].ColumnWidth = 13;
                excelApp.Columns[4].ColumnWidth = 13;
                excelApp.Columns[5].ColumnWidth = 13;
                excelApp.Columns[6].ColumnWidth = 13;
                excelApp.Columns[7].ColumnWidth = 13;
                excelApp.Columns[8].ColumnWidth = 13;
                excelApp.Columns[9].ColumnWidth = 13;
                excelApp.Columns[10].ColumnWidth = 13;
                excelApp.Columns[11].ColumnWidth = 13;
                excelApp.Columns[12].ColumnWidth = 13;
                excelApp.Columns[13].ColumnWidth = 13;
                excelApp.Columns[14].ColumnWidth = 13;
                excelApp.Columns[15].ColumnWidth = 13;
                excelApp.Columns[16].ColumnWidth = 13;
                excelApp.Columns[17].ColumnWidth = 13;
                excelApp.Columns[18].ColumnWidth = 13;
                excelApp.Columns[19].ColumnWidth = 13;
                excelApp.Columns[20].ColumnWidth = 13;
                excelApp.Columns[21].ColumnWidth = 13;
                excelApp.Columns[22].ColumnWidth = 13;
                excelApp.Columns[23].ColumnWidth = 13;
                excelApp.Columns[24].ColumnWidth = 13;
                excelApp.Columns[25].ColumnWidth = 13;
                excelApp.Columns[26].ColumnWidth = 13;
                excelApp.Columns[27].ColumnWidth = 13;
                excelApp.Columns[28].ColumnWidth = 13;
                excelApp.Columns[29].ColumnWidth = 13;
                excelApp.Columns[30].ColumnWidth = 13;
                excelApp.Columns[31].ColumnWidth = 13;
                excelApp.Columns[32].ColumnWidth = 13;
                excelApp.Columns[33].ColumnWidth = 13;




                for (int i = 1; i < dgv.Columns.Count + 1; i++) // -ид(+1)
                {
                    excelApp.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++) //-ид
                    {
                        if (i == 0 && j == 2)
                        {
                            int a = 0;
                        }
                        excelApp.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
                    }
                }
                excelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                excelApp.ActiveWorkbook.Saved = true;
                excelApp.Workbooks.Close();
                excelApp.Quit();
                //Marshal.FinalReleaseComObject(excelApp.Workbooks);
                //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
                excelProcess.Kill();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExcelImport(dataGridView1);
        }
    }
}
