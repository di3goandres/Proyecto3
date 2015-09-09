using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Centralizador.DAO
{
    public enum TipoRetornoRegistroUsuarios
    {
        NOEXITOSO = 1,
        EXITOSO = 2,
        FALLIDO = 3,
        EXISTE = 4,
    }

    public static class PrefijoTipoRetornoRegistroUsuarios
    {
        public static int ToMecanismoSeleccion(string me)
        {
            switch (me)
            {
                case "EXITOSO": return Convert.ToInt32(TipoRetornoRegistroUsuarios.EXITOSO);
                case "FALLIDO": return Convert.ToInt32(TipoRetornoRegistroUsuarios.FALLIDO);
                case "NOEXITOSO": return Convert.ToInt32(TipoRetornoRegistroUsuarios.NOEXITOSO);
                case "EXISTE": return Convert.ToInt32(TipoRetornoRegistroUsuarios.EXISTE);
                default: return 0;
            }
        }
        public static string EnumToMecanismoSeleccion(int me)
        {
            switch (me)
            {
                case 1: return "EXITOSO";
                case 2: return "FALLIDO";
                case 3: return "NOEXITOSO";
                case 4: return "EXISTE";
                default: return "0";
            }
        }
    }
}
