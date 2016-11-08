
namespace Transversal
{
    public interface IProveedorAlfabeto
    {
        char[] Alfabeto { get; }
        char SeparadorOraciones { get; }
        char SeparadorParrafos { get; }
        char[] SeparadoresPalabras { get; }
        string SeparadorParrafosCompleto { get; }
    }
}
