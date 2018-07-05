using System;
using System.Collections.Generic;
using Appointment = TC.SCBS.DTO.Appointment;

namespace TC.SCBS.DataAccess.Interfaces
{
    public interface IAppointmentRepository
    {

        List<Appointment> GetAppointmentsDto();
        Appointment GetAppointmentDto(int id);
        Entities.Appointment GetAppointmentEntity(int id);
        bool DoesAppointmentExist(int appointmentId);

        void AddOrUpdateAppointment(Appointment appointment);
        void RemoveAppointment(int appointmentId);

        int NumberOfAppointmentsOnCenter(int centerId, DateTime dateOfAppointment);
    }
}
