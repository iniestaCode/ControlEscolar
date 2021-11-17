using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CE.Entidades;

namespace CE.Datos
{
    public class DatosUsuario
    {
            
        public Request<List<Usuario>> GetUsuarios()
        {
            try
            {
                using(DBConnection db = new DBConnection())
                {
                    List<Usuario> listaUsuarios = db.Usuario.ToList();
                    return new Request<List<Usuario>>() { Mensaje = "Se encontraron estos usuarios", Respuesta = listaUsuarios };
                }
            }catch(Exception ex)
            {
                return new Request<List<Usuario>>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Usuario> GetUsuario(string usuario)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Usuario usr= db.Usuario.First(u => u.Usuario1 == usuario);
                    return new Request<Usuario>() { Mensaje = "Se encontró el usuario", Respuesta = usr };
                }
            }catch(Exception ex)
            {
                return new Request<Usuario>() { Exito = false, Error=ex.Message };
            }
        }

        public Request<Usuario> CreateUsuario(string usuario, string password, int ID_Rol)
        {
            try
            {
                using(DBConnection db = new DBConnection())
                {                                     
                    Usuario usuarioE = new Usuario() { Usuario1 = usuario, Contrasenia=Encriptar(password), FK_ID_Rol = ID_Rol };
                    db.Usuario.Add(usuarioE);
                    db.SaveChanges();
                    return new Request<Usuario> { Mensaje = "Se registró el usuario con éxito", Respuesta = usuarioE };
                }
            }catch(Exception ex)
            {
                return new Request<Usuario>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Usuario> UpdateUsuario(string usuario)
        {
            try
            {
                using(DBConnection db = new DBConnection())
                {
                    Usuario user = db.Usuario.First(u => u.Usuario1==usuario);
                    db.Usuario.Attach(user);
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new Request<Usuario>() { Mensaje = "Se modificó el usuario con éxito", Respuesta=user };
                }
            }catch(Exception ex)
            {
                return new Request<Usuario>() { Exito = false, Error = ex.Message };
            }
        }
        public Request<Usuario> ChangeContrasenia (string usuario, string password)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Usuario user = db.Usuario.First(u => u.Usuario1 == usuario);
                    user.Contrasenia = Encriptar(password);
                    db.Usuario.Attach(user);
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new Request<Usuario>() { Mensaje = "Se modificó la contraseña con éxito", Respuesta = user };
                }
            }
            catch (Exception ex)
            {
                return new Request<Usuario>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Usuario> DeleteUsuario(string usuario)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Usuario user = db.Usuario.First(u => u.Usuario1 == usuario);
                    db.Usuario.Remove(user);
                    db.SaveChanges();
                    return new Request<Usuario>() {  Respuesta = user };
                }
            }
            catch (Exception ex)
            {
                return new Request<Usuario>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Usuario> Login (string usuario, string password)
        {
            try
            {
                using(DBConnection db = new DBConnection())
                {
                    Usuario user = db.Usuario.First(u => u.Usuario1 == usuario);
                    if(user != null)
                    {
                        if(user.Contrasenia == Encriptar(password))
                        {
                            return new Request<Usuario>() { Mensaje = "Bienvenido", Respuesta = user };
                        }
                        else
                        {
                            return new Request<Usuario>() { Exito = false, Mensaje = "Datos incorrectos", Respuesta = user };
                        }
                    }
                    else
                    {
                        return new Request<Usuario>() { Exito = false, Mensaje = "El usuario no existe" };
                    }
                }
            }catch(Exception ex)
            {
                return new Request<Usuario>() { Exito = false, Error = ex.Message };
            }
        }
      
        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted =
            System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        
        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted =
            Convert.FromBase64String(_cadenaAdesencriptar);
            //result = 
            System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
