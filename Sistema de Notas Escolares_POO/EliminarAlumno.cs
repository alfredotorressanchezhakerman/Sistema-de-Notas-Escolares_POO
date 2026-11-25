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
    public partial class EliminarAlumno: Form
    {
        private readonly string _rutaArchivo;
        public EliminarAlumno(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
        }
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
            dgvClearAlumno.Rows.Clear();

            dgvClearAlumno.Columns.Add("Matricula", "Matrícula");
            dgvClearAlumno.Columns.Add("Nombre", "Nombre");

            string rutaArchivo = "alumnos.txt";
            if (System.IO.File.Exists(rutaArchivo))
            {
                string[] lineas = System.IO.File.ReadAllLines(rutaArchivo);
                foreach (string linea in lineas)
                {
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        string[] datos = linea.Split('|');
                        if (datos.Length == 2)
                        {
                            dgvClearAlumno.Rows.Add(datos[0], datos[1]);
                        }
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 frm = new Form2();
            frm.Show();
            //PITOOOOOOOOOOOOOOOOO
        }
    }
}
