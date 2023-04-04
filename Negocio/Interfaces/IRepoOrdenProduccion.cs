using Datos;
using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Interfaces
{
    public interface IRepoOrdenProduccion
    {
        void IniciarOP(ModeloOP op);
        List<ModeloOP> ListarOP();
        //void ActualizarOP(ModeloOP op, ModeloJornadaLaboral jornada);
        ModeloOP BuscarOP(int num_op);
        bool ComprobarUnicidad(int num_op);
        void IniciarJornada(ModeloJornadaLaboral jornada);
        void PausarOP(int numero_op);
        void ReanudarOP(int numero_op, int legajo);
        void FinalizarOP(int numero_op);
        List<ModeloOP> BuscarOP_EP();
        Orden_Produccion BuscarOPPorJornada(int Id_jornada);
        //int ObtenerOPAsociada(int idUsuario);
    }
}
