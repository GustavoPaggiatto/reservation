using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using Reservation.Domains.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Data
{
    /// <summary>
    /// Abstract class of Repository pattern.
    /// In this layer, all repositories inherit the characteristics of this class.
    /// With this we guarantee the execution of an important pillar of OO and, consequently, we achieve an excellent code reduction.
    /// Note that other layers dont know that data is persisted with EF Core Code First (encapsulation pillar).
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        internal readonly ReservationContext _dbContext;
        protected readonly DbSet<T> _entities;
        protected ILog _logger;
        protected string _defaultExceptionText;

        public BaseRepository(ILog logger, IConfiguration config)
        {
            string sqlStr = config.GetConnectionString("default");
            
            this._dbContext = new ReservationContext(sqlStr);
            this._entities = this._dbContext.Set<T>();
            this._logger = logger;
            this._defaultExceptionText = "An unexpected error occurred while";
        }

        /// <summary>
        /// Base method for deleting an object from database.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public Result Delete(T instance)
        {
            var result = new Result();
            this._logger.Debug("Starting method Delete(); Tier: Repository; Class: BaseRepository.");

            try
            {
                this._entities.Remove(instance);
                this._dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"{this._defaultExceptionText} deleting record, " +
                    $"try again or request technical team to view logs etc.");
            }

            this._logger.Debug("Finishing method Delete(); Tier: Repository; Class: BaseRepository.");
            return result;
        }

        /// <summary>
        /// Base method for deleting a object collection from database.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        public Result Delete(IEnumerable<T> instances)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Base method for getting all objects from database.
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<T>> Get()
        {
            var result = new Result<IEnumerable<T>>();
            this._logger.Debug("Starting method Get(); Tier: Repository; Class: BaseRepository.");

            try
            {
                result.Content = this._entities.AsEnumerable();
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"{this._defaultExceptionText} getting record by 'Id', " +
                    $"try again or request technical team to view logs etc.");
            }

            this._logger.Debug("Finishing method Get(); Tier: Repository; Class: BaseRepository.");
            return result;
        }

        /// <summary>
        /// Base method for getting an object from database (async).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<T>> Get(int id)
        {
            var result = new Result<T>();
            this._logger.Debug("Starting method Get(id); Tier: Repository; Class: BaseRepository.");

            try
            {
                result.Content = await this._entities.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"{this._defaultExceptionText} getting record by 'Id', " +
                    $"try again or request technical team to view logs etc.");
            }

            this._logger.Debug("Finishing method Get(id); Tier: Repository; Class: BaseRepository.");
            return result;
        }

        /// <summary>
        /// Base method for getting an object collection from database filtering by related ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual Result<IEnumerable<T>> Get(IEnumerable<int> ids)
        {
            var result = new Result<IEnumerable<T>>();
            this._logger.Debug("Starting method Get(ids); Tier: Repository; Class: BaseRepository.");

            try
            {
                result.Content = this._entities.Where(s => ids.Any(id => id == s.Id));
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"{this._defaultExceptionText} getting record by 'Ids', " +
                    $"try again or request technical team to view logs etc.");
            }

            this._logger.Debug("Finishing method Get(ids); Tier: Repository; Class: BaseRepository.");
            return result;
        }

        /// <summary>
        /// Base method for insert an object to database.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public Result Insert(T instance)
        {
            var result = new Result();
            this._logger.Debug("Starting method Insert(); Tier: Repository; Class: BaseRepository.");

            try
            {
                this._entities.Add(instance);
                this._dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"{this._defaultExceptionText} inserting record, " +
                    $"try again or request technical team to view logs etc.");
            }

            this._logger.Debug("Finishing method Insert(); Tier: Repository; Class: BaseRepository.");
            return result;
        }

        /// <summary>
        /// Base method for insert an object collection to database.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        public Result Insert(IEnumerable<T> instances)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Base method for update an object from database.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public Result Update(T instance)
        {
            var result = new Result();
            this._logger.Debug("Starting method Update(); Tier: Repository; Class: BaseRepository.");

            try
            {
                if (instance != null)
                {
                    this._entities.Update(instance);
                    this._dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"{this._defaultExceptionText} updating record, " +
                    $"try again or request technical team to view logs etc.");
            }

            this._logger.Debug("Finishing method Update(); Tier: Repository; Class: BaseRepository.");
            return result;
        }

        /// <summary>
        /// Base method for update an object collection from database.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        public Result Update(IEnumerable<T> instances)
        {
            throw new System.NotImplementedException();
        }
    }
}
