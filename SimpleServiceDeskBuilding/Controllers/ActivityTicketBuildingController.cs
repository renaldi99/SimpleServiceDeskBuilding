using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleServiceDeskBuilding.DTOs;
using SimpleServiceDeskBuilding.Exceptions;
using SimpleServiceDeskBuilding.Helpers;
using SimpleServiceDeskBuilding.Message;
using SimpleServiceDeskBuilding.Models;
using SimpleServiceDeskBuilding.Services;

namespace SimpleServiceDeskBuilding.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityTicketBuildingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityTicketBuildingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [RequestSizeLimit(5 * 1024 * 1024)]
        [HttpPost("[action]")]
        public async Task<ActionResult> ProcessActivityTicketBuilding([FromForm] ActivityTicketBuildingReqDto ticket)
        {
            var checkUser = await _unitOfWork.UserService.GetUserById(ticket.id_user_done);
            if (checkUser.identity_employee == "internal")
            {
                throw new BadRequestException("Process activity can only taken by vendor");
            }

            var isStatusExists = Constants.status_ticket.Contains(ticket.status);
            if (!isStatusExists)
            {
                throw new BadRequestException("Status activity ticket invalid");
            }

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

                var dataActivity = new ActivityTicketBuildingModel
                {
                    id = Guid.Parse(ticket.id_activity),
                    id_user_done = Guid.Parse(ticket.id_user_done),
                    file_attachment = newFileImageName,
                    description = ticket.description ?? "",
                    status = ticket.status
                };

                var dataTicketBuilding = await _unitOfWork.ActivityTicketBuildingService.ProcessActivityTicketBuilding(dataActivity);

                return Ok(new DefaultMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Success update activity ticket" });
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetListAllActivityTicketuilding()
        {
            var res = await _unitOfWork.ActivityTicketBuildingService.GetAllActivityTicketBuilding();

            return Ok(new PayloadMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Success get all activity ticket", payload = res });
        }
    }
}
