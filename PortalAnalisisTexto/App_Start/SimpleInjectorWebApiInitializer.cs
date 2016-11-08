[assembly: WebActivator.PostApplicationStartMethod(typeof(PortalAnalisisTexto.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace PortalAnalisisTexto.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using AnalizadorTexto;
    using Transversal;
    
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IProveedorAlfabeto, AlfabetoEstandar>();
            container.Register<IServicioAnalizadorTexto, ServicioAnalizadorDirecto>();
            container.Register<IAnalizadorTexto, AnalizadorTextoEstandar>();
            container.Register<IRepositorioResultadoAnalisisTexto, ReporteEstadisticasADisco>();
        }
    }
}