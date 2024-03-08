namespace Modulo_Reclutamiento_Web.Models
{
    public class Leaflet_Information
    {
        public int Key { get; set; }
        public string Key_Prospectus { get; set; }
        public string Signature { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string Marital_Status { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Salary { get; set; }
        public bool IsValidatedByRepresentantive { get; set; } 
        public List<Beneficiaries> Beneficiaries { get; set; }

        public int Document { get; set; }
        public string Document_Status { get; set; }

        public  List<Signatures> Document_Signatures { get; set; }
        public FingerPrints FingerPrints { get; set; }
    }
}
