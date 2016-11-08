using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class SolicitarDatosXConsola : GeneradorTextos.IProveedorDatosGeneradorTextos
    {
        private const int TAMANOMINIMO = 1024;
        private const int TAMANOMAXIMO = 1048576;

        public GeneradorTextos.DatosGeneradorTextos ObtenerDatos()
        {
            var datos = new GeneradorTextos.DatosGeneradorTextos();
            bool ok = false;
            do
            {
                datos..MinParrafos = SolicitarDato("Tamaño Minimo de Texto: ");
                datos.MaxParrafos = SolicitarDato("Maximo de Parrafos x Texto: ");
                datos.MinOracionesXParrafo = SolicitarDato("Minimo de Oraciones x parrafo: ");
                datos.MaxOracionesXParrafo = SolicitarDato("Maximo de Oraciones x parrafo: ");
                datos.MinPalabrasXOracion = SolicitarDato("Minimo de Palabras x Oracion: ");
                datos.MaxPalabrasXOracion = SolicitarDato("Maximo de Palabras x Oracion: ");
                datos.MinLetrasXPalabra = SolicitarDato("Minimo de Letras x Palabra: ");
                datos.MaxLetrasXPalabra = SolicitarDato("Maximo de Letras x Palabra: ");
                try
                {
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
            if (datos.MinLetrasXPalabra * datos.MinPalabrasXOracion * datos.MinParrafos * datos.MinOracionesXParrafo < TAMANOMINIMO)
                throw new ArgumentException("TamañoMinimoDelTexto", "El texto podria generarse con un tamaño menor al minimo que es " + TAMANOMINIMO.ToString() + " caracteres");

            if (datos.MinLetrasXPalabra * datos.MaxPalabrasXOracion * datos.MaxParrafos * datos.MaxOracionesXParrafo > TAMANOMAXIMO)
                throw new ArgumentException("TamañoMaximoDelTexto", "El texto podria generarse con un tamaño mayor al maximo que es " + TAMANOMINIMO.ToString() + " caracteres");

            if (datos.MinLetrasXPalabra < 2)
                throw new ArgumentOutOfRangeException("minLetrasXPalabra", "Minimo 2 Letras Por Palabra");

            if (datos.MinPalabrasXOracion < 1)
                throw new ArgumentOutOfRangeException("minPalabrasXOracion", "Minimo 1 Palabra Por Oracion");

            if (datos.MinOracionesXParrafo < 1)
                throw new ArgumentOutOfRangeException("minOracionesXParrafo", "Minimo 1 Oracion Por Parrafo");

            if (datos.MinParrafos <= 1)
                throw new ArgumentOutOfRangeException("minParrafos", "Minimo 1 Parrafo");

            if (datos.MaxLetrasXPalabra < datos.MinLetrasXPalabra)
                throw new ArgumentException("El maximo de Letras por Palabras debe ser mayor o igual al minimo");

            if (datos.MaxPalabrasXOracion < datos.MinPalabrasXOracion)
                throw new ArgumentException("El maximo de Palabras por Oracion debe ser mayor o igual al minimo");

            if (datos.MaxParrafos < datos.MinParrafos)
                throw new ArgumentException("El maximo de Oraciones por Parrafo debe ser mayor o igual al minimo");

            if (datos.MaxParrafos < datos.MinParrafos)
                throw new ArgumentException("El maximo de Parrafos debe ser mayor o igual al minimo");
        }
    }
}
