using System;

namespace Reservation.Domains.Entities
{
    /// <summary>
    /// Represent a Reserve.
    /// </summary>
    public sealed class Reserve : BaseEntity
    {
        public DateTime Schedule { get; set; }
        public int ContactId { get; set; }
        public int Ranking { get; set; }
        public bool Favorite { get; set; }
        public string Description { get; set; }
        public Contact Contact { get; set; }
    }
}
