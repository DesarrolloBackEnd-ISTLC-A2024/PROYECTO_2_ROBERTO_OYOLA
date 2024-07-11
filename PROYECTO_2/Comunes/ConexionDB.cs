using Microsoft.AspNetCore.Hosting.Server;
using PROYECTO_2.Model;
using System.Data;
using System.Data.SqlClient;

namespace PROYECTO_2.Comunes
{
    public class ConexionDB
    {
        public static SqlConnection conexion;

        public static SqlConnection abrirConexion()
        {
            conexion = new SqlConnection("Server = LancelotPC; Database = PROYECTO_2; Trusted_Connection = True;");
            conexion.Open();
            return conexion;
        }

        #region Cliente
        public static List<Cliente> GetClientes()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandText = "SP_GET_CLIENTES";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return llenarClientes(dataSet.Tables[0]);
        }

        public static Cliente GetCliente(String cedula)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandText = "SP_GET_CLIENTE";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PV_CEDULA", cedula);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return llenarClientes(dataSet.Tables[0])[0];
        }

        public static void PostCliente(Cliente objCliente)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandText = "SP_INS_CLIENTES";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PV_CEDULA", objCliente.cedula);
            cmd.Parameters.AddWithValue("@PV_NOMBRES", objCliente.nombre);
            cmd.ExecuteNonQuery();
        }

        public static void PutCliente(string cedula, Cliente objCliente)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandText = "SP_UPD_CLIENTES";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PV_CEDULA", cedula);
            cmd.Parameters.AddWithValue("@PV_NOMBRES", objCliente.nombre);
            cmd.ExecuteNonQuery();
        }

        public static void DeleteCliente(string cedula)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandText = "SP_DEL_CLIENTES";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PV_CEDULA", cedula);
            cmd.ExecuteNonQuery();
        }

        private static List<Cliente> llenarClientes(DataTable dataTable)
        {
            List<Cliente> lRespuesta = new List<Cliente>();
            Cliente objeto = new Cliente();
            foreach (DataRow dr in dataTable.Rows)
            {
                objeto = new Cliente();
                objeto.cedula = dr["CEDULA"].ToString();
                objeto.nombre = dr["NOMBRE"].ToString();
                objeto.estado = dr["ESTADO"].ToString();
                lRespuesta.Add(objeto);
            }
            return lRespuesta;
        }

        #endregion

        #region Producto

        public static List<Producto> GetProductos()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandText = "SP_GET_PRODUCTOS";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return llenarProductos(dataSet.Tables[0]);
        }

        public static Producto GetProducto(string codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandText = "SP_GET_PRODUCTO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PV_CODIGO", codigo);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return llenarProductos(dataSet.Tables[0])[0];
        }

        public static void PostProducto(Producto objProducto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandText = "SP_INS_PRODUCTOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PV_CODIGO", objProducto.codigo);
            cmd.Parameters.AddWithValue("@PV_DESCRIPCION", objProducto.descripcion);
            cmd.Parameters.AddWithValue("@PF_PRECIO", objProducto.precio);
            cmd.ExecuteNonQuery();
        }

        public static void PutProducto(string codigo, Producto objProducto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandText = "SP_UPD_PRODUCTOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PV_CODIGO", codigo);
            cmd.Parameters.AddWithValue("@PV_DESCRIPCION", objProducto.descripcion);
            cmd.Parameters.AddWithValue("@PF_PRECIO", objProducto.precio);
            cmd.ExecuteNonQuery();
        }

        public static void DeleteProducto(string codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandText = "SP_DEL_PRODUCTOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PV_CODIGO", codigo);
            cmd.ExecuteNonQuery();
        }

        private static List<Producto> llenarProductos(DataTable dataTable)
        {
            List<Producto> lRespuesta = new List<Producto>();
            Producto objeto = new Producto();
            foreach (DataRow dr in dataTable.Rows)
            {
                objeto = new Producto();
                objeto.id_producto = Convert.ToInt32(dr["ID_PRODUCTO"]);
                objeto.codigo = dr["CODIGO"].ToString();
                objeto.descripcion = dr["DESCRIPCION"].ToString();
                objeto.precio = Convert.ToDecimal(dr["PRECIO"]);
                objeto.estado = dr["ESTADO"].ToString();
                lRespuesta.Add(objeto);
            }
            return lRespuesta;
        }

        #endregion

    }
}
