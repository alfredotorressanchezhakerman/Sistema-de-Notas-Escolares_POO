using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Notas_Escolares_POO
{
    public partial class EliminarAlumno: Form
    {
        private const string RutaArchivo = "Alumnos.txt";
        public EliminarAlumno()
        {
            InitializeComponent();
            CargarAlumnos();
        }

        private void EliminarAlumno_Load(object sender, EventArgs e)
        {
            CargarAlumnos();
        }

        private void CargarAlumnos()
        {
            dgvClearAlumno.Columns.Clear();
            dgvClearAlumno.Columns.Add("Matricula", "Matrícula");
            dgvClearAlumno.Columns.Add("Nombre", "Nombre");
            dgvClearAlumno.Columns.Add("Materia", "Materia");
            dgvClearAlumno.Columns.Add("Calificacion", "Calificación");

            if (!File.Exists(RutaArchivo)) return;

            foreach (string linea in File.ReadAllLines(RutaArchivo))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                string[] datos = linea.Split(',');
                if (datos.Length >= 4)
                {
                    dgvClearAlumno.Rows.Add(datos[0], datos[1], datos[2], datos[3]);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 frm = new Form2();
            frm.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClearAlumno.CurrentRow == null || dgvClearAlumno.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccione un registro para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvClearAlumno.Rows.Remove(dgvClearAlumno.CurrentRow);

            List<string> lineasRestantes = new List<string>();
            foreach (DataGridViewRow row in dgvClearAlumno.Rows)
            {
                if (row.IsNewRow) continue;
                lineasRestantes.Add($"{row.Cells[0].Value},{row.Cells[1].Value},{row.Cells[2].Value},{row.Cells[3].Value}");
            }

            File.WriteAllLines(RutaArchivo, lineasRestantes);
            MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
