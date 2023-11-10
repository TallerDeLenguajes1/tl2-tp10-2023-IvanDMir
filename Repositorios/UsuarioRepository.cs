
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace repositorios{
    public class UsuarioRepositorio{
        private string cadenaConexion = "Data Source=DB/Tareas.db;Cache=Shared";

        public List<Usuario>  GetAll(){
             {
            var queryString = @"SELECT * FROM usuario;";
            List<Usuario> Usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Usuario = new Usuario();
                        Usuario.id_usuario = Convert.ToInt32(reader["id"]);
                        Usuario.nombre_De_Usuario = reader["nombre_de_usuario"].ToString();
                        Usuarios.Add(Usuario);
                    }
                }
                connection.Close();
            }
            return Usuarios;
        }
        }


        public void Modificar(int id, Usuario usuarioModificado){
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE usuario SET (nombre_de_usuario) = @nombre_de_usuario WHERE id = @Id";
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuarioModificado.nombre_De_Usuario));
            command.Parameters.Add(new SQLiteParameter("@Id", usuarioModificado.id_usuario));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();       
        }
        public void Crear(Usuario usuarioCreado){
         var query = $"INSERT INTO usuario (nombre_de_usuario) VALUES (@nombre_de_usuario)";
         using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuarioCreado.nombre_De_Usuario));

            command.ExecuteNonQuery();
            connection.Close();
         }
    }
    public void eliminar(int id){

            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM usuario WHERE id = @Id;";
            command.Parameters.Add(new SQLiteParameter("@Id",id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
    }

      public Usuario GetById(int id_usuario)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var usuarioBuscado = new Usuario();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM usuario WHERE id = @Id";
            command.Parameters.Add(new SQLiteParameter("@Id", id_usuario));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   
                        usuarioBuscado.id_usuario = Convert.ToInt32(reader["id"]);
                        usuarioBuscado.nombre_De_Usuario = reader["nombre_de_usuario"].ToString();
                }
            }
            connection.Close();

            return (usuarioBuscado);
        }
    }
}
