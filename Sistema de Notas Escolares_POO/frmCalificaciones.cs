using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Notas_Escolares_POO
{
    public partial class frmCalificaciones : Form
    {
        public frmCalificaciones()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void frmCalificaciones_Load(object sender, EventArgs e)
        {

        }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 Regresar = new Form2();
            Regresar.Show();
        }
    }
}
