using System;
using System.Runtime.Serialization;
using FluentValidation;
using FluentValidation.Attributes;

namespace TC.SCBS.DTO
{
    [DataContract]
    public class Appointment
    {
        [DataMember]
        public int Id;

        [DataMember]
        public string ClientFullName;

        [DataMember]
        public DateTime Date;

        [DataMember]
        public Center Center;

        [DataMember]
        public int CenterId;
        public bool ShouldSerializeCenterId() => false;

    }


    /// <summary>
    /// Represents a new appointment being requested by the client. 
    /// </summary>
    /// <remarks>
    /// Class is used as a simple way to encapsulate validation on the HTTP POST verb action.
    /// </remarks>
    [Validator(typeof(AppointmentAddValidator))]
    public class AppointmentAdd: Appointment
    {
    }


    /// <summary>
    /// Represents a updated appointment being requested by the client. 
    /// </summary>
    /// <remarks>
    /// Class is used as a simple way to encapsulate validation on the HTTP PUT verb action.
    /// </remarks>
    [Validator(typeof(AppointmentUpdateValidator))]
    public class AppointmentUpdate: Appointment
    {
    }

    [Validator(typeof(AppointmentDeleteValidator))]
    public class AppointmentDelete : Appointment
    {
    }


    /// <summary>
    /// Validation rules when attempting to "POST" an appointment. 
    /// </summary>
    /// <remarks>
    /// Appointment Id must be null
    /// </remarks>
    public class AppointmentAddValidator : AbstractValidator<Appointment>
    {
        public AppointmentAddValidator()
        {
            RuleFor(x => x.Id).Empty();
            RuleFor(x => x.ClientFullName).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.CenterId).NotEmpty();
            
        }
    }

    /// <summary>
    /// Validation rules when attempting to "PUT" an appointment. 
    /// </summary>
    /// <remarks>
    /// Appointment Id must be not null
    /// </remarks>
    public class AppointmentUpdateValidator : AbstractValidator<AppointmentUpdate>
    {
        public AppointmentUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ClientFullName).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.CenterId).NotEmpty();
        }
    }


    /// <summary>
    /// Validation rules when attempting to "PUT" an appointment. 
    /// </summary>
    /// <remarks>
    /// Appointment Id must not be null
    /// </remarks>
    public class AppointmentDeleteValidator : AbstractValidator<AppointmentDelete>
    {
        public AppointmentDeleteValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }


}
