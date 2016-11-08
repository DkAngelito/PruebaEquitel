
namespace GeneradorTextos.Old
{
    public class AlfabetoEstandar : IProveedorAlfabetos
    {
        private static readonly char[] _alfabeto = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz".ToCharArray();
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
            get { return '\n'; }
        }


        private static readonly char[] _separadorPalabras = " ,".ToCharArray();
        public char[] SeparadoresPalabras
        {
            get { return _separadorPalabras; }
        }

        public string SeparadorParrafosCompleto
        {
            get { return ".\n"; }
        }
    }
}
