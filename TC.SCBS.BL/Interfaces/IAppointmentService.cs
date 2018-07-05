using System.Collections.Generic;
using TC.SCBS.DTO;

namespace TC.SCBS.BL.Services
{
    public interface IAppointmentService
    {
        List<Appointment> GetAppointments();
        Appointment GetAppointment(int id);
        OperationAddResult AddAppointment(Appointment appointment);
        OperationUpdateResult UpdateAppointment(Appointment appointment);
        OperationDeleteResult DeleteAppointment(int id);
    }
}
