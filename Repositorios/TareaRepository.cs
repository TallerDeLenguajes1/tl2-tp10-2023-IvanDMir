using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace repositorios{
    public class TareaRepositorios{

         private string cadenaConexion = "Data Source=DB/Tareas.db;Cache=Shared";

         public void Crear(Tarea tarea){
         var query = $"INSERT INTO tarea (id_tablero,nombre,estado,descripcion,color,id_usuario_asignado) VALUES (@id_tablero,@nombre,@estado,@desc,@color,@idUsuario) ";
         using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            connection.Open();

            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@desc", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@idUsuario", tarea.IdUsuarioAsignado));
            
            command.ExecuteNonQuery();
            connection.Close();
         }
    }
     public void Update(int id,Tarea tarea) {
            string queryText = "UPDATE tarea SET id_tablero=@id_tablero=, nombre = @nombre,estado=@estado descripcion = @desc,color = @color Id_usuario_asginado = @IdUsuario " + 
                                "WHERE id = @id";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));
            query.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            query.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            query.Parameters.Add(new SQLiteParameter("@desc", tarea.Descripcion));
            query.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            query.Parameters.Add(new SQLiteParameter("@idUsuario", tarea.IdUsuarioAsignado));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
         }
          public Tarea GetById(int id) {
            string queryText = "SELECT * FROM tarea WHERE id = @id";
            Tarea tarea = new Tarea();
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                using(SQLiteDataReader reader = query.ExecuteReader()) {
                    while(reader.Read()) {
                        tarea.Id= Convert.ToInt32(reader["id"]);
                        tarea.IdTablero =Convert.ToInt32(reader["id_tablero"]);
                        tarea.Estado = (Estados) Convert.ToInt32(reader["estado"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString();
                        tarea.Color = reader["color"] == DBNull.Value ? null : reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["Id_Usuario_asignado"]);
                    }
                }
                connection.Close();
            }
            return tarea;
        }
         public List<Tarea> GetByUser(int userId) {
            string queryText = "SELECT * FROM tarea WHERE Id_Usuario_asignado = @id";
            List<Tarea> Tareas = new List<Tarea>();
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", userId));
                connection.Open();
                using(SQLiteDataReader reader = query.ExecuteReader()) {
                    while(reader.Read()) {
                            var tarea = new Tarea();
                            tarea.Id= Convert.ToInt32(reader["id"]);
                            tarea.IdTablero =Convert.ToInt32(reader["id_tablero"]);
                            tarea.Estado = (Estados) Convert.ToInt32(reader["estado"]);
                            tarea.Nombre = reader["nombre"].ToString();
                            tarea.Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString();
                            tarea.Color = reader["color"] == DBNull.Value ? null : reader["color"].ToString();
                            tarea.IdUsuarioAsignado = Convert.ToInt32(reader["Id_Usuario_asignado"]);
                            
                            Tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
    
            return Tareas;
        }
        public List<Tarea> GetByTablero(int TableroId) {
            string queryText = "SELECT * FROM tarea WHERE Id_tablero = @id";
            List<Tarea> Tareas = new List<Tarea>();
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", TableroId));
                connection.Open();
                using(SQLiteDataReader reader = query.ExecuteReader()) {
                    while(reader.Read()) {
                            var tarea = new Tarea();
                            tarea.Id= Convert.ToInt32(reader["id"]);
                            tarea.IdTablero =Convert.ToInt32(reader["id_tablero"]);
                            tarea.Estado = (Estados) Convert.ToInt32(reader["estado"]);
                            tarea.Nombre = reader["nombre"].ToString();
                            tarea.Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString();
                            tarea.Color = reader["color"] == DBNull.Value ? null : reader["color"].ToString();
                            tarea.IdUsuarioAsignado = Convert.ToInt32(reader["Id_Usuario_asignado"]);
                            
                            Tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
    
            return Tareas;
        }
 public void Delete(int id) {
            string queryText = "DELETE FROM tarea WHERE id = @id";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
         }
          public void UpdateUsuario(int idUsuario,Tarea tarea) {
            string queryText = "UPDATE tarea SETId_usuario_asginado = @IdUsuario " + 
                                "WHERE id = @id";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
            query.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
         }


    }
}