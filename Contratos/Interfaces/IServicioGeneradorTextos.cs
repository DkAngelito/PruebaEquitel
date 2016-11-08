using System;

namespace GeneradorTextos
{
    public interface IServicioGeneradorTextos
    {
        string[] GenerarTextos(int cantidad, DatosGeneradorTextos datos);        
    }
}
