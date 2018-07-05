using System;
using System.Net;
using System.Web.Http;
using TC.SCBS.BL.Services;
using TC.SCBS.DTO;

namespace TC.SCBS.WebApi.Controllers
{

    public class AppointmentsController : ApiController
    {
        private readonly IAppointmentService _appointmentService;
        private const string ErrorMsgCenterDoesNotExist = "The TC service center requested does not exist";

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        // GET api/centers

        [HttpGet]
        public IHttpActionResult GetAllAppointments()
        {
            var appointments = _appointmentService.GetAppointments();
            return (appointments == null || appointments.Count == 0) ? NotFound() : (IHttpActionResult)Ok(appointments);
        }

        // GET api/centers/5

        [HttpGet]
        public IHttpActionResult GetAppointment(int id)
        {
            var appointment = _appointmentService.GetAppointment(id);
            return appointment == null ? (IHttpActionResult)NotFound() : Ok(appointment);
        }



        /// <summary>
        /// Attempts to add an appointment to the centralized database 
        /// </summary>
        /// <remarks>
        /// Valid request: Client full name, appointment date (valid format only), and center id required.
        /// </remarks>
        /// <param name="appointment"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddAppointment(AppointmentAdd appointment)
        {

            var result = _appointmentService.AddAppointment(appointment);

            switch (result.Outcome)
            {
                case OperationAddResult.AppointmentOperation.CenterDoesNotExist:
                    return BadRequest(ErrorMsgCenterDoesNotExist);
                case OperationAddResult.AppointmentOperation.CenterAccommodationFull:
                    return Content(HttpStatusCode.Conflict, appointment);
                case OperationAddResult.AppointmentOperation.AppointmentCreated:
                    return CreatedAtRoute("DefaultApi", new { id = result.Appointment.Id }, result.Appointment); //todo fix the create at route

                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
        /// <summary>
        /// Attempts to update an appointment on the centralized database 
        /// </summary>
        /// <remarks>
        /// Valid request: Appointment id, client full name, appointment date (valid format only), and center id required.
        /// </remarks>
        /// <param name="appointment"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult UpdateAppointment(AppointmentUpdate appointment)
        {

            var result = _appointmentService.UpdateAppointment(appointment);
            switch (result.Outcome)
            {
                case OperationUpdateResult.AppointmentOperation.AppointmentDoesNotExist:
                    return NotFound();
                case OperationUpdateResult.AppointmentOperation.CenterDoesNotExist:
                    return BadRequest(ErrorMsgCenterDoesNotExist);
                case OperationUpdateResult.AppointmentOperation.CenterAccommodationFull:
                    return Content(HttpStatusCode.Conflict, appointment);
                case OperationUpdateResult.AppointmentOperation.AppointmentUpdated:
                    return StatusCode(HttpStatusCode.NoContent);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Attempts to remove an appointment from the centralized database
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        [HttpDelete]       
        public IHttpActionResult CancelAppointment(AppointmentDelete appointment)
        {
            var result = _appointmentService.DeleteAppointment(appointment.Id);

            switch (result.Outcome)
            {
                case OperationDeleteResult.AppointmentOperation.AppointmentDoesNotExist:
                    return NotFound();
                case OperationDeleteResult.AppointmentOperation.AppointmentDeleted:
                    return StatusCode(HttpStatusCode.NoContent);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
