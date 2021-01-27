using log4net;
using Microsoft.AspNetCore.Mvc;
using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using Reservation.Domains.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Reservation.Domains.Adaptees;
using Reservation.Domains.Interfaces.Adapters;
using System.Threading.Tasks;

namespace Reservation.Api.Controllers
{
    /// <summary>
    /// Controller Reserve (corresponding to Mediator of Reserve).
    /// </summary>
    [ApiController]
    [Route("Reserve")]
    public class ReserveController : ControllerBase
    {
        private readonly ILog _logger;

        public ReserveController(ILog logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Create reserve with all log traces. This action is 'POST' method due security reasons.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public Result Create(Reserve model)
        {
            this._logger.Debug("Starting method Create(); Tier: Api; Class: ReserveController.");

            var service = this.HttpContext.RequestServices.GetService<IReserveService>();
            var result = service.Insert(model);

            this._logger.Debug("Finishing method Create(); Tier: Api; Class: ReserveController.");

            return result;
        }

        /// <summary>
        /// Edit reserve with all log traces. This action is 'POST' method due security reasons.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Edit")]
        public Result Edit(Reserve model)
        {
            this._logger.Debug("Starting method Edit(); Tier: Api; Class: ReserveController.");

            var service = this.HttpContext.RequestServices.GetService<IReserveService>();
            var result = service.Update(model);

            this._logger.Debug("Finishing method Edit(); Tier: Api; Class: ReserveController.");

            return result;
        }

        /// <summary>
        /// Set ranking of reserve with all log traces. This action is 'POST' method due security reasons.
        /// </summary>
        /// <param name="reserve"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetRanking")]
        public async Task<Result> SetRanking(Reserve reserve)
        {
            var result = new Result();
            this._logger.Debug("Starting method SetRanking(); Tier: Api; Class: ReserveController.");

            var service = this.HttpContext.RequestServices.GetService<IReserveService>();
            var getResult = await service.Get(reserve.Id);

            if (getResult.Error)
            {
                result.AddError(getResult.Messages.First());
                return result;
            }

            getResult.Content.Ranking = reserve.Ranking;
            result = service.Update(getResult.Content);

            this._logger.Debug("Finishing method SetRanking(); Tier: Api; Class: ReserveController.");

            return result;
        }

        /// <summary>
        /// Set if reserve is favorite with all log traces. This action is 'POST' method due security reasons.
        /// </summary>
        /// <param name="reserve"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetFavorite")]
        public async Task<Result> SetFavorite(Reserve reserve)
        {
            var result = new Result();
            this._logger.Debug("Starting method SetFavorite(); Tier: Api; Class: ReserveController.");

            var service = this.HttpContext.RequestServices.GetService<IReserveService>();
            var getResult = await service.Get(reserve.Id);

            if (getResult.Error)
            {
                result.AddError(getResult.Messages.First());
                return result;
            }

            getResult.Content.Favorite = reserve.Favorite;
            result = service.Update(getResult.Content);

            this._logger.Debug("Finishing method SetFavorite(); Tier: Api; Class: ReserveController.");

            return result;
        }

        /// <summary>
        /// Get reserve by 'id' attribute with all log traces.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public async Task<Result<Reserve>> GetById(int id)
        {
            this._logger.Debug("Starting method GetById(); Tier: Api; Class: ReserveController.");

            var service = this.HttpContext.RequestServices.GetService<IReserveService>();
            var result = await service.Get(id);

            this._logger.Debug("Finishing method GetById(); Tier: Api; Class: ReserveController.");

            return result;
        }

        /// <summary>
        /// Get all contacts with reservs (ReserveContact type) in database with all log traces.
        /// Here we use a Adapter (IReserveContactAdapter) for adapt those two types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public Result<IEnumerable<ReserveContact>> GetAll()
        {
            this._logger.Debug("Starting method GetAll(); Tier: Api; Class: ReserveController.");

            var result = new Result<IEnumerable<ReserveContact>>();
            var reserveService = this.HttpContext.RequestServices.GetService<IReserveService>();

            var reserveResult = reserveService.Get();

            if (reserveResult.Error)
            {
                result.AddError(reserveResult.Messages.First());
                return result;
            }

            var reservs = reserveResult.Content;

            if (reservs == null || reservs.Count() == 0)
                return result;

            var contactService = this.HttpContext.RequestServices.GetService<IContactService>();
            var contactResult = contactService.Get(reservs.Select(r => r.ContactId));

            if (contactResult.Error)
            {
                result.AddError(contactResult.Messages.First());
                return result;
            }

            var contacts = contactResult.Content;

            var adapter = this.HttpContext.RequestServices.GetService<IReserveContactAdapter>();
            result = adapter.Adaptee(reservs, contacts);

            this._logger.Debug("Finishing method GetAll(); Tier: Api; Class: ReserveController.");

            return result;
        }
    }
}
