using System;
using System.Collections.Generic;
using System.Text;

namespace CModels.INV_JAC
{
    public class E_Chasis
    {
        public int j_cod_marca { get; set; }
        public string j_nro_factura { get; set; }
        public string j_tido_doc_factura { get; set; }
        public DateTime j_fecha_factura { get; set; }
        public string j_concesionario_asignado { get; set; }
        public string j_clase { get; set; }
        public string j_sub_clase { get; set; }
        public string j_estado { get; set; }
        public int j_clasificacion_exonerado { get; set; }
        public string j_codigo_interno { get; set; }
    }
}
