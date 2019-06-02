using RRHHOrtiz.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRHHOrtiz
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }
                

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void AbrirFormInPanel(object Formhijo, string titulo = "Inicio")
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            TituloLabel.Text = titulo;
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        }
        
        

        private void iconminimizar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconrestaurar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void btnprod_Click(object sender, EventArgs e)
        {
            var form = new IdiomasForm();
            AbrirFormInPanel(form, "Idiomas");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new CandidatosForm();
            AbrirFormInPanel(form, "Candidatos");
        }

        private void BarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GoToHome();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form = new PuestosForm();
            AbrirFormInPanel(form, "Puestos");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GoToHome();
        }
        private void GoToHome()
        {
            var form = new DashboardForm();
            AbrirFormInPanel(form, "Inicio");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new EmpleadosForm();
            AbrirFormInPanel(form, "Empleados");
        }
    }
}
