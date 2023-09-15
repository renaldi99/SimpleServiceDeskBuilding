namespace SimpleServiceDeskBuilding.Services.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserService UserService { get; set; }
        public ITicketBuildingService TicketBuildingService { get; set; }
        public IActivityTicketBuildingService ActivityTicketBuildingService { get; set; }

        public UnitOfWork(IUserService userService, ITicketBuildingService ticketBuildingService, IActivityTicketBuildingService activityTicketBuildingService)
        {
            UserService = userService;
            TicketBuildingService = ticketBuildingService;
            ActivityTicketBuildingService = activityTicketBuildingService;
        }
    }
}
