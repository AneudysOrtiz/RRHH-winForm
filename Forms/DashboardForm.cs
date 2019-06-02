using RRHHOrtiz.BD;
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
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using(var db = new RRHHOrtizEntities())
            {
                lblCan.Text = db.Candidatos.Where(x => x.Estado == "Activo").Count().ToString();
                lblEmp.Text = db.Empleados.Where(x => x.Estado).Count().ToString();
                lblPuestos.Text = db.Puestos.Where(x => x.Estado).Count().ToString();
            }
        }
    }
}
