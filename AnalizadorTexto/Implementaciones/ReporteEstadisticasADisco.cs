using System.Threading.Tasks;

namespace AnalizadorTexto
{
    public class ReporteEstadisticasADisco : IRepositorioResultadoAnalisisTexto
    {
        // Estos parametros deberian estar en un archivo de configuración u otro repositorio
        // pero por simplicidad en la prueba los dejo quemados aca.
        private const string RUTAARCHIVOS = @"C:\Temp\PruebaEquitel\";
        private const string NOMBREARCHIVO = "Resultados_{0}_{1}.txt";

        static ReporteEstadisticasADisco()
        {
            if (!System.IO.Directory.Exists(RUTAARCHIVOS))
            {
                System.IO.Directory.CreateDirectory(RUTAARCHIVOS);
            }
        }

        public void GuardarDatos(Transversal.Entidades.DatosAnalisisTexto datos, ResultadoAnalisisTexto resultado)
        {
            Task.Factory.StartNew(() =>
                {
                    using (var archivo = new System.IO.StreamWriter(System.IO.Path.Combine(RUTAARCHIVOS, string.Format(NOMBREARCHIVO, datos.IdCliente, datos.IdPeticion))))
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
