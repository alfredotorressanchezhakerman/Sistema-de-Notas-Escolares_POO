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
            //Fhaaa
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

        private void Form2_Load(object sender, EventArgs e)
        {
            
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

                Matricula = matricula;
                Nombre = nombre;
            }
            public abstract string MostrarDatos();

            public override string ToString()
            {
                return Matricula + " - " + Nombre;
            }


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
                        calificaciones[materia] = calificacion; // Actualiza la calificación si la materia ya existe
                    }
                    else
                    {
                        calificaciones.Add(materia, calificacion); // Agrega una nueva materia y calificación
                    }
                }
                public double ObtenerCalificacion(string materia)
                {
                    if (Calificaciones.Count == 0)  return 0.0; 
                         // Retorna 0 si no hay calificaciones registradas
                    
                    double suma = 0;

                    foreach (var calificacion in Calificaciones.Values)
                    {
                        suma += calificacion;
                    }
                    
                    return suma / Calificaciones.Count; // Retorna el promedio de calificaciones
                }

                public override string MostrarDatos()
                {
                    return "Alumno: " + Nombre + ", Matrícula: " + Matricula + ", Promedio: " + ObtenerCalificacion("").ToString("F2");
                }


            }
        }

    }
    
}
