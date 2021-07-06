﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Services
{
    public interface IPaymentQueueService
    {
        Task SendMessageAsync<T>(T serviceBusMessage, string queueName);
    }
}
