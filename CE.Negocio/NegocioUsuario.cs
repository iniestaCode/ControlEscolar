using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CE.Datos;
using Datos;
using CE.Entidades;
namespace CE.Negocio
{
    public class NegocioUsuario
    {

        readonly DatosUsuario datosUsuario = new DatosUsuario();

        public Request<List<Usuario>> GetUsuarios => datosUsuario.GetUsuarios();

        public Request<Usuario> GetUsuario(string username) => datosUsuario.GetUsuario(username);

        public Request<Usuario> CreateUsuario(string usuario, string password, int ID_Rol) => datosUsuario.CreateUsuario(usuario, password, ID_Rol);

        public Request<Usuario> UpdateUsuario(string usuario) => datosUsuario.UpdateUsuario(usuario);

        public Request<Usuario> DeleteUsuario(string usuario) => datosUsuario.DeleteUsuario(usuario);

        public Request<Usuario> ChangeContrasenia(string usuario, string password) => datosUsuario.ChangeContrasenia(usuario, password);

        public Request<Usuario> Login(string usuario, string password) => datosUsuario.Login(usuario, password);



    }
}
