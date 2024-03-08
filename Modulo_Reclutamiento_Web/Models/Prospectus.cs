namespace Modulo_Reclutamiento_Web.Models
{
    public class Prospectus
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Date { get; set; }
        public bool IsValidatedRecruiter { get; set; }
        public bool IsValidatedReprecentative { get; set; }
    }
}
