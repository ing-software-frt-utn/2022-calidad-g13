using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ModelModelo
    {
        public int SKU { get; set; }

        [DisplayName("Modelo")]
        public string Denominacion { get; set; }

        [DisplayName("Limite Superior Observado")]
        public int LimiteSuperiorObs { get; set; }

        [DisplayName("Limite Inferior Observado")]
        public int LimiteInferiorObs { get; set; }

        [DisplayName("Limite Superior Reproceso")]
        public int LimiteSuperiorRep { get; set; }

        [DisplayName("Limite Inferior Reproceso")]
        public int LimiteInferiorRep { get; set; }


        public List<ModeloColor> Colores { get; set; }  



    }
}
