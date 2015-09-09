using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Web.Script.Serialization;
using System.Threading;
using System.Net;


namespace Uniandes.FileControl
{

    public class FileNameControl
    {
        public string Original { get; set; }
        public string Generated { get; set; }
        public string FullFilename { get; set; }
    }
    public class FileControl
    {
        private List<string> _MimeTypesAllowed;

        private static Thread cleaner;

        private int _MaxSize;


        public new List<FileNameControl> AntivirusFileNames
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxSize">Tamaño maximo en bytes</param>
        public FileControl(int maxSize)
        {
            _MimeTypesAllowed = new List<string>();
            AntivirusFileNames = new List<FileNameControl>();
            _MaxSize = maxSize;
            //  _InitCleaner();
        }

        /// <summary>
        /// Constructor con tipos mime permitidos.
        /// </summary>
        /// <param name="mimeTypes">Tipos mime permitidos</param>
        /// <param name="maxSize">Tamaño maximo en bytes</param>
        public FileControl(int maxSize, List<string> mimeTypes)
        {
            AntivirusFileNames = new List<FileNameControl>();
            _MaxSize = maxSize;
            _MimeTypesAllowed = mimeTypes;
            // _InitCleaner();
        }

        /// <summary>
        /// Inicia el hilo para la limpieza de archivos de la carpeta de antivirus.
        /// </summary>
        private void _InitCleaner()
        {
            if (cleaner == null)
            {
                cleaner = new Thread(new ThreadStart(_CleanAntivirussDirectory));
                cleaner.Start();
            }
        }

