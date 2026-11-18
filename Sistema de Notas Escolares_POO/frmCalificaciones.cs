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

namespace Sistema_de_Notas_Escolares_POO
{
    public partial class frmCalificaciones : Form
    {
        private readonly string _rutaArchivo;
        public frmCalificaciones(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
        }
        public frmCalificaciones()
        {
            InitializeComponent();
            CargarCalificaciones();
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
            // Antes de cerrar o regresar, guarda los datos actuales del DataGridView
            GuardarCalificaciones();

            this.Hide();
            Form2 Regresar = new Form2();
            Regresar.Show();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvAlumno.CurrentRow != null && !string.IsNullOrWhiteSpace(txtMateriaAlumno.Text) && !string.IsNullOrWhiteSpace(txtCalificacionAlumno.Text))
            {
                // Actualiza la fila seleccionada en el DataGridView
                dgvAlumno.CurrentRow.Cells["Materia"].Value = txtMateriaAlumno.Text;
                dgvAlumno.CurrentRow.Cells["Calificacion"].Value = txtCalificacionAlumno.Text;

                // Guarda todas las filas del DataGridView en el archivo
                GuardarCalificaciones();

                MessageBox.Show("Calificación guardada con éxito ;)", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtCalificacionAlumno.Clear();
                txtMateriaAlumno.Clear();

                // Refresca el DataGridView para mostrar los datos guardados
                RefrescarGrid();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un alumno y llene los campos de materia y calificación.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GuardarCalificaciones()
        {
            using (StreamWriter sw = new StreamWriter("Calificaciones.txt", false))
            {
                foreach (DataGridViewRow row in dgvAlumno.Rows)
                {
                    if (row.Cells["Matricula"].Value != null && row.Cells["Nombre"].Value != null)
                    {
                        string matricula = row.Cells["Matricula"].Value.ToString();
                        string nombre = row.Cells["Nombre"].Value.ToString();
                        string materia = row.Cells["Materia"].Value?.ToString() ?? "";
                        string calificacion = row.Cells["Calificacion"].Value?.ToString() ?? "";
                        sw.WriteLine($"{matricula},{nombre},{materia},{calificacion}");
                    }
                }
            }
        }
        private void CargarCalificaciones()
        {
            string rutaArchivo = "Calificaciones.txt";
            if (File.Exists(rutaArchivo))
            {
                if (dgvAlumno.Columns.Count == 0)
                {
                    dgvAlumno.Columns.Add("Matricula", "Matrícula");
                    dgvAlumno.Columns.Add("Nombre", "Nombre");
                    dgvAlumno.Columns.Add("Materia", "Materia");
                    dgvAlumno.Columns.Add("Calificacion", "Calificación");
                }
                dgvAlumno.Rows.Clear();
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAlumno.CurrentRow != null)
            {
                dgvAlumno.CurrentRow.Cells["Materia"].Value = "";
                dgvAlumno.CurrentRow.Cells["Calificacion"].Value = "";
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un alumno para eliminar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvAlumno_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvAlumno.CurrentRow != null)
            {
                txtMateriaAlumno.Text = dgvAlumno.CurrentRow.Cells["Materia"].Value?.ToString();
                txtCalificacionAlumno.Text = dgvAlumno.CurrentRow.Cells["Calificacion"].Value?.ToString();
            }
        }


        public void RefrescarGrid()
        {
            dgvAlumno.Rows.Clear();
            string rutaArchivo = "Calificaciones.txt";
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

        public List<Persona> ObtenerTodas()
        {
            List<Persona> personas = new List<Persona>();
            if (!File.Exists(_rutaArchivo)) return personas;

            string[] lineas = File.ReadAllLines(_rutaArchivo);

            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                string[] datos = linea.Split('|');
                if (datos.Length != 4)
                    continue;

                var datoss = personas.Select(p => new
                {
                    Matricula = p.Matricula,
                    Nombre = p.Nombre
                });
            }
            return personas;
        }

        public void GuardarTodas(List<Persona> personas)
        {
            List<string> lineas = new List<string>();
            foreach (Persona persona in personas)
            {
                lineas.Add($"{persona.Nombre}|{persona.Matricula}");
            }
            File.WriteAllLines(_rutaArchivo, lineas);
        }
    }
}
