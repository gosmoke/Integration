using System.Collections.Generic;
using Integration.Exceptions.Types;

namespace Integration
{
    public class ServiceMessage
    {
        public Severity Severity { get; set; }
        public string Message { get; set; }
        public ExceptionType ExceptionType { get; set; } = ExceptionType.None;
        public IEnumerable<ServiceMessage> Details { get; set; }
    }
}
