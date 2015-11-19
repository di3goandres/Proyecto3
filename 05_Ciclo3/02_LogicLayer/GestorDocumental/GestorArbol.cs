using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniandes.Utilidades;
using Uniandes.Controlador;
using Operador.Entity;

namespace Uniandes.GestorDocumental
{
    public class GestorArbol
    {
        /// <summary>
        /// Metodo para obtener el arbol de documentos y archivos por uno usuario.
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public List<TreeField> ObtenerArbolPorUsuario(String UID, Boolean ObtenerArchivos)
        {

            try
            {

                CarpetaPersonalDao cDao = new CarpetaPersonalDao();
                MetadataArchivoDao metaDataDao = new MetadataArchivoDao();
                List<TreeField> retorno = new List<TreeField>();
                List<MetadataArchivos> listaArchivos = new List<MetadataArchivos>();
                var listaCarpetas = cDao.ObtenerTodasCarpetasPorUsuario(UID);
                if (ObtenerArchivos)
                {
                    listaArchivos = metaDataDao.ObtenerArchivosPorUsuario(UID);
                }
                var listaAtributosFile = new Li_attr();
                listaAtributosFile = (new Li_attr() { rel = "file" });
                var listaAtributosDocumentos = new Li_attr();
                listaAtributosDocumentos = (new Li_attr() { rel = "folder" });
                retorno.Add(new TreeField()
                {
                    id = "0",
                    parent = "#",
                    text = "Inicial",
                    state = new Estados() { disabled = false, opened = true, selected = true },
                    icon = "",
                    li_attr = new Li_attr() { rel = "Inicial" },
                    type = "Inicial"


                });
                foreach (var dataCarpetas in listaCarpetas)
                {
                    TreeField agregar = new TreeField()
                    {
                        id = dataCarpetas.IdCarpetaPersonal.ToString(),
                        //parent = dataCarpetas.idCarpetaPadre == 0 ? "#" : dataCarpetas.idCarpetaPadre.ToString(),
                        parent = dataCarpetas.idCarpetaPadre.ToString(),
                        text = dataCarpetas.NombreCarpeta,
                        state = new Estados() { disabled = false, opened = dataCarpetas.idCarpetaPadre == null ? true : false, selected = dataCarpetas.idCarpetaPadre == null ? true : false },
                        icon = "",
                        li_attr = listaAtributosDocumentos,
                        type = "folder"
                    };
                    retorno.Add(agregar);
                }

                foreach (var dataArchivos in listaArchivos)
                {
                    TreeField agregar = new TreeField()
                    {
                        id = dataArchivos.idDMtadataArchivo.ToString(),
                        parent = dataArchivos.idCarpetaPersonal == null ? "0" : dataArchivos.idCarpetaPersonal.ToString(),

                        text = dataArchivos.nombre,
                        state = new Estados() { disabled = false, opened = dataArchivos.idCarpetaPersonal == null ? true : false, selected = dataArchivos.idCarpetaPersonal == null ? true : false },
                        icon = "glyphicon glyphicon-file",
                        li_attr = listaAtributosFile,
                        type = "file"


                    };
                    retorno.Add(agregar);
                }

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }
    }
}
