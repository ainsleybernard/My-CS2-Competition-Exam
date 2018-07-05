using System.Runtime.Serialization;

namespace TC.SCBS.DTO
{
    [DataContract]
    public class Center
    {
        [DataMember]
        public int Id;

        [DataMember]
        public string Name;

        [DataMember]
        public string StreetAddress;

        [DataMember]
        public int CenterTypeId;

        [DataMember]
        public string CenterTypeValue;

        public int MaxAccommodationQuantityPerDay;
    }
}
