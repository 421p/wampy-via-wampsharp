using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WampExample.Tickets;
using WampSharp.V2;

namespace WampExample
{
    internal class Program
    {
        public static Task StartRouter() => Task.Run(() =>
        {
            var host = new DefaultWampHost("ws://127.0.0.1:11011/ws");
            host.Open();
        });


        public static Task StartServer() => Task.Run(async () =>
        {
            var factory = new DefaultWampChannelFactory();
            var channel = factory.CreateJsonChannel("ws://127.0.0.1:11011/ws", "realm1");

            await channel.Open();

            Console.WriteLine("channel ready");

            var realm = channel.RealmProxy;
            var subject = realm.Services.GetSubject<int>("ticket.count.changed");

            TicketStorage.CountChanged += count => subject.OnNext(count);

            Console.WriteLine("topic ready");

            await realm.Services.RegisterCallee(new TicketBuyerCallee());

            Console.WriteLine("procedures ready");
        });


        public static void Main(string[] args)
        {
            StartRouter().ContinueWith(x => StartServer());

            Console.WriteLine("Press any key to stop...");
            Console.ReadLine();
        }
    }
}