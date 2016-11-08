using System;

namespace GeneradorTextos
{
    public class ServicioGenTextosSerial : IServicioGeneradorTextos
    {
        private IGeneradorTexto _generador;
        
        public ServicioGenTextosSerial(IGeneradorTexto generador)
        {
            _generador = generador;
        }

        public string[] GenerarTextos(int cantidad, DatosGeneradorTextos datos)
        {
            ValidarDatos(cantidad, datos);
            var textos = new string[cantidad];
            for (var i = 0; i < cantidad; i++)
            {
                textos[i] = _generador.GenerarTexto(datos);                
            }
            return textos;
        }

        private void ValidarDatos(int cantidad, DatosGeneradorTextos datos)
        {
            if (cantidad < 1)
                throw new ArgumentOutOfRangeException("cantidad", "la cantidad de textos a generar debe ser mayor a 0");
        }
    }
}
