using System;
using System.Threading.Tasks;

namespace AnalizadorTexto
{
    public class ReporteEstadisticasADisco : IRepositorioResultadoAnalisisTexto
    {
        private static string _rutaArchivos = @"C:\Temp\PruebaEquitel\";
        private static string _nombreArchivo = "Resultados_{0}_{1}.txt";

        static ReporteEstadisticasADisco()
        {
            _rutaArchivos = System.Configuration.ConfigurationManager.AppSettings["RUTAARCHIVOS"];
            if (string.IsNullOrEmpty(_rutaArchivos))
                throw new Exception("Se debe configurar la ruta de guardado de los archivos bajo el appSetting RUTAARCHIVOS");

            _nombreArchivo = System.Configuration.ConfigurationManager.AppSettings["NOMBREARCHIVO"];
            if (string.IsNullOrEmpty(_nombreArchivo))
                throw new Exception("Se debe configurar el formato de nombre de los archivos bajo el appSetting NOMBREARCHIVO");

            if (!System.IO.Directory.Exists(_rutaArchivos))
            {
                System.IO.Directory.CreateDirectory(_rutaArchivos);
            }
        }

        public void GuardarDatos(Transversal.Entidades.DatosAnalisisTexto datos, ResultadoAnalisisTexto resultado)
        {
            Task.Factory.StartNew(() =>
                {
                    using (var archivo = new System.IO.StreamWriter(System.IO.Path.Combine(_rutaArchivos, string.Format(_nombreArchivo, datos.IdCliente, datos.IdPeticion))))
                    {
                        archivo.WriteLine("ESTADISTICAS");
                        archivo.Write("Palabras Terminadas en con la letra n: ");
                        archivo.WriteLine(resultado.PalabrasTerminadasEnn);
                        archivo.Write("Oraciones con más de 15 palabras: ");
                        archivo.WriteLine(resultado.OracionesConMasDe15Pal);
                        archivo.Write("Parrafos: ");
                        archivo.WriteLine(resultado.Parrafos);
                        archivo.Write("Caractes Alfanumericos distintos a n o N: ");
                        archivo.WriteLine(resultado.NumeroCaracteresDistintosANn);
                        archivo.WriteLine("TEXTO");
                        archivo.Write(datos.Texto);
                    }
                });
        }
    }
}   
