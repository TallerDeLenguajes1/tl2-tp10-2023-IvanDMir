
using tl2_tp10_2023_IvanDMir.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

public interface IUsuarioRepository {
    void Crear(Usuario Usuario);
    void Modificar(int id, Usuario Usuario);
    List<Usuario> GetAll();
    Usuario GetById(int id);
    void eliminar(int id);
}

namespace tl2_tp10_2023_IvanDMir.repositorios{
    public class UsuarioRepositorio:IUsuarioRepository{
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
                        var usuario = new Usuario();
                        usuario.id_usuario = Convert.ToInt32(reader["id"]);
                        usuario.nombre_De_Usuario = reader["nombre_de_usuario"].ToString();
                        usuario.contrasena = reader["contrasena"].ToString();
                        usuario.rol = (Roles)Convert.ToInt32(reader["rol"]);
                        
                        Usuarios.Add(usuario);
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
            command.CommandText = "UPDATE usuario SET (nombre_de_usuario) = @nombre_de_usuario, (contrasena) = @contrasena WHERE id = @Id";
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuarioModificado.nombre_De_Usuario));
            command.Parameters.Add(new SQLiteParameter("@Id", usuarioModificado.id_usuario));
            command.Parameters.Add(new SQLiteParameter("@contrasena", usuarioModificado.contrasena));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();       
        }
        public void Crear(Usuario usuarioCreado){
         var query = $"INSERT INTO usuario (nombre_de_usuario,contrasena,rol) VALUES (@nombre_de_usuario,@contrasena,@rol)";
         using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuarioCreado.nombre_De_Usuario));
            command.Parameters.Add(new SQLiteParameter("@contrasena", usuarioCreado.contrasena));
            command.Parameters.Add(new SQLiteParameter("@rol", usuarioCreado.rol));
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
                        usuarioBuscado.contrasena = reader["contrasena"].ToString();
                        usuarioBuscado.rol =(Roles) Convert.ToInt32(reader["rol"]);
                }
            }
            connection.Close();

            return (usuarioBuscado);
        }
    }

}