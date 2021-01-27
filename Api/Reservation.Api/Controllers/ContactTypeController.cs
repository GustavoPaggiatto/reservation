using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using Reservation.Domains.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Reservation.Api.Controllers
{
    /// <summary>
    /// Controller Contact Type (corresponding to Mediator of Contact Type).
    /// </summary>
    [ApiController]
    [Route("ContactType")]
    public class ContactTypeController : ControllerBase
    {
        private readonly ILog _logger;

        public ContactTypeController(ILog logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Get all contact types in database with all log traces.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public Result<IEnumerable<ContactType>> GetAll()
        {
            this._logger.Debug("Starting method GetAll(); Tier: Api; Class: ContactTypeController.");

            var service = this.HttpContext.RequestServices.GetService<IContactTypeService>();
            var result = service.Get();

            this._logger.Debug("Finishing method GetAll(); Tier: Api; Class: ContactTypeController.");

            return result;
        }
    }
}
