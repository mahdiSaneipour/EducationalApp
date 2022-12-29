using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Order
{
    public interface IOrderServices
    {
        public int AddOrder(int userId, int courseId);
    }
}
