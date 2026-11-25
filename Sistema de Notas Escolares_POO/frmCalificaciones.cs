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
using static Sistema_de_Notas_Escolares_POO.Form2;
using static Sistema_de_Notas_Escolares_POO.Form2.Persona;

namespace Sistema_de_Notas_Escolares_POO
{
    public partial class frmCalificaciones : Form
    {
        private const string RutaArchivo = "Alumnos.txt";
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
            ConfigurarGrid();
            CargarCalificaciones();
        }

        private void ConfigurarGrid()
        {
            dgvAlumno.Columns.Clear();
            dgvAlumno.Columns.Add("Matricula", "Matrícula");
            dgvAlumno.Columns.Add("Nombre", "Nombre");
            dgvAlumno.Columns.Add("Materia", "Materia");
            dgvAlumno.Columns.Add("Calificacion", "Calificación");
        }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            GuardarCalificaciones();

            this.Close();
            Form2 Regresar = new Form2();
            Regresar.Show();
        }

        private void CargarCalificaciones()
        {
            dgvAlumno.Rows.Clear();
            if (!File.Exists(RutaArchivo)) return;

            foreach (string linea in File.ReadAllLines(RutaArchivo))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;

                string[] datos = linea.Split(',');
                if (datos.Length >= 4)
                {
                    dgvAlumno.Rows.Add(datos[0], datos[1], datos[2], datos[3]);
                }
            }
        }

        private void GuardarCalificaciones()
        {
            List<string> lineas = new List<string>();
            foreach (DataGridViewRow row in dgvAlumno.Rows)
            {
                if (row.IsNewRow || row.Cells["Matricula"].Value == null) continue;

                string matricula = row.Cells["Matricula"].Value.ToString();
                string nombre = row.Cells["Nombre"].Value?.ToString() ?? "";
                string materia = row.Cells["Materia"].Value?.ToString() ?? "";
                string calificacion = row.Cells["Calificacion"].Value?.ToString() ?? "";

                lineas.Add($"{matricula},{nombre},{materia},{calificacion}");
            }

            File.WriteAllLines(RutaArchivo, lineas);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvAlumno.CurrentRow == null || dgvAlumno.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccione un alumno de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtCalificacionAlumno.Text, out double calif))
            {
                MessageBox.Show("Ingrese una calificación numérica válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Validamos usando la clase Alumno (rango 0 a 10 y materia no vacía)
                string mat = dgvAlumno.CurrentRow.Cells["Matricula"].Value.ToString();
                string nom = dgvAlumno.CurrentRow.Cells["Nombre"].Value.ToString();
                Alumno alumno = new Alumno(mat, nom);
                alumno.AgregarCalificacion(txtMateriaAlumno.Text, calif);

                dgvAlumno.CurrentRow.Cells["Materia"].Value = txtMateriaAlumno.Text.Trim();
                dgvAlumno.CurrentRow.Cells["Calificacion"].Value = calif.ToString("F2");

                GuardarCalificaciones();
                MessageBox.Show("Calificación guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtMateriaAlumno.Clear();
                txtCalificacionAlumno.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAlumno.CurrentRow == null || dgvAlumno.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccione un alumno para limpiar su calificación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvAlumno.CurrentRow.Cells["Materia"].Value = "";
            dgvAlumno.CurrentRow.Cells["Calificacion"].Value = "";
            GuardarCalificaciones();
            MessageBox.Show("Calificación eliminada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvAlumno_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dgvAlumno.Rows[e.RowIndex].IsNewRow)
            {
                txtMateriaAlumno.Text = dgvAlumno.Rows[e.RowIndex].Cells["Materia"].Value?.ToString();
                txtCalificacionAlumno.Text = dgvAlumno.Rows[e.RowIndex].Cells["Calificacion"].Value?.ToString();
            }
        }

        public void RefrescarGrid()
        {
            dgvAlumno.Rows.Clear();
            string rutaArchivo = "Alumnos.txt";
            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);
                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(',');
                    if (datos.Length >= 4)
                    {
                        dgvAlumno.Rows.Add(datos[0], datos[1], datos[2], datos[3]);
                    }
                }
            }
        }
    }
}
