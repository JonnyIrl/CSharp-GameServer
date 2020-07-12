using GameServer.Implementations;
using System;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating Server Manager");
            ServerManager serverManager = new ServerManager();
            serverManager.StartListening();
        }
    }
}
