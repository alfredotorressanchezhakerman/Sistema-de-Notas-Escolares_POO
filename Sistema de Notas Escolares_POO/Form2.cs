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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevoAlumn_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAlumno Alumno = new frmAlumno();
            Alumno.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCalificaciones_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCalificaciones Calificaciones = new frmCalificaciones();
            Calificaciones.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaterias_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMaterias Materia = new frmMaterias();
            Materia.Show();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmReporte Reporte = new frmReporte();
            Reporte.Show();
        }
    }
}
