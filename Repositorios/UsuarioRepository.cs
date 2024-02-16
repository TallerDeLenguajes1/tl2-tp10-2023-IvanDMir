
using tl2_tp10_2023_IvanDMir.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace tl2_tp10_2023_IvanDMir.repositorios{
public interface IUsuarioRepositorio {
    void Crear(Usuario Usuario);
    void Modificar(int id, Usuario Usuario);
    List<Usuario> GetAll();
    Usuario GetById(int id);
    Usuario GetByUsuario(string nombreUsuario);
    void eliminar(int id);
    Usuario Existe(string usuario, string contrasena);
    
}


    public class UsuarioRepositorio:IUsuarioRepositorio{
        private string cadenaConexion;



         public UsuarioRepositorio(string CadenaConexion)
    {
        this.cadenaConexion = CadenaConexion;
    }

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
            if(Usuarios == null){
                throw new Exception("No hay usuarios creados");
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
            connection.Open();
            using (SQLiteCommand comando = new SQLiteCommand("PRAGMA  foreign_keys=1;",connection)){
                        comando.ExecuteNonQuery();
                }
      
            command.Parameters.Add(new SQLiteParameter("@Id",id));
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
            if(usuarioBuscado == null){
                throw new Exception("No existe tal usuario");
            }
            return (usuarioBuscado);
            
        }
           public Usuario GetByUsuario(string usuario) {
        string queryText = "SELECT * FROM usuario WHERE nombre_de_usuario = @usuario";
        Usuario usu = new Usuario();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
            SQLiteCommand query = new SQLiteCommand(queryText, connection);
            query.Parameters.Add(new SQLiteParameter("@usuario", usuario));
            connection.Open();
            using(SQLiteDataReader reader = query.ExecuteReader()) {
                if(reader.Read()) {
                    usu.id_usuario = Convert.ToInt32(reader["id"]);
                    usu.nombre_De_Usuario = reader["nombre_de_usuario"].ToString();
                    usu.rol = (Roles)Convert.ToInt32(reader["rol"]);
                    usu.contrasena = reader["contrasena"].ToString();
                } else {
                    throw new Exception("Usuario no encontrado");
                }
            }
            connection.Close();
        }
        return usu;
    }
         public Usuario Existe(string usuario,string contrasena){
            string Query = "SELECT * FROM usuario WHERE nombre_de_usuario = @nombreDeUsuario AND contrasena = @contrasena";
            Usuario existencia = new Usuario();
           using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){ 
            SQLiteCommand query = new SQLiteCommand(Query, connection);
            query.Parameters.Add(new SQLiteParameter("@nombreDeUsuario", usuario));
            query.Parameters.Add(new SQLiteParameter("@contrasena", contrasena));
         connection.Open();
            using(SQLiteDataReader reader = query.ExecuteReader()) {
                if(reader.Read()) {
                    existencia.id_usuario = Convert.ToInt32(reader["id"]);
                    existencia.nombre_De_Usuario = reader["nombre_de_usuario"].ToString();
                    existencia.rol = (Roles)Convert.ToInt32(reader["rol"]);
                    existencia.contrasena = reader["contrasena"].ToString();
                } else {
                    throw new Exception("Los campos no coinciden");
                }
            }
            connection.Close();
        }
        return existencia;
         }
    }

}