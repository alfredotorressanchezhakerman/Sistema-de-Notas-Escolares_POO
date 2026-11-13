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

namespace Sistema_de_Notas_Escolares_POO
{
    public partial class frmAlumno : Form
    {
        private readonly string _rutaArchivo;

        public frmAlumno(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
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

        public string Matricula { get; private set; }
        public string Nombre { get; private set; }

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
            if (string.IsNullOrWhiteSpace(txtMatricula.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por Favor llene los campos.", "Campos vacios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rutaArchivo = "Alumnos.txt";

            try
            {
                frmAlumno nuevoAlumno = new frmAlumno();
                nuevoAlumno.Matricula = txtMatricula.Text;
                nuevoAlumno.Nombre = txtNombre.Text;

                string alumnoData = $"{nuevoAlumno.Matricula},{nuevoAlumno.Nombre}";

                using(StreamWriter sw = new StreamWriter(rutaArchivo, true))
                {
                    sw.WriteLine(alumnoData);
                }

                MessageBox.Show("Alumno agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                frmAlumno Alumno = new frmAlumno();
                Alumno.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el alumno: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void frmAlumno_Load(object sender, EventArgs e)
        {

        }
    }
    public abstract class Persona
    {
        public string Matricula { get; }
        public string Nombre { get; }

        public Persona(string matricula, string nombre)
        {
            Matricula = matricula;
            Nombre = nombre;
        }

        public abstract string MostrarDatos();
        public override string ToString() => $"{Matricula} - {Nombre}";
    }
}
