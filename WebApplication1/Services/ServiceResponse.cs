using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class ServiceResponse<T>
    {
        public T data { get; set; }
        public bool success { get; set; } = true;
        public string Message { get; set; } = null;
    }
}
