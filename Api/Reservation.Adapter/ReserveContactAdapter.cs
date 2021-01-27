using log4net;
using Reservation.Domains.Adaptees;
using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using Reservation.Domains.Interfaces.Adapters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Reservation.Adapter
{
    /// <summary>
    /// Reserve and Contact adapter (adapt this two types in only one and return to client).
    /// </summary>
    public sealed class ReserveContactAdapter : BaseAdapter<ReserveContact>, IReserveContactAdapter
    {
        private readonly ILog _logger;

        public ReserveContactAdapter(ILog logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Receive reservs and contacts collection, iterate this, formatting each one and return.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override Result<IEnumerable<ReserveContact>> Adaptee(params object[] source)
        {
            var result = new Result<IEnumerable<ReserveContact>>();
            var content = new List<ReserveContact>();

            try
            {
                var reservs = source[0] as IEnumerable<Reserve>;
                var contacts = source[1] as IEnumerable<Contact>;

                foreach (var reserve in reservs)
                {
                    var contact = contacts.First(c => c.Id == reserve.ContactId);
                    content.Add(new ReserveContact()
                    {
                        ContactInfo = contact,
                        ReserveInfo = reserve,
                        FormatedSchedule = reserve.Schedule.ToString("f", new CultureInfo("en-US"))
                    });
                }

                result.Content = content;
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                result.AddError("An error occurred while formatting reservation / contact data.");
            }

            return result;
        }
    }
}
