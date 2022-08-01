using AcessoDados;
using Entidades;
using Microsoft.EntityFrameworkCore;
using RepositorioInterface;
using SimpleCrypto;

namespace Repositorio
{
    public class LoginRepositorio : RepositorioGenerico<Login>, ILoginRepositorio
    {
        public Login Autenticar(string login, string senha)
        {
            if ((string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha)))
                throw new ArgumentNullException("Alguns argumentos faltaram");

            try
            {
                Login _login;

                using (var context = new MyDbContext())
                {
                    _login = context.Login.FirstOrDefault(x => x.LoginNome.ToUpper() == login.ToUpper());
                }

                if (_login == null)
                    return null;

                try
                {
                    var crypt = new PBKDF2();

                    if (string.Compare(_login.Senha, crypt.Compute(senha, _login.Sal)) != 0)
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro crítico", ex);
                }

                return _login;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Erro crítico LoginNegocio");
            }
        }

        public Login GetPorIdLogin(int id)
        {            
            try
            {
                Login _login;

                using (var context = new MyDbContext())
                {
                    _login = context.Login.FirstOrDefault(x => x.LoginId == id);
                }

                if (_login == null)
                    return null;

                return _login;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Erro crítico LoginNegocio");
            }
        }

        public Login Inserir(Login entrada)
        {
            try
            {
                
                //entrada.Key = Guid.NewGuid().ToString().Replace("-", "");
                using (var context = new MyDbContext())
                {
                    if (!string.IsNullOrEmpty(entrada.Senha) && entrada.Senha == entrada.ConfirmacaoSenha)
                    {
                        var crypt = new PBKDF2();
                        var encrypts = crypt.Compute(entrada.Senha);
                        entrada.Senha = encrypts;
                        entrada.ConfirmacaoSenha = encrypts;
                        entrada.Sal = crypt.Salt;
                    }

                    var loginAdicionado = context.Login.Add(entrada);

                    context.SaveChanges();
                }

                entrada.Senha = string.Empty;

                return entrada;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Login Atualizar(Login entrada)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var object_update = context.Login.Where(u => u.LoginId == entrada.LoginId).FirstOrDefault();

                    if (object_update != null)
                    {
                        object_update.LoginNome = entrada.LoginNome;

                        if (!string.IsNullOrEmpty(entrada.Senha) && entrada.Senha != "*")
                        {
                            var crypt = new PBKDF2();
                            var encrypts = crypt.Compute(entrada.Senha);
                            object_update.Senha = encrypts;
                            object_update.Sal = crypt.Salt;
                        }
                        object_update.Email = entrada.Email;
                        context.Entry(object_update).State = EntityState.Modified;

                        
                    }
                }

                return entrada;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}