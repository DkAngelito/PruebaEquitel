using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalAnalisisTexto.Controllers
{
    public class ValuesController : ApiController
    {
        private Transversal.IServicioAnalizadorTexto _analizador;

        public ValuesController(Transversal.IServicioAnalizadorTexto analizador)
        {
            _analizador = analizador;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(Transversal.Entidades.DatosAnalisisTexto value)
        {
            _analizador.Analizar(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
