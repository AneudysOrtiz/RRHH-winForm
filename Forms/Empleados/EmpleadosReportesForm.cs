using RRHHOrtiz.BD;
using RRHHOrtiz.ReportForms;
using RRHHOrtiz.StaticForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRHHOrtiz.Forms.Candidatos
{
    public partial class EmpleadosReportesForm : Form
    {
        private RRHHOrtizEntities db;
        private string reportType = "";

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        public EmpleadosReportesForm()
        {

            InitializeComponent();
            db = new RRHHOrtizEntities();

            TituloLabel.Text = "Reportes de empleados";
            //panelSalario.Visible = false;
            //panelSalario.Hide();

        }        


        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var reportForm = new Form();
            var query = db.Empleados;
            var list = new List<Empleado>();
            switch (reportType)
            {
                case "IngresoFecha":
                    query.Where(x => x.FechaIngreso.Date >= dateTimePicker1.Value.Date && x.FechaIngreso.Date <= dateTimePicker2.Value.Date);
                    list = query.ToList();
                    reportForm = new IngresoEmpleadoReportForm(list);
                    reportForm.Show();
                    break;

                case "RangoSalarial":
                    var desde = numericUpDown1.Value;
                    var hasta = numericUpDown2.Value;
                    query.Where(x => x.Salario >= desde && x.Salario <= hasta);
                    list = query.ToList();
                    reportForm = new SalarioEmpleadosForm(list, desde, hasta);
                    reportForm.Show();
                    break;
            }
            
        }
      
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                reportType = "IngresoFecha";
                //panelRango.Location = panelSalario.Location;
                panelSalario.Visible = false;
                panelRango.Visible = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                reportType = "RangoSalarial";
                panelSalario.Location = panelRango.Location;
                panelSalario.Size = panelRango.Size;
                panelRango.Visible = false;
                panelSalario.Visible = true;
            }
        }

        private void EmpleadosReportesForm_Load(object sender, EventArgs e)
        {
            panelSalario.Visible = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Minimum = numericUpDown1.Value;
        }
    }
}
