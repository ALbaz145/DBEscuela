using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace DBEscuela
{
    public partial class Form1 : Form
    {
        SqlConnection conexion = new SqlConnection(@"Data Source=LAPTOP-NNMSLGKO; Initial Catalog=Escuelas; integrated security=true");
        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridConsultarEscuela();
            DataGridConsultarDepartamento();
            DataGridConsultarCarrera();
            CargarDropBoxDepartamento();
            DataGridConsultarMateria();
            CargarDropBoxCarrera();
            DataGridConsultarMaestro();
            DataGridConsultarEstudiante();
            CargarDropBoxEscuela();
            CargarDropBoxDepartamentoMaestro();
            CargarDropBoxIDCarrera();
        }

        //****************MODULO DE ESCUELA*******************************//
        private void DataGridConsultarEscuela()
        {
            conexion.Open();
            string query = "SELECT * FROM Escuela";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Escuela");
            DataGridViewEscuela.DataSource = dataSet.Tables["Escuela"];
            conexion.Close();

        }
        private void ButtonPageEscuela_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(5);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand altas = new SqlCommand("insert into Escuela (ID_Escuela, Nombre, Direccion, Telefono, Correo, Director) values(@ID_Escuela, @Nombre, @Direccion, @Telefono, @Correo, @Director)", conexion);

            altas.Parameters.AddWithValue("ID_Escuela", TextBoxID_Escuela.Text);
            altas.Parameters.AddWithValue("Nombre", TextBoxNombreEscuela.Text);
            altas.Parameters.AddWithValue("Direccion", TextBoxDireccionEscuela.Text);
            altas.Parameters.AddWithValue("Telefono", TextBoxTelefonoEscuela.Text);
            altas.Parameters.AddWithValue("Correo", TextBoxCorreoEscuela.Text);
            altas.Parameters.AddWithValue("Director", TextBoxDirectorEscuela.Text);
            altas.ExecuteNonQuery();

            TextBoxID_Escuela.Clear();
            TextBoxNombreEscuela.Clear();
            TextBoxDireccionEscuela.Clear();
            TextBoxTelefonoEscuela.Clear();
            TextBoxCorreoEscuela.Clear();
            TextBoxDirectorEscuela.Clear();

            MessageBox.Show("Escuela Registrada");
            conexion.Close();
            DataGridConsultarEscuela();
            CargarDropBoxEscuela();
            CargarDropBoxDepartamento();
        }
        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            SqlCommand consulta = new SqlCommand("SELECT * FROM Escuela WHERE ID_Escuela = @ID_Escuela", conexion);

            conexion.Open();
            consulta.Parameters.AddWithValue("ID_Escuela", TextBoxID_Escuela.Text);

            SqlDataReader reader = consulta.ExecuteReader();
            while (reader.Read())
            {
                TextBoxID_Escuela.Text = reader[0].ToString();
                TextBoxNombreEscuela.Text = reader[1].ToString();
                TextBoxDireccionEscuela.Text = reader[2].ToString();
                TextBoxTelefonoEscuela.Text = reader[3].ToString();
                TextBoxCorreoEscuela.Text = reader[4].ToString();
                TextBoxDirectorEscuela.Text = reader[5].ToString();
            }
            conexion.Close();
            // button3.Enabled = true;
            // button2.Enabled = true;
        }
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Tambien eliminara Estudiantes, Departamentos y Maestros ligados a esta Escuela!",
                                     "Estas Seguro de Borrar esta Entrada ?",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // If 'Yes', do something here.
                conexion.Open();
                string baja = "DELETE FROM Escuela WHERE ID_Escuela = @ID_Escuela";

                SqlCommand comando = new SqlCommand(baja, conexion);
                comando.Parameters.AddWithValue("ID_Escuela", TextBoxID_Escuela.Text);

                comando.ExecuteNonQuery();

                comando.Dispose();
                comando = null;
                TextBoxID_Escuela.Clear();
                TextBoxNombreEscuela.Clear();
                TextBoxDireccionEscuela.Clear();
                TextBoxTelefonoEscuela.Clear();
                TextBoxCorreoEscuela.Clear();
                TextBoxDirectorEscuela.Clear();

                conexion.Close();
                MessageBox.Show("Escuela Eliminada");
                DataGridConsultarEscuela();
                CargarDropBoxEscuela();
                CargarDropBoxDepartamento();
                DataGridConsultarDepartamento();
                DataGridConsultarEstudiante();
                DataGridConsultarMaestro();
            }
            
        }
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand("UPDATE Escuela SET ID_Escuela=@ID_Escuela, Nombre=@Nombre, Direccion=@Direccion, Telefono=@Telefono,Correo=@Correo, Director=@Director WHERE ID_Escuela=@ID_Escuela", conexion);

            comando.Parameters.AddWithValue("ID_Escuela", TextBoxID_Escuela.Text);
            comando.Parameters.AddWithValue("Nombre", TextBoxNombreEscuela.Text);
            comando.Parameters.AddWithValue("Direccion", TextBoxDireccionEscuela.Text);
            comando.Parameters.AddWithValue("Telefono", TextBoxTelefonoEscuela.Text);
            comando.Parameters.AddWithValue("Correo", TextBoxCorreoEscuela.Text);
            comando.Parameters.AddWithValue("Director", TextBoxDirectorEscuela.Text);

            comando.ExecuteNonQuery();

            MessageBox.Show("Modificación Completa");
            conexion.Close();


            DataGridConsultarEscuela();
            CargarDropBoxEscuela();
            CargarDropBoxDepartamento();
        }
        //********************************************************************//
        //********************* MODULO DE DEPARTAMENTO **********************//
        private void DataGridConsultarDepartamento()
        {
            conexion.Open();
            string query = "SELECT * FROM Departamento";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Departamento");
            DataGridViewDepartamento.DataSource = dataSet.Tables["Departamento"];
            conexion.Close();

        }
        private void ButtonPageDepartamento_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(1);
        }
        private void CargarDropBoxDepartamento()
        {
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT ID_Escuela FROM Escuela", conexion);
            DataTable dataT1 = new DataTable();
            adaptador.Fill(dataT1);
            DropdownID_EscuelaDepartamento.DataSource = dataT1;
            DropdownID_EscuelaDepartamento.DisplayMember = "ID_Escuela";
            DropdownID_EscuelaDepartamento.ValueMember = "ID_Escuela";

        }
        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton12_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand altas = new SqlCommand("insert into Departamento (ID_Departamento, Nombre, Descripcion, ID_Escuela) values(@ID_Departamento, @Nombre, @Descripcion, @ID_Escuela)", conexion);

            altas.Parameters.AddWithValue("ID_Departamento", TextBoxID_Departamento.Text);
            altas.Parameters.AddWithValue("Nombre", TextBoxNombreDepartamento.Text);
            altas.Parameters.AddWithValue("Descripcion", TextBoxDescripcionDepartamento.Text);
            altas.Parameters.AddWithValue("ID_Escuela", DropdownID_EscuelaDepartamento.Text);
            altas.ExecuteNonQuery();

            TextBoxID_Departamento.Clear();
            TextBoxNombreDepartamento.Clear();
            TextBoxDescripcionDepartamento.Clear();

            MessageBox.Show("Departamento Registrado");
            conexion.Close();
            DataGridConsultarDepartamento();
            CargarDropBoxDepartamento();
            CargarDropBoxDepartamentoMaestro();
        }

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            SqlCommand consulta = new SqlCommand("SELECT * FROM Departamento WHERE ID_Departamento = @ID_Departamento", conexion);

            conexion.Open();
            consulta.Parameters.AddWithValue("ID_Departamento", TextBoxID_Departamento.Text);

            SqlDataReader reader = consulta.ExecuteReader();
            while (reader.Read())
            {
                TextBoxID_Departamento.Text = reader[0].ToString();
                TextBoxNombreDepartamento.Text = reader[1].ToString();
                TextBoxDescripcionDepartamento.Text = reader[2].ToString();
                DropdownID_EscuelaDepartamento.Text = reader[3].ToString();
            }
            conexion.Close();
            // button3.Enabled = true;
            // button2.Enabled = true;
        }

        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Tambien eliminara los Maestros ligados a este Departamento!",
                                     "Estas Seguro de Borrar esta Entrada ?",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                conexion.Open();
                string baja = "DELETE FROM Departamento WHERE ID_Departamento = @ID_Departamento";

                SqlCommand comando = new SqlCommand(baja, conexion);
                comando.Parameters.AddWithValue("ID_Departamento", TextBoxID_Departamento.Text);

                comando.ExecuteNonQuery();

                comando.Dispose();
                comando = null;
                TextBoxID_Departamento.Clear();
                TextBoxNombreDepartamento.Clear();
                TextBoxDescripcionDepartamento.Clear();

                conexion.Close();
                MessageBox.Show("Departamento Eliminado");
                DataGridConsultarDepartamento();
                CargarDropBoxDepartamento();
                CargarDropBoxDepartamentoMaestro();
            }
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand("UPDATE Departamento SET ID_Departamento=@ID_Departamento, Nombre=@Nombre, Descripcion=@Descripcion, ID_Escuela=@ID_Escuela WHERE ID_Departamento=@ID_Departamento", conexion);

            comando.Parameters.AddWithValue("ID_Departamento", TextBoxID_Departamento.Text);
            comando.Parameters.AddWithValue("Nombre", TextBoxNombreDepartamento.Text);
            comando.Parameters.AddWithValue("Descripcion", TextBoxDescripcionDepartamento.Text);
            comando.Parameters.AddWithValue("ID_Escuela", DropdownID_EscuelaDepartamento.Text);

            comando.ExecuteNonQuery();

            MessageBox.Show("Modificación Completa");
            conexion.Close();


            DataGridConsultarDepartamento();
            CargarDropBoxDepartamento();
            CargarDropBoxDepartamentoMaestro();
        }
        //*****************************************************+**********************//
        //*****************************MODULO CARRERA******************************//
        //**************************************************************************//
        private void DataGridConsultarCarrera()
        {
            conexion.Open();
            string query = "SELECT * FROM Carrera";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Carrera");
            DataGridViewCarrera.DataSource = dataSet.Tables["Carrera"];
            conexion.Close();

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(2);
        }

        private void bunifuImageButton8_Click_1(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand altas = new SqlCommand("insert into Carrera (ID_Carrera, Nombre, Descripcion, Duracion) values(@ID_Carrera, @Nombre, @Descripcion, @Duracion)", conexion);

            altas.Parameters.AddWithValue("ID_Carrera", TextBoxID_Carrera.Text);
            altas.Parameters.AddWithValue("Nombre", TextBoxNombreCarrera.Text);
            altas.Parameters.AddWithValue("Descripcion", TextBoxDescripcionCarrera.Text);
            altas.Parameters.AddWithValue("Duracion", TextBoxDuracionCarrera.Text);
            altas.ExecuteNonQuery();

            TextBoxID_Carrera.Clear();
            TextBoxNombreCarrera.Clear();
            TextBoxDescripcionCarrera.Clear();
            TextBoxDuracionCarrera.Clear();

            MessageBox.Show("Carrera Registrada");
            conexion.Close();
            CargarDropBoxCarrera();
            DataGridConsultarCarrera();
            CargarDropBoxIDCarrera();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            SqlCommand consulta = new SqlCommand("SELECT * FROM Carrera WHERE ID_Carrera = @ID_Carrera", conexion);

            conexion.Open();
            consulta.Parameters.AddWithValue("ID_Carrera", TextBoxID_Carrera.Text);

            SqlDataReader reader = consulta.ExecuteReader();
            while (reader.Read())
            {
                TextBoxID_Carrera.Text = reader[0].ToString();
                TextBoxNombreCarrera.Text = reader[1].ToString();
                TextBoxDescripcionCarrera.Text = reader[2].ToString();
                TextBoxDuracionCarrera.Text = reader[3].ToString();
            }
            conexion.Close();
            // button3.Enabled = true;
            // button2.Enabled = true;
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Tambien eliminara las Materias ligados a esta Carrera!",
                                     "Estas Seguro de Borrar esta Entrada ?",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {

                conexion.Open();
                string baja = "DELETE FROM Carrera WHERE ID_Carrera = @ID_Carrera";

                SqlCommand comando = new SqlCommand(baja, conexion);
                comando.Parameters.AddWithValue("ID_Carrera", TextBoxID_Carrera.Text);

                comando.ExecuteNonQuery();

                comando.Dispose();
                comando = null;
                TextBoxID_Carrera.Clear();
                TextBoxNombreCarrera.Clear();
                TextBoxDescripcionCarrera.Clear();
                TextBoxDuracionCarrera.Clear();

                conexion.Close();
                MessageBox.Show("Carrera Eliminada");
                DataGridConsultarCarrera();
                CargarDropBoxCarrera();
                CargarDropBoxIDCarrera();
                DataGridConsultarCarrera();
                DataGridConsultarMateria();
            }
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand("UPDATE Carrera SET ID_Carrera=@ID_Carrera, Nombre=@Nombre, Descripcion=@Descripcion, Duracion=@Duracion WHERE ID_Carrera=@ID_Carrera", conexion);

            comando.Parameters.AddWithValue("ID_Carrera", TextBoxID_Carrera.Text);
            comando.Parameters.AddWithValue("Nombre", TextBoxNombreCarrera.Text);
            comando.Parameters.AddWithValue("Descripcion", TextBoxDescripcionCarrera.Text);
            comando.Parameters.AddWithValue("Duracion", TextBoxDescripcionCarrera.Text);

            comando.ExecuteNonQuery();

            MessageBox.Show("Modificación Completa");
            conexion.Close();

            CargarDropBoxCarrera();
            DataGridConsultarCarrera();
            CargarDropBoxIDCarrera();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
        //******************************************************************************//
        //******************** MODULO MATERIA *****************************************//
        private void DataGridConsultarMateria()
        {
            conexion.Open();
            string query = "SELECT * FROM Materia";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Materia");
            DataGridViewMateria.DataSource = dataSet.Tables["Materia"];
            conexion.Close();

        }
        private void CargarDropBoxCarrera()
        {
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT ID_Carrera FROM Carrera", conexion);
            DataTable dataT1 = new DataTable();
            adaptador.Fill(dataT1);
            DropdownID_CarreraMateria.DataSource = dataT1;
            DropdownID_CarreraMateria.DisplayMember = "ID_Carrera";
            DropdownID_CarreraMateria.ValueMember = "ID_Carrera";

        }

        private void ButtonPageMateria_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(3);
        }

        private void bunifuImageButton16_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand altas = new SqlCommand("insert into Materia (ID_Materia, Nombre, Descripcion, Creditos, ID_Carrera) values(@ID_Materia, @Nombre, @Descripcion, @Creditos, @ID_Carrera)", conexion);

            altas.Parameters.AddWithValue("ID_Materia", TextBoxID_Materia.Text);
            altas.Parameters.AddWithValue("Nombre", TextBoxNombreMateria.Text);
            altas.Parameters.AddWithValue("Descripcion", TextBoxDescripcionMateria.Text);
            altas.Parameters.AddWithValue("Creditos", TextBoxCreditosMateria.Text);
            altas.Parameters.AddWithValue("ID_Carrera", DropdownID_CarreraMateria.Text);
            altas.ExecuteNonQuery();

            TextBoxID_Materia.Clear();
            TextBoxNombreMateria.Clear();
            TextBoxDescripcionMateria.Clear();
            TextBoxCreditosMateria.Clear();

            MessageBox.Show("Materia Registrada");
            conexion.Close();
            DataGridConsultarMateria();
        }

        private void bunifuImageButton14_Click(object sender, EventArgs e)
        {
            SqlCommand consulta = new SqlCommand("SELECT * FROM Materia WHERE ID_Materia = @ID_Materia", conexion);

            conexion.Open();
            consulta.Parameters.AddWithValue("ID_Materia", TextBoxID_Materia.Text);

            SqlDataReader reader = consulta.ExecuteReader();
            while (reader.Read())
            {
                TextBoxID_Materia.Text = reader[0].ToString();
                TextBoxNombreMateria.Text = reader[1].ToString();
                TextBoxDescripcionMateria.Text = reader[2].ToString();
                TextBoxCreditosMateria.Text = reader[3].ToString();
                DropdownID_CarreraMateria.Text = reader[4].ToString();
            }
            conexion.Close();
            // button3.Enabled = true;
            // button2.Enabled = true;
        }

        private void bunifuImageButton15_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string baja = "DELETE FROM Materia WHERE ID_Materia = @ID_Materia";

            SqlCommand comando = new SqlCommand(baja, conexion);
            comando.Parameters.AddWithValue("ID_Materia", TextBoxID_Materia.Text);

            comando.ExecuteNonQuery();

            comando.Dispose();
            comando = null;
            TextBoxID_Materia.Clear();
            TextBoxNombreMateria.Clear();
            TextBoxDescripcionMateria.Clear();
            TextBoxCreditosMateria.Clear();

            conexion.Close();
            MessageBox.Show("Materia Eliminada");
            DataGridConsultarMateria();
        }

        private void bunifuImageButton13_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand("UPDATE Materia SET ID_Materia=@ID_Materia, Nombre=@Nombre, Descripcion=@Descripcion, Creditos=@Creditos, ID_Carrera=@ID_Carrera WHERE ID_Materia=@ID_Materia", conexion);

            comando.Parameters.AddWithValue("ID_Materia", TextBoxID_Materia.Text);
            comando.Parameters.AddWithValue("Nombre", TextBoxNombreMateria.Text);
            comando.Parameters.AddWithValue("Descripcion", TextBoxDescripcionMateria.Text);
            comando.Parameters.AddWithValue("Creditos", TextBoxCreditosMateria.Text);
            comando.Parameters.AddWithValue("ID_Carrera", DropdownID_CarreraMateria.Text);
            comando.ExecuteNonQuery();

            MessageBox.Show("Modificación Completa");
            conexion.Close();

            DataGridConsultarMateria();
        }
        //*************************************************************************//
        //******************* MODULO MAESTRO *************************************//
        private void DataGridConsultarMaestro()
        {
            conexion.Open();
            string query = "SELECT * FROM Maestro";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Maestro");
            DataGridViewMaestro.DataSource = dataSet.Tables["Maestro"];
            conexion.Close();
        }
           private void CargarDropBoxDepartamentoMaestro()
            {
                SqlDataAdapter adaptador = new SqlDataAdapter("SELECT ID_Departamento FROM Departamento", conexion);
                DataTable dataT1 = new DataTable();
                adaptador.Fill(dataT1);
            DropdownID_DepartamentoMaestro.DataSource = dataT1;
            DropdownID_DepartamentoMaestro.DisplayMember = "ID_Departamento";
                DropdownID_DepartamentoMaestro.ValueMember = "ID_Departamento";

            }
        
        private void ButtonPageMaestro_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(4);
        }

        private void bunifuImageButton20_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand altas = new SqlCommand("insert into Maestro (ID_Maestro, Nombre, Apellido, Titulo, ID_Departamento) values(@ID_Maestro, @Nombre, @Apellido, @Titulo, @ID_Departamento)", conexion);

            altas.Parameters.AddWithValue("ID_Maestro", TextBoxID_Maestro.Text);
            altas.Parameters.AddWithValue("Nombre", TextBoxNombreMaestro.Text);
            altas.Parameters.AddWithValue("Apellido", TextBoxApellidoMaestro.Text);
            altas.Parameters.AddWithValue("Titulo", TextBoxTituloMaestro.Text);
            altas.Parameters.AddWithValue("ID_Departamento", DropdownID_DepartamentoMaestro.Text);
            altas.ExecuteNonQuery();

            TextBoxID_Maestro.Clear();
            TextBoxNombreMaestro.Clear();
            TextBoxApellidoMaestro.Clear();
            TextBoxTituloMaestro.Clear();

            MessageBox.Show("Maestro Registrado");
            conexion.Close();
            DataGridConsultarMaestro();
        }

        private void bunifuImageButton18_Click(object sender, EventArgs e)
        {
            SqlCommand consulta = new SqlCommand("SELECT * FROM Maestro WHERE ID_Maestro = @ID_Maestro", conexion);

            conexion.Open();
            consulta.Parameters.AddWithValue("ID_Maestro", TextBoxID_Maestro.Text);

            SqlDataReader reader = consulta.ExecuteReader();
            while (reader.Read())
            {
                TextBoxID_Maestro.Text = reader[0].ToString();
                TextBoxNombreMaestro.Text = reader[1].ToString();
                TextBoxApellidoMaestro.Text = reader[2].ToString();
                TextBoxTituloMaestro.Text = reader[3].ToString();
                DropdownID_DepartamentoMaestro.Text = reader[4].ToString();
            }
            conexion.Close();
            // button3.Enabled = true;
            // button2.Enabled = true;
        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string baja = "DELETE FROM Maestro WHERE ID_Maestro = @ID_Maestro";

            SqlCommand comando = new SqlCommand(baja, conexion);
            comando.Parameters.AddWithValue("ID_Maestro", TextBoxID_Maestro.Text);

            comando.ExecuteNonQuery();

            comando.Dispose();
            comando = null;
            TextBoxID_Maestro.Clear();
            TextBoxNombreMaestro.Clear();
            TextBoxApellidoMaestro.Clear();
            TextBoxTituloMaestro.Clear();

            conexion.Close();
            MessageBox.Show("Maestro Eliminado");
            DataGridConsultarMaestro();
        }

        private void bunifuImageButton17_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand("UPDATE Maestro SET ID_Maestro=@ID_Maestro, Nombre=@Nombre, Apellido=@Apellido, Titulo=@Titulo, ID_Departamento=@ID_Departamento WHERE ID_Maestro=@ID_Maestro", conexion);

            comando.Parameters.AddWithValue("ID_Maestro", TextBoxID_Maestro.Text);
            comando.Parameters.AddWithValue("Nombre", TextBoxNombreMaestro.Text);
            comando.Parameters.AddWithValue("Apellido", TextBoxApellidoMaestro.Text);
            comando.Parameters.AddWithValue("Titulo", TextBoxTituloMaestro.Text);
            comando.Parameters.AddWithValue("ID_Departamento", DropdownID_DepartamentoMaestro.Text);
            comando.ExecuteNonQuery();

            MessageBox.Show("Modificación Completa");
            conexion.Close();

            DataGridConsultarMaestro();
        }
        //*******************************************************************************//
        //****************** MODULO ESTUDIANTE ******************************************//

        private void DataGridConsultarEstudiante()
        {
            conexion.Open();
            string query = "SELECT * FROM Estudiante";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Estudiante");
            DataGridViewEstudiante.DataSource = dataSet.Tables["Estudiante"];
            conexion.Close();
        }
        private void CargarDropBoxEscuela()
        {
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT ID_Escuela FROM Escuela", conexion);
            DataTable dataT1 = new DataTable();
            adaptador.Fill(dataT1);
            DropdownID_EscuelaEstudiante.DataSource = dataT1;
            DropdownID_EscuelaEstudiante.DisplayMember = "ID_Escuela";
            DropdownID_EscuelaEstudiante.ValueMember = "ID_Escuela";

        }
        private void CargarDropBoxIDCarrera()
        {
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT ID_Carrera FROM Carrera", conexion);
            DataTable dataT1 = new DataTable();
            adaptador.Fill(dataT1);
            DropdownID_CarreraEstudiante.DataSource = dataT1;
            DropdownID_CarreraEstudiante.DisplayMember = "ID_Carrera";
            DropdownID_CarreraEstudiante.ValueMember = "ID_Carrera";

        }

        private void bunifuTextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtonPageEstudiante_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(6);
        }

        private void bunifuImageButton24_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand altas = new SqlCommand("insert into Estudiante (ID_Estudiante, Nombre, Apellido, FechaNacimiento, Genero, DireccionCasa, Telefono, Correo, ID_Carrera, ID_Escuela) values(@ID_Estudiante, @Nombre, @Apellido, @FechaNacimiento, @Genero, @DireccionCasa, @Telefono, @Correo, @ID_Carrera, @ID_Escuela)", conexion);

            altas.Parameters.AddWithValue("ID_Estudiante", TextBoxID_Estudiante.Text);
            altas.Parameters.AddWithValue("Nombre", TextBoxNombreEstudiante.Text);
            altas.Parameters.AddWithValue("Apellido", TextBoxApellidoEstudiante.Text);
            altas.Parameters.AddWithValue("FechaNacimiento", TextBoxFechaNacimientoEstudiante.Text);
            altas.Parameters.AddWithValue("Genero", TextBoxGeneroEstudiante.Text);
            altas.Parameters.AddWithValue("DireccionCasa", TextBoxDireccionEstudiante.Text);
            altas.Parameters.AddWithValue("Telefono", TextBoxTelefonoEstudiante.Text);
            altas.Parameters.AddWithValue("Correo", TextBoxCorreoEstudiante.Text);
            altas.Parameters.AddWithValue("ID_Carrera", DropdownID_CarreraEstudiante.Text);
            altas.Parameters.AddWithValue("ID_Escuela", DropdownID_EscuelaEstudiante.Text);
            altas.ExecuteNonQuery();

            TextBoxID_Estudiante.Clear();
            TextBoxNombreEstudiante.Clear();
            TextBoxApellidoEstudiante.Clear();
            TextBoxFechaNacimientoEstudiante.Clear();
            TextBoxGeneroEstudiante.Clear();
            TextBoxDireccionEstudiante.Clear();
            TextBoxTelefonoEstudiante.Clear();
            TextBoxCorreoEstudiante.Clear();

            MessageBox.Show("Estudiante Registrado");
            conexion.Close();
            DataGridConsultarEstudiante();
        }

        private void bunifuImageButton22_Click(object sender, EventArgs e)
        {
            SqlCommand consulta = new SqlCommand("SELECT * FROM Estudiante WHERE ID_Estudiante = @ID_Estudiante", conexion);

            conexion.Open();
            consulta.Parameters.AddWithValue("ID_Estudiante", TextBoxID_Estudiante.Text);

            SqlDataReader reader = consulta.ExecuteReader();
            while (reader.Read())
            {
                TextBoxID_Estudiante.Text = reader[0].ToString();
                TextBoxNombreEstudiante.Text = reader[1].ToString();
                TextBoxApellidoEstudiante.Text = reader[2].ToString();
                TextBoxFechaNacimientoEstudiante.Text = reader[3].ToString();
                TextBoxGeneroEstudiante.Text = reader[4].ToString();
                TextBoxDireccionEstudiante.Text = reader[5].ToString();
                TextBoxTelefonoEstudiante.Text= reader[6].ToString();
                TextBoxCorreoEstudiante.Text = reader[7].ToString();
                DropdownID_CarreraEstudiante.Text = reader[8].ToString();
                DropdownID_EscuelaEstudiante.Text = reader[9].ToString();

            }
            conexion.Close();
            // button3.Enabled = true;
            // button2.Enabled = true;
        }

        private void bunifuImageButton23_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string baja = "DELETE FROM Estudiante WHERE ID_Estudiante = @ID_Estudiante";

            SqlCommand comando = new SqlCommand(baja, conexion);
            comando.Parameters.AddWithValue("ID_Estudiante", TextBoxID_Estudiante.Text);

            comando.ExecuteNonQuery();

            comando.Dispose();
            comando = null;
            TextBoxID_Estudiante.Clear();
            TextBoxNombreEstudiante.Clear();
            TextBoxApellidoEstudiante.Clear();
            TextBoxFechaNacimientoEstudiante.Clear();
            TextBoxGeneroEstudiante.Clear();
            TextBoxDireccionEstudiante.Clear();
            TextBoxTelefonoEstudiante.Clear();
            TextBoxCorreoEstudiante.Clear();

            conexion.Close();
            MessageBox.Show("Estudiante Eliminado");
            DataGridConsultarEstudiante();
        }

        private void bunifuImageButton21_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand("UPDATE Estudiante SET ID_Estudiante=@ID_Estudiante, Nombre=@Nombre, Apellido=@Apellido, FechaNacimiento=@FechaNacimiento, Genero=@Genero, DireccionCasa=@DireccionCasa, Telefono=@Telefono, Correo=@Correo, ID_Carrera=@ID_Carrera, ID_Escuela=@ID_Escuela WHERE ID_Estudiante=@ID_Estudiante", conexion);

            comando.Parameters.AddWithValue("ID_Estudiante", TextBoxID_Estudiante.Text);
            comando.Parameters.AddWithValue("Nombre", TextBoxNombreEstudiante.Text);
            comando.Parameters.AddWithValue("Apellido", TextBoxApellidoEstudiante.Text);
            comando.Parameters.AddWithValue("FechaNacimiento", TextBoxFechaNacimientoEstudiante.Text);
            comando.Parameters.AddWithValue("Genero", TextBoxGeneroEstudiante.Text);
            comando.Parameters.AddWithValue("DireccionCasa", TextBoxDireccionEstudiante.Text);
            comando.Parameters.AddWithValue("Telefono", TextBoxTelefonoEstudiante.Text);
            comando.Parameters.AddWithValue("Correo", TextBoxCorreoEstudiante.Text);
            comando.Parameters.AddWithValue("ID_Carrera", DropdownID_CarreraEstudiante.Text);
            comando.Parameters.AddWithValue("ID_Escuela", DropdownID_EscuelaEstudiante.Text);
            comando.ExecuteNonQuery();

            MessageBox.Show("Modificación Completa");
            conexion.Close();

            DataGridConsultarEstudiante();
        }
    }
}
