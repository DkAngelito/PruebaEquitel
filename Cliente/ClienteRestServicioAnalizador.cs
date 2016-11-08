using System;
using System.Net.Http;

namespace Cliente
{
    public class ClienteRestServicioAnalizador : Transversal.IServicioAnalizadorTexto
    {
        static HttpClient client;

        static ClienteRestServicioAnalizador()
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["UriServicioTextos"];
            if (string.IsNullOrEmpty(url))
                throw new Exception("Debe configurar la URL del servicio en el appSettings UriServicioTextos");


            System.Net.ServicePointManager.UseNagleAlgorithm = true;
            System.Net.ServicePointManager.Expect100Continue = true;
            System.Net.ServicePointManager.CheckCertificateRevocationList = true;
            System.Net.ServicePointManager.DefaultConnectionLimit = 1000;

            var myUri = new Uri(url);

            var servicePoint = System.Net.ServicePointManager.FindServicePoint(myUri);

            client = new HttpClient();
            client.BaseAddress = myUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));            
        }

        public void Analizar(Transversal.Entidades.DatosAnalisisTexto datos)
        {

            var tarea = client.PostAsJsonAsync("api/AnalizadorTextos", datos);

            tarea.ContinueWith(task =>
                {
                    if (!task.Result.IsSuccessStatusCode)
                    {
                        Console.WriteLine(string.Format("La patecion {0} no se proceso correctamente. Codigo: {1}", datos.IdPeticion, task.Result.IsSuccessStatusCode));
                    }
                });
        }
    }
}
