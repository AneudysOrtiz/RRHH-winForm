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
    public partial class LoginForm : Form
    {
        private RRHHOrtizEntities db;
        private Usuario user;

        public LoginForm()
        {
            InitializeComponent();           
            db = new RRHHOrtizEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                MessageBox.Show("Todos los campos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            pictureBox2.Visible = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private bool IsValid()
        {
            if (username.Text.Length == 0 || password.Text.Length == 0)
                return false;

            return true;
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            string userName = username.Text;
            string pass = PasswordHelper.HashPassword(password.Text);
            user = db.Usuarios.FirstOrDefault(x => (x.Username == userName || x.Email == userName) && x.Password == pass && x.Estado);
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox2.Visible = false;
            if (user != null)
            {
                EnvVariable.CurrentUser = string.Format("{0} {1}", user.Nombre, user.Apellido);
                //WindowState = FormWindowState.Minimized;
                Visible = false;
                var form = new MainForm();
                form.ShowDialog();
                //Close();
            }
            else
            {
                MessageBox.Show("El usuario o clave son invalidos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
           
        }

        private void password_OnValueChanged(object sender, EventArgs e)
        {
            password.isPassword = true;
        }
    }
}