        /// <summary>
        /// Realiza la limpieza de la carpeta de antivirus.
        /// </summary>
        private void _CleanAntivirussDirectory()
        {
            var antivirusDir = FileConfigSettings.GetAntivirusDirectory();
            var dirName = antivirusDir.Path;
            while (true)
            {
                Thread.Sleep(TimeSpan.FromMinutes(30));
                if (Directory.Exists(dirName) == false) continue;
                string[] files = Directory.GetFiles(dirName);
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.LastAccessTime < DateTime.Now.AddMinutes(-30))
                        try
                        {
                            fi.Delete();
                        }
                        catch { }
                }
            }
        }

        /// <summary>
        /// Metodo encargado verificar el tamaño de los archivos subidos.
        /// </summary>
        /// <param name="fileCollection"></param>
        public void VerifyMaxSize(HttpFileCollection fileCollection)
        {
            List<string> fileNamesExeption = new List<string>();
            for (int i = 0; i <= fileCollection.Count - 1; i++)
            {
                HttpPostedFile postedFile = fileCollection[i];
                if (postedFile.ContentLength > 0)
                {
                    var filename = Path.GetFileName(postedFile.FileName);
                    if (postedFile.ContentLength > _MaxSize)
                        fileNamesExeption.Add(filename);
                }
            }
            if (fileNamesExeption.Any())
            {
                throw new MaximumSizeExeption(string.Format("El tamaño máximo de los archivos es de {0} Kilobytes. Existe(n) {1} archivo(s) con un tamaño superior", _MaxSize / 1000, fileNamesExeption.Count), fileNamesExeption);
            }
        }

        /// <summary>
        /// Metodo encargado verificar el tamaño de los archivos subidos.
        /// </summary>
        /// <param name="fileCollection">Archivos a verificar</param>
        public void VerifyMimeTypes(HttpFileCollection fileCollection)
        {
            if (_MimeTypesAllowed.Any() == false)
                return;
            List<string> fileNamesExeption = new List<string>();
            for (int i = 0; i <= fileCollection.Count - 1; i++)
            {
                HttpPostedFile postedFile = fileCollection[i];
                if (postedFile.ContentLength > 0)
                {
                    var filename = Path.GetFileName(postedFile.FileName);
                    if (_MimeTypesAllowed.Contains(postedFile.ContentType) == false)
                        fileNamesExeption.Add(filename);
                }
            }
            if (fileNamesExeption.Any())
            {
                throw new InvalidMimeTypesException(string.Format("El tipo mime de {0} archivos no está permitido", fileNamesExeption.Count), fileNamesExeption, _MimeTypesAllowed);
            }
        }

        /// <summary>
        /// Metodo encargado de subir una colección de archivos para ser revisados por el antivirus
        /// </summary>
        /// <param name="fileCollection"></param>
        public void UploadFileTsoScan(HttpFileCollection fileCollection)
        {
            VerifyMaxSize(fileCollection);
            VerifyMimeTypes(fileCollection);

            var antivirusDir = FileConfigSettings.GetAntivirusDirectory();
            if (antivirusDir == null)
                throw new Exception("La ruta de almacenamiento para analisis no ha sido encontrada");
            var path = antivirusDir.Path;
            int maxSize = 0;
            string nombreArchivo = "";

            if (antivirusDir.IsFtp)
            {
                for (int i = 0; i <= fileCollection.Count - 1; i++)
                {
                    HttpPostedFile postedFile = fileCollection[i];
                    if (postedFile.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(postedFile.FileName);
                        var generatedName = GetFileName(postedFile.FileName, i.ToString());
                        var filePath = Path.Combine(path, generatedName);
                        AntivirusFileNames.Add(new FileNameControl() { Original = filename, Generated = generatedName, FullFilename = filePath });
                        maxSize += postedFile.ContentLength;
                        nombreArchivo = generatedName;
                        _SendFileStreamToFtp(postedFile.InputStream, antivirusDir.Path, antivirusDir.FtpUser, antivirusDir.FtpPassword, generatedName);
                        //if (postedFile.ContentLength > maxSize)
                    }
                }
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                for (int i = 0; i <= fileCollection.Count - 1; i++)
                {
                    HttpPostedFile postedFile = fileCollection[i];
                    if (postedFile.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(postedFile.FileName);
                        var generatedName = GetFileName(postedFile.FileName, i.ToString());
                        var filePath = Path.Combine(path, generatedName);
                        nombreArchivo = generatedName;
                        AntivirusFileNames.Add(new FileNameControl() { Original = filename, Generated = generatedName, FullFilename = filePath });
                        postedFile.SaveAs(filePath);
                        //if (postedFile.ContentLength > maxSize)
                        maxSize += postedFile.ContentLength;
                    }
                }
            }

            _VerifyAntivirusFiles(maxSize + 100);
        }

        public String GetFileName(string originalFilename, string extraName)
        {
            var now = DateTime.Now.ToString("ddMMyyyy_hhmmss");
            var extension = "";
            var nameSplit = Path.GetFileName(originalFilename).Split('.');
            if (nameSplit.Length > 1)
                extension = nameSplit[nameSplit.Length - 1];
            return now + "_" + (extraName ?? "") + "." + extension;
        }

        private void _VerifyAntivirusFiles(int fileSize)
        {
            var antivirusDir = FileConfigSettings.GetAntivirusDirectory();
            if (antivirusDir == null)
            {
                throw new Exception("La ruta de almacenamiento para analisis no ha sido encontrada");
            }
            var path = antivirusDir.Path;

            try
            {
                Thread.Sleep(System.TimeSpan.FromSeconds(fileSize * (double)antivirusDir.ScanningSecondsPerByte));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error mientras esperaba el escaneo del archivo"), ex);
            }

            var removedFiles = new List<string>();
            foreach (var filenameControl in AntivirusFileNames)
            {
                if (antivirusDir.IsFtp)
                {
                    if (!_ExistFileInFtp(path + filenameControl.Generated, antivirusDir.FtpUser, antivirusDir.FtpPassword))
                        removedFiles.Add(filenameControl.Original);
                }
                else if (File.Exists(filenameControl.FullFilename) == false)
                    removedFiles.Add(filenameControl.Original);
            }

            if (removedFiles.Any())
                throw new VirusFileExeption(string.Format("Se han eliminado {0} por el antivirus.", removedFiles.Count), removedFiles);

        }

        public bool _ExistFileInFtp(string fullPath, string ftpUser, string ftpPassword)
        {
            var request = (FtpWebRequest)WebRequest.Create(fullPath);
            request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Envio de archivos de la ruta de antivirus a otra carpeta ya sea local o FTP configurada en el web.config en la sección FilesSection
        /// </summary>
        /// <param name="destinationKey"></param>
        /// <param name="fileName"></param>
        public void CopyAntivirusToRepositorio(string destinationKey, string fileName)
        {
            try
            {
                var repository = FileConfigSettings.GetRepositoryDirectory(destinationKey);
                if (repository.Path.Equals(FileConfigSettings.GetAntivirusDirectory().Path))
                    return;
                if (repository == null)
                {
                    throw new RepositoryKeyExeption("No se encontro ninguna configuración de repositorio para la llave " + destinationKey);
                }

                var antivirusDir = FileConfigSettings.GetAntivirusDirectory();
                if (antivirusDir == null)
                {
                    throw new RepositoryKeyExeption("No se encontro ninguna configuración de repositorio para la llave del Antivirus");
                }
                if (antivirusDir.IsFtp)
                {
                    var generateFilename = GetFileName(fileName, "0");
                    _SendFtpToFtp(fileName, generateFilename, antivirusDir.Path, repository.Path, antivirusDir.FtpUser, repository.FtpUser, antivirusDir.FtpPassword, repository.FtpPassword);

                }
                else
                {
                    var filePath = Path.Combine(FileConfigSettings.GetAntivirusDirectory().Path, fileName);
                    if (File.Exists(filePath) == false)
                    {
                        throw new Exception(string.Format("El archivo {0} no existe.", filePath));
                    }


                    var filename = Path.GetFileName(filePath);
                    if (repository.IsFtp == false)
                    {
                        if (!Directory.Exists(repository.Path))
                            Directory.CreateDirectory(repository.Path);
                        File.Copy(filePath, Path.Combine(repository.Path, filename), true);
                    }
                    else
                    {
                        string user = repository.FtpUser;
                        string password = repository.FtpPassword;
                        _SendLocalToFtp(filePath, repository.Path, user, password, filename);
                    }
                    File.Delete(filePath);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Copia archivos entre repositorios, basado en la clave de que se encuentre configurada en el web.config en la secciòn FilesSection
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <param name="destinationKey"></param>
        /// <param name="fileName"></param>
        public void CopyRepositoryToRepository(string sourceKey, string destinationKey, string sourceFileName, string destinationFileName)
        {
            if (sourceFileName == null)
                throw new Exception("The parameter sourcefilename is required.");
            if (destinationFileName == null)
                destinationFileName = sourceFileName;
            var sourceRepository = FileConfigSettings.GetRepositoryDirectory(sourceKey);
            var destinationRepository = FileConfigSettings.GetRepositoryDirectory(destinationKey);

            if (sourceRepository == null)
                throw new RepositoryKeyExeption("No se encontro ninguna configuración de repositorio para la llave " + sourceKey);
            if (destinationRepository == null)
                throw new RepositoryKeyExeption("No se encontro ninguna configuración de repositorio para la llave " + destinationKey);

            ///FTP path
            ///
            var sourcePath = sourceRepository.Path;
            var destinationPath = destinationRepository.Path;
            var sourceFilePath = Path.Combine(sourcePath, sourceFileName);
            var destinationFilePath = Path.Combine(destinationPath, destinationFileName);

            if (sourcePath.Equals(destinationPath))
                return;

            ///FTP Credentials
            ///
            string sourceUser = sourceRepository.FtpUser;
            string sourcePassword = sourceRepository.FtpPassword;
            string destinationUser = destinationRepository.FtpUser;
            string destinationPassword = destinationRepository.FtpPassword;
            ///Si es local
            ///
            if (sourceRepository.IsFtp == false)
            {
                if (File.Exists(sourceFilePath) == false)
                    throw new FileNotFoundException(string.Format("El archivo {0} no existe.", sourceFilePath));
                ///Si es FTP
                ///
                if (destinationRepository.IsFtp == true)
                {
                    _SendLocalToFtp(sourceFilePath, destinationPath, destinationUser, destinationPassword, destinationFileName);
                    return;
                }
                var nombre = Path.GetFileName(sourceFilePath);
                var destino = Path.Combine(destinationPath, nombre);
                if (Directory.Exists(destinationPath) == false)
                    Directory.CreateDirectory(destinationPath);
                File.Copy(sourceFilePath, destino, true);
            }
            ///Si es FTP
            ///
            else
            {
                ///Si es local
                ///
                if (destinationRepository.IsFtp == false)
                {
                    _SendFtpToLocal(sourceFileName, destinationFileName, destinationPath, sourcePath, sourceUser, sourcePassword);
                }
                ///Si es FTP
                ///
                else
                {
                    _SendFtpToFtp(sourceFileName, destinationFileName, sourcePath, destinationPath, sourceUser, destinationUser, sourcePassword, destinationPassword);
                }

            }
        }


        /// <summary>
        /// Download file from FTP repository
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <param name="destinationKey"></param>
        /// <param name="fileName"></param>
        public byte[] GetFileFromFtpRepository(string sourceKey, string fileName)
        {
            var sourceRepository = FileConfigSettings.GetRepositoryDirectory(sourceKey);

            if (sourceRepository == null)
                throw new RepositoryKeyExeption("No se encontro ninguna configuración de repositorio para la llave " + sourceKey);
            var sourcePath = sourceRepository.Path;

            ///FTP Credentials
            ///
            string sourceUser = sourceRepository.FtpUser;
            string sourcePassword = sourceRepository.FtpPassword;

            WebClient requestRepository = new WebClient();
            requestRepository.Credentials = new NetworkCredential(sourceUser, sourcePassword);
            byte[] fileData = requestRepository.DownloadData(Path.Combine(sourcePath, fileName));
            return fileData;
        }

        /// <summary>
        /// Envia un archivo de una ruta local a un ftp
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ftpPath"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        private void _SendFileStreamToFtp(Stream file, string ftpPath, string user, string password, string destinyFileName)
        {

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Path.Combine(ftpPath, destinyFileName));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(user, password);

            Stream ftpStream = request.GetRequestStream();

            int length = 1024;
            byte[] buffer = new byte[length];
            int bytesRead = 0;

            do
            {
                bytesRead = file.Read(buffer, 0, length);
                ftpStream.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);

            file.Close();
            file.Dispose();
            ftpStream.Close();
            ftpStream.Dispose();

        }

        /// <summary>
        /// Envia un archivo de una ruta local a un ftp
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ftpPath"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        private void _SendLocalToFtp(string filePath, string ftpPath, string user, string password, string destinyFileName)
        {

            FileInfo toUpload = new FileInfo(filePath);
            if (destinyFileName == null)
                destinyFileName = toUpload.Name;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Path.Combine(ftpPath, destinyFileName));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(user, password);

            Stream ftpStream = request.GetRequestStream();
            FileStream file = File.OpenRead(filePath);

            int length = 1024;
            byte[] buffer = new byte[length];
            int bytesRead = 0;

            do
            {
                bytesRead = file.Read(buffer, 0, length);
                ftpStream.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);

            file.Close();
            file.Dispose();
            ftpStream.Close();
            ftpStream.Dispose();

        }

        /// <summary>
        /// Envia un archivo de una ruta FTP a una ruta local
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ftpPath"></param>
        /// <param name="downloadPath"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        private void _SendFtpToLocal(string sourceFileName, string destinationFilename, string ftpPath, string downloadPath, string user, string password)
        {
            WebClient request = new WebClient();
            request.Credentials = new NetworkCredential(user, password);
            byte[] fileData = request.DownloadData(Path.Combine(ftpPath, sourceFileName));

            FileStream file = File.Create(Path.Combine(downloadPath, destinationFilename));
            file.Write(fileData, 0, fileData.Length);
            file.Close();
            file.Dispose();
        }

        /// <summary>
        /// Envio de archivos entre rutas FTP
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ftpRepositoryPath"></param>
        /// <param name="ftpPublicPath"></param>
        /// <param name="userRepository"></param>
        /// <param name="userPublic"></param>
        /// <param name="passwordRepository"></param>
        /// <param name="passwordPublic"></param>
        private void _SendFtpToFtp(string sourceFileName, string destinationFilename, string ftpRepositoryPath, string ftpPublicPath, string userRepository, string userPublic, string passwordRepository, string passwordPublic)
        {
            WebClient requestRepository = new WebClient();
            requestRepository.Credentials = new NetworkCredential(userRepository, passwordRepository);
            byte[] fileData = requestRepository.DownloadData(Path.Combine(ftpRepositoryPath, sourceFileName));


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Path.Combine(ftpPublicPath, destinationFilename));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(userRepository, passwordRepository);

            Stream ftpStream = request.GetRequestStream();

            ftpStream.Write(fileData, 0, fileData.Length);
            ftpStream.Close();
            ftpStream.Dispose();
        }

    }

}
