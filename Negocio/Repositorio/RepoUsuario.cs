using Datos;
using Negocio.Interfaces;
using Negocio.Modelos;
using System.Linq;


namespace Negocio.Repositorio
{
    public class RepoUsuario : IRepoUsuario
    {
        public ModeloUsuario BuscarUsuario(int legajo)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {

                return MapearDB(db.Usuario.Find(legajo));

            }
        }

        private ModeloUsuario MapearDB(Usuario tabla)
        {
            if (tabla == null)
            {
                return null;
            }

            return new ModeloUsuario()
            {
                Legajo = tabla.Legajo,
                Nombre_Apellido = tabla.Nombre_Apellido,
                Email = tabla.Email,
                Contraseña = tabla.Contraseña,
                Rol = tabla.Rol,

            };
        }

        ModeloUsuario IRepoUsuario.ObtenerUsuario(string username, string password)
        {

            using (var context = new TFI_ControlCalidadEntities())
            {
                var usuario = context.Usuario.FirstOrDefault(u => u.Email == username && u.Contraseña == password);
                var user = new ModeloUsuario
                {
                    Email = usuario.Email,
                    Contraseña = usuario.Contraseña,
                    Nombre_Apellido = usuario.Nombre_Apellido,
                    Rol = usuario.Rol,
                    Legajo = usuario.Legajo,


                };
                return user;
            }



        }

        // Validaciones
        public bool ComprobarDisponibilidad(int legajo)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                // Se busca una orden de producción que tenga al usuario con el legajo proporcionado asignado
                var OP = db.Orden_Produccion.FirstOrDefault(op => op.Usuario.Legajo == legajo && op.Estado == "En_Proceso");

                // Si se encuentra una orden de producción asignada al usuario, no se puede crear otra
                if (OP != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }




    }

}
