using System;

namespace GeneradorTextos
{
    public class DatosGeneradorTextos
    {
        private const int TAMANOMINIMODEF = 1024;
        private const int TAMANOMAXIMODEF = 2048;

        private int _tamanoMinimo = TAMANOMINIMODEF;
        private int _tamanoMaximo = TAMANOMAXIMODEF;

        public int TamanoMinimo
        {
            get { return _tamanoMinimo; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("El valor debe ser mayor que 0");
                else if(value > _tamanoMaximo)
                    throw new ArgumentOutOfRangeException("El valor debe ser menor o igual que el minimo");
                _tamanoMinimo = value;
            }
        }

        public int TamanoMaximo
        {
            get { return _tamanoMaximo; }
            set
            {
                if (value < _tamanoMinimo)
                    throw new ArgumentOutOfRangeException("El valor debe ser mayor o igual que el minimo");
                _tamanoMaximo = value;
            }
        }
    }
}
