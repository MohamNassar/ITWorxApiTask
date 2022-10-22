using System;
using System.Collections.Generic;

namespace Domain.Exceptions
{
    public class AggregateBadRequestException : AggregateException
    {
        public AggregateBadRequestException(IEnumerable<Exception> innerExceptions)
           : base(innerExceptions)
        {
        }
    }
}