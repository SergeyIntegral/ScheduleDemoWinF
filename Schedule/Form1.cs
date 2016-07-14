using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DAL;
using Schedule.DAL;

using Microsoft.Office.Interop.Excel;
using Schedule.DAL.Projection;
using ScheduleNahui;
using ScheduleNahui.DAL.Projection;
using Application = System.Windows.Forms.Application;


namespace Schedule
{
    public partial class Form1 : Form
    {
        internal readonly IRepositoryProvider _repositoryProvider;
        private CreateEmployee _createEmployeeForm;
        private EditEmployee _editEmployeeForm;
        //private ViewHolidays _viewHolidays;
        private List<Employee> _employees;
        private List<Employee> _employees2;
        public Employee SelectedEmployee;
        private List<HolidayProjection> _holiday;
        private CreateHoliday _createHolidayForm;
        private EditHoliday _editHolidayForm;
        private EditSchedule _editScheduleForm;
        private CreateHand _createHandleForm;
        public List<Position> Positions;
        public List<Schedule> Schedules;
        public DateTime January = new DateTime(2016, 1, 1);
        public DateTime February = new DateTime(2016, 2, 1);
        public DateTime March = new DateTime(2016, 3, 1);
        public DateTime April = new DateTime(2016, 4, 1);
        public DateTime May = new DateTime(2016, 5, 1);
        public DateTime June = new DateTime(2016, 6, 1);
        public DateTime July = new DateTime(2016, 7, 1);
        public DateTime August = new DateTime(2016, 8, 1);
        public DateTime September = new DateTime(2016, 9, 1);
        public DateTime October = new DateTime(2016, 10, 1);
        public DateTime November = new DateTime(2016, 11, 1);
        public DateTime December = new DateTime(2016, 12, 1);
        public List<EmployeeProjection> azaza;
        public List<Schedule> Schedules1;
        public List<ScheduleProjectionForDg> schdg;
        public Employee Employee { get; set; }

        public Holiday Holiday { get; set; }

        public Schedule Schedule { get; set; }

        public Form1()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            _repositoryProvider = new EntityRepositoryProvider<ScheduleBdContext>();
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _employees = _repositoryProvider.GetRepository<Employee>().GetAll().ToList();
            listBox1.DataSource = _employees;
            listBox1.DisplayMember = "FIO";
            button7.Image = Properties.Resources.close1;
            button10.Image = Properties.Resources.close1;
            //button11.Image = ScheduleNahui.Properties.Resources.excel;
            button12.Image = Properties.Resources.close1;

            dataGridView1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label1.Visible = false;

            pictureBox1.Image = Properties.Resources.logo1;
            pictureBox2.Image = Properties.Resources.logo1;
            pictureBox3.Image = Properties.Resources.logo1;
            label2.Visible = false;
            label3.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            groupBox1.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            groupBox2.Visible = false;
            dataGridView2.Visible = false;
            button10.Visible = false;
            button12.Visible = false;
            button13.Visible = false;

            comboBox3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;

            //Positions = _repositoryProvider.GetRepository<Position>().GetAll().ToList();
            //comboBox1.DataSource = Positions;
            //comboBox1.ValueMember = "Id";
            //comboBox1.DisplayMember = "Title";

