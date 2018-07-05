
namespace TC.SCBS.DTO
{
    public class OperationAddResult
    {
        //possible outcomes
        public enum AppointmentOperation
        {
            CenterDoesNotExist,
            CenterAccommodationFull,
            AppointmentCreated
        }

        public AppointmentOperation Outcome;
        public Appointment Appointment;
    }

    public class OperationUpdateResult
    {
        //possible outcomes
        public enum AppointmentOperation
        {
            AppointmentDoesNotExist,
            CenterDoesNotExist,
            CenterAccommodationFull,
            AppointmentUpdated
        }

        public AppointmentOperation Outcome;
        public Appointment Appointment;
    }

    public class OperationDeleteResult
    {
        //possible outcomes
        public enum AppointmentOperation
        {
            AppointmentDoesNotExist,
            AppointmentDeleted
        }

        public AppointmentOperation Outcome;
    }
}
