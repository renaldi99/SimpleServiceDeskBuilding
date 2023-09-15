using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleServiceDeskBuilding.DTOs;
using SimpleServiceDeskBuilding.Exceptions;
using SimpleServiceDeskBuilding.Helpers;
using SimpleServiceDeskBuilding.Message;
using SimpleServiceDeskBuilding.Models;
using SimpleServiceDeskBuilding.Services;
using Sprache;
using System.Net.Sockets;
using static Dapper.SqlMapper;

namespace SimpleServiceDeskBuilding.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketBuildingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketBuildingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [RequestSizeLimit(5 * 1024 * 1024)]
        [HttpPost("[action]")]
        public async Task<ActionResult> CreateTicketBuilding([FromForm] TicketBuildingReqDto ticket)
        {
            if (!ticket.file.ContentType.Equals("image/png"))
            {
                throw new BadRequestException("Upload image must be image/png");
            }

            IFormFile file = ticket.file;
            var fileImageName = Path.GetFileName(file.FileName);
            var newFileImageName = Guid.NewGuid() + "_" + fileImageName;

            var locationPathStorage = Path.Combine(Directory.GetCurrentDirectory(), "Public\\Storage");
            var locationTargetFile = Path.Combine(locationPathStorage, newFileImageName);

            try
            {
                if (System.IO.File.Exists(locationTargetFile))
                {
                    throw new BadRequestException("File Already Exists");
                }

                using (var filestream = new FileStream(locationTargetFile, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    filestream.Flush();
                }

                var dataTicket = new TicketBuildingModel
                {
                    id_user_created = Guid.Parse(ticket.id_user_created),
                    title = ticket.title,
                    description = ticket.description,
                    file_attachment = newFileImageName
                };

                var resCreateTicketBuilding = await _unitOfWork.TicketBuildingService.CreateTicketBuilding(dataTicket);

                return Ok(new DefaultMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Success create ticket building" });
            }
            catch
            {
                if (System.IO.File.Exists(locationTargetFile))
                {
                    System.IO.File.Delete(locationTargetFile);
                }
                throw;
            }

        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetTicketBuildingById([FromBody] TicketBuildingByIdDto ticket)
        {
            var dataTicketBuilding = await _unitOfWork.TicketBuildingService.GetTicketBuildingById(ticket.ticket_id);

            return Ok(new PayloadMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Success get data ticket building", payload = dataTicketBuilding });
        }
    }
}
