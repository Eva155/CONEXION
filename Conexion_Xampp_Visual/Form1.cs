using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Conexion_Xampp_Visual
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string conexion = "datasource=127.0.0.1;port=3306;username=root;password=;database=practica;";
        MySqlConnection cn = new MySqlConnection(conexion);

        //Es lo que se va a visualizar en la ventana 
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = llenar_grid();
        }

        //Select
        public DataTable llenar_grid(){
            //Abre la base de datos
            cn.Open();
            DataTable dt = new DataTable();
            String llenar = "select * from persona";
            MySqlCommand cmd = new MySqlCommand(llenar, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            //Cierra la base datos
            cn.Close();
            return dt;
        }

        //Insertar registros a la tabla
        private void button1_Click(object sender, EventArgs e)
        {
            //Abre la base de datos
            cn.Open();
            string insertar = "INSERT INTO persona(id, nombre, ap_paterno, ap_materno, direccion) values(@id,@nombre,@ap_paterno,@ap_materno,@direccion)";
            //Instancia
            MySqlCommand cmd = new MySqlCommand(insertar, cn);
            //Parametros
            cmd.Parameters.AddWithValue("@id", textId.Text);
            cmd.Parameters.AddWithValue("@nombre", textNombre.Text);
            cmd.Parameters.AddWithValue("@ap_paterno", textAp_paterno.Text);
            cmd.Parameters.AddWithValue("@ap_materno", textAp_materno.Text);
            cmd.Parameters.AddWithValue("@direccion", textDireccion.Text);
            cmd.ExecuteNonQuery();
            //Cierra la base datos
            cn.Close();
            MessageBox.Show("Los datos fueron agregados correctamente :)");
            dataGridView1.DataSource = llenar_grid();
        }

        //Modificar registros
        private void button2_Click(object sender, EventArgs e)
        {
            //Abre la base de datos
            cn.Open();
            string actualizar = "UPDATE persona SET ID=@id, nombre=@nombre, ap_paterno=@ap_paterno, ap_materno=@ap_materno, direccion=@direccion WHERE id=@id";
            //Instancia
            MySqlCommand cmd = new MySqlCommand(actualizar, cn);
            //Parametros
            cmd.Parameters.AddWithValue("@id", textId.Text);
            cmd.Parameters.AddWithValue("@nombre", textNombre.Text);
            cmd.Parameters.AddWithValue("@ap_paterno", textAp_paterno.Text);
            cmd.Parameters.AddWithValue("@ap_materno", textAp_materno.Text);
            cmd.Parameters.AddWithValue("@direccion", textDireccion.Text);
            cmd.ExecuteNonQuery();
            //Cierra la base datos
            cn.Close();
            MessageBox.Show("Los datos fueron actualizados correctamente :)");
            dataGridView1.DataSource = llenar_grid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textNombre.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textAp_paterno.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textAp_materno.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textDireccion.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch
            {

            }
        }

        //Eliminar registros
        private void button3_Click(object sender, EventArgs e)
        {
            //Abre la base de datos
            cn.Open();
            string eliminar = "DELETE FROM persona WHERE id=@id";
            //Instancia
            MySqlCommand cmd = new MySqlCommand(eliminar, cn);
             //Parametro
            cmd.Parameters.AddWithValue("@id", textId.Text);
            cmd.ExecuteNonQuery();
            //Cierra la base datos
            cn.Close();
            MessageBox.Show("Los datos fueron eliminados correctamente :)");
            dataGridView1.DataSource = llenar_grid();
        }

        //Limpiar
        private void button4_Click(object sender, EventArgs e)
        {
            textId.Clear();
            textNombre.Clear();
            textAp_paterno.Clear();
            textAp_materno.Clear();
            textDireccion.Clear();
        }
    }
}
