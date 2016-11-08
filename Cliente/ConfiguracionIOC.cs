using GeneradorTextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal;

namespace Cliente
{
    internal class ConfiguracionIOC
    {
        public void Configurar(SimpleInjector.Container container)
        {
            container.Register<ClienteGenerador>(); 
            container.Register<IGeneradorTexto, GeneradorTextosSimple>();
            container.Register<IProveedorAlfabeto, AlfabetoEstandar>();
            container.Register<IProveedorDatosGeneradorTextos, SolicitarDatosXConsola>();
            container.Register<IServicioGeneradorTextos, ServicioGenTextosParalelos>();
            container.Register<IServicioAnalizadorTexto, ClienteRestServicioAnalizador>();
            
            // Configuracion si se quiere realizar el analisis directo, sin pasar por el servicio.
            // container.Register<IServicioAnalizadorTexto, ServicioAnalizadorDirecto>();
            // container.Register<IAnalizadorTexto, AnalizadorTextoEstandar>();
            // container.Register<IRepositorioResultadoAnalisisTexto, ReporteEstadisticasADisco>();
        }
    }
}
