using System.Collections.Generic;
using TC.SCBS.DataAccess.Interfaces;
using TC.SCBS.DTO;
using Appointment = TC.SCBS.DTO.Appointment;

namespace TC.SCBS.BL.Services
{
    /// <summary>
    /// Appointment business service layer 
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICenterService _centerService;

        public AppointmentService(IAppointmentRepository appointmentRepository, ICenterService centerService)
        {
            _appointmentRepository = appointmentRepository;
            _centerService = centerService;
        }

        /// <summary>
        /// returns list of appointments
        /// </summary>
        /// <returns></returns>
        public List<Appointment> GetAppointments()
        {
            return _appointmentRepository.GetAppointmentsDto();
        }

        /// <summary>
        /// returns single appointment by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Appointment GetAppointment(int id)
        {
            return _appointmentRepository.GetAppointmentDto(id);
        }

   

        /// <summary>
        /// Checks to see if an appointment exist by the appointment id
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        private bool DoesAppointmentExist(int appointmentId)
        {
            return _appointmentRepository.DoesAppointmentExist(appointmentId);
        }

        /// <summary>
        /// Checks to see if a specific TC Service Center is available for accommodation on the requested date.
        /// </summary>
        /// <remarks>The Max Accomodation Quantity is set via Centers table in the db.</remarks>
        /// <param name="appointment"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        private bool IsServiceCenterAvailable(Appointment appointment, DTO.Center center)
        {
            var numberOfAppointmentsOnCenter = _appointmentRepository.NumberOfAppointmentsOnCenter(
              appointment.CenterId, appointment.Date);

            return numberOfAppointmentsOnCenter < center.MaxAccommodationQuantityPerDay;
        }

        /// <summary>
        /// Attempts to book a TC Service Center for a specific day under requestee name
        /// </summary>
        /// <remarks>
        /// 1. A valid TC Center must be requested.
        /// 2. TC Center must not exceed max accommodation per day
        /// </remarks>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public OperationAddResult AddAppointment(Appointment appointment)
        {

            var center = _centerService.GetCenter(appointment.CenterId);
            
            //add center to appointment to allow for center object to be part of response.
            appointment.Center = center;

            if (center == null)
            {
                return new OperationAddResult() { Outcome = OperationAddResult.AppointmentOperation.CenterDoesNotExist, Appointment = appointment };
            }

            if (!IsServiceCenterAvailable(appointment,center))
            {
                return new OperationAddResult() { Outcome = OperationAddResult.AppointmentOperation.CenterAccommodationFull, Appointment = appointment };
            }

            AddOrUpdateAppointment(appointment);

            return new OperationAddResult() { Outcome = OperationAddResult.AppointmentOperation.AppointmentCreated, Appointment = appointment };

        }


        /// <summary>
        /// Attempts to update an appointment
        /// </summary>
        /// <remarks>
        /// 1. Appointment must exist before updating
        /// 2. Valid exiting center on update must be requested.
        /// 3. TC Center must not exceed max accommodation per day
        /// </remarks>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public OperationUpdateResult UpdateAppointment(Appointment appointment)
        {

            if (!DoesAppointmentExist(appointment.Id))
            {
                return new OperationUpdateResult() { Outcome = OperationUpdateResult.AppointmentOperation.AppointmentDoesNotExist, Appointment = appointment };
            }

            var center = _centerService.GetCenter(appointment.CenterId);

            if (center == null)
            {
                return new OperationUpdateResult() { Outcome = OperationUpdateResult.AppointmentOperation.CenterDoesNotExist, Appointment = appointment };
            }

            if (!IsServiceCenterAvailable(appointment, center))
            {
                return new OperationUpdateResult() { Outcome = OperationUpdateResult.AppointmentOperation.CenterAccommodationFull, Appointment = appointment };
            }


            AddOrUpdateAppointment(appointment);

            return new OperationUpdateResult() { Outcome = OperationUpdateResult.AppointmentOperation.AppointmentUpdated, Appointment = appointment };

        }

        /// <summary>
        /// Attempts remove an appointment
        /// </summary>
        /// <remarks>
        /// Appointment must exist before removal.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public OperationDeleteResult DeleteAppointment(int id)
        {
            if (!DoesAppointmentExist(id))
            {
                return new OperationDeleteResult() { Outcome = OperationDeleteResult.AppointmentOperation.AppointmentDoesNotExist };
            }

            RemoveAppointment(id);

            return new OperationDeleteResult() { Outcome = OperationDeleteResult.AppointmentOperation.AppointmentDeleted };

        }

        /// <summary>
        /// Adds or update appointment entity to db.
        /// </summary>
        /// <param name="appointment"></param>
        private void AddOrUpdateAppointment(Appointment appointment)
        {
            _appointmentRepository.AddOrUpdateAppointment(appointment);
        }

        /// <summary>
        /// Removes an appointment entity from the db.
        /// </summary>
        /// <param name="appointmentId"></param>
        private void RemoveAppointment(int appointmentId)
        {
            _appointmentRepository.RemoveAppointment(appointmentId);

        }

    }
}
