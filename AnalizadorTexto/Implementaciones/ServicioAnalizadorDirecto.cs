
namespace AnalizadorTexto
{
    public class ServicioAnalizadorDirecto : Transversal.IServicioAnalizadorTexto
    {
        IAnalizadorTexto _analizador;
        IRepositorioResultadoAnalisisTexto _repositorio;

        public ServicioAnalizadorDirecto(IAnalizadorTexto analizador, IRepositorioResultadoAnalisisTexto repositorio)
        {
            _analizador = analizador;
            _repositorio = repositorio;
        }

        public void Analizar(Transversal.Entidades.DatosAnalisisTexto datos)
        {
            _repositorio.GuardarDatos(datos, _analizador.AnalizarTexto(datos.Texto));
        }
    }
}
