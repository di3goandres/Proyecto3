using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniandes.FileControl
{
    /// <summary>
    /// Excepción para eliminación de archivos por antivirus
    /// </summary>
    public class RepositoryKeyExeption : Exception
    {

        public RepositoryKeyExeption(string message)
            : base(message)
        { }

        public RepositoryKeyExeption(string message, Exception ex)
            : base(message, ex)
        { }
    }
    /// <summary>
    /// Excepción para eliminación de archivos por antivirus
    /// </summary>
    public class VirusFileExeption : Exception
    {
        /// <summary>
        /// Nombres de los archivos han sido eliminados por el antivirus.
        /// </summary>
        public List<string> FileNames { get; set; }


        public VirusFileExeption(string message, List<string> fileNames)
            : base(message)
        {
            FileNames = fileNames;
        }

        public VirusFileExeption(string message, Exception ex, List<string> fileNames)
            : base(message, ex)
        {
            FileNames = fileNames;

        }
    }

    /// <summary>
    /// Excepción para tamaño maximo de archivos
    /// </summary>
    public class MaximumSizeExeption : Exception
    {
        /// <summary>
        /// Nombres de los archivos que superan el tamaño máximo.
        /// </summary>
        public List<string> FileNames { get; set; }


        public MaximumSizeExeption(string message, List<string> fileNames)
            : base(message)
        {
            FileNames = fileNames;
        }

        public MaximumSizeExeption(string message, Exception ex, List<string> fileNames)
            : base(message, ex)
        {
            FileNames = fileNames;

        }
    }

    /// <summary>
    /// Excepción de tipos MIME no validos
    /// </summary>
    public class InvalidMimeTypesException : Exception
    {
        /// <summary>
        /// Nombres de los archivos que tienen un tipo MIME no valido.
        /// </summary>
        public List<string> FileNames { get; set; }

        /// <summary>
        /// Nombres de los tipos MIME validos.
        /// </summary>
        public List<string> ValidMimeTypes { get; set; }

        public InvalidMimeTypesException(string message, List<string> fileNames, List<string> validMimeTypes)
            : base(message)
        {
            ValidMimeTypes = validMimeTypes;
            FileNames = fileNames;
        }

        public InvalidMimeTypesException(string message, Exception ex, List<string> fileNames, List<string> validMimeTypes)
            : base(message, ex)
        {
            ValidMimeTypes = validMimeTypes;
            FileNames = fileNames;
        }
    }
}
