using SimpleServiceDeskBuilding.Exceptions;
using SimpleServiceDeskBuilding.Helpers;
using SimpleServiceDeskBuilding.Models;
using SimpleServiceDeskBuilding.Repositories;
using System.Data;
using System.Net.Sockets;

namespace SimpleServiceDeskBuilding.Services.Impl
{
    public class TicketBuildingService : ITicketBuildingService
    {
        private readonly IGenericRepository _repository;
        private readonly IActivityTicketBuildingService _activityTicketBuildingService;

        public TicketBuildingService(IGenericRepository repository, IActivityTicketBuildingService activityTicketBuildingService)
        {
            _repository = repository;
            _activityTicketBuildingService = activityTicketBuildingService;
        }

        public async Task<int> CreateTicketBuilding(TicketBuildingModel ticket)
        {
            string query = $"insert into public.tb_master_ticket_building(id_user_created, title, description, file_attachment) values (@id_user_created, @title, @description, @file_attachment) returning *;";

            var createNewTicket = await _repository.SaveAsync<TicketBuildingModel>(query, ticket);
            if (createNewTicket == null)
            {
                throw new Exception("Error when insert data");
            }

            Guid currentIdTicket = createNewTicket.id;
            DateTime dateNow= DateTime.Now;

            // cuma ini saja diperlukan saat insert ticket
            var activityTicket = new ActivityTicketBuildingModel
            {
                id_ticket = currentIdTicket,
                id_user_done = Guid.Empty,
                status = Constants.create,
                last_created_at = DateTime.Parse(dateNow.ToString("yyyy-MM-dd HH:mm:dd"))
            };

            var createNewActivityTicket = await _activityTicketBuildingService.CreateActivityTicketBuilding(activityTicket);
            if (createNewActivityTicket == 0)
            {
                // bikin logic hapus ticket yang ditable tb_master_ticket_building
                await DeleteTicketBuildingById(currentIdTicket.ToString());
                throw new Exception("Error when insert data");
            }

            return createNewActivityTicket;
        }

        public async Task<int> DeleteTicketBuildingById(string ticket_id)
        {
            string query = $"delete * from public.tb_master_ticket_building where id = ${ticket_id}";
            var deleteTicket = await _repository.DeleteAsync(query, new { });
            if (deleteTicket == 0)
            {
                throw new Exception("Error when delete data");
            }

            return deleteTicket;
        }

        public async Task<TicketBuildingModel> GetTicketBuildingById(string ticket_id)
        {
            string query = $"select * from public.tb_master_ticket_building where id = '{ticket_id}'";

            var dataTicket = await _repository.FindByAsync<TicketBuildingModel>(query, new { });
            if (dataTicket == null)
            {
                throw new NotFoundException("Data ticket building not found");
            }

            return dataTicket;
        }
        
    }
}
