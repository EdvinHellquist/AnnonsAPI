using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace AnnonsAPIgood.Models;

public partial class Annonsorer
{
    public int AnId { get; set; }

    public string AnNamn { get; set; } = null!;

    public long AnTele { get; set; }

    public long? AnOrgnr { get; set; }

    public long? AnSubnr { get; set; }
    [JsonIgnore]
    public int? AnFakturaadress { get; set; }
    [JsonIgnore]
    public int? AnUtdadress { get; set; }
    
    public virtual Adress? AnFakturaadressNavigation { get; set; }
    
    public virtual Adress? AnUtdadressNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Adress> TblAdresses { get; set; } = new List<Adress>();
    [JsonIgnore]
    public virtual ICollection<Ad> TblAds { get; set; } = new List<Ad>();
}
