using System;
using System.Text;

namespace GeneradorTextos
{
    public class GeneradorTextosSimple : IGeneradorTexto
    {
        private Transversal.IProveedorAlfabeto _alfabeto;

        static int seed = Environment.TickCount;

        static readonly System.Threading.ThreadLocal<Random> _random = new System.Threading.ThreadLocal<Random>(
            () => new Random(System.Threading.Interlocked.Increment(ref seed)));

        public GeneradorTextosSimple(Transversal.IProveedorAlfabeto alfabeto)
        {
            _alfabeto = alfabeto;
        }

        public string GenerarTexto(DatosGeneradorTextos datos)
        {
            return GenerarTexto(_random.Value.Next(datos.TamanoMinimo, datos.TamanoMaximo));
        }

        private string GenerarTexto(int tamano)
        {
            var texto = new StringBuilder(tamano);

            int tamAlfabeto = _alfabeto.Alfabeto.Length;
            for (var i = 0; i < tamano - 1; i++)
            {
                texto.Append(_alfabeto.Alfabeto[_random.Value.Next(0, tamAlfabeto)]);
            }

            texto.Append(_alfabeto.SeparadorParrafos);
            return texto.ToString().Replace(_alfabeto.SeparadorParrafos.ToString(), _alfabeto.SeparadorParrafosCompleto);
        }
    }
}
