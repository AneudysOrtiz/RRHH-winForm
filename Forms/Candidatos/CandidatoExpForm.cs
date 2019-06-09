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
    public partial class CandidatoExpForm : Form
    {
        private ExperienciasLaborale _exp;
        public CandidatoExpForm(ref ExperienciasLaborale exp)
        {
            InitializeComponent();
            _exp = exp;
        }

        private void CandidatoExpForm_Load(object sender, EventArgs e)
        {
            TituloLabel.Text = "Experiencia laboral";
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
            _exp.Empresa = txtEmpresa.Text;
            _exp.PuestoOcupado = txtPuesto.Text;
            _exp.Salario = txtSalario.Value;

            Close();
        }
    }
}
