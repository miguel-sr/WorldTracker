using Microsoft.AspNetCore.Mvc;
using System.Net;
using WorldTracker.Common.Interfaces;

namespace WorldTracker.Web
{
    public class AppControllerBase : ControllerBase
    {
        /// <summary>
        /// Generic method to execute actions with centralized error handling.
        /// This method catches HTTP request exceptions and general errors,
        /// returning the appropriate status code.
        /// </summary>
        /// <returns>Returns an <see cref="IActionResult"/> representing the result of the action execution or the occurred error.</returns>
        protected async Task<IActionResult> ExecuteWithErrorHandling(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (HttpRequestException error)
            {
                var status = (int)(error.StatusCode ?? HttpStatusCode.InternalServerError);
                return StatusCode(status, error.Message);
            }
            catch (Exception error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }

        /// <summary>
        /// Creates a <see cref="CreatedAtActionResult"/> that generates an HTTP 201 (Created) response,
        /// including the created object in the response body and the URL to access it.
        /// </summary>
        /// <param name="actionName">The name of the action used to generate the URL of the created resource.</param>
        /// <param name="value">Object that contains the identifier (ID) of the created resource.</param>
        /// <returns>A <see cref="CreatedAtActionResult"/> representing the creation of the resource with the appropriate location.</returns>
        public virtual CreatedAtActionResult CreatedAtAction<TId>(string actionName, IEntity<TId> value)
        {
            return CreatedAtAction(actionName, new { id = value.Id }, value);
        }

        public virtual CreatedAtActionResult CreatedAtAction<T, TId>(string actionName, T value, Func<T, TId> idSelector)
        {
            return CreatedAtAction(actionName, new { id = idSelector(value) }, value);
        }
    }
}
