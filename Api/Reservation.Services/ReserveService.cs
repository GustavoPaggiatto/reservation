using log4net;
using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using Reservation.Domains.Interfaces.Repositories;
using Reservation.Domains.Interfaces.Services;
using System;

namespace Reservation.Services
{
    /// <summary>
    /// Business object to manipulate Reserve instances.
    /// </summary>
    public sealed class ReserveService : BaseService<Reserve>, IReserveService
    {
        private IContactService _contactService;

        public ReserveService(
            IReserveRepository repository,
            ILog logger,
            IContactService contactService) : base(repository, logger)
        {
            this._contactService = contactService;
        }

        /// <summary>
        /// Insert a reserve in repository.
        /// Here we have a override due the necessary validations.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result Insert(Reserve instance)
        {
            this._logger.Debug("Starting method Insert(); Tier: Service; Class: ReserveService.");

            Result result = new Result();

            try
            {
                if (instance.Description == null || string.IsNullOrEmpty(instance.Description))
                {
                    result.AddError("Description was not set.");
                    return result;
                }

                instance.Description = instance.Description.Trim();

                if (instance.Schedule == DateTime.MinValue)
                {
                    result.AddError("Birthdate was not informed.");
                    return result;
                }

                if (instance.ContactId == 0)
                {
                    result = this._contactService.Insert(instance.Contact);

                    if (result.Error)
                        return result;
                }

                result = base.Insert(instance);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"An error occurred while inserting Reserve, {this._defaultExceptionText}");
            }
            finally
            {
                this._logger.Debug("Finishing method Insert(); Tier: Service; Class: ReserveService.");
            }

            return result;
        }

        /// <summary>
        /// Update a reserve in repository.
        /// Here we have a override due the necessary validations.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result Update(Reserve instance)
        {
            this._logger.Debug("Starting method Update(); Tier: Service; Class: ReserveService.");

            Result result = new Result();

            try
            {
                if (instance.Description == null || string.IsNullOrEmpty(instance.Description))
                {
                    result.AddError("Description was not set.");
                    return result;
                }

                instance.Description = instance.Description.Trim();

                if (instance.Schedule == DateTime.MinValue)
                {
                    result.AddError("Birthdate was not informed.");
                    return result;
                }

                if (instance.ContactId == 0)
                {
                    result.AddError("Contact was not informed.");
                    return result;
                }

                result = base.Update(instance);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"An error occurred while updating Reserve, {this._defaultExceptionText}");
            }
            finally
            {
                this._logger.Debug("Finishing method Update(); Tier: Service; Class: ReserveService.");
            }

            return result;
        }
    }
}
