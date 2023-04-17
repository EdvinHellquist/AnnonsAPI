using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace AnnonsAPIgood.Models;

public partial class Adress
{
    public int AdrId { get; set; }

    public string AdrGata { get; set; } = null!;

    public string AdrOrt { get; set; } = null!;

    public int AdrPostnummer { get; set; }
    [JsonIgnore]
    public int AdrAnnonsor { get; set; }
    [JsonIgnore]
    public virtual Annonsorer? AdrAnnonsorNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Annonsorer> TblAnnonsorerAnFakturaadressNavigations { get; set; } = new List<Annonsorer>();
    [JsonIgnore]
    public virtual ICollection<Annonsorer> TblAnnonsorerAnUtdadressNavigations { get; set; } = new List<Annonsorer>();
}
