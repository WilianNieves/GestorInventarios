using System;
using System.Collections.Generic;
using System.Text;

namespace CModels.INV_JAC
{
    public class E_Inventario_Sia
    {
        public string chasis { get; set; }
        public string motor { get; set; }
        public string nro_factura { get; set; }
        public string tipo_doc_factura { get; set; }
        public string fecha_factura { get; set; }
        public string concesionario_asignado { get; set; }
        public string marca { get; set; }
        public string descripcion_factura { get; set; }
        public string veh_color { get; set; }
        public string fsc { get; set; }
        public string familia { get; set; }
        public double costo { get; set; }
        public double precio { get; set; }
        public string categoria_cliente { get; set; }
        public string clase { get; set; }
        public string sub_clase { get; set; }
        public string linea { get; set; }
        public string estado { get; set; }
        public string id_cliente { get; set; }
        public string descripcion_color { get; set; }
        public string autorizacion_eje { get; set; }
        public int version_anio { get; set; }
        public int clasificacion_exonerado { get; set; }
        //public string clasificacion_exonerado { get; set; }
        public string codigo_interno { get; set; }
        public string nfact1 { get; set; }
        public string nfact2 { get; set; }
        public string nfact3 { get; set; }
        public DateTime fecha_reg_factura { get; set; }
        public int cod_marca { get; set; }
        public void separar_numfact() 
        {
            var arraynfact = nro_factura.Split('-');
            nfact1 = arraynfact[0];
            nfact2 = arraynfact[1];
            nfact3 = arraynfact[2];
        }
    }
}