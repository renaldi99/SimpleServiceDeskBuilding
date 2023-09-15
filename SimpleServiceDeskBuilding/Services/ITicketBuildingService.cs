using SimpleServiceDeskBuilding.Models;

namespace SimpleServiceDeskBuilding.Services
{
    public interface ITicketBuildingService
    {
        /// <summary>
        /// Create new ticket building
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        Task<int> CreateTicketBuilding(TicketBuildingModel ticket);
        /// <summary>
        /// Get ticket building by id
        /// </summary>
        /// <param name="ticket_id"></param>
        /// <returns></returns>
        Task<TicketBuildingModel> GetTicketBuildingById(string ticket_id);
        /// <summary>
        /// Delete ticket building by id
        /// </summary>
        /// <param name="ticket_id"></param>
        /// <returns></returns>
        Task<int> DeleteTicketBuildingById(string ticket_id);

    }
}
