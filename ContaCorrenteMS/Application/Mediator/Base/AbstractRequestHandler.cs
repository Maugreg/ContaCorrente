using ContaCorrente.Domain;
using ContaCorrente.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContaCorrenteMS.Application.Mediator.Base
{
    public abstract class AbstractRequestHandler<T> : IRequestHandler<T, Response> where T : IRequest<Response>
    {
        internal abstract HandleResponse HandleIt(T request, CancellationToken cancellationToken);

        public Task<Response> Handle(T request, CancellationToken cancellationToken)
        {
            Response r = new Response();

            if (request == null)
                return Task.FromResult(r);

            try
            {
                var result = HandleIt(request, cancellationToken);
                r.Content = result.Content;

            }
            catch (DomainValidationException ex)
            {
                r.DomainValidationMessages = ex.ValidationError;

            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = Path.GetFileName(frame.GetFileName());
                r.Error = $"Exception error: {ex.Message} file name: {file} - line {line}";
            }

            return Task.FromResult(r);
        }
    }

    internal class HandleResponse
    {
        public object Content { get; set; }
    }
}
