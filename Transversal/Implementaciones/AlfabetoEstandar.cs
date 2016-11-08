
namespace Transversal
{
    public class AlfabetoEstandar : IProveedorAlfabeto
    {
        // Se repiten caracteres especiales (separadores de palabra, oracion y parrafo) para aumentar la probabilidad de que aparezcan.
        private static readonly char[] _alfabeto = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz               ,,,,,,....***".ToCharArray();

        public char[] Alfabeto
        {
            get { return _alfabeto; }
        }

        public char SeparadorOraciones
        {
            get { return '.'; }
        }

        public char SeparadorParrafos
        {
            get { return '*'; }
        }

        public string SeparadorParrafosCompleto
        {
            get { return ".\n"; }
        }

        private static readonly char[] _separadorPalabras = " ,".ToCharArray();
        public char[] SeparadoresPalabras
        {
            get { return _separadorPalabras; }
        }
    }
}
