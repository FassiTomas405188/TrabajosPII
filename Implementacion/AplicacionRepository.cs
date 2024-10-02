using Practica02.Data;
using Practica02.Models;
using System.Data;
using System.Data.SqlClient;

namespace Practica02.Implementacion
{
    public class AplicacionRepository : IAplicacionRepository
    {
        private SqlConnection _connection;

        public AplicacionRepository()
        {
            _connection = new SqlConnection(Properties.Resources.Conexion);
        }
        public bool Delete(int id)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@codigo", id));
            int rows = DataHelper.GetInstance().ExecuteSPDML("SP_REGISTRAR_BAJA_ARTICULO", parameters);
            return rows == 1;
        }

        public List<Articulo> GetAll()
        {
            List<Articulo> lst = new List<Articulo>();
            var helper = DataHelper.GetInstance();
            var t = helper.ExecuteSPQuery("SP_RECUPERAR_ARTICULO", null);
            foreach (DataRow row in t.Rows)
            {
                int id = Convert.ToInt32(row["idProducto"]);
                string nombre = row["nombre"].ToString();
                int precio = Convert.ToInt32(row["precio"]);
                bool activo = Convert.ToBoolean(row["activo"]);

                Articulo articulo = new Articulo()
                {
                    IdProducto = id,
                    Nombre = nombre,
                    Precio = precio,
                    Activo = activo
                };
                lst.Add(articulo);
            }
            return lst;
        }

        public Articulo GetById(int id)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@idProducto", id));
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULO_POR_CODIGO", parameters);

            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                int codigo = Convert.ToInt32(row["idProducto"]);
                string nombre = row["nombre"].ToString();
                int precio = Convert.ToInt32(row["precio"]);
                bool activo = Convert.ToBoolean(row["activo"]);

                Articulo articulo = new Articulo()
                {
                    IdProducto = codigo,
                    Nombre = nombre,
                    Precio = precio,
                    Activo = activo
                };
                return articulo;

            }
            return null;
        }

        public bool Save(Articulo oArticulo)
        {
            bool result = true;
            string query = "SP_GUARDAR_ARTICULO";

            try
            {
                if (oArticulo != null)
                {
                    _connection.Open();
                    var cmd = new SqlCommand(query, _connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", oArticulo.IdProducto);
                    cmd.Parameters.AddWithValue("@nombre", oArticulo.Nombre);
                    cmd.Parameters.AddWithValue("@stock", oArticulo.Precio);
                    result = cmd.ExecuteNonQuery() == 1; 
                }
            }
            catch (SqlException sqlEx)
            {
                result = false;
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return result;
        }
    }
}
