using SimpleServiceDeskBuilding.DTOs;
using SimpleServiceDeskBuilding.Exceptions;
using SimpleServiceDeskBuilding.Helpers;
using SimpleServiceDeskBuilding.Models;
using SimpleServiceDeskBuilding.Repositories;

namespace SimpleServiceDeskBuilding.Services.Impl
{
    public class ActivityTicketBuildingService : IActivityTicketBuildingService
    {
        private readonly IGenericRepository _repository;

        public ActivityTicketBuildingService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateActivityTicketBuilding(ActivityTicketBuildingModel activity_ticket)
        {
            string query = $"insert into public.tb_master_activity_ticket_building(id_ticket, id_user_done, status, description, file_attachment, last_created_at, last_updated_at, last_finished_at, is_closed) values ('{activity_ticket.id_ticket}', '{activity_ticket.id_user_done}', '{activity_ticket.status}', '{activity_ticket.description}', '{activity_ticket.file_attachment}', '{activity_ticket.last_created_at}', '{activity_ticket.last_updated_at}', '{activity_ticket.last_finished_at}', '{activity_ticket.is_closed}')";

            var createActivityTicket = await _repository.SaveAsync(query, new { });
            if (createActivityTicket == 0)
            {
                throw new Exception("Error when insert data");
            }

            return createActivityTicket;
        }

        public async Task<List<GetAllActivityTicketBuildingDto>> GetAllActivityTicketBuilding()
        {
            string query = $"select atb.id as id_activity, atb.id_ticket as id_ticket, atb.id_user_done as pic, atb.status as status,\r\natb.description as desc_activity, atb.file_attachment as file_activity, atb.last_created_at,\r\natb.last_updated_at, atb.last_finished_at, tb.id_user_created, tb.title, tb.description, tb.file_attachment\r\nfrom public.tb_master_ticket_building tb inner join public.tb_master_activity_ticket_building atb\r\non atb.id_ticket = tb.id";

            var getAllActivityTicket = await _repository.FindAllByAsync<GetAllActivityTicketBuildingDto>(query, new { });
            return getAllActivityTicket;
        }

        public async Task<int> ProcessActivityTicketBuilding(ActivityTicketBuildingModel activityTicket)
        {
            DateTime dateNow = DateTime.Now;

            if (activityTicket.status == Constants.done)
            {
                activityTicket.last_updated_at = Convert.ToDateTime(dateNow.ToString("yyyy-MM-dd HH:mm:ss"));
                activityTicket.last_finished_at = Convert.ToDateTime(dateNow.ToString("yyyy-MM-dd HH:mm:ss"));
                activityTicket.is_closed = true;
            }

            if (activityTicket.status == Constants.progress)
            {
                activityTicket.last_updated_at = Convert.ToDateTime(dateNow.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            string query = $"update public.tb_master_activity_ticket_building set (id_user_done, status, description, file_attachment, last_updated_at, last_finished_at, is_closed) = ('{activityTicket.id_user_done}', '{activityTicket.status}', '{activityTicket.description}', '{activityTicket.file_attachment}', '{activityTicket.last_updated_at}', '{activityTicket.last_finished_at}', '{activityTicket.is_closed}') where id = '{activityTicket.id}'";

            var processActivity = await _repository.UpdateAsync(query, new { });
            if (processActivity == 0)
            {
                throw new NotFoundException("Error when update ticket building");
            }

            return processActivity;
        }
    }
}
