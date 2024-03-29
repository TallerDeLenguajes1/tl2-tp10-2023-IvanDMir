using tl2_tp10_2023_IvanDMir.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace tl2_tp10_2023_IvanDMir.repositorios{
public interface ITareasRepositorio {
    void Crear(Tarea Tarea);
    void Update(int id, Tarea Tarea);
    List<Tarea> GetByAsignado(int id);
    Tarea GetById(int id);
    List<Tarea> GetByUser(int userId);
    List<Tarea> GetByTablero(int TableroId);
    List<Tarea> GetAll();
    void Delete(int id);
    void Asignar(int userId, int tareaId);
}

    public class TareaRepositorio:ITareasRepositorio{

         private string cadenaConexion ;

         public TareaRepositorio(){}
          public TareaRepositorio(string CadenaConexion)
    {
        this.cadenaConexion = CadenaConexion;
    }

         public List<Tarea>  GetAll(){
             {
            var queryString = @"SELECT * FROM tarea;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id= Convert.ToInt32(reader["id"]);
                        tarea.IdTablero =Convert.ToInt32(reader["id_tablero"]);
                        tarea.Estado = (Estados) Convert.ToInt32(reader["estado"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString();
                        tarea.Color = reader["color"] == DBNull.Value ? null : reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["Id_Usuario_asignado"]);
                        tarea.NombreTablero = ObtenerTablero(tarea.IdTablero);
                        tarea.nombreAsignado = ObtenerUsuarioAsignado(tarea.IdUsuarioAsignado);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
              if (tareas==null){ 
            throw new Exception("No hay tareas.");
              }
            return tareas;
            
        }
        }

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
     public void Update(int id, Tarea tarea) {
    string queryText = "UPDATE tarea SET id_tablero = @id_tablero, nombre = @nombre, estado = @estado, descripcion = @desc, color = @color, id_usuario_asignado = @idUsuario " +
                       "WHERE id = @id";
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
        SQLiteCommand query = new SQLiteCommand(queryText, connection);
        query.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));
        query.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
        query.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
        query.Parameters.Add(new SQLiteParameter("@desc", tarea.Descripcion));
        query.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
        query.Parameters.Add(new SQLiteParameter("@idUsuario", tarea.IdUsuarioAsignado));
        query.Parameters.Add(new SQLiteParameter("@id", id));
        connection.Open();
        query.ExecuteNonQuery();
        connection.Close();
        }
     }

     public List<Tarea> GetByAsignado(int id) {
        string queryText = "SELECT * FROM tarea WHERE id_usuario_asignado = @id";
        List<Tarea> tareas = new List<Tarea>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
            SQLiteCommand query = new SQLiteCommand(queryText, connection);
            query.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using(SQLiteDataReader reader = query.ExecuteReader()) {
                while(reader.Read()) {
                    var tarea = new Tarea() {
                        Id = Convert.ToInt32(reader["id"]),
                        IdTablero = Convert.ToInt32(reader["id_tablero"]),
                        Nombre = reader["nombre"].ToString(),
                        Estado = (Estados)Convert.ToInt32(reader["estado"]),
                        Descripcion = reader["descripcion"].ToString(),
                        Color = reader["color"].ToString(),
                        IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]),
                        nombreAsignado = ObtenerUsuarioAsignado(Convert.ToInt32(reader["id_usuario_asignado"])),
                        NombreTablero = ObtenerTablero(Convert.ToInt32(reader["id_tablero"]))

                    };
                    tareas.Add(tarea);
                }
            }
            connection.Close();
        }
        return tareas;
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
                        tarea.NombreTablero = ObtenerTablero(tarea.IdTablero);
                        tarea.nombreAsignado = ObtenerUsuarioAsignado(tarea.IdUsuarioAsignado);
                    }
                }
                connection.Close();
            }
              if (tarea==null){ 
            throw new Exception("Tarea no existente.");
              }
            return tarea;
          }
    
         public List<Tarea> GetByUser(int IdUsuario) {
            string queryText = "SELECT t.id, t.nombre, t.descripcion, color, estado, t.id_tablero, id_usuario_asignado FROM tarea t " +
                            "INNER JOIN tablero b ON t.id_tablero = b.id_Tablero WHERE id_usuario_propietario = @id";
               List<Tarea> tareas = new List<Tarea>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
            SQLiteCommand query = new SQLiteCommand(queryText, connection);
            query.Parameters.Add(new SQLiteParameter("@id", IdUsuario));
            connection.Open();
            using(SQLiteDataReader reader = query.ExecuteReader()) {
                while(reader.Read()) {
                    var tarea = new Tarea() {
                        Id = Convert.ToInt32(reader["id"]),
                        IdTablero = Convert.ToInt32(reader["id_tablero"]),
                        Nombre = reader["nombre"].ToString(),
                        Estado = (Estados)Convert.ToInt32(reader["estado"]),
                        Descripcion = reader["descripcion"].ToString(),
                        Color = reader["color"].ToString(),
                        IdUsuarioAsignado =Convert.ToInt32(reader["id_usuario_asignado"]),
                        nombreAsignado = ObtenerUsuarioAsignado(Convert.ToInt32(reader["id_usuario_asignado"])),
                        NombreTablero = ObtenerTablero(Convert.ToInt32(reader["id_tablero"]))
                    };
                    tareas.Add(tarea);
                }
            }
            connection.Close();
            }
             if (tareas==null){ 
            throw new Exception("Tareas no existentes.");
              }
            return tareas;
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
                            tarea.Color = reader["color"].ToString();
                            tarea.IdUsuarioAsignado = Convert.ToInt32(reader["Id_Usuario_asignado"]);
                             tarea.NombreTablero = ObtenerTablero(tarea.IdTablero);
                        tarea.nombreAsignado = ObtenerUsuarioAsignado(tarea.IdUsuarioAsignado);
                            
                            Tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
             if (Tareas==null){ 
            throw new Exception("Tareas no existentes.");
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
           public void Asignar(int userId, int tareaId) {
        string queryText = "UPDATE tarea SET id_usuario_asignado = @id_usuario_asignado WHERE id = @id";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)) {
            SQLiteCommand query = new SQLiteCommand(queryText, connection);
            query.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", userId));
            query.Parameters.Add(new SQLiteParameter("@id", tareaId));
            connection.Open();
            int rowsAffected = query.ExecuteNonQuery();
            connection.Close();
        }
    }
    private string ObtenerUsuarioAsignado(int id){ 
        UsuarioRepositorio repo = new UsuarioRepositorio(cadenaConexion);
        Usuario usu = repo.GetById(id);
        return usu.nombre_De_Usuario;
    }
    private string ObtenerTablero(int id){ 
        TableroRepositorio repo = new TableroRepositorio(cadenaConexion);
        Tablero tab = repo.GetById(id);
        return tab.Nombre;


    
    }
         

    }
    
}