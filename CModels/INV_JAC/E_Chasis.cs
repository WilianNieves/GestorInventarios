using System;
using System.Collections.Generic;
using System.Text;

namespace CModels.INV_JAC
{
    public class E_Chasis
    {
        public int d_cod_marca { get; set; }
        public string d_nro_factura { get; set; }
        public string d_tido_doc_factura { get; set; }
        public DateTime d_fecha_factura { get; set; }
        public string d_concesionario_asignado { get; set; }
        public string d_clase { get; set; }
        public string d_sub_clase { get; set; }
        public string d_estado { get; set; }
        public int d_clasificacion_exonerado { get; set; }
        public string d_codigo_interno { get; set; }
    }
}
