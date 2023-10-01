using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TesteAccenture.Controllers
{
    public class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();


        protected ActionResult ResponseCustomizada(object result = null)
        {
            if (ValidarOperacao())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Erros.ToArray() }
            }));
        }

        protected ActionResult ResponseCustomizada(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                AddErros(erro.ErrorMessage);
            }

            return ResponseCustomizada();
        }

        protected bool ValidarOperacao()
        {
            return !Erros.Any();
        }

        protected void LimparErros()
        {
            Erros.Clear();
        }

        protected void AddErros(string erro)
        {
            Erros.Add(erro);
        }
    }
}
