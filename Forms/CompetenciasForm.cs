using RRHHOrtiz.BD;
using RRHHOrtiz.StaticForms;
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
    public partial class CompetenciasForm : Form
    {
        private RRHHOrtizEntities db;
        private int idSelected = 0;

        public CompetenciasForm()
        {
            InitializeComponent();
        }

        private void IdiomasForm_Load(object sender, EventArgs e)
        {
            db = new RRHHOrtizEntities();
            LoadEntities();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadEntities()
        {
            dataGridView1.DataSource = db.Competencias.ToList();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Guardar();
        }


        private bool IsValid()
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("El nombre es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            return true;
        }

        private void Clean()
        {
            txtNombre.Text = "";
        }



        private void button3_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            idSelected = (int)dataGridView1.Rows[rowindex].Cells[0].Value;
            txtNombre.Text = (string)dataGridView1.Rows[rowindex].Cells[1].Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void button2_Click(object sender, EventArgs e)
        {           
            Buscar();
        }

        private void Buscar()
        {
            if (filtro.SelectedItem == null)
                return;

            string busq = filtro.SelectedItem.ToString();
            switch (busq)
            {
                case "Nombre":
                    dataGridView1.DataSource = db.Competencias.Where(x => x.Descripcion.Contains(busqueda.Text)).ToList();
                    break;
            }
        }

        private void Eliminar()
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            var id = dataGridView1.Rows[rowindex].Cells[0].Value;

            var entity = db.Competencias.Find(id);
            DialogResult dr = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                if (entity != null)
                {
                    entity.Estado = false;
                    db.Competencias.AddOrUpdate(entity);
                    db.SaveChanges();
                    MessageBox.Show("Registro eliminado!");
                    LoadEntities();
                }
                else
                    MessageBox.Show("No existe registro");

            }
        }

        private async Task Guardar()
        {
            if (IsValid())
            {
                var loading = new LoadingForm();
                BeginInvoke((Action)(() => loading.ShowDialog()));

                var entity = new Competencia()
                {
                    CompetenciaId = idSelected,
                    Descripcion = txtNombre.Text,
                    Estado = comboBox1.SelectedItem.Equals("Activo")
                };

                db.Competencias.AddOrUpdate(entity);
                await db.SaveChangesAsync();
                Clean();
                LoadEntities();
                idSelected = 0;

                loading.Close();
            }
        }

    }
}
