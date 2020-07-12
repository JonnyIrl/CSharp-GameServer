using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Interfaces
{
    public interface IServerCode
    {
        Guid ServerId { get; set; }
        string ServerConnectCode { get; set; }
        IServerCode GenerateServerCode();
    }
}
