using ContaCorrente.Domain.Validation;
using System;
using System.Collections.Generic;

namespace ContaCorrente.Domain
{
    public class Response
    {
        public object Content { get; set; }
        public string Error { get; set; }
        public IEnumerable<DomainValidationMessage> DomainValidationMessages { get; set; }
    }
}
