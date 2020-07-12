using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Interfaces
{
    public interface IServerManager
    {
        bool CreateServer(Guid serverId);
        bool ProcessCommand(Guid serverId, string message);
    }
}
