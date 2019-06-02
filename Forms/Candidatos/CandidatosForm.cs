using RRHHOrtiz.BD;
using RRHHOrtiz.Forms.Candidatos;
using RRHHOrtiz.ViewModels;
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
    public partial class CandidatosForm : Form
    {
        private RRHHOrtizEntities db;

        public CandidatosForm()
        {
            InitializeComponent();
        }

        private void CandidatosForm_Load(object sender, EventArgs e)
        {
            db = new RRHHOrtizEntities();
            LoadEntities();
        }

        private void LoadEntities()
        {
            var list = db.Candidatos.Where(x=>x.Estado == "Activo").ToList();
            Transform(list);
        }

        private void Transform(List<Candidato> list)
        {
            var data = new List<CandidatoViewModel>();
            foreach (var item in list)
            {
                var candidato = new CandidatoViewModel()
                {
                    CandidatoId = item.CadidatoId,
                    Cedula = item.Cedula,
                    Departamento = item.Departamento,
                    Nombre = item.Nombre,
                    Puesto = item.Puesto.Nombre,
                    Recomendado = item.Recomendado,
                    Salario = string.Format("{0:C}", item.Salario),
                    Telefono = item.Telefono,
                    Direccion = item.Direccion
                };

                candidato.Competencias = string.Join(", ", item.CompetenciasCandidatos.Select(x => x.Competencia.Descripcion));
                candidato.Capacitaciones = string.Join(", ", item.CapacitacionesCandidatos.Select(x => x.Capacitacione.Descripcion));
                candidato.Idiomas = string.Join(", ", item.IdiomasCandidatos.Select(x => x.Idioma.Nombre));
                candidato.Experiencia = string.Join(", ", item.ExperienciasLaborales.Select(x => x.PuestoOcupado));

                data.Add(candidato);
            }

            dataGridView1.DataSource = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new CandidatoEditForm();
            form.ShowDialog();

            LoadEntities();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            int idSelected = (int)dataGridView1.Rows[rowindex].Cells[0].Value;
            var form = new CandidatoEditForm(idSelected);
            form.ShowDialog();            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var list = new List<Candidato>();
            string param = busqueda.Text;
            string criterio = filtro.SelectedItem.ToString();
            switch (criterio)
            {
                case "Cedula":
                    list = db.Candidatos.Where(x => x.Cedula.Replace("-", "").Contains(param) && x.Estado == "Activo").ToList();
                    Transform(list);
                    break;

                case "Puesto":
                    list = db.Candidatos.Where(x => x.Puesto.Nombre.Contains(param) && x.Estado == "Activo").ToList();
                    Transform(list);
                    break;

                case "Capacitacion":
                    var capacitaciones = db.Capacitaciones.Where(x => x.Descripcion.ToLower().Contains(param.ToLower())).Select(x=>x.CapacitacionId);
                    var candidatList = db.CapacitacionesCandidatos.Where(x => capacitaciones.Contains(x.CapacitacionId)).Select(x => x.CandidatoId);
                    list = db.Candidatos.Where(x => candidatList.Contains(x.CadidatoId) && x.Estado == "Activo").ToList();
                    Transform(list);
                    break;

                case "Competencia":
                    var competencias = db.Competencias.Where(x => x.Descripcion.ToLower().Contains(param.ToLower())).Select(x => x.CompetenciaId);
                    var candidatList2 = db.CompetenciasCandidatos.Where(x => competencias.Contains(x.CompetenciaId)).Select(x => x.CandidatoId);
                    list = db.Candidatos.Where(x => candidatList2.Contains(x.CadidatoId) && x.Estado == "Activo").ToList();
                    Transform(list);
                    break;
                default:
                    break;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Eliminar()
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            var id = dataGridView1.Rows[rowindex].Cells[0].Value;

            var entity = db.Candidatos.Find(id);
            DialogResult dr = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                if (entity != null)
                {
                    db.Candidatos.Remove(entity);
                    db.SaveChanges();
                    MessageBox.Show("Registro eliminado!");
                    LoadEntities();
                }
                else
                    MessageBox.Show("No existe registro");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int rowindex = dataGridView1.CurrentCell.RowIndex;
            int idSelected = (int)dataGridView1.Rows[rowindex].Cells[0].Value;
            var c = db.Candidatos.Find(idSelected);
            var form = new ContratarForm(c);
            form.ShowDialog();

            LoadEntities();
        }
    }
}
