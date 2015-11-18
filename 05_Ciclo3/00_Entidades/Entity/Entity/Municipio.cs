using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operador.Entity
{
    public class Municipio
    {
        public int IdMunicipio { get; set; }

        public int IdDepartamento { get; set; }

        public string NombreMunicipio { get; set; }

        public string CodigoDANE { get; set; }
    }
}
