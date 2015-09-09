using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Uniandes.FileControl
{
    /// <summary>
    /// Seccion principal de configuración del control de archivos
    /// </summary>
    public class FilesSection : ConfigurationSection
    {
        /// <summary>
        /// Configuración de ruta de antivirus con su respectivo tiempo de espera, bytes x segundos
        /// </summary>
        [ConfigurationProperty("AntivirusDirectory", IsRequired = true)]
        public AntivirusDirectory AntivirusDirectory
        {
            get
            {
                return (AntivirusDirectory)this["AntivirusDirectory"];
            }
            set
            {
                this["AntivirusDirectory"] = value;
            }
        }

        /// <summary>
        /// Listado de repositorios con los que se puede hacer transferencia de archivos
        /// </summary>
        [ConfigurationProperty("RepositoryDirectories", IsRequired = false)]
        public RepositoryDirectoryCollection RepositoryDirectories
        {
            get
            {
                return (RepositoryDirectoryCollection)this["RepositoryDirectories"];
            }
            set
            {
                this["RepositoryDirectories"] = value;
            }
        }

    }

    /// <summary>
    /// Elemento de la sección desde donde se configura la ruta de antivirus y su tiempo de espera
    /// </summary>
    public class AntivirusDirectory : ConfigurationElement
    {
        /// <summary>
        /// Ruta en la que se almacenan los archivos para su revisión
        /// </summary>
        [ConfigurationProperty("Path", IsRequired = true)]
        public string Path
        {
            get
            {
                return (string)this["Path"];
            }
            set
            {
                this["Path"] = value;
            }
        }

        /// <summary>
        /// Es una ruta FTP o una ruta local
        /// </summary>
        [ConfigurationProperty("isFtp", IsRequired = true)]
        public bool IsFtp
        {
            get
            {
                return (bool)this["isFtp"];
            }
            set
            {
                this["isFtp"] = value;
            }
        }

        /// <summary>
        /// Usuario para acceso al directorio FTP
        /// </summary>
        [ConfigurationProperty("user", IsRequired = true)]
        public string FtpUser
        {
            get
            {
                return (string)this["user"];
            }
            set
            {
                this["user"] = value;
            }
        }

        /// <summary>
        /// Contraseña para acceso al directorio FTP
        /// </summary>
        [ConfigurationProperty("password", IsRequired = true)]
        public string FtpPassword
        {
            get
            {
                return (string)this["password"];
            }
            set
            {
                this["password"] = value;
            }
        }

        /// <summary>
        /// Tiempo aproximado en segundos que se tarda el escaneo de un byte
        /// </summary>
        [ConfigurationProperty("ScanningSecondsPerByte", IsRequired = true)]
        public decimal ScanningSecondsPerByte
        {
            get
            {
                return (decimal)this["ScanningSecondsPerByte"];
            }
            set
            {
                this["ScanningSecondsPerByte"] = value;
            }
        }
    }

    /// <summary>
    /// Lista de repositorios disponibles entre los que se puede realizar transferencia de archivos
    /// </summary>
    public class RepositoryDirectoryCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RepositoryDirectory();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RepositoryDirectory)element).Key;
        }


        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        /// <summary>
        /// The element name of the configuration elements in the config file,
        /// as set at RepositoryDirectoryCollection.ElementName
        /// </summary>
        protected override string ElementName
        {
            get { return "add"; }
        }

        public IList<RepositoryDirectory> ToList()
        {
            return this.Cast<RepositoryDirectory>().ToList();
        }
    }

    /// <summary>
    /// Elemento de la colección de repositorios, contiene ruta y credenciales
    /// </summary>
    public class RepositoryDirectory : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }
        /// <summary>
        /// Ruta del directorio
        /// </summary>
        [ConfigurationProperty("path", IsRequired = true)]
        public string Path
        {
            get
            {
                return (string)this["path"];
            }
            set
            {
                this["path"] = value;
            }
        }

        /// <summary>
        /// Es una ruta FTP o una ruta local
        /// </summary>
        [ConfigurationProperty("isFtp", IsRequired = true)]
        public bool IsFtp
        {
            get
            {
                return (bool)this["isFtp"];
            }
            set
            {
                this["isFtp"] = value;
            }
        }

        /// <summary>
        /// Usuario para acceso al directorio FTP
        /// </summary>
        [ConfigurationProperty("user", IsRequired = true)]
        public string FtpUser
        {
            get
            {
                return (string)this["user"];
            }
            set
            {
                this["user"] = value;
            }
        }

        /// <summary>
        /// Contraseña para acceso al directorio FTP
        /// </summary>
        [ConfigurationProperty("password", IsRequired = true)]
        public string FtpPassword
        {
            get
            {
                return (string)this["password"];
            }
            set
            {
                this["password"] = value;
            }
        }
    }

}
