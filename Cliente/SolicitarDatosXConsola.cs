using System;

namespace Cliente
{
    class SolicitarDatosXConsola : GeneradorTextos.IProveedorDatosGeneradorTextos
    {
        private const int TAMANOMINIMO = 1024;
        private const int TAMANOMAXIMO = 4096;

        public GeneradorTextos.DatosGeneradorTextos ObtenerDatos()
        {
            var datos = new GeneradorTextos.DatosGeneradorTextos();
            bool ok = false;
            do
            {                
                try
                {
                    datos.TamanoMinimo = SolicitarDato("Tamaño Minimo de Texto: ");
                    datos.TamanoMaximo = SolicitarDato("Tamaño Maximo de Texto: ");

                    ValidarDatos(datos);
                    ok = true;
                }
                catch (ArgumentException ex)
                {
                    ok = false;
                    Console.WriteLine(string.Format("Datos Incorrectos: {0}\nIntente de nuevo.", ex.Message));
                }

            }
            while (!ok);
            return datos;
        }

        private int SolicitarDato(string mensaje)
        {
            int valor;
            do
            {
                Console.Write(mensaje);
            }
            while (!int.TryParse(Console.ReadLine(), out valor));
            return valor;
        }

        private void ValidarDatos(GeneradorTextos.DatosGeneradorTextos datos)
        {
            if (datos.TamanoMinimo < TAMANOMINIMO)
                throw new ArgumentException("TamanoMinimo", "El texto podria generarse con un tamaño menor al minimo que es " + TAMANOMINIMO.ToString() + " caracteres");

            if (datos.TamanoMaximo> TAMANOMAXIMO)
                throw new ArgumentException("TamanoMaximo", "El texto podria generarse con un tamaño mayor al maximo que es " + TAMANOMINIMO.ToString() + " caracteres");
        }
    }
}
