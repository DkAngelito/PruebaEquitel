using System;
using System.Threading.Tasks;

namespace GeneradorTextos
{
    public class ServicioGenTextosParalelos : IServicioGeneradorTextos 
    {        
        private IGeneradorTexto _generador;
        
        public ServicioGenTextosParalelos(IGeneradorTexto generador)
        {
            _generador = generador;
        }

        public string[] GenerarTextos(int cantidad, DatosGeneradorTextos datos)
        {
            ValidarDatos(cantidad, datos);
            var textos = new string[cantidad];
            Parallel.For(0, cantidad, i =>
            {
                textos[i] = _generador.GenerarTexto(datos);
            });
            return textos;
        }

        private void ValidarDatos(int cantidad, DatosGeneradorTextos datos)
        {
            if (cantidad < 1)
                throw new ArgumentOutOfRangeException("cantidad", "la cantidad de textos a generar debe ser mayor a 0");
        }    
    }
}