            Schedules1 = _repositoryProvider.GetRepository<Schedule>().GetAll().ToList();
            //dataGridView2.DataSource = Schedules1;
            schdg =
                _repositoryProvider.GetRepository<Schedule>()
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
                    }).ToList();
            dataGridView2.DataSource = schdg;
            dataGridView2.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false; //emp
            dataGridView2.Columns[7].Visible = false; //id
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedEmployee = (Employee) listBox1.SelectedItem;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var query = _repositoryProvider.GetRepository<Employee>().Find(SelectedEmployee.Id);
                _repositoryProvider.GetRepository<Employee>().RemoveById(SelectedEmployee.Id);
                _repositoryProvider.SaveChanges();
                listBox1.DataSource = _repositoryProvider.GetRepository<Employee>().GetAll().ToList();
                RefreshGridHolidays();
            }
            else
            {
                MessageBox.Show("Выделить!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _createEmployeeForm = new CreateEmployee();
            _createEmployeeForm.Owner = this;
            _createEmployeeForm.ShowDialog();

            foreach (var createEmployeeForm in Application.OpenForms)
            {
                if (!(createEmployeeForm is CreateEmployee))
                {
                    listBox1.DataSource = _repositoryProvider.GetRepository<Employee>().GetAll().ToList();
                }

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = false;
            dataGridView1.Visible = true;
            label1.Visible = true;
            RefreshGridHolidays();
            button12.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _createHolidayForm = new CreateHoliday();
            _createHolidayForm.Owner = this;
            _createHolidayForm.ShowDialog();
            foreach (var createHolidayForm in Application.OpenForms)
            {
                if (!(createHolidayForm is CreateHoliday))
                {
                    RefreshGridHolidays();
                }

            }
        }

        private void RefreshGridHolidays()
        {
            _holiday =
                _repositoryProvider.GetRepository<Holiday>().GetAll().Select(x => new HolidayProjection()
                {
                    EndDate = x.EndDate,
                    StartDate = x.StartDate,
                    FIO = x.Employee.Name + " " + x.Employee.LastName + " " + x.Employee.MiddleName,
                    Id = x.Id
                }).ToList();


            dataGridView1.DataSource = _holiday;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[3, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Holiday = _repositoryProvider.GetRepository<Holiday>().Find(id);

                _editHolidayForm = new EditHoliday();
                _editHolidayForm.Owner = this;
                _editHolidayForm.ShowDialog();
                foreach (var editHolidayFor in Application.OpenForms)
                {
                    if (!(editHolidayFor is EditHoliday))
                    {
                        RefreshGridHolidays();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выделите строку таблицы !");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            button1.Visible = false;
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Visible = false;
            groupBox1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            button1.Visible = true;
            button8.Visible = false;
            button9.Visible = true;


        }

        public int moment = 0;


        private void button8_Click(object sender, EventArgs e)
        {


            int ProfId = 0;
            int ProfId2 = 0;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    ProfId = 1;
                    ProfId2 = 1;
                    break;

                case 1:
                    ProfId = 3;
                    ProfId2 = 4;
                    break;

                case 2:
                    ProfId = 2;
                    ProfId2 = 2;
                    break;
                case 3:
                    ProfId = 5;
                    ProfId2 = 5;
                    break;
                case 4:
                    ProfId = 6;
                    ProfId2 = 6;
                    break;

            }


            #region определенеи месяца на оснавании комбобокса

            int mm2 = 0;
            if (comboBox2.SelectedIndex == 0)
            {
                moment = December.Month;
                mm2 = moment + 1;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                moment = January.Month;
                mm2 = moment + 1;
            }
            if (comboBox2.SelectedIndex == 2)
            {
                moment = February.Month;
                mm2 = moment + 1;
            }
            if (comboBox2.SelectedIndex == 3)
            {
                moment = March.Month;
                mm2 = moment + 1;
            }
            if (comboBox2.SelectedIndex == 4)
            {
                moment = April.Month;
                mm2 = moment + 1;
            }
            if (comboBox2.SelectedIndex == 5)
            {
                moment = May.Month;
                mm2 = moment + 1;
            }
            if (comboBox2.SelectedIndex == 6)
            {
                moment = June.Month;
                mm2 = moment + 1;
            }
            if (comboBox2.SelectedIndex == 7)
            {
                moment = July.Month;
                mm2 = moment + 1;
            }

            if (comboBox2.SelectedIndex == 8)
            {
                moment = August.Month;
                mm2 = moment + 1;
            }
            if (comboBox2.SelectedIndex == 9)
            {
                moment = September.Month;
                mm2 = moment + 1;
            }
            if (comboBox2.SelectedIndex == 10)
            {
                moment = October.Month;
                mm2 = moment + 1;
            }
            if (comboBox2.SelectedIndex == 11)
            {
                moment = November.Month;
                mm2 = moment + 1;
            }

            #endregion ss

            int year = Convert.ToInt32(textBox5.Text);

            var proverkaNaIdiota = _repositoryProvider.GetRepository<Schedule>().GetAll()
                .Where(x => x.Employee.PositionId == ProfId
                            || x.Employee.PositionId == ProfId2)
                .Where(x => x.Date.Value.Year == year
                            && x.Date.Value.Month == mm2).ToList();
            if (proverkaNaIdiota.Count > 0)
            {
                MessageBox.Show("Расписание на этот месяц уже существует !");

                return;
            }




            TimeSpan tstart = TimeSpan.Parse(textBox1.Text);
            TimeSpan tend = TimeSpan.Parse(textBox2.Text);
            TimeSpan tsum = tend - tstart;
            TimeSpan summury = new TimeSpan(0,0,0);

            if (tsum.Hours == 12)
            {
                summury = new TimeSpan(11, 00, 00);
            }
            if (tsum.Hours == 10)
            {
                summury = new TimeSpan(09, 00, 00);
            }
            if (tsum.Hours == 8)
            {
                summury = new TimeSpan(07, 00, 00);
            }
            if (tsum.Hours == 6)
            {
                summury = new TimeSpan(05, 00, 00);

            }
            if (tsum.Hours == 13)
            {
                summury = new TimeSpan(11, 00, 00);

            }
            if (tsum.Hours == 7)
            {
                summury = new TimeSpan(06, 00, 00);
            }
            //все челики по должности из айди
            _employees2 =
                _repositoryProvider.GetRepository<Employee>()
                    .GetAll()
                    .Where(x => x.Position.Id == (int) ProfId || x.Position.Id == (int) ProfId2)
                    .ToList();


            //всё распесиние для челиков на данной должности
            var query =
                _repositoryProvider.GetRepository<Schedule>().GetAll().
                    Where(x => x.Employee.Position.Id == (int) ProfId).ToList();

            //праздники
            var festival = _repositoryProvider.GetRepository<Feast>().GetAll().ToList();
            //if (query.Count==0)
            //{
            //    MessageBox.Show("Для сотрудников на данной должности нет записей за предыдущий месяц");
            //}

            //все выходные 
            var dayOff = _repositoryProvider.GetRepository<Weekend>().GetAll().ToList();





            List<Schedule> listForInputSchedules = new List<Schedule>();
            List<DateTime> dayOfWork;
            DateTime NowDay;

            switch ((int) comboBox1.SelectedIndex)
            {
                // для операторов
                case 0:
                    //поиск предыдущего росписания для челиков по выбранной должности
                    //var FindPrevSheduleForSelectPosition = query.Where(x => x.EmployeeId == _employees2)
                    //        .Where(x => x.Date.Value.Month == moment)
                    //        .OrderBy(x => x.Date).LastOrDefault();

                    var FindPrevSheduleForSelectPosition = query.Where(x => x.Date.Value.Month == moment);

                    List<Schedule> listOfprevWork = new List<Schedule>();

                    #region `123

                    //foreach (var el in FindPrevSheduleForSelectPosition)
                    //{
                    //    bool findIt = false;
                    //    foreach (var em in _employees2)
                    //    {
                    //        if (el.EmployeeId == em.Id)
                    //        {
                    //            findIt = true;
                    //            break;
                    //        }
                    //    }

                    //    if (findIt)
                    //    {
                    //        listOfprevWork.Add(el);
                    //    }
                    //}

                    #endregion

                    for (int h = 0; h < _employees2.Count(); h++)
                    {
                        var uu1 = query.Where(x => x.EmployeeId == _employees2[h].Id)
                            .Where(x => x.Date.Value.Month == moment)
                            .OrderBy(x => x.Date).LastOrDefault();
                        listOfprevWork.Add(uu1);
                    }

                    //перебирать дни 
                    //определить является ли день праздником
                    //определить является ли день выходным
                    //если день рабочий(нет на 2 предыдущих вопроса)
                    //выбираем челиков которые не в отпуске 
                    //то на основании очков работы выбираем двух для работы
                    //если  у этих двух совпадают очки работы  то


                    dayOfWork = new List<DateTime>();

                    NowDay = new DateTime(year, moment + 1, 1);

                    while (NowDay.Month == moment + 1)
                    {
                        //определить является ли день праздником
                        if (festival.Exists(x => x.Mounth == NowDay.Month && x.DayOfMounth == NowDay.Day))
                        {
                            NowDay = NowDay.AddDays(1);
                            continue;
                        }

                        //определить является ли день выходным для оператора
                        List<Weekend> dayOffForOperator = dayOff.Where(x => x.PositionId == 1).ToList();

                        List<DayOfWeek> DayOffOperator = getDayOff(dayOffForOperator);

                        if (DayOffOperator.IndexOf(NowDay.DayOfWeek) != -1)
                        {
                            NowDay = NowDay.AddDays(1);
                            continue;
                        }

                        dayOfWork.Add(NowDay);
                        NowDay = NowDay.AddDays(1);
                    }

                    int CountPeopleWork =
                        _repositoryProvider.GetRepository<CountOfPeopleIntTheWorkDay>()
                            .GetAll()
                            .Where(a => a.PositionId == 1).First().CountPeopleWork;

                    var sssdwds = query
                        .GroupBy(x => x.Date)
                        .Select(x => x.FirstOrDefault())
                        .OrderByDescending(x => x.Date)
                        .Take(CountPeopleWork)
                        .ToList();



                    //подсчёт очков работы

                    List<int[]> PointWorkMain = new List<int[]>();

                    //int[][] PointWork = new int[_employees2.Count][];
                    //[][количество дней от послднего дня работы, id челика, количество отработанных в конче месяца, занятость 1- не занят 0 - занят]
                    for (int i = 0; i < _employees2.Count; i++)
                    {
                        //PointWork[i] = new int[3];
                        PointWorkMain.Add(new int[3]);
                        PointWorkMain[i][1] = _employees2[i].Id;

                        int counterLastDayWork = 0;
                        for (int j = 0; j < sssdwds.Count; j++)
                        {
                            int existWorkDay = query.Where(x => x.EmployeeId == _employees2[i].Id)
                                .Where(x => x.Date == sssdwds[j].Date).ToList().Count;
                            if (existWorkDay != 0)
                            {
                                counterLastDayWork++;
                            }
                        }

                        PointWorkMain[i][2] = counterLastDayWork;
                    }

                    //поиск последнего дня работы, количество дней от послднего дня работы
                    for (int i = 0; i < listOfprevWork.Count; i++)
                    {
                        PointWorkMain[i][0] = Convert.ToInt32((dayOfWork[0] - listOfprevWork[i].Date.Value).TotalDays);
                    }

                    foreach (var dow in dayOfWork)
                    {
                        int _CountPeopleWork = CountPeopleWork;

                        List<int[]> PointWork = new List<int[]>();



                        //ToDo
                        //на основании отпуска определять вносить этого челика в этот массив или не вносить
                        //если челик вышел из отпуска(вчера был отпуск) присвоить ему "как довно не работал" 1000 
                        foreach (var el in PointWorkMain)
                        {
                            //определить в отпуске ли чел
                            //получить все отпуска челика

                            int indexMass = el[1];

                            var holydays =
                                _repositoryProvider.GetRepository<Holiday>()
                                    .GetAll()
                                    .Where(x => x.EmployeeId == indexMass)
                                    .ToList();


                            List<DateTime[]> AllHolidays = new List<DateTime[]>();
                            for (int k = 0; k < holydays.Count; k++)
                            {
                                AllHolidays.Add(new DateTime[] {holydays[k].StartDate, holydays[k].EndDate});
                            }

                            //является ли сегодня отпуском
                            bool NowVacation = false;
                            foreach (var ah in AllHolidays)
                            {
                                //el[0] = new DateTime();
                                if (ah[0] <= dow && dow <= ah[1])
                                {
                                    NowVacation = true;
                                    break;
                                }
                            }

                            if (!NowVacation)
                            {
                                int[] mass = new int[el.Length];
                                Array.Copy(el, mass, el.Length);
                                PointWork.Add(mass);
                            }
                        }



                        for (int k = 0; k < CountPeopleWork; k++)
                        {
                            //сначала смотрим по критерию "как довно не работал" 
                            //сортируем по убыванию пузырёк
                            for (int i = 0; i < PointWork.Count - 1; i++)
                            {
                                for (int j = 0; j < PointWork.Count - 1; j++)
                                {
                                    if (PointWork[i][0] < PointWork[i + 1][0])
                                    {
                                        int bufValue = PointWork[i + 1][0];
                                        int bufIndex = PointWork[i + 1][1];
                                        int bufWork = PointWork[i + 1][2];

                                        PointWork[i + 1][0] = PointWork[i][0];
                                        PointWork[i + 1][1] = PointWork[i][1];
                                        PointWork[i + 1][2] = PointWork[i][2];

                                        PointWork[i][0] = bufValue;
                                        PointWork[i][1] = bufIndex;
                                        PointWork[i][2] = bufWork;
                                    }
                                }
                            }

                            int counter = 0;

                            for (int i = 1; i < PointWork.Count; i++)
                            {
                                if (PointWork[i][0] == PointWork[0][0])
                                {
                                    counter++;
                                }
                            }

                            //если количество потенциальных работников больше чем требуется количества операторов
                            if (counter >= 1)
                            {
                                //сортируем по возврастанию пузырёк
                                for (int i = 0; i < PointWork.Count - 1; i++)
                                {
                                    for (int j = 0; j < PointWork.Count - 1; j++)
                                    {
                                        if (PointWork[i][2] > PointWork[i + 1][2])
                                        {
                                            int bufValue = PointWork[i + 1][0];
                                            int bufIndex = PointWork[i + 1][1];
                                            int bufWork = PointWork[i + 1][2];

                                            PointWork[i + 1][0] = PointWork[i][0];
                                            PointWork[i + 1][1] = PointWork[i][1];
                                            PointWork[i + 1][2] = PointWork[i][2];

                                            PointWork[i][0] = bufValue;
                                            PointWork[i][1] = bufIndex;
                                            PointWork[i][2] = bufWork;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //--@--
                            }

                            int indexMain = PointWorkMain.IndexOf
                                (PointWorkMain
                                    .Where(x => x[1] == PointWork[0][1])
                                    .FirstOrDefault()
                                );
                            PointWorkMain[indexMain][0] = 1;
                            PointWorkMain[indexMain][2]++;

                            //TimeSpan endt = ;
                            //записать верхний и стереть его из PointWork
                            if (dow.DayOfWeek == DayOfWeek.Saturday)
                            {
                                tend = new TimeSpan(18, 30, 00);
                            }
                            else
                            {
                                // endt = new TimeSpan(Convert.ToInt32(textBox2.Text.Split(':')[1]), Convert.ToInt32(textBox2.Text.Split(':')[2]), Convert.ToInt32(textBox2.Text.Split(':')[2]));
                                tend = TimeSpan.Parse(textBox2.Text);
                            }
                            TimeSpan strt = TimeSpan.Parse(textBox1.Text);
                            listForInputSchedules.Add(new Schedule()
                            {
                                EmployeeId = PointWork[0][1],
                                Date = dow,
                                StartTime = tstart,
                                EndTime = tend,
                                SumTime = tend-tstart,
                                Summary = Convert.ToInt32((tend - tstart).TotalHours-1)
                            });

                            PointWork.Remove(PointWork.First());
                            _CountPeopleWork--;

                            if (PointWork.Count == 0)
                            {
                                int hgfd = 2;
                            }

                            if (k == CountPeopleWork - 1)
                            {
                                for (int i = 0; i < PointWorkMain.Count; i++)
                                {
                                    //if (PointWorkMain[i][1] != PointWork[0][1])
                                    //if (PointWork.IndexOf(PointWorkMain[i]) != -1 )
                                    if (PointWork.Where(x => x[1] == PointWorkMain[i][1]).FirstOrDefault() != null)

                                    {
                                        PointWorkMain[i][0]++;
                                        PointWorkMain[i][2] = 0;
                                    }
                                }
                            }
                        }




                    }




                    break;
                //case 0 ------------------------------------------------------------------
                case 1:

                    ////всё распесиние для челиков на данной должности
                    //int yearLastMonth = Convert.ToInt32(textBox5.Text);

                    ////найти росписание для начальника и контралёра
                    //var workDayLastMonth =
                    //    _repositoryProvider.GetRepository<Schedule>().GetAll()
                    //    .Where(x => x.Employee.Position.Id == (int)ProfId
                    //        || x.Employee.Position.Id == (int)ProfId2)
                    //        .Where(x => x.Date.Value.Month == moment
                    //        && x.Date.Value.Year == yearLastMonth)
                    //        .ToList();

                    //найти рабочие дни для них

                    //List<List<Schedule>> listoflist = new List<List<Schedule>>();

                    //foreach (var el in _employees2)
                    //{
                    //    int idEMP = el.Id;
                    //    var workDayLastMontOfEMP =
                    //        workDayLastMonth
                    //        .Where(x => x.EmployeeId == idEMP)
                    //        .OrderByDescending(x => x.Date)
                    //        .ToList();

                    //    //listoflist.Add(workDayLastMontOfEMP);
                    //}

                    int countStep = -1;
                    //DateTime A_EMP;
                    //DateTime B_EMP;
                    //do
                    //{
                    //    countStep++;
                    //    A_EMP = listoflist[0][countStep].Date.Value;
                    //    B_EMP = listoflist[1][countStep].Date.Value;

                    //} while (A_EMP == B_EMP);

                    //int idLastWorkEmp = 0;
                    //int numberWorkEmp = 0;


                    //if (A_EMP > B_EMP)
                    //{
                    //    numberWorkEmp = 0;
                    //    //idLastWorkEmp = listoflist[0][countStep].EmployeeId;
                    //}
                    //else
                    //{
                    //    numberWorkEmp = 1;
                    //    //idLastWorkEmp = listoflist[1][countStep].EmployeeId;
                    //}
                    //получить список всех рабочих дней пред месяца
                    var LastMounthWorkDays = _repositoryProvider.GetRepository<Schedule>().GetAll()
                        .Where(x => x.Date.Value.Month == moment && x.Date.Value.Year == year)
                        .GroupBy(x => x.Date)
                        .Select(x => x.FirstOrDefault())
                        .OrderByDescending(x => x.Date).Select(x => x.Date).ToList();

                    int numberWorkEmp = 0;
                    int idOne = _employees2[0].Id;
                    int idTwo = _employees2[1].Id;
                    foreach (var el in LastMounthWorkDays)
                    {
                        countStep++;
                        var fghjk = _repositoryProvider.GetRepository<Schedule>().GetAll()
                            .Where(x => x.Date == el).Where(x => x.EmployeeId == idOne || x.EmployeeId == idTwo)
                            .ToList();
                        if (fghjk.Count < 2)
                        {
                            if (fghjk.FirstOrDefault().EmployeeId == idOne)
                            {
                                numberWorkEmp = 0;
                            }
                            else
                            {
                                numberWorkEmp = 1;
                            }

                            break;
                        }
                    }
                    dayOfWork = new List<DateTime>();
                    NowDay = new DateTime(year, moment + 1, 1);

                    while (NowDay.Month == moment + 1)
                    {
                        //определить является ли день праздником
                        if (festival.Exists(x => x.Mounth == NowDay.Month && x.DayOfMounth == NowDay.Day))
                        {
                            NowDay = NowDay.AddDays(1);
                            continue;
                        }

                        //определить является ли день выходным для оператора
                        List<Weekend> dayOffForOperator = dayOff.Where(x => x.PositionId == 1).ToList();

                        List<DayOfWeek> DayOffOperator = getDayOff(dayOffForOperator);

                        if (DayOffOperator.IndexOf(NowDay.DayOfWeek) != -1)
                        {
                            NowDay = NowDay.AddDays(1);
                            continue;
                        }

                        dayOfWork.Add(NowDay);
                        NowDay = NowDay.AddDays(1);
                    }

                    //теперь создадим россписание на основании данных о том кто работал и этапе работы
                    foreach (var dow in dayOfWork)
                    {
                        int idWorkerOne = 0;
                        int idWorkerTwo = 0;
                        switch (countStep)
                        {
                            //работает только 1 челик
                            case 0:
                                idWorkerOne = _employees2[numberWorkEmp].Id;

                                if (!NowHoliday(dow, idWorkerOne))
                                {
                                    listForInputSchedules.Add(new Schedule()
                                    {
                                        EmployeeId = idWorkerOne,
                                        Date = dow,
                                        StartTime = new TimeSpan(07, 30, 00),
                                        EndTime = new TimeSpan(20, 30, 00),
                                        SumTime = new TimeSpan(11, 00, 00),
                                        Summary = 11

                                    });
                                }


                                countStep++;



                                break;
                            //с утра работает тот кто работал вчера
                            case 1:

                                if (numberWorkEmp == 0)
                                {
                                    idWorkerOne = _employees2[0].Id;
                                    idWorkerTwo = _employees2[1].Id;

                                    //numberWorkEmp = 1;
                                }
                                else
                                {
                                    idWorkerOne = _employees2[1].Id;
                                    idWorkerTwo = _employees2[0].Id;
                                }
                                //с утра

                                if (!NowHoliday(dow, idWorkerOne))
                                {
                                    listForInputSchedules.Add(new Schedule()
                                    {
                                        EmployeeId = idWorkerOne,
                                        Date = dow,
                                        StartTime = new TimeSpan(07, 30, 00),
                                        EndTime = new TimeSpan(15, 30, 00),
                                        SumTime = new TimeSpan(07, 00, 00),
                                        Summary = 7
                                    });
                                }

                                //с обеда
                                if (!NowHoliday(dow, idWorkerTwo))
                                {
                                    listForInputSchedules.Add(new Schedule()
                                    {
                                        EmployeeId = idWorkerTwo,
                                        Date = dow,
                                        StartTime = new TimeSpan(13, 30, 00),
                                        EndTime = new TimeSpan(20, 30, 00),
                                        SumTime = new TimeSpan(07, 00, 00),
                                        Summary = 7
                                    });
                                }

                                countStep++;
                                break;
                            //тот кто последний работал полный день тот работатет с обеда
                            case 2:

                                if (numberWorkEmp == 0)
                                {
                                    idWorkerOne = _employees2[1].Id;
                                    idWorkerTwo = _employees2[0].Id;
                                }
                                else
                                {
                                    idWorkerOne = _employees2[0].Id;
                                    idWorkerTwo = _employees2[1].Id;
                                }
                                //с утра
                                if (!NowHoliday(dow, idWorkerOne))
                                {
                                    listForInputSchedules.Add(new Schedule()
                                    {
                                        EmployeeId = idWorkerOne,
                                        Date = dow,
                                        StartTime = new TimeSpan(07, 30, 00),
                                        EndTime = new TimeSpan(15, 30, 00),
                                        SumTime = new TimeSpan(07, 00, 00),
                                        Summary = 7
                                    });
                                }

                                //с обеда
                                if (!NowHoliday(dow, idWorkerTwo))
                                {
                                    listForInputSchedules.Add(new Schedule()
                                    {
                                        EmployeeId = idWorkerTwo,
                                        Date = dow,
                                        StartTime = new TimeSpan(13, 30, 00),
                                        EndTime = new TimeSpan(20, 30, 00),
                                        SumTime = new TimeSpan(06, 00, 00),
                                        Summary = 6
                                    });
                                }


                                if (numberWorkEmp == 0)
                                {
                                    numberWorkEmp = 1;
                                }
                                else
                                {
                                    numberWorkEmp = 0;
                                }
                                countStep = 0;



                                break;
                        }
                    }
                    dayOfWork = new List<DateTime>();
                    NowDay = new DateTime(year, moment + 1, 1);

                    while (NowDay.Month == moment + 1)
                    {
                        if (festival.Exists(x => x.Mounth == NowDay.Month && x.DayOfMounth == NowDay.Day))
                        {
                            NowDay = NowDay.AddDays(1);
                            continue;
                        }
                        if (NowDay.DayOfWeek == DayOfWeek.Sunday)
                        {

                            dayOfWork.Add(NowDay);
                        }
                        NowDay = NowDay.AddDays(1);

                    }
                    int idWorkerOnee = 0;
                    int idWorkerTwoo = 0;
                    for (int i = 0; i < dayOfWork.Count; i++)
                    {
                        if ((i + 1)%2 == 0)
                        {
                            idWorkerTwoo  = _employees2.Where(x=>x.PositionId==3).FirstOrDefault().Id; 
                            listForInputSchedules.Add(new Schedule()
                            {
                                EmployeeId = idWorkerTwoo,
                                Date = dayOfWork[i],
                                StartTime = new TimeSpan(09, 00, 00),
                                EndTime = new TimeSpan(11, 00, 00),
                                SumTime = new TimeSpan(02, 00, 00),
                                Summary = 2
                            });
                        }
                        else
                        {
                            idWorkerOnee = _employees2.Where(x => x.PositionId ==4).FirstOrDefault().Id;
                            
                            listForInputSchedules.Add(new Schedule()
                            {
                                EmployeeId = idWorkerOnee,
                                Date = dayOfWork[i],
                                StartTime = new TimeSpan(09, 00, 00),
                                EndTime = new TimeSpan(11, 00, 00),
                                SumTime = new TimeSpan(02, 00, 00),
                                Summary = 2
                            });
                        }

                    }

                    break; //case 1
                default:



                    dayOfWork = new List<DateTime>();

                    NowDay = new DateTime(year, moment + 1, 1);

                    if (ProfId != 5)
                    {

                        while (NowDay.Month == moment + 1)
                        {
                            //определить является ли день праздником
                            if (festival.Exists(x => x.Mounth == NowDay.Month && x.DayOfMounth == NowDay.Day))
                            {
                                NowDay = NowDay.AddDays(1);
                                continue;
                            }

                            //определить является ли день выходным для оператора
                            List<Weekend> dayOffForOtherEmployes = dayOff.Where(x => x.PositionId == ProfId).ToList();

                            List<DayOfWeek> DayOffOther = getDayOff(dayOffForOtherEmployes);

                            if (DayOffOther.IndexOf(NowDay.DayOfWeek) != -1)
                            {
                                NowDay = NowDay.AddDays(1);
                                continue;
                            }

                            dayOfWork.Add(NowDay);
                            NowDay = NowDay.AddDays(1);
                        }
                        TimeSpan ddt = TimeSpan.Parse(textBox2.Text);



                        for (int g = 0; g < _employees2.Count; g++)
                        {


                            for (int i = 0; i < dayOfWork.Count; i++)
                            {

                                if (dayOfWork[i].DayOfWeek == DayOfWeek.Saturday)
                                {
                                    ddt = new TimeSpan(15, 00, 00);
                                }
                                else
                                {
                                    ddt = TimeSpan.Parse(textBox2.Text);
                                }

                                if (!NowHoliday(dayOfWork[i], _employees2[g].Id))
                                {


                                    listForInputSchedules.Add(new Schedule()
                                    {
                                        EmployeeId = _employees2[g].Id,
                                        StartTime = tstart,
                                        EndTime = ddt,
                                        SumTime = ddt - tstart,
                                        Summary = Convert.ToInt32((ddt - tstart).TotalHours - 1),
                                        Date = dayOfWork[i].Date

                                    });
                                }

                            }

                        }
                    }
                    else
                    {
                        while (NowDay.Month == moment + 1)
                        {
                            //определить является ли день праздником
                            if (festival.Exists(x => x.Mounth == NowDay.Month && x.DayOfMounth == NowDay.Day))
                            {
                                NowDay = NowDay.AddDays(1);
                                continue;
                            }

                            //определить является ли день выходным для оператора
                            List<Weekend> dayOffForOtherEmployes = dayOff.Where(x => x.PositionId == ProfId).ToList();

                            List<DayOfWeek> DayOffOther = getDayOff(dayOffForOtherEmployes);

                            if (DayOffOther.IndexOf(NowDay.DayOfWeek) != -1)
                            {
                                NowDay = NowDay.AddDays(1);
                                continue;
                            }

                            dayOfWork.Add(NowDay);
                            NowDay = NowDay.AddDays(1);
                        }
                        TimeSpan ddt = TimeSpan.Parse(textBox2.Text);



                        for (int g = 0; g < _employees2.Count; g++)
                        {


                            for (int i = 0; i < dayOfWork.Count; i++)
                            {

                                if (dayOfWork[i].DayOfWeek == DayOfWeek.Saturday)
                                {
                                    ddt = new TimeSpan(15, 00, 00);
                                }
                                else
                                {
                                    ddt = TimeSpan.Parse(textBox2.Text);
                                }

                                if (!NowHoliday(dayOfWork[i], _employees2[g].Id))
                                {


                                    listForInputSchedules.Add(new Schedule()
                                    {
                                        EmployeeId = _employees2[g].Id,
                                        StartTime = tstart,
                                        EndTime = ddt,
                                        SumTime = ddt - tstart,
                                        Summary = Convert.ToInt32((ddt - tstart).TotalHours),
                                        Date = dayOfWork[i].Date

                                    });
                                }

                            }

                        }

                    }

                    break;

            } //switch

            foreach (var el in listForInputSchedules)
            {
                _repositoryProvider.GetRepository<Schedule>().Add(el);
            }
            _repositoryProvider.SaveChanges();

            //-------------------------------------

            #region помойка

            ////var wwews = query.Where(x => x.Date.Value.Month == moment)

            ////var ss1sss = query.Where(x => x.EmployeeId == 3).Where(x => x.Date.Value.Month == moment).OrderBy(x=>x.Date).Last();
            ////var sssss = query.Where(x => x.Date.Value.Month == moment).OrderBy(x => x.Date).Last();
            //azaza = new List<EmployeeProjection>();


            //for (int h = 0; h < _employees2.Count(); h++)
            //{
            //    //if(_employees2[h].Schedules==null)

            //    var sssss = query.Where(x => x.EmployeeId == _employees2[h].Id)
            //        .Where(x => x.Date.Value.Month == moment)
            //        .OrderBy(x => x.Date).LastOrDefault();
            //    var ss = _repositoryProvider.GetRepository<Schedule>().GetAll().Where(x => x.Id == sssss.Id);
            //    DateTime ifordef = new DateTime();
            //    //int ssssId;


            //    if (sssss == null)
            //    {
            //        int zalupa = 0;
            //        if (comboBox3.SelectedIndex == 0)
            //        {
            //            zalupa = 4;


            //        }
            //        if (comboBox3.SelectedIndex == 1)
            //        {
            //            zalupa = 3;
            //        }

            //        MessageBox.Show(
            //            "Вероятнее всего для какого-то сотрудника на данной должности нет записей в БД за предыдущий месяц" +
            //            "\n" + "Вероятнее всего это новый сотрудник \n Впишите его первый день работы");
            //        Form2 gg = new Form2();
            //        gg.ShowDialog();

            //        ifordef = gg.dateTimePicker1.Value.Subtract(TimeSpan.FromDays(zalupa));
            //        //sssss.Id = ss.Last().Id + 1;
            //        int a = 0;


            //    }
            //    else
            //    {
            //        ifordef = sssss.Date.Value;
            //    }
            //    //++
            //    var _employeeId = _employees2[h].Id;
            //    var holydays =
            //        _repositoryProvider.GetRepository<Holiday>()
            //            .GetAll()
            //            .Where(x => x.EmployeeId == _employeeId) //==sssss.EmployeeId
            //            .ToList();
            //    //
            //    //DateTime ifordef;
            //    //if (sssss == null)
            //    //{
            //    //     ifordef = null;

            //    //}
            //    //else
            //    //{
            //    //    ifordef = sssss.Date.Value;

            //    TimeSpan time1 = TimeSpan.Parse(textBox1.Text);
            //    TimeSpan time2 = TimeSpan.Parse(textBox2.Text);
            //    TimeSpan time3 = time2 - time1;
            //    //}
            //    //() ? null : sssss.Date.Value;
            //    azaza.Add(new EmployeeProjection()
            //    {
            //        //Id = sssss.Id,
            //        Name = _employees2[h].Name,
            //        LastName = _employees2[h].Name,
            //        MiddleName = _employees2[h].MiddleName,
            //        LastMounthDateTime = ifordef,
            //        PositionId = _employees2[h].PositionId,
            //        Position = _employees2[h].Position.Title,
            //        DatesOfMounth = new List<DateTime>(),
            //        StartTime = TimeSpan.Parse(textBox1.Text),
            //        EndTime = TimeSpan.Parse(textBox2.Text),
            //        SumTime = time3,
            //        //EmployeeId = sssss.EmployeeId,
            //        EmployeeId = _employeeId,

            //        //++
            //        AllHolidays = new List<DateTime[]>()

            //    });

            //    Schedule sc1111 = new Schedule()
            //    {
            //        Date = DateTime.Now,
            //        //Employee = Employee,
            //        EmployeeId = 2,

            //        EndTime = TimeSpan.Zero,
            //        StartTime = TimeSpan.Zero,
            //        SumTime = TimeSpan.Zero

            //    };




            //    int day = 0;

            //    for (int k = 0; k < holydays.Count; k++)
            //    {
            //        azaza.Last().AllHolidays.Add(new DateTime[] { holydays[k].StartDate, holydays[k].EndDate });
            //    }



            //    if (comboBox3.SelectedIndex == 0)
            //    {
            //        day = 4;


            //    }
            //    if (comboBox3.SelectedIndex == 1)
            //    {
            //        day = 3;
            //    }

            //    //azaza.Last().DatesOfMounth.Add(azaza.Last().LastMounthDateTime.AddDays(day));
            //    DateTime NowDay = new DateTime(azaza.Last().LastMounthDateTime.Year, moment + 1, 1);

            //    #region добавление с учетом праздников и выходных(воскресенье)

            //    var feasts = _repositoryProvider.GetRepository<Feast>().GetAll().ToList();

            //    do
            //    {
            //        if (NowDay.Day == 15)
            //        {
            //            int sdsdsdsdsdsd = 2;
            //        }
            //        bool NowVacation = false;
            //        //является ли сегодня отпуском
            //        foreach (var el in azaza.Last().AllHolidays)
            //        {
            //            //el[0] = new DateTime();
            //            if (el[0] <= NowDay && NowDay <= el[1]) //не отрабатывает
            //                                                    // if (el[0].Day <= NowDay.Day && el[0].Month <= NowDay.Month && el[0].Year <= NowDay.Year && NowDay.Day <= el[1].Day && NowDay.Month <= el[1].Month && NowDay.Year <= el[1].Year)
            //            {
            //                NowVacation = true;
            //                break;
            //            }
            //        }

            //        //проврека на отпуск
            //        if (NowVacation)
            //        {
            //            NowDay = NowDay.AddDays(1);
            //            continue;
            //        }


            //        NowVacation = false;
            //        //является ли сегодня праздником
            //        foreach (var el in feasts)
            //        {
            //            if (NowDay.Month == el.Mounth && NowDay.Day == el.DayOfMounth)
            //            {
            //                NowVacation = true;
            //                break;
            //            }
            //        }

            //        //проверка на праздник
            //        if (NowVacation)
            //        {
            //            NowDay = NowDay.AddDays(1);
            //            continue;
            //        }

            //        NowVacation = false;
            //        //был ли вчера отпуск
            //        foreach (var el in azaza.Last().AllHolidays)
            //        {
            //            if (el[1].AddDays(1) == NowDay)
            //            {
            //                NowVacation = true;
            //                break;
            //            }
            //        }

            //        var weekends = _repositoryProvider.GetRepository<Weekend>().GetAll().ToList();

            //        List<DayOfWeek> wwww = new List<DayOfWeek>();

            //        #region iffff

            //        foreach (var el in weekends)
            //        {
            //            if (el.HolidayForPosition == 7)
            //            {
            //                wwww.Add(DayOfWeek.Sunday);
            //            }
            //            if (el.HolidayForPosition == 6)
            //            {
            //                wwww.Add(DayOfWeek.Saturday);
            //            }
            //            if (el.HolidayForPosition == 5)
            //            {
            //                wwww.Add(DayOfWeek.Friday);
            //            }
            //            if (el.HolidayForPosition == 4)
            //            {
            //                wwww.Add(DayOfWeek.Thursday);
            //            }
            //            if (el.HolidayForPosition == 3)
            //            {
            //                wwww.Add(DayOfWeek.Wednesday);
            //            }
            //            if (el.HolidayForPosition == 2)
            //            {
            //                wwww.Add(DayOfWeek.Tuesday);
            //            }
            //            if (el.HolidayForPosition == 1)
            //            {
            //                wwww.Add(DayOfWeek.Monday);
            //            }

            //        }

            //        #endregion iffff

            //        bool segodniaNEvihodnoi = false;
            //        //сегодня выходной
            //        if (wwww.IndexOf(NowDay.DayOfWeek) != -1)
            //        {
            //            NowDay = NowDay.AddDays(1);
            //            continue;
            //        }
            //        //сегодня НЕ выходной
            //        else
            //        {
            //            segodniaNEvihodnoi = true;
            //            //azaza.Last().DatesOfMounth.Add(NowDay);
            //        }

            //        //вчера был отпуск
            //        if (NowVacation)
            //        {
            //            if (segodniaNEvihodnoi)
            //            {
            //                azaza.Last().DatesOfMounth.Add(NowDay);
            //                NowDay = NowDay.AddDays(1);
            //                continue;
            //            }

            //        }

            //        //вчера НЕ был отпуск
            //        else
            //        {
            //            if (azaza.Last().LastMounthDateTime == null)
            //            {
            //                if (segodniaNEvihodnoi)
            //                {
            //                    azaza.Last().DatesOfMounth.Add(NowDay);
            //                    NowDay = NowDay.AddDays(1);
            //                    continue;
            //                }

            //            }
            //            else
            //            {

            //                DateTime FirstWorkInMOnth;
            //                if (azaza.Last().DatesOfMounth.Count == 0)
            //                {
            //                    FirstWorkInMOnth = azaza.Last().LastMounthDateTime.AddDays(day);
            //                }
            //                else
            //                {
            //                    FirstWorkInMOnth = azaza.Last().DatesOfMounth.Last().AddDays(day);
            //                }

            //                //3 - из комбобокса day
            //                if (FirstWorkInMOnth <= NowDay)
            //                {

            //                    if (segodniaNEvihodnoi)
            //                    {
            //                        azaza.Last().DatesOfMounth.Add(NowDay);
            //                        NowDay = NowDay.AddDays(1);
            //                        continue;
            //                    }
            //                }
            //                else
            //                {
            //                    NowDay = NowDay.AddDays(1);
            //                    continue;
            //                }
            //            }
            //        }
            //    } while (NowDay.Month == moment + 1);

            //    #endregion



            //    //daylentai














            //    #region добавление просто по графику без учета воскресенья и гос праздников

            //    //                do
            //    //                {

            //    //                    azaza.Last().DatesOfMounth
            //    //                        .Add(azaza.Last().DatesOfMounth.Last().AddDays(day));
            //    //                    azaza.Last().StartTime = time1;
            //    //                    azaza.Last().EndTime = time2;
            //    //                    azaza.Last().SumTime = time3;

            //    //                } while (azaza.Last().DatesOfMounth.Last().AddDays(day).Month == moment + 1); //while (azaza.Last().DatesOfMounth.Last().AddDays(day)==azaza.Last().LastMount);
            //    //            }

            //    #endregion




            //}

            //for (int i = 0; i < azaza.Count; i++)
            //{

            //    for (int j = 0; j < azaza[i].DatesOfMounth.Count; j++)
            //    {
            //        Schedule sch = new Schedule
            //        {
            //            EmployeeId = azaza[i].EmployeeId,
            //            EndTime = azaza[i].EndTime,
            //            StartTime = azaza[i].StartTime,
            //            //Id = azaza[i].Id,
            //            SumTime = azaza[i].SumTime,
            //            Date = azaza[i].DatesOfMounth[j]
            //        };

            //        //--
            //        _repositoryProvider.GetRepository<Schedule>().Add(sch);
            //        _repositoryProvider.SaveChanges();



            //        //1) получить всех людей из таблицы с отпусками с таким же 
            //        //id как у того, на которого составляем расписание
            //        //1.1 выборка по id человека
            //        //

            //        //2)Получаем лист расписаний
            //        //3)проверяем если дата в листе расписаний содержит дату начала или конца отпуска
            //        //то удаляем расписание на этого человека в текущем месяце

            //        var queryOtpusk =
            //            _repositoryProvider.GetRepository<Holiday>()
            //                .GetAll()
            //                .Where(x => x.EmployeeId == sch.EmployeeId);
            //        var schedulya = _repositoryProvider.GetRepository<Schedule>().GetAll().ToList();
            //        //if(schedulya)
            //    }
            //}



            ////var wwwww = query.Where(x => x.Date.Value.Month == moment).Where(x => x.Date.Value == 


            ////Where(x => x.Date.Value.Month == moment).ToList()



            #endregion

            //-------------------------------------
        }

        private bool NowHoliday(DateTime dayH, int idEMP)
        {
            var holydays =
                _repositoryProvider.GetRepository<Holiday>()
                    .GetAll()
                    .Where(x => x.EmployeeId == idEMP)
                    .ToList();


            List<DateTime[]> AllHolidays = new List<DateTime[]>();
            for (int k = 0; k < holydays.Count; k++)
            {
                AllHolidays.Add(new DateTime[] {holydays[k].StartDate, holydays[k].EndDate});
            }

            //является ли сегодня отпуском
            bool NowVacation = false;
            foreach (var ah in AllHolidays)
            {
                //el[0] = new DateTime();
                if (ah[0] <= dayH && dayH <= ah[1])
                {
                    NowVacation = true;
                    break;
                }
            }

            return NowVacation;
        }

        private List<DayOfWeek> getDayOff(List<Weekend> weekends)
        {
            List<DayOfWeek> wwww = new List<DayOfWeek>();

            foreach (var el in weekends)
            {
                if (el.HolidayForPosition == 7)
                {
                    wwww.Add(DayOfWeek.Sunday);
                }
                if (el.HolidayForPosition == 6)
                {
                    wwww.Add(DayOfWeek.Saturday);
                }
                if (el.HolidayForPosition == 5)
                {
                    wwww.Add(DayOfWeek.Friday);
                }
                if (el.HolidayForPosition == 4)
                {
                    wwww.Add(DayOfWeek.Thursday);
                }
                if (el.HolidayForPosition == 3)
                {
                    wwww.Add(DayOfWeek.Wednesday);
                }
                if (el.HolidayForPosition == 2)
                {
                    wwww.Add(DayOfWeek.Tuesday);
                }
                if (el.HolidayForPosition == 1)
                {
                    wwww.Add(DayOfWeek.Monday);
                }
            }

            return wwww;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Visible = false;
            groupBox2.Visible = true;
            button1.Visible = false;
            dataGridView2.Visible = true;
            button10.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Visible = false;
            groupBox2.Visible = false;
            button1.Visible = true;
            button9.Visible = true;
            dataGridView2.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            schdg =
                _repositoryProvider.GetRepository<Schedule>()
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
                    }).Where(a => EntityFunctions.TruncateTime(a.Date).ToString().Contains(textBox3.Text)).ToList();
            dataGridView2.DataSource = schdg;
            dataGridView2.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        private void ExcelImport(DataGridView dgv, int columns, int shiftheader, int shiftcolumn)
        {
            saveFileDialog1.InitialDirectory = "C:";
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

                if (columns == 2)
                {
                    excelApp.Columns[1].ColumnWidth = 20;
                    excelApp.Columns[2].ColumnWidth = 120;
                }
                else
                {
                    excelApp.Columns[1].ColumnWidth = 50;
                    excelApp.Columns[2].ColumnWidth = 25;
                    excelApp.Columns[3].ColumnWidth = 35;

                }

                for (int i = 1; i < dgv.Columns.Count - shiftheader; i++) // -ид(+1)
                {
                    excelApp.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count - shiftcolumn; j++) //-ид
                    {
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

        private void button11_Click(object sender, EventArgs e)
        {
            //ExcelImport(dataGridView2, 3, 4, 5);
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView2[7, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Schedule = _repositoryProvider.GetRepository<Schedule>().Find(id);

                _editScheduleForm = new EditSchedule();
                _editScheduleForm.Owner = this;
                _editScheduleForm.ShowDialog();
                foreach (var editsch in Application.OpenForms)
                {
                    if (!(editsch is EditSchedule))
                    {
                        schdg =
                            _repositoryProvider.GetRepository<Schedule>()
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
                                }).ToList();
                        dataGridView2.DataSource = schdg;
                        dataGridView2.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                        dataGridView2.Columns[3].Visible = false;
                        dataGridView2.Columns[4].Visible = false;
                        dataGridView2.Columns[5].Visible = false;
                        dataGridView2.Columns[6].Visible = false; //emp
                        dataGridView2.Columns[7].Visible = false; //id
                        dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    }
                }
            }
            else
            {
                MessageBox.Show("Выделите строку таблицы !");
            }
        }


        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            button4.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            button12.Visible = false;
            label1.Visible = false;
        }

        #region в помойку

        //private void button13_Click(object sender, EventArgs e)
        //{

        //    int Year = Convert.ToInt32(textBox4.Text);
        //    int mounth = 5;


        //    List<DateTime> masDayOfmonth = new List<DateTime>() { new DateTime(Year, mounth, 1 )};

        //    for (int i = 0; i < System.DateTime.DaysInMonth(Year,mounth)-1; i++)
        //    {
        //        masDayOfmonth.Add(masDayOfmonth.Last().AddDays(1));
        //    }



        //    var peoples = _repositoryProvider.GetRepository<.Employee>().GetAll().ToList();

        //    List<ProjForExcelDg> Raspisanie = new List<ProjForExcelDg>();

        //    for (int i = 0; i < peoples.Count; i++)
        //    {
        //        Raspisanie.Add(new ProjForExcelDg()
        //        {
        //            FIO = peoples[i].Name + peoples[i].LastName + peoples[i].MiddleName,
        //            Dolj = peoples[i].Position.Title
        //        });
        //        var _employeeId = peoples[i].Id;
        //        var workdays = _repositoryProvider.GetRepository<.Schedule>().GetAll().
        //            Where(x => x.EmployeeId == _employeeId)
        //            .Where(x => x.Date.Value.Year == Year && x.Date.Value.Month == mounth)
        //            .ToList();



        //        for (int j = 0; j < workdays.Count; j++)
        //        {
        //            string aa = workdays[i].StartTime.ToString().Split(':')[0] + ":" + workdays[i].StartTime.ToString().Split(':')[1];
        //            string bb = workdays[i].EndTime.ToString().Split(':')[0] + ":" +
        //                        workdays[i].EndTime.ToString().Split(':')[1];
        //            Raspisanie.Last().ScheduleTable.Add(workdays[j].Date.Value, aa + " - " + bb);
        //        }

        //    }

        //    Form3 a = new Form3();
        //    dataGridView3.DataSource = Raspisanie;

        //    //создать новые хеадары

        //    for (int i = 0; i < masDayOfmonth.Count; i++)
        //    {
        //        DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
        //        newColumn.HeaderText = masDayOfmonth[i].ToString().Split(' ')[0];
        //        newColumn.Name = "ID" + Convert.ToString(i);

        //        dataGridView3.Columns.Add(newColumn);

        //        int indexColumn = dataGridView3.Columns.Count - 3;
        //        //int indexColumn = i;

        //        for (int indexRow = 0; indexRow < dataGridView3.Rows.Count; indexRow++)
        //        {
        //            if (indexRow == 2 && masDayOfmonth[i].Day ==4)
        //            {
        //                int ssssll = 2;
        //            }
        //            bool qwer = Raspisanie[indexRow].ScheduleTable.ContainsKey(masDayOfmonth[i]);

        //            if (Raspisanie[indexRow].ScheduleTable.ContainsKey(masDayOfmonth[i]))
        //            {
        //                dataGridView3.Rows[indexRow].Cells[indexColumn].Value =  Raspisanie[indexRow].ScheduleTable[masDayOfmonth[i]];
        //            }

        //        }
        //    }




        //    //a.ShowDialog();
        //}

        #endregion

        private void button14_Click(object sender, EventArgs e)
        {
            Form3 a = new Form3();
            a.Owner = this;
            a.ShowDialog();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != null && textBox4.Text != null)
            {
                schdg =
                    _repositoryProvider.GetRepository<Schedule>()
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
                        })
                        .Where(a => EntityFunctions.TruncateTime(a.Date).ToString().Contains(textBox3.Text))
                        .Where(a => a.FIO.Contains(textBox4.Text))
                        .ToList();
                dataGridView2.DataSource = schdg;
                dataGridView2.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                dataGridView2.Columns[7].Visible = false;
                dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            button13.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //ExcelImport(dataGridView2, 3, 4, 5);
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView2[7, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                var sch1 = _repositoryProvider.GetRepository<Schedule>().Find(id);
                _repositoryProvider.GetRepository<Schedule>().Remove(sch1);
                _repositoryProvider.SaveChanges();

                #region ref

                schdg =
                    _repositoryProvider.GetRepository<Schedule>()
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
                        })
                        .Where(x => x.FIO.Contains(textBox4.Text))
                        .Where(x => x.Date.ToString().Contains(textBox3.Text))
                        .ToList();
                dataGridView2.DataSource = schdg;
                dataGridView2.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false; //emp
                dataGridView2.Columns[7].Visible = false; //id
                dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                #endregion


            }
            else
            {
                MessageBox.Show("Выделите строку таблицы");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            _createHandleForm = new CreateHand();
            _createHandleForm.Owner = this;
            _createHandleForm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                comboBox3.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                comboBox3.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
            }
            else
            {
                comboBox3.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {

            int mm2 = 0;
            int mm1 = 0;
            if (comboBox2.SelectedIndex == 0)
            {
                mm1 = December.Month;
                mm2 = mm1 + 1;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                mm1 = January.Month;
                mm2 = mm1 + 1;
            }
            if (comboBox2.SelectedIndex == 2)
            {
                mm1 = February.Month;
                mm2 = mm1 + 1;
            }
            if (comboBox2.SelectedIndex == 3)
            {
                mm1 = March.Month;
                mm2 = mm1 + 1;
            }
            if (comboBox2.SelectedIndex == 4)
            {
                mm1 = April.Month;
                mm2 = mm1 + 1;
            }
            if (comboBox2.SelectedIndex == 5)
            {
                mm1 = May.Month;
                mm2 = mm1 + 1;
            }
            if (comboBox2.SelectedIndex == 6)
            {
                mm1 = June.Month;
                mm2 = mm1 + 1;
            }
            if (comboBox2.SelectedIndex == 7)
            {
                mm1 = July.Month;
                mm2 = mm1 + 1;
            }

            if (comboBox2.SelectedIndex == 8)
            {
                mm1 = August.Month;
                mm2 = mm1 + 1;
            }
            if (comboBox2.SelectedIndex == 9)
            {
                mm1 = September.Month;
                mm2 = mm1 + 1;
            }
            if (comboBox2.SelectedIndex == 10)
            {
                mm1 = October.Month;
                mm2 = mm1 + 1;
            }
            if (comboBox2.SelectedIndex == 11)
            {
                mm1 = November.Month;
                mm2 = mm1 + 1;
            }




            int ProfId = 0;
            int ProfId2 = 0;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    ProfId = 1;
                    ProfId2 = 1;
                    break;

                case 1:
                    ProfId = 3;
                    ProfId2 = 4;
                    break;

                case 2:
                    ProfId = 2;
                    ProfId2 = 2;
                    break;
                case 3:
                    ProfId = 5;
                    ProfId2 = 5;
                    break;
                case 4:
                    ProfId = 6;
                    ProfId2 = 6;
                    break;

            }
            int year = Convert.ToInt32(textBox5.Text);
            List<Schedule> delq = new List<Schedule>();
            delq = _repositoryProvider.GetRepository<Schedule>().GetAll()
                .Where(x => x.Employee.PositionId == ProfId || x.Employee.PositionId == ProfId2).
                Where(x => x.Date.Value.Year == year && x.Date.Value.Month == mm2).ToList();
            if (mm2 != 0)
            {
                if (ProfId2 != 0 || ProfId != 0)
                {
                    for (int i = 0; i < delq.Count; i++)
                    {
                        _repositoryProvider.GetRepository<Schedule>().Remove(delq[i]);
                    }
                    _repositoryProvider.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Не выбрана должность либо месяц!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Не выбрана должность либо месяц!");
                return;
            }
            int a = 2;

        }

        public Employee editEmpl { get; set; }
        private void button18_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                editEmpl = _repositoryProvider.GetRepository<Employee>().Find(SelectedEmployee.Id);
                _editEmployeeForm = new EditEmployee();
                _editEmployeeForm.Owner = this;
                _editEmployeeForm.ShowDialog();
            }
            foreach(var createEmployeeForm in Application.OpenForms)
            {
                if (!(createEmployeeForm is EditEmployee))
                {
                    listBox1.DataSource = _repositoryProvider.GetRepository<Employee>().GetAll().ToList();
                }

            }
        }
    }
}
