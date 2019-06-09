using RRHHOrtiz.BD;
using RRHHOrtiz.Forms.Candidatos;
using RRHHOrtiz.ViewModels;
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

namespace RRHHOrtiz.Forms
{
    public partial class EmpleadosForm : Form
    {
        private RRHHOrtizEntities db;

        public EmpleadosForm()
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
            var list = db.Empleados.Where(x=>x.Estado).ToList();
            dataGridView1.DataSource = list;
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
            if (filtro.SelectedItem == null)
                return;
        
            var list = new List<Empleado>();
            string param = busqueda.Text;
            string criterio = filtro.SelectedItem.ToString();
            switch (criterio)
            {
                case "Cedula":
                    list = db.Empleados.Where(x => x.Cedula.Replace("-", "").Contains(param.Replace("-", "")) && x.Estado).ToList();
                    dataGridView1.DataSource = list;
                    break;

                case "Nombre":
                    list = db.Empleados.Where(x => x.Nombre.ToLower().Contains(param.ToLower()) && x.Estado).ToList();
                    dataGridView1.DataSource = list;
                    break;

                case "Puesto":
                    list = db.Empleados.Where(x => x.Puesto.ToLower().Contains(param.ToLower()) && x.Estado).ToList();
                    dataGridView1.DataSource = list;
                    break;

                case "Activos":
                    list = db.Empleados.Where(x => x.Estado).ToList();
                    dataGridView1.DataSource = list;
                    break;

                case "Despedidos":
                    list = db.Empleados.Where(x => !x.Estado).ToList();
                    dataGridView1.DataSource = list;
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
            if (dataGridView1.CurrentCell == null)
                return;

            int rowindex = dataGridView1.CurrentCell.RowIndex;
            var id = dataGridView1.Rows[rowindex].Cells[0].Value;

            var entity = db.Empleados.Find(id);
            DialogResult dr = MessageBox.Show("Desea despedir este empleado?", "Despedir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                if (entity != null)
                {
                    entity.Estado = false;
                    db.Empleados.AddOrUpdate(entity);
                    db.SaveChanges();
                    MessageBox.Show("Empleado despedido!");
                    LoadEntities();
                }
                else
                    MessageBox.Show("No existe registro");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form = new EmpleadosReportesForm();
            form.ShowDialog();

        }

        private void filtro_SelectedValueChanged(object sender, EventArgs e)
        {
            string criterio = filtro.SelectedItem.ToString();
            switch (criterio)
            {
                case "Activos":
                    busqueda.Hide();
                    break;

                case "Despedidos":
                    busqueda.Hide();
                    break;
                    
                default:
                    busqueda.Show();
                    break;
            }
        }
    }
}
