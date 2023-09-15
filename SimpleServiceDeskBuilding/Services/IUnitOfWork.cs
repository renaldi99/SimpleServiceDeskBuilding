namespace SimpleServiceDeskBuilding.Services
{
    public interface IUnitOfWork
    {
        public IUserService UserService { get; }
        public ITicketBuildingService TicketBuildingService { get; }
        public IActivityTicketBuildingService ActivityTicketBuildingService { get; }
    }
}
