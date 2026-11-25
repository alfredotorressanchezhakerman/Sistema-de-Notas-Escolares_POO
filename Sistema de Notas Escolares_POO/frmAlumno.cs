using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static Sistema_de_Notas_Escolares_POO.Form2;
using static Sistema_de_Notas_Escolares_POO.Form2.Persona;

namespace Sistema_de_Notas_Escolares_POO
{
    public partial class frmAlumno : Form
    {
        private const string RutaArchivo = "Alumnos.txt";
        public frmAlumno()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 Regresar = new Form2();
            Regresar.Show();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Usa la clase POO para validar automáticamente matrícula y nombre
                Alumno nuevoAlumno = new Alumno(txtMatricula.Text, txtNombre.Text);

                // Se guarda con 4 columnas (materia y calificación vacías al inicio)
                string registro = $"{nuevoAlumno.Matricula},{nuevoAlumno.Nombre},,";
                File.AppendAllLines(RutaArchivo, new[] { registro });

                MessageBox.Show("Alumno agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMatricula.Clear();
                txtNombre.Clear();
                txtMatricula.Focus();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAlumno_Load(object sender, EventArgs e)
        {

        }
    }
}
