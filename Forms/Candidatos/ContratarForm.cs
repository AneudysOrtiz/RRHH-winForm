using RRHHOrtiz.BD;
using RRHHOrtiz.ReportForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRHHOrtiz.Forms.Candidatos
{
    public partial class ContratarForm : Form
    {
        private List<Puesto> puestos;
        private Candidato candidato;
        public ContratarForm(Candidato candidato)
        {
            InitializeComponent();
            this.candidato = candidato;
            txtDept.Text = candidato.Departamento;
            txtSalario.Text = candidato.Salario.ToString();
            txtCedula.Text = candidato.Cedula;
            txtTel.Text = candidato.Telefono;
            txtDir.Text = candidato.Direccion;
            txtNombre.Text = candidato.Nombre;
            txtRecomendado.Text = candidato.Recomendado;
            puestos = new RRHHOrtizEntities().Puestos.ToList();
            puestos.ForEach(x => comboBox1.Items.Add(x.Nombre));
            comboBox1.SelectedItem = candidato.Puesto.Nombre;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateCandidato();
            var form = new ContratoReportForm(candidato);
            form.Show();
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UpdateCandidato()
        {
            this.candidato.Puesto.Nombre = comboBox1.SelectedItem.ToString();
            candidato.Departamento = txtDept.Text;
            candidato.Salario = decimal.Parse(txtSalario.Text);
            candidato.PuestoId1 = puestos.FirstOrDefault(x => x.Nombre == comboBox1.SelectedItem.ToString()).PuestoId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Esta seguro que desea contratar este candidato?", "Contratar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {

                using (var db = new RRHHOrtizEntities())
                {
                    UpdateCandidato();

                    var find = db.Candidatos.Find(candidato.CadidatoId);                    
                    find = candidato;
                    find.Estado = "Contratado";
                    db.Candidatos.AddOrUpdate(find);
                    var emp = new Empleado()
                    {
                        CandidatoId = candidato.CadidatoId,
                        Puesto = candidato.Puesto.Nombre,
                        Cedula = candidato.Cedula,
                        Departamento = candidato.Departamento,
                        FechaIngreso = DateTime.Now,
                        Estado = true,
                        Nombre = candidato.Nombre,
                        Salario = candidato.Salario.Value,
                        Telefono = candidato.Telefono,
                        Direccion = candidato.Direccion
                    };
                    db.Empleados.Add(emp);
                    var puesto = db.Puestos.Find(candidato.PuestoId1);
                    puesto.Cupo -= 1;
                    db.SaveChanges();
                }

                MessageBox.Show("El candidato fue contratado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }

        }
        
    }
}
