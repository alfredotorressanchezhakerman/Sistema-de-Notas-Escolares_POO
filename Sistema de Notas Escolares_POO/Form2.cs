using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Sistema_de_Notas_Escolares_POO
{
    public partial class Form2 : Form
    {
        private const string RutaArchivo = "Alumnos.txt";
        public Form2()
        {
            InitializeComponent();
        }

        private void CargarResumen()
        {
            dvgMenu.Columns.Clear();
            dvgMenu.Columns.Add("Matricula", "Matrícula");
            dvgMenu.Columns.Add("Nombre", "Nombre");
            dvgMenu.Columns.Add("Materia", "Materia");
            dvgMenu.Columns.Add("Calificacion", "Calificación");

            HashSet<string> alumnosUnicos = new HashSet<string>();
            double sumaCalificaciones = 0;
            int totalCalificaciones = 0;

            if (File.Exists(RutaArchivo))
            {
                foreach (string linea in File.ReadAllLines(RutaArchivo))
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    string[] datos = linea.Split(',');
                    if (datos.Length >= 4)
                    {
                        dvgMenu.Rows.Add(datos[0], datos[1], datos[2], datos[3]);
                        alumnosUnicos.Add(datos[0]);

                        if (double.TryParse(datos[3], out double calif))
                        {
                            sumaCalificaciones += calif;
                            totalCalificaciones++;
                        }
                    }
                }
            }

            lblTotalAlumn.Text = alumnosUnicos.Count.ToString();
            lblPromedioGeneral.Text = totalCalificaciones > 0
                ? (sumaCalificaciones / totalCalificaciones).ToString("F2")
                : "0.00";
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
            this.Hide();
            EliminarAlumno Eliminar = new EliminarAlumno();
            Eliminar.Show();
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

        private void Form2_Load(object sender, EventArgs e)
        {
            CargarResumen();
        }

        public abstract class Persona
        {
            public string Matricula { get; protected set; }
            public string Nombre { get; protected set; }

            public Persona(string matricula, string nombre)
            {
                if (string.IsNullOrWhiteSpace(matricula))
                {
                    throw new ArgumentException("La matrícula no puede estar vacía.", nameof(matricula));
                }
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
                }

                Matricula = matricula.Trim();
                Nombre = nombre.Trim();
            }
            public abstract string MostrarDatos();

            public override string ToString() => $"{Matricula} - {Nombre}";


            //====================================================================
            //CLASE ALUMNO HIJA 
            //====================================================================
            public class Alumno : Persona
            {
                private Dictionary<string, double> calificaciones;

                public Dictionary<string, double> Calificaciones
                {
                    get { return calificaciones; }
                }

                public Alumno(string matricula, string nombre) : base(matricula, nombre)
                {
                    calificaciones = new Dictionary<string, double>();
                }

                

                public void AgregarCalificacion(string materia, double calificacion)
                {
                    if (string.IsNullOrWhiteSpace(materia))
                    {
                        throw new ArgumentException("La materia no puede estar vacía.", nameof(materia));
                    }
                    if (calificacion < 0 || calificacion > 10)
                    {
                        throw new ArgumentOutOfRangeException(nameof(calificacion), "La calificación debe estar entre 0 y 10.");
                    }
                    if (calificaciones.ContainsKey(materia))
                    {
                        calificaciones[materia] = calificacion; 
                    }
                    else
                    {
                        calificaciones.Add(materia, calificacion); 
                    }
                }
                public double ObtenerCalificacion(string materia)
                {
                    if (Calificaciones.Count == 0)  return 0; 
                    
                    double suma = 0;

                    foreach (var calificacion in Calificaciones.Values)
                    {
                        suma += calificacion;
                    }
                    
                    return suma / Calificaciones.Count; 
                }

                public override string MostrarDatos()
                {
                    return "Alumno: " + Nombre + ", Matrícula: " + Matricula + ", Promedio: " + ObtenerCalificacion("").ToString("F2");
                }


            }
        }

        private void lblTotalAlumn_Click(object sender, EventArgs e)
        {

        }

        private void lblPromedioGeneral_Click(object sender, EventArgs e)
        {

        }
    }
    
}
