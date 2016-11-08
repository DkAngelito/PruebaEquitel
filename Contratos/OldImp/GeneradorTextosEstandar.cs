using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GeneradorTextos.Old
{
    public class GeneradorTextosEstandar : IGeneradorTexto
    {
        private const int TAMANOMINIMO = 1024;
        private const int TAMANOMAXIMO = 1048576;

        private IProveedorAlfabetos _alfabeto;
        //private static Random _random= new Random();

        static int seed = Environment.TickCount;

        static readonly System.Threading.ThreadLocal<Random> _random = new System.Threading.ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public GeneradorTextosEstandar(IProveedorAlfabetos alfabeto)
        {
            _alfabeto = alfabeto;            
        }

        public string GenerarTexto(Old.DatosGeneradorTextos datos)
        {
            ValidarParametros(datos);

            return GenerarTexto(_random.Value.Next(datos.MinParrafos, datos.MaxParrafos), datos.MinOracionesXParrafo, datos.MaxOracionesXParrafo, datos.MinPalabrasXOracion, datos.MaxPalabrasXOracion, datos.MinLetrasXPalabra, datos.MaxLetrasXPalabra);
        }

        private string GenerarTexto(int parrafos, int minOracionesXParrafo, int maxOracionesXParrafo, int minPalabrasXOracion, int maxPalabrasXOracion, int minLetrasXPalabra, int maxLetrasXPalabra)
        {
            StringBuilder texto = new StringBuilder();

            for (var i = 0; i < parrafos; i++)
            {
                texto.Append(GenerarParrafo(_random.Value.Next(minPalabrasXOracion, maxPalabrasXOracion), minPalabrasXOracion, maxPalabrasXOracion, minLetrasXPalabra, maxLetrasXPalabra));
                texto.Append(_alfabeto.SeparadorParrafos);
            }
            return texto.ToString();
        }

        private string GenerarParrafo(int oraciones, int minPalabrasXOracion, int maxPalabrasXOracion, int minLetrasXPalabra, int maxLetrasXPalabra)
        {
            StringBuilder parrafo = new StringBuilder();

            for (var i = 0; i < oraciones; i++)
            {
                parrafo.Append(GenerarOracion(_random.Value.Next(minPalabrasXOracion, maxPalabrasXOracion), minLetrasXPalabra, maxLetrasXPalabra));
                parrafo.Append(_alfabeto.SeparadorOraciones);
            }
            return parrafo.ToString();
        }

        private string GenerarOracion(int palabras, int tamanoMinPalabra, int tamanoMaxPalabra)
        {
            StringBuilder oracion = new StringBuilder();

            for (var i = 0; i < palabras - 1; i++)
            {
                oracion.Append(GenerarPalabra(_random.Value.Next(tamanoMinPalabra, tamanoMaxPalabra)));
                oracion.Append(_alfabeto.SeparadoresPalabras[_random.Value.Next(0, _alfabeto.SeparadoresPalabras.Length)]);
            }

            // La ultima palabra se genera fuera del ciclo para no estar validando cada vez si es la ultima palabra
            // para añadir el separador.
            oracion.Append(GenerarPalabra(_random.Value.Next(tamanoMinPalabra, tamanoMaxPalabra)));

            return oracion.ToString();
        }

        private string GenerarPalabra(int tamano)
        {
            StringBuilder palabra = new StringBuilder(tamano);
            int tamAlfabeto = _alfabeto.Alfabeto.Length;
            for (var i = 0; i < tamano - 1; i++)
            {
                palabra.Append(_alfabeto.Alfabeto[_random.Value.Next(0, tamAlfabeto)]);
            }
            return palabra.ToString();
        }

        private void ValidarParametros(DatosGeneradorTextos datos)
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
