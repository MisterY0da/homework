namespace LibraryAPI.HttpModels;

public record LibraryViewModel
{
    public int CollectionSize { get; init; }
    public int ReadersPerMonth { get; init; }
    public int SupplyVolume { get; init; }
    public string Address { get; init; }
}