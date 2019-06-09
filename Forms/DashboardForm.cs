using RRHHOrtiz.BD;
using RRHHOrtiz.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRHHOrtiz.Forms
{
    public partial class DashboardForm : Form
    {
        private string candidatos, empleados, puestos;
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            using (var db = new RRHHOrtizEntities())
            {
                candidatos = db.Candidatos.Where(x => x.Estado == "Activo").Count().ToString();
                empleados = db.Empleados.Where(x => x.Estado).Count().ToString();
                puestos = db.Puestos.Where(x => x.Estado).Count().ToString();
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblNombre.Text = string.Format("Bienvenido {0}", EnvVariable.CurrentUser).ToUpper();
            lblCan.Text = candidatos;
            lblEmp.Text = empleados;
            lblPuestos.Text = puestos;
        }
    }
}
