using RRHHOrtiz.BD;
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
    public partial class CandidatoEditForm : Form
    {
        private Candidato candidato;
        private RRHHOrtizEntities db;
        private List<Competencia> competencias;
        private List<Capacitacione> capacitaciones;
        private List<Puesto> puestos;
        private List<Idioma> idiomas;
        private int currentId = 0;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        public CandidatoEditForm(int id = 0)
        {

            InitializeComponent();
            db = new RRHHOrtizEntities();

            LoadRelatedData();

            if (id != 0)
            {
                currentId = id;
                LoadCandidate();
                TituloLabel.Text = "Editar candidato";
            }
            else
                TituloLabel.Text = "Nuevo candidato";

        }

        private void LoadRelatedData()
        {
            competencias = db.Competencias.ToList();
            competencias.ForEach(item => checkedListBox1.Items.Add(item.Descripcion));

            capacitaciones = db.Capacitaciones.ToList();
            capacitaciones.ForEach(item => checkedListBox2.Items.Add(item.Descripcion));

            idiomas = db.Idiomas.ToList();
            idiomas.ForEach(item => checkedListBox3.Items.Add(item.Nombre));

            puestos = db.Puestos.ToList();
            puestos.ForEach(x => comboBox1.Items.Add(x.Nombre));
        }

        private void LoadCandidate()
        {
            var current = db.Candidatos.Find(currentId);

            txtCedula.Text = current.Cedula;
            txtDept.Text = current.Departamento;
            txtRecomendado.Text = current.Recomendado;
            txtNombre.Text = current.Nombre;
            comboBox1.SelectedItem = current.Puesto.Nombre;
            txtSalario.Text = current.Salario.ToString();
            txtTel.Text = current.Telefono;
            txtDir.Text = current.Direccion;

            foreach (var item in current.CompetenciasCandidatos)
            {
                int index = competencias.FindIndex(x => x.CompetenciaId == item.CompetenciaId);
                checkedListBox1.SetItemChecked(index, true);
            }

            foreach (var item in current.CapacitacionesCandidatos)
            {
                int index = capacitaciones.FindIndex(x => x.CapacitacionId == item.CapacitacionId);
                checkedListBox2.SetItemChecked(index, true);
            }

            foreach (var item in current.IdiomasCandidatos)
            {
                int index = idiomas.FindIndex(x => x.IdiomaId == item.IdiomaId);
                checkedListBox3.SetItemChecked(index, true);
            }

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

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            var loading = new LoadingForm();
            BeginInvoke((Action)(() => loading.ShowDialog()));

            candidato = new Candidato()
            {
                CadidatoId = currentId,
                Cedula = txtCedula.Text,
                Departamento = txtDept.Text,
                Estado = "Activo",
                Recomendado = txtRecomendado.Text,
                Nombre = txtNombre.Text,
                PuestoId1 = puestos.FirstOrDefault(x => x.Nombre == comboBox1.SelectedItem.ToString()).PuestoId,
                Salario = decimal.Parse(txtSalario.Text),
                Telefono = txtTel.Text,
                Direccion = txtDir.Text
            };

            db.Candidatos.AddOrUpdate(candidato);
            try
            {
                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                loading.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkedListBox1.CheckedItems.Count > 0)
            {
                foreach (var item in checkedListBox1.CheckedItems)
                {
                    var cmpTmp = competencias.FirstOrDefault(x => x.Descripcion == item.ToString());
                    var tmp = new CompetenciasCandidato()
                    {
                        CandidatoId = candidato.CadidatoId,
                        CompetenciaId = cmpTmp.CompetenciaId
                    };

                    db.CompetenciasCandidatos.Add(tmp);
                }
            }

            if (checkedListBox2.CheckedItems.Count > 0)
            {
                foreach (var item in checkedListBox2.CheckedItems)
                {
                    var cmpTmp = capacitaciones.FirstOrDefault(x => x.Descripcion == item.ToString());
                    var tmp = new CapacitacionesCandidato()
                    {
                        CandidatoId = candidato.CadidatoId,
                        CapacitacionId = cmpTmp.CapacitacionId
                    };
                    db.CapacitacionesCandidatos.Add(tmp);
                }
            }

            if (checkedListBox3.CheckedItems.Count > 0)
            {
                foreach (var item in checkedListBox3.CheckedItems)
                {
                    var cmpTmp = idiomas.FirstOrDefault(x => x.Nombre == item.ToString());
                    var tmp = new IdiomasCandidato()
                    {
                        CandidatoId = candidato.CadidatoId,
                        IdiomaId = cmpTmp.IdiomaId
                    };
                    db.IdiomasCandidatos.Add(tmp);
                }
            }

            await db.SaveChangesAsync();
            loading.Close();

            this.Close();


        }

        private bool IsValid()
        {

            if (txtCedula.Text == "")
            {
                MessageBox.Show("La cedula es requerida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtCedula.Text.Length < 13)
            {
                MessageBox.Show("La cedula es invalida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtNombre.Text == "")
            {
                MessageBox.Show("El nombre es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtTel.Text == "")
            {
                MessageBox.Show("El telefono es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtDir.Text == "")
            {
                MessageBox.Show("La direccion es requerida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("El puesto al que aplica es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            decimal r;
            if (!decimal.TryParse(txtSalario.Text, out r) && txtSalario.Text.Length > 0)
            {
                MessageBox.Show("El salario aspirado es invalido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtDept.Text == "")
            {
                MessageBox.Show("El departamento es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (db.Candidatos.Any(x => x.Cedula == txtCedula.Text) && currentId == 0)
            {
                MessageBox.Show("Existe un candidato con esta cedula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
