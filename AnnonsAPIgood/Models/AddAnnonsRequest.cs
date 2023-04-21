namespace AnnonsAPIgood.Models
{
    public class AddAnnonsRequest
    {
        public int AdId { get; set; }
        public int AdPris { get; set; }
        public string AdInnehåll { get; set; }
        public string AdRubrik { get; set; }
        public int AdAnnonspris { get; set; }
        public int AdAnnonsor { get; set; }
    }
}
