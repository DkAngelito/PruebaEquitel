using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServicioBackEndRest.Controllers
{
    public class AnalizadorTextosController : ApiController
    {
        private Transversal.IServicioAnalizadorTexto _analizador;
        private static int totalCount = 0;

        public AnalizadorTextosController(Transversal.IServicioAnalizadorTexto analizador)
        {
            _analizador = analizador;
        }

        // GET: api/AnalizadorTextos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AnalizadorTextos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AnalizadorTextos
        public void Post(Transversal.Entidades.DatosAnalisisTexto value)
        {
            _analizador.Analizar(value);
            System.Threading.Interlocked.Add(ref totalCount, 1);
            if (totalCount >= 20000)
                totalCount = totalCount;

        }

        // PUT: api/AnalizadorTextos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AnalizadorTextos/5
        public void Delete(int id)
        {
        }
    }
}
