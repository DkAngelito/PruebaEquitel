using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class Program
    {
        internal static SimpleInjector.Container Container;

        static void Main(string[] args)
        {
            // Configuración inicial del Injector de Dependencias.
            try
            {
                Container = new SimpleInjector.Container();
                var configIOC = new ConfiguracionIOC();
                configIOC.Configurar(Container);
                Container.Verify();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Ocurrio un error al verificar las dependencias.\n" +
                    "Revise la configuración, recompile y vuelva a ejecutar la aplicación.\n" +
                    "Detalle del error: {0}", ex));
                throw;
            }

            Container.GetInstance<ClienteGenerador>().Iniciar();            
        }
    }
}
