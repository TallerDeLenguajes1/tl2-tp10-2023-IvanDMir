using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace repositorios{
    public class TableroRepositorios{

         private string cadenaConexion = "Data Source=DB/Tareas.db;Cache=Shared";

          public void Crear(Tablero tablero){
         var query = $"INSERT INTO Tablero (nombre,descripcion,id_usuario_propietario) VALUES (@nombre,@desc,@idUsuario) ";
         using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@desc", tablero.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@idUsuario", tablero.IdUsuarioPropietario));
            
            command.ExecuteNonQuery();
            connection.Close();
         }
          }
         public List<Tablero>  GetAll(){
             {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> Tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tablero.IdUsuarioPropietario =reader["id_usuario_propietario"] == DBNull.Value ? null : Convert.ToInt32(reader["id_usuario_propietario"]);
                        Tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return Tableros;
        }
        }
         public void Update(int id, Tablero tablero) {
            string queryText = "UPDATE Tablero SET nombre = @nombre, descripcion = @descripcion, Id_Usuario_Propietario = @IdUsuarioPropietario " + 
                                "WHERE id = @id";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                query.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                query.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                query.Parameters.Add(new SQLiteParameter("@Id_Usuario_Propietario", tablero.IdUsuarioPropietario));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
         }
         public Tablero GetById(int id) {
            string queryText = "SELECT * FROM Tablero WHERE id = @id";
            Tablero tablero = new Tablero();
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                using(SQLiteDataReader reader = query.ExecuteReader()) {
                    while(reader.Read()) {
                        tablero.IdTablero= Convert.ToInt32(reader["id"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString();
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["Id_Usuario_Propietario"]);
                    }
                }
                connection.Close();
            }
            return tablero;
        }
         public List<Tablero> GetByUser(int userId) {
            string queryText = "SELECT * FROM Tablero WHERE Id_Usuario_Propietario = @id";
            List<Tablero> tableros = new List<Tablero>();
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", userId));
                connection.Open();
                using(SQLiteDataReader reader = query.ExecuteReader()) {
                    while(reader.Read()) {
                        var tablero = new Tablero();
                            tablero.IdTablero = Convert.ToInt32(reader["id"]);
                            tablero.Nombre = reader["nombre"].ToString();
                            tablero.Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString();
                            tablero.IdUsuarioPropietario = Convert.ToInt32(reader["board_owner_id"]);
                        
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
    
            return tableros;
        }
         public void Delete(int id) {
            string queryText = "DELETE FROM Tablero WHERE id = @id";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
         }
    }
}

