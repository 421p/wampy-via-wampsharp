using System;
using WampSharp.V2.Rpc;

namespace WampExample.Tickets
{
    public class TicketBuyerCallee
    {
        [WampProcedure("ticket.buy.one")]
        public int BuyTicket()
        {
            Console.WriteLine("BuyTicket procedure called");
            return TicketStorage.BuyOne();
        }

        [WampProcedure("ticket.get.count")]
        public int GetCount()
        {
            Console.WriteLine("GetCount procedure called");
            return TicketStorage.GetCount();
        }
    }
}