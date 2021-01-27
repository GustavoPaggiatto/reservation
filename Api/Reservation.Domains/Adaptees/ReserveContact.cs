namespace Reservation.Domains.Adaptees
{
    /// <summary>
    /// Represent a Reserve and Contact types union.
    /// </summary>
    public sealed class ReserveContact
    {
        public string FormatedSchedule { get; set; }
        public Domains.Entities.Reserve ReserveInfo { get; set; }
        public Domains.Entities.Contact ContactInfo { get; set; }
    }
}
