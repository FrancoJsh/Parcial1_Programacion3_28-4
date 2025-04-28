using Microsoft.Data.SqlClient;
using Parcial1_Programacion3_28_4.Models;

namespace Parcial1_Programacion3_28_4.Data
{
    public class PacienteDatos
    {
        private string conexionString = "Data Source=TOJESHO\\SQLEXPRESS;Initial Catalog=parcial1Programacion3;Integrated Security=True; Trust Server Certificate=true";

        public List<Paciente> ListarPacientes()
        {
            string query = "select * from Pacientes";
            List<Paciente> listaPacientes = new List<Paciente>();
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                SqlCommand command = new SqlCommand(query, conexion);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listaPacientes.Add(new Paciente
                    {
                        NombrePaciente = reader["NombrePaciente"].ToString(),
                        ObraSocialId = (int)reader["ObraSocialId"],
                        Edad = (int)reader["Edad"],
                        Sintomas = reader["Sintomas"].ToString(),
                        ObraSocialElegida = ListarObrasSociales((int)reader["ObraSocialId"]).FirstOrDefault()
                    });
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return listaPacientes;

        }

        public List<ObraSocial> ListarObrasSociales(int idObra)
        {
            string query = "select * from ObraSociales";
            if (idObra > 0)
                query += $" where Id = {idObra}";
            List<ObraSocial> listaObrasSociales = new List<ObraSocial>();
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                SqlCommand command = new SqlCommand(query, conexion);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listaObrasSociales.Add(new ObraSocial
                    {
                        Id = (int)reader["Id"],
                        NombreObraSocial = reader["NombreObraSocial"].ToString()
                    });
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return listaObrasSociales;
        }

        public string CrearPaciente(Paciente paciente)
        {
            string Validación = "";


            if (string.IsNullOrEmpty(paciente.NombrePaciente) || string.IsNullOrEmpty(paciente.Sintomas) || paciente.Edad < 0)
            
                return Validación = "Ingreso incorrecto de datos";
            
            
            string query = $"insert into Pacientes (NombrePaciente, ObraSocialId, Edad, Sintomas) values ('{paciente.NombrePaciente}', {paciente.ObraSocialId}, {paciente.Edad}, '{paciente.Sintomas}')";
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                SqlCommand command = new SqlCommand(query, conexion);
                command.ExecuteNonQuery();
                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public List<CantidadInscriptosObraSocial> ListaInscriptos()
        {
            List<CantidadInscriptosObraSocial> lista = new List<CantidadInscriptosObraSocial>();
            string query = "select ObraSociales.NombreObraSocial, count(*) as CantidadPacientes\r\nfrom Pacientes \r\njoin ObraSociales on Pacientes.ObraSocialId = ObraSociales.Id\r\n group by ObraSociales.NombreObraSocial";
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                SqlCommand command = new SqlCommand(query, conexion);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new CantidadInscriptosObraSocial
                    {
                        NombreObraSocial = reader["NombreObraSocial"].ToString(),
                        CantidadPacientes = (int)reader["CantidadPacientes"]
                    });
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return lista;   

        }

    }
}
