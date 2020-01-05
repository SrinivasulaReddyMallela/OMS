using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entities
{
    public enum OrderStatus
    {
        Placed = 1,
        Approved = 2,
        Cancelled = 3,
        Friday = 4,
        InDelivery = 5,
        Completed = 6
    }
}
