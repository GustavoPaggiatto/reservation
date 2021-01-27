using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservation.Domains.Interfaces.Services
{
    /// <summary>
    /// Represent a base contract of business implementations.
    /// Here we have a base resources for optimize code reusing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// Ensure low coupling (avoid using 'new')
        /// </summary>
        /// <returns></returns>
        T New();

        /// <summary>
        /// Insert instance in database (instance is inherit from 'BaseEntity').
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Result Insert(T instance);

        /// <summary>
        /// Insert instances in database (instances are inherit from 'BaseEntity').
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        Result Insert(IEnumerable<T> instances);

        /// <summary>
        /// Update instance in database (instance inherit from 'BaseEntity').
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Result Update(T instance);

        /// <summary>
        /// Update instances in database (instance inherit from 'BaseEntity').
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        Result Update(IEnumerable<T> instances);

        /// <summary>
        /// Delete instance in database (instance inherit from 'BaseEntity').
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Result Delete(T instance);

        /// <summary>
        /// Delete instances in database (instance inherit from 'BaseEntity').
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        Result Delete(IEnumerable<T> instances);

        /// <summary>
        /// Get all instances in database (instance inherit from 'BaseEntity').
        /// </summary>
        /// <returns></returns>
        Result<IEnumerable<T>> Get();

        /// <summary>
        /// Get instance in database by 'id' attribute (instance inherit from 'BaseEntity').
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<T>> Get(int id);

        /// <summary>
        /// Get all instances in database that 'id' in colletion array parameter (instance inherit from 'BaseEntity').
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result<IEnumerable<T>> Get(IEnumerable<int> ids);
    }
}
