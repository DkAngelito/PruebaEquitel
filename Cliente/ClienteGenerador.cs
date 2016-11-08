using System;
using System.Threading.Tasks;

namespace Cliente
{
    internal class ClienteGenerador
    {
        Guid _idCliente;
        GeneradorTextos.IProveedorDatosGeneradorTextos _datos;
        GeneradorTextos.IServicioGeneradorTextos _generador;
        Transversal.IServicioAnalizadorTexto _analizador;

        public ClienteGenerador(GeneradorTextos.IProveedorDatosGeneradorTextos datos, GeneradorTextos.IServicioGeneradorTextos generador, Transversal.IServicioAnalizadorTexto analizador)
        {
            _datos = datos;
            _generador = generador;
            _analizador = analizador;
            _idCliente = Guid.NewGuid();
        }

        public void Iniciar()
        {
            int cantidad;
            do
            {
                Console.Write("Numero de textos a generar: ");
            }
            while (!int.TryParse(Console.ReadLine(), out cantidad));

            var data = _datos.ObtenerDatos();
            Console.Write("\nDatos Obtenidos.\nPresione cualquier tecla Para iniciar la generación...");
            Console.ReadKey();

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var textos = _generador.GenerarTextos(cantidad, data);
            sw.Stop();

            Console.WriteLine(string.Format("\nGeneracion Terminada.\nDuración: {0}\nOprima cualquier tecla para publicar...", sw.Elapsed));
            Console.ReadKey();            
            Console.WriteLine(string.Format("Programando Envio de peticiones.", sw.Elapsed));

            sw.Reset();
            sw.Start();
            Publicar(textos);
            sw.Stop();
            Console.WriteLine(string.Format("\nProceso Terminado.\nDuración: {0}\n", sw.Elapsed));
            Console.ReadLine();
        }

        private void Publicar(string[] textos)
        {
            if (textos == null)
                throw new ArgumentNullException("textos");

            Parallel.For(0, textos.Length, i =>
            {
                var datos = new Transversal.Entidades.DatosAnalisisTexto() { IdCliente = _idCliente, IdPeticion = i, Texto = textos[i] };
                _analizador.Analizar(datos);
            });
        }
    }
}
