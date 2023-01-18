using MySqlConnector;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string nombre = txtNombre.Text;
            string paterno = txtPaterno.Text;
            string materno = txtMaterno.Text;
            Double sueldo = double.Parse(txtSueldo.Text);
            String fechaingreso = txtFechaIngreso.Text;

            string sql1 = "INSERT INTO usuarios (Login, Nombre, Paterno, Materno) VALUES ('" + login + "', '" + nombre + "', '" + 
                paterno + "', '" + materno + "')" ;
            string sql2 = "INSERT INTO empleados (Sueldo, FechaIngreso) VALUES ('" + sueldo + "', '" + fechaingreso + "')";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando1 = new MySqlCommand(sql1, conexionBD);
                MySqlCommand comando2 = new MySqlCommand(sql2, conexionBD);
                comando1.ExecuteNonQuery();
                comando2.ExecuteNonQuery();
                MessageBox.Show("Registro Guardado ");
                limpiar();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al guardar " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            String login = txtLogin.Text;
            MySqlDataReader reader= null;

            string sql1 = "SELECT Login, Nombre, Paterno, Materno, Sueldo, FechaIngreso FROM usuarios INNER JOIN empleados ON usuarios.userId = empleados.userId WHERE Login LIKE '"+ login +"' LIMIT 1 ";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();


            try
            {
                MySqlCommand comando1 = new MySqlCommand (sql1, conexionBD);
                reader = comando1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtCodigo.Text = reader.GetString(0);
                        txtNombre.Text = reader.GetString(1);
                        txtPaterno.Text = reader.GetString(2);
                        txtMaterno.Text = reader.GetString(3);
                        txtSueldo.Text = reader.GetDouble(4).ToString();
                        txtFechaIngreso.Text = reader.GetDateTime(5).ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string login = txtLogin.Text;
            string nombre = txtNombre.Text;
            string paterno = txtPaterno.Text;
            string materno = txtMaterno.Text;
            double sueldo = double.Parse(txtSueldo.Text);
            String fechaingreso = txtFechaIngreso.Text;

            string sql1 = "UPDATE usuarios INNER JOIN empleados ON usuarios.userId = empleados.userId SET Login='" + login + "', Nombre= '" + nombre + "', Paterno= '" + paterno + "', Materno= '" + materno + "', Sueldo= '"+ sueldo +"', FechaIngreso= '"+ fechaingreso +"' WHERE Login= '"+ login +"' ";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando1 = new MySqlCommand(sql1, conexionBD);
                comando1.ExecuteNonQuery();
                MessageBox.Show("Registro Actualizado ");
                limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Actializar " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;

            string sql1 = "DELETE usuarios, empleados FROM usuarios INNER JOIN empleados ON usuarios.userId = empleados.userId WHERE Login= '" + login + "' ";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando1 = new MySqlCommand(sql1, conexionBD);
                comando1.ExecuteNonQuery();
                MessageBox.Show("Registro Eliminado ");
                limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            txtLogin.Text = "";
            txtNombre.Text = "";
            txtPaterno.Text = "";
            txtMaterno.Text = "";
            txtSueldo.Text = "";
            txtFechaIngreso.Text = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            SLDocument sl = new SLDocument();

            int celdaCabecera = 4;

            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Usuarios");
            sl.SetCellValue("B" + celdaCabecera, "Login");
            sl.SetCellValue("C" + celdaCabecera, "Nombre");
            sl.SetCellValue("D" + celdaCabecera, "Paterno");
            sl.SetCellValue("E" + celdaCabecera, "Materno");
            sl.SetCellValue("F" + celdaCabecera, "Sueldo");
            sl.SetCellValue("G" + celdaCabecera, "FechaIngreso");

            string sql = "SELECT Login, Nombre, Paterno, Materno, Sueldo, FechaIngreso FROM usuarios INNER JOIN empleados ON usuarios.userId = empleados.userId";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            MySqlCommand comando = new MySqlCommand(sql, conexionBD);
            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                celdaCabecera++;
                sl.SetCellValue("B" + celdaCabecera, reader["Login"].ToString());
                sl.SetCellValue("C" + celdaCabecera, reader["Nombre"].ToString());
                sl.SetCellValue("D" + celdaCabecera, reader["Paterno"].ToString());
                sl.SetCellValue("E" + celdaCabecera, reader["Materno"].ToString());
                sl.SetCellValue("F" + celdaCabecera, reader["Sueldo"].ToString());
                sl.SetCellValue("G" + celdaCabecera, reader["FechaIngreso"].ToString());

            }


            sl.SaveAs("Excel.xlsx");
        }
    }
}
