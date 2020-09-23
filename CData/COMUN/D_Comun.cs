using System;
using System.Collections.Generic;
using System.Text;

namespace CData.COMUN
{
    public static class D_Comun
    {
        public static string getfecha(DateTime fecha) 
        {
            fecha.ToLocalTime();
            var fec_actual = DateTime.Now;
            var day = fecha.Day;
            var month = fecha.Month;
            var year = fecha.Year;
            var hour = fec_actual.Hour;
            var minutes = fec_actual.Minute;
            var seconds = fec_actual.Second;
            var fechar = (day + "-" + month + "-" + year + " " + hour + ":" + minutes + ":" + seconds);
            var fechares = Convert.ToString(fechar);
            var date = "To_Date('" + fechares + "','dd-mm-yyyy hh24:mi:ss')";
            return date;
        }
    }
}


namespace CapaDatos.COMUN
{
    public static class D_Comun
    {
        private static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
        public static DateTime getDesde(DateTime fecha)
        {
            return ChangeTime(fecha.ToLocalTime(), 0, 0, 0, 0);
        }
        public static DateTime gethasta(DateTime fecha)
        {

            return ChangeTime(fecha.ToLocalTime(), 23, 59, 59, 0);
        }
        public static DateTime gethasta(DateTime? fecha)
        {
            DateTime fecha1;
            if (fecha is null)
            {
                fecha1 = DateTime.Now;
            }
            else
            {
                fecha1 = (DateTime)fecha;
            }
            fecha1.AddHours(23);
            fecha1.AddMinutes(59);
            fecha1.AddSeconds(59);
            return fecha1;
            //return ChangeTime(fecha1.ToLocalTime(), 23, 59, 59, 0);
        }
        public static string getfecha(DateTime fecha)
        {
            fecha.ToLocalTime();
            var fec_actual = DateTime.Now;
            var day = fecha.Day;
            var month = fecha.Month;
            var year = fecha.Year;
            var hour = fec_actual.Hour;
            var minutes = fec_actual.Minute;
            var seconds = fec_actual.Second;
            var fechar = (day + "-" + month + "-" + year + " " + hour + ":" + minutes + ":" + seconds);
            //fecha() + new TimeSpan(hour, minutes, seconds);
            var fechares = Convert.ToString(fechar);
            var date = "To_Date('" + fechares + "', 'dd-mm-yyyy hh24:mi:ss')";
            return date;
        }
        public static string getdesdestring(DateTime fecha)
        {
            fecha.ToLocalTime();
            var day = fecha.Day;
            var month = fecha.Month;
            var year = fecha.Year;
            var hour = 0;
            var minutes = 0;
            var seconds = 0;
            var fechar = (day + "-" + month + "-" + year + " " + hour + ":" + minutes + ":" + seconds);
            var fechares = Convert.ToString(fechar);
            var date = "To_Date('" + fechares + "', 'dd-mm-yyyy hh24:mi:ss')";
            return date;
        }
        public static string gethastastring(DateTime? fecha)
        {
            DateTime fecha1;
            var day = 0;
            var month = 0;
            var year = 0;
            var hour = 23;
            var minutes = 59;
            var seconds = 59;
            if (fecha is null)
            {
                fecha1 = DateTime.Now;
                day = fecha1.Day;
                month = fecha1.Month;
                year = fecha1.Year;
            }
            else
            {
                fecha1 = (DateTime)fecha;
                day = fecha1.Day;
                month = fecha1.Month;
                year = fecha1.Year;
            }

            var fechar = (day + "-" + month + "-" + year + " " + hour + ":" + minutes + ":" + seconds);
            var fechares = Convert.ToString(fechar);
            var date = "To_Date('" + fechares + "', 'dd-mm-yyyy hh24:mi:ss')";
            return date;
        }
        public static string getfecha(DateTime? fecha)
        {
            DateTime fecha1;
            var day = 0;
            var month = 0;
            var year = 0;
            var hour = 23;
            var minutes = 59;
            var seconds = 59;
            var date = "";

            if (fecha is null)
            {
                date = "''";
            }
            else
            {
                fecha1 = (DateTime)fecha;
                day = fecha1.Day;
                month = fecha1.Month;
                year = fecha1.Year;

                var fechar = (day + "-" + month + "-" + year + " " + hour + ":" + minutes + ":" + seconds);
                var fechares = Convert.ToString(fechar);
                date = "To_Date('" + fechares + "', 'dd-mm-yyyy hh24:mi:ss')";
            }
            return date;
        }
    }
}