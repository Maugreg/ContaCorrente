using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Domain.Validation
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException(IEnumerable<DomainValidationMessage> messages) : base()
        {
            this.ValidationError = messages;
        }

        public IEnumerable<DomainValidationMessage> ValidationError { get; }
    }
}
