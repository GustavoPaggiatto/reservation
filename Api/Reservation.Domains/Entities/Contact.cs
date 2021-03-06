﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Reservation.Domains.Entities
{
    /// <summary>
    /// Represent a Contact.
    /// </summary>
    public sealed class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int ContactTypeId { get; set; }
        public string Logo { get; set; }
        public ContactType ContactType { get; set; }
        public IEnumerable<Reserve> Reservs { get; set; }
    }
}
