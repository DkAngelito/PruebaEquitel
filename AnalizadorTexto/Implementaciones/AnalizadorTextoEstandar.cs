using System;
using System.Linq;

namespace AnalizadorTexto
{
    public class AnalizadorTextoEstandar : IAnalizadorTexto
    {
        private Transversal.IProveedorAlfabeto _alfabeto;

        // Se dejan los caracteres excluidos Quemados para agilizar el proceso y estaticos para ahorrar memoria.
        private static readonly char[] carExcluidos = { 'n', 'N' };
        private readonly char[] carEspeciales;

        public AnalizadorTextoEstandar(Transversal.IProveedorAlfabeto alfabeto)
        {
            _alfabeto = alfabeto;

            // Guardar Todos los CaractesEspeciales en nueva lista para facilitar la busqueda.
            carEspeciales = new char[_alfabeto.SeparadoresPalabras.Length + 2];
            Array.Copy(_alfabeto.SeparadoresPalabras, carEspeciales, _alfabeto.SeparadoresPalabras.Length);
            carEspeciales[_alfabeto.SeparadoresPalabras.Length] = _alfabeto.SeparadorOraciones;
            carEspeciales[_alfabeto.SeparadoresPalabras.Length + 1] = _alfabeto.SeparadorParrafos;
        }

        // Este metodo sacrifica un poco de mantenibilidad para mejorar el rendimiento a comparacion del metodo AnalizarTextoOld.
        public ResultadoAnalisisTexto AnalizarTexto(string texto)
        {
            var res = new ResultadoAnalisisTexto();
            var buffer = texto.ToCharArray();

            try
            {
                int palabrasXOracion = 0;
                bool antEspecial = false;
                for (var i = 0; i < buffer.Length -1; i++)
                {
                    if (carEspeciales.Contains(buffer[i]))
                    {
                        // Se evita el caso de varios caracteres especiales seguidos
                        if (!antEspecial)
                        {
                            antEspecial = true;
                            palabrasXOracion++;
                            if (palabrasXOracion == 16)
                                res.OracionesConMasDe15Pal++;
                        }
                        if (i > 0 && buffer[i - 1] == 'n')
                            res.PalabrasTerminadasEnn++;

                        if (buffer[i] == _alfabeto.SeparadorOraciones)
                        {
                            palabrasXOracion = 0;
                            if (buffer[i + 1] == '\n')
                            {
                                res.Parrafos++;
                                i++;
                            }
                        }
                    }
                    else
                    {
                        antEspecial = false;
                        if (!carExcluidos.Contains(buffer[i]))
                            res.NumeroCaracteresDistintosANn++;
                    }
                }
            }
            finally
            {
                buffer = null;
            }
            return res;
        }

        public ResultadoAnalisisTexto AnalizarTextoOld(string texto)
        {
            var res = new ResultadoAnalisisTexto();
            var buffer = texto.Replace(_alfabeto.SeparadorParrafosCompleto, _alfabeto.SeparadorParrafos.ToString()).ToCharArray();

            try
            {
                int palabrasXOracion = 0;
                bool antEspecial = false;
                for (var i = 0; i < buffer.Length; i++)
                {
                    if (carEspeciales.Contains(buffer[i]))
                    {
                        // Se evita el caso de varios caracteres especiales seguidos
                        if (!antEspecial)
                        {
                            antEspecial = true;
                            palabrasXOracion++;
                            if (palabrasXOracion == 16)
                                res.OracionesConMasDe15Pal++;
                        }

                        if (buffer[i] == _alfabeto.SeparadorOraciones)
                            palabrasXOracion = 0;
                        else if (buffer[i] == _alfabeto.SeparadorParrafos)
                        {
                            res.Parrafos++;
                            palabrasXOracion = 0;
                        }                        

                        if (i > 0 && buffer[i - 1] == 'n')
                            res.PalabrasTerminadasEnn++;
                    }
                    else
                    {
                        antEspecial = false;
                        if (!carExcluidos.Contains(buffer[i]))
                            res.NumeroCaracteresDistintosANn++;
                    }

                }
            }
            finally
            {
                buffer = null;
            }
            return res;
        }
    }
}
