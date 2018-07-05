using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using TC.SCBS.DataAccess.Interfaces;
using TC.SCBS.Entities;
using Appointment = TC.SCBS.DTO.Appointment;
using Center = TC.SCBS.DTO.Center;

namespace TC.SCBS.DataAccess.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly TCSCBSContext _dbContext;

        public AppointmentRepository(TCSCBSContext tcscbsContext)
        {
            _dbContext = tcscbsContext;
        }

        //todo get active appointments only

        #region "Queries returns DTO Types"


        public List<Appointment> GetAppointmentsDto()
        {
            var query = from appointments in _dbContext.Appointments
                        join centers in _dbContext.Centers on appointments.Center_Id equals centers.Center_Id into gj
                        from subCenter in gj.DefaultIfEmpty()
                        join centerTypes in _dbContext.CenterTypes on subCenter.Center_Type_Id equals centerTypes.Center_Type_Id into ab
                        from subCenterType in ab.DefaultIfEmpty()
                        select new Appointment
                        {
                            Id = appointments.Appointment_Id,
                            Date = appointments.Date_Of_Appointment,
                            ClientFullName = appointments.Client_Full_Name,
                            Center = new Center() { Id = subCenter.Center_Id, Name = subCenter.Name, StreetAddress = subCenter.Street_Address, CenterTypeId = subCenterType.Center_Type_Id, CenterTypeValue = subCenterType.Value }


                        };

            return query.ToList();
        }

        public Appointment GetAppointmentDto(int id)
        {
            var query = from appointments in _dbContext.Appointments.Where(x => x.Appointment_Id == id)
                        join centers in _dbContext.Centers on appointments.Center_Id equals centers.Center_Id into gj
                        from subCenter in gj.DefaultIfEmpty()
                        join centerTypes in _dbContext.CenterTypes on subCenter.Center_Type_Id equals centerTypes.Center_Type_Id into ab
                        from subCenterType in ab.DefaultIfEmpty()
                        select new Appointment
                        {
                            Id = appointments.Appointment_Id,
                            Date = appointments.Date_Of_Appointment,
                            ClientFullName = appointments.Client_Full_Name,
                            Center = new Center() { Id = subCenter.Center_Id, Name = subCenter.Name, StreetAddress = subCenter.Street_Address, CenterTypeId = subCenterType.Center_Type_Id,CenterTypeValue = subCenterType.Value }


                        };

            return query.FirstOrDefault();

        }

        #endregion

        #region "Queries returns EF Entity Types"
        public Entities.Appointment GetAppointmentEntity(int id)
        {
            //todo where active only
            return _dbContext.Appointments.FirstOrDefault(x => x.Appointment_Id == id);
        }
        #endregion

        public bool DoesAppointmentExist(int appointmentId)
        {
            //todo where active only
            return _dbContext.Appointments.Any(x => x.Appointment_Id == appointmentId);
        }



        public int NumberOfAppointmentsOnCenter(int centerId, DateTime dateOfAppointment)
        {

            return
                _dbContext.Appointments.Count(
                    x =>
                        x.Center_Id == centerId &&
                        x.Date_Of_Appointment.Year == dateOfAppointment.Year &&
                        x.Date_Of_Appointment.Month == dateOfAppointment.Month &&
                        x.Date_Of_Appointment.Day == dateOfAppointment.Day);

        }



        public void AddOrUpdateAppointment(Appointment appointment)
        {
            var entity = new Entities.Appointment()
            {
                Appointment_Id = appointment.Id,
                Client_Full_Name = appointment.ClientFullName,
                Date_Of_Appointment = appointment.Date,
                Center_Id = appointment.CenterId

            };

            _dbContext.Appointments.AddOrUpdate(entity);
            _dbContext.SaveChanges();

            //new appointments id are only set after being saved to the db.
            //therefore set appointment id on DTO object which is returned to the client.
            appointment.Id = entity.Appointment_Id;
        }

        /// <summary>
        /// Removes an appointment entity from the db.
        /// </summary>
        /// <param name="appointmentId"></param>
        public void RemoveAppointment(int appointmentId)
        {
            var appointment = GetAppointmentEntity(appointmentId);

            _dbContext.Appointments.Remove(appointment);
            _dbContext.SaveChanges();
        }


    }
}
