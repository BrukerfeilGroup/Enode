﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Common.Enums;

namespace Brukerfeil.Enode.Common.Repositories
{
    public interface IDifiMessageRepository
    {
        Task<IEnumerable<DifiMessage>> GetDifiMessagesAsync(string organizationId, Direction direction);
        Task<DifiMessage> GetDifiMessageAsync(string organizationId, string msgId);
    }
}
