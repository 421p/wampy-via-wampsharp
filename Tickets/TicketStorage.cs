using System;

namespace WampExample.Tickets
{
    public class TicketStorage
    {
        private static int _count = 100;

        public static int GetCount() => _count;

        public static event Action<int> CountChanged;

        public static int BuyOne()
        {
            if (_count == 0)
            {
                throw new Exception("no more tickets");
            }

            var id = _count;

            --_count;

            CountChanged?.Invoke(_count);

            return id;
        }
    }
}