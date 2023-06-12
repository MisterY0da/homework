namespace LibraryAPI.Application.Models;

public class LibraryCatalogDto
{
    public int CollectionSize { get; set; }
    public int ReadersPerMonth { get; set; }
    public int SupplyVolume { get; set; }
    public string Address { get; set; }
}