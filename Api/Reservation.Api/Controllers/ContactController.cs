using log4net;
using Microsoft.AspNetCore.Mvc;
using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using Reservation.Domains.Interfaces.Services;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Reservation.Api.Controllers
{
    /// <summary>
    /// Controller Contact (corresponding to Mediator of Contact).
    /// </summary>
    [ApiController]
    [Route("Contact")]
    public class ContactController : ControllerBase
    {
        private readonly ILog _logger;

        public ContactController(ILog logger) { this._logger = logger; }

        /// <summary>
        /// Get contacts filtering by 'name' attribute with all log traces.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByName")]
        public Result<IEnumerable<Contact>> GetByName(string name)
        {
            this._logger.Debug("Starting method Get(); Tier: Api; Class: ContractController.");

            var service = this.HttpContext.RequestServices.GetService<IContactService>();
            var result = service.Get(name);

            this._logger.Debug("Finishing method Get(); Tier: Api; Class: ContractController.");

            return result;
        }

        /// <summary>
        /// Get contact by 'id' attribute with all log traces (return only one entity).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public async Task<Result<Contact>> GetById(int id)
        {
            this._logger.Debug("Starting method Get(id); Tier: Api; Class: ContractController.");

            var service = this.HttpContext.RequestServices.GetService<IContactService>();
            var result = await service.Get(id);

            this._logger.Debug("Finishing method Get(id); Tier: Api; Class: ContractController.");

            return result;
        }

        /// <summary>
        /// Get all contacts in database with all log traces.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public Result<IEnumerable<Contact>> GetAll()
        {
            this._logger.Debug("Starting method Get(); Tier: Api; Class: ContractController.");

            var service = this.HttpContext.RequestServices.GetService<IContactService>();
            var result = service.Get();

            this._logger.Debug("Finishing method Get(); Tier: Api; Class: ContractController.");

            return result;
        }

        /// <summary>
        /// Delete a contact by 'id' attribute with all log traces.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete")]
        public Result Delete(Contact model)
        {
            this._logger.Debug("Starting method Delete(); Tier: Api; Class: ContractController.");

            var service = this.HttpContext.RequestServices.GetService<IContactService>();
            var result = service.Delete(model);

            this._logger.Debug("Finishing method Delete(); Tier: Api; Class: ContractController.");

            return result;
        }

        /// <summary>
        /// Create contact with all log traces. This action is 'POST' method due security reasons.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public Result Create(Contact model)
        {
            this._logger.Debug("Starting method Create(); Tier: Api; Class: ContractController.");

            var service = this.HttpContext.RequestServices.GetService<IContactService>();
            var result = service.Insert(model);

            this._logger.Debug("Finishing method Create(); Tier: Api; Class: ContractController.");

            return result;
        }

        /// <summary>
        /// Edit contact with all log traces. This action is 'POST' method due security reasons.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Edit")]
        public Result Edit(Contact model)
        {
            this._logger.Debug("Starting method Edit(); Tier: Api; Class: ContractController.");

            var service = this.HttpContext.RequestServices.GetService<IContactService>();
            var result = service.Update(model);

            this._logger.Debug("Finishing method Edit(); Tier: Api; Class: ContractController.");

            return result;
        }
    }
}
