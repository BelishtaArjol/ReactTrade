using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ResultMessage<T>
    {
        public T ResultData { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
