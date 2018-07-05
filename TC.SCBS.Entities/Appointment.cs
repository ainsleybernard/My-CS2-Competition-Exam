namespace TC.SCBS.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Appointment_Id { get; set; }

        public int Center_Id { get; set; }

        public DateTime Date_Of_Appointment { get; set; }

        public string Client_Full_Name { get; set; }

        public virtual Center Center { get; set; }
    }
}
