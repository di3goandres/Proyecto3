
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operador.Entity
{
    public enum TIPO_IDENTIFICACION
    {

            CC =1,
            CE = 2,
            RC = 3,
            PS= 4,
            NU= 5,
            TI = 6

      
    }

    public static class PrefijoEnumTIPO_IDENTIFICACION
    {
        public static int ToTipoIdentificacionId(string me)
        {
            switch (me)
            {

                case "CC": return Convert.ToInt32(TIPO_IDENTIFICACION.CC);
                case "CE": return Convert.ToInt32(TIPO_IDENTIFICACION.CE);
                case "RC": return Convert.ToInt32(TIPO_IDENTIFICACION.RC);
                case "PS": return Convert.ToInt32(TIPO_IDENTIFICACION.PS);
                case "NU": return Convert.ToInt32(TIPO_IDENTIFICACION.NU);
                case "TI": return Convert.ToInt32(TIPO_IDENTIFICACION.TI);
      

                default: return 0;
            }
        }
        public static string EnumToTIPO_IDENTIFICACION(int me)
        {
            switch (me)
            {
                case 1: return "CC".ToUpper();
                case 2: return "CE".ToUpper();
                case 3: return "RC".ToUpper();
                case 4: return "PS".ToUpper();
                case 5: return "NU".ToUpper();
                case 6: return "TI".ToUpper();
                default: return "NA";
            }
        }


        public static string EnumToTIPO_IDENTIFICACIONCOMPLETO(int me)
        {
            switch (me)
            {
                case 1: return "Cédula de Ciudadania".ToUpper();
                case 2: return "Cédula de Extranjeria".ToUpper();
                case 3: return "Registro Civil".ToUpper();
                case 4: return "Pasaporte".ToUpper();
                case 5: return "Numero Unico de Identificación Personal".ToUpper();
                case 6: return "Tarjeta de Identidad".ToUpper();
                default: return "NA";
            }
        }
    }

}
