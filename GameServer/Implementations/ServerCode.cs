using GameServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Implementations
{
    public class ServerCode : IServerCode
    {
        public Guid ServerId { get; set; }
        public string ServerConnectCode { get; set; }

        public ServerCode()
        {
            
        }

        public IServerCode GenerateServerCode()
        {
            return new ServerCode()
            {
                ServerId = Guid.NewGuid(),
                ServerConnectCode = GenerateRandomConnectCode()
            };

        }

        private string GenerateRandomConnectCode()
        {
            return "12AS";
        }

    }
}
