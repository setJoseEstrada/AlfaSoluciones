using Entidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class JwtTokenService
    {
        public Login DecodeJwtToken(string token)
        {
            // EXTRAE LA INFORMACIÓN DEL TOKEN JWT.
            var handler = new JwtSecurityTokenHandler();
            var _token = handler?.ReadJwtToken(token);

            // CREA EL PERFIL DE INFORMACIÓN DEL USUARIO
            // A PARTIR DE LOS CLAIMS DEL TOKEN JWT
            var _logininfo = new Login()
            {
                correo = _token?.Claims?.
                    SingleOrDefault(x => x.Type == "corre")?.Value,

                contrasena = _token?.Claims?.
                    SingleOrDefault(x => x.Type == "contrasena")?.Value

             
            };
            return _logininfo;
        }
    }

    // CREAMOS LA INTERFAZ DE LA CLASE, PARA PODER 
    // INYECTARLA POR DEPENDENCIAS.
    public interface IJwtTokenService
    {
        Login DecodeJwtToken(string token);
    }
}

