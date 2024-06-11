namespace AppMobile
{
    public class Student
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int GroupeId { get; set; }
        public int PromotionId { get; set; }
        public DateTime DateTimeGenerated { get; set; }
        public string QrCode { get; set; }
    }
}
