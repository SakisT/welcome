using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using welcome.Models;

namespace welcome.ViewModels
{
    public class ReservationView
    {
        public Reservation Reservation { get; set; }
        public StayRoom[] StayRooms { get; set; }

        public StayPerson[] StayPersons { get; set; }
        public Deposit[] Deposits { get; set; }
        public Pricelist Pricelist { get; set; }
        public Agent Agent { get; set; }

        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
    }
}
