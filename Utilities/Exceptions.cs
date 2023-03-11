using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Exceptions.Types
{
    public enum ExceptionType
    {
        None,
        AccessViolationException,
        ArgumentException,
        ArgumentNullException,
        ArgumentOutOfRangeException,
        ConcurrencyException,
        Exception,
        IndexOutOfRangeException,
        InvalidOperationException,
        NullReferenceException
    }
}
