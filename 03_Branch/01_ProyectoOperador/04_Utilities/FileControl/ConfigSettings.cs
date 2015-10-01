using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniandes.FileControl
{
    public class FileConfigSettings
    {
        public const string RUTA_ANTIVIRUS = "RutaAntivirus";
        public const string RUTA_REPOSITORIO = "RutaRepositorio";
        public const string TIPO_REPOSITORIO = "TipoRepositorio";
        public const string TIEMPO_ESCANEO_BYTE = "SegundosEscaneoByte";

        public static decimal GetScanningSecondsPerByte()
        {
            var fileSection = (FilesSection)System.Configuration.ConfigurationManager
                            .GetSection("FileControl/FilesSection");
            return fileSection.AntivirusDirectory.ScanningSecondsPerByte;
        }

        public static AntivirusDirectory GetAntivirusDirectory()
        {
            var fileSection = (FilesSection)System.Configuration.ConfigurationManager.GetSection("FileControl/FilesSection");
            return fileSection.AntivirusDirectory;
        }

        public static RepositoryDirectory GetRepositoryDirectory(string key)
        {
            var section = (FilesSection)System.Configuration.ConfigurationManager
                .GetSection("FileControl/FilesSection");
            var collection = section.RepositoryDirectories;
            var repository = collection.ToList().Where(x => x.Key.Equals(key)).FirstOrDefault();
            return repository;
            //return null;
        }
    }
}
