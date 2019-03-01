using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Infrastructure.Helper
{
    public class RESTSecurityAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                if (context.HttpContext.Request.Headers["token"].Count <= 0 ||
                    context.HttpContext.Request.Headers["token"] == "")
                {
                    var semToken = new JsonResult("informe o token 'token' no header da requisição.") { StatusCode = 400 };
                    context.Result = semToken;
                    return;
                }

                string token = context.HttpContext.Request.Headers["token"];

                if (!Authorize(token))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validar o token JWT, esta estatico. Mas a intencao e token dinamico. Conforme as regras de Seguranca
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool Authorize(string token)
        {
            try
            {
                if (token == "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NjU3ODkifQ.rqspsTJ79_Oz_FKfOvOw9ForZTsK5HOHxjNL6ctngr8")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
