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

namespace RRHHOrtiz.Forms.Candidatos
{
    public partial class CapacitacionForm : Form
    {
        private Capacitacione _exp;
        public CapacitacionForm(ref Capacitacione exp)
        {
            InitializeComponent();
            _exp = exp;
        }

        private void CandidatoExpForm_Load(object sender, EventArgs e)
        {
            TituloLabel.Text = "Capacitaciones";
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            _exp = null;
            Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            iconcerrar_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _exp.FechaDesde = dateTimePicker1.Value;
            _exp.FechaHasta = dateTimePicker2.Value;
            _exp.Descripcion = txtEmpresa.Text;
            _exp.Nivel = txtPuesto.Text;
            _exp.Institucion = textBox1.Text;

            Close();
        }
    }
}
