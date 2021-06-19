using ePizzaHub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Interfaces
{
    public interface IUserAccessor
    {
        User GetUser();
    }
}
