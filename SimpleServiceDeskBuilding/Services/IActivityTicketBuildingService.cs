using SimpleServiceDeskBuilding.DTOs;
using SimpleServiceDeskBuilding.Models;

namespace SimpleServiceDeskBuilding.Services
{
    public interface IActivityTicketBuildingService
    {
        /// <summary>
        /// Create information status activity ticket building
        /// </summary>
        /// <param name="activity_ticket"></param>
        /// <returns></returns>
        Task<int> CreateActivityTicketBuilding(ActivityTicketBuildingModel activity_ticket);
        /// <summary>
        /// Process information status activity ticket building
        /// </summary>
        /// <param name="activityTicket"></param>
        /// <returns></returns>
        Task<int> ProcessActivityTicketBuilding(ActivityTicketBuildingModel activityTicket);
        /// <summary>
        /// Get all information activity ticket building
        /// </summary>
        /// <returns></returns>
        Task<List<GetAllActivityTicketBuildingDto>> GetAllActivityTicketBuilding();
    }
}
