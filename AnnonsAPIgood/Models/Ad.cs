using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace AnnonsAPIgood.Models;

public partial class Ad
{
    public int AdId { get; set; }

    public int AdPris { get; set; }

    public string? AdInnehåll { get; set; }

    public string AdRubrik { get; set; } = null!;

    public int AdAnnonspris { get; set; }

    public int AdAnnonsor { get; set; }
    [JsonIgnore]
    public virtual Annonsorer? AdAnnonsorNavigation { get; set; }
}
