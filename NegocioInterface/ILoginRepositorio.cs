using Entidades;

namespace RepositorioInterface
{
    public interface ILoginRepositorio : IRepositorioGenerico<Login>
    {
        Login Autenticar(string login, string senha);
        Login GetPorIdLogin(int id);
        Login Inserir(Login entrada);
    }
}