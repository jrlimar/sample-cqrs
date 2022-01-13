using Dev.Core.Messages.Notifications;
using Dev.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Dev.Api.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly IDomainNotification notificador;
        public ControllerBase(IDomainNotification notificador)
        {
            this.notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            return BadRequest(new
            {
                errors = notificador.ObterNotificacoes().Select(n => n.Value)
            });
        }

        protected void NotificarErro(string mensagem)
        {
            notificador.Handle(new DomainNotification("error" , mensagem));
        }
    }
}
