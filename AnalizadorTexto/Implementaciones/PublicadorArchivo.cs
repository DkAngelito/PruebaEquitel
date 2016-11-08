
namespace AnalizadorTexto
{
    public class PublicadorArchivo : Transversal.IServicioAnalizadorTexto
    {
        // Estos parametros deberian estar en un archivo de configuración u otro repositorio
        // pero por simplicidad en la prueba los dejo quemados aca.
        private const string RUTAARCHIVOS = @"C:\Temp\PruebaEquitel\";
        private const string NOMBREARCHIVO = "Texto_{0}_{1}.txt";

        public PublicadorArchivo()
        {
            if (!System.IO.Directory.Exists(RUTAARCHIVOS))
            {
                System.IO.Directory.CreateDirectory(RUTAARCHIVOS);
            }
        }

        public void Analizar(Transversal.Entidades.DatosAnalisisTexto value)
        {
            using (var archivo = new System.IO.StreamWriter(System.IO.Path.Combine(RUTAARCHIVOS, string.Format(NOMBREARCHIVO, value.IdCliente, value.IdPeticion))))
            {
                archivo.Write(value.Texto);
            }
        }
    }
}
