using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Application.OrderProcess
{
    public interface IOrderProcess
    {
        public void CreateOrderProcess(string topic, string message);
    }
}
