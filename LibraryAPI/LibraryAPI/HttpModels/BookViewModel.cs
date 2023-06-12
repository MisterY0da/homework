namespace LibraryAPI.HttpModels;

public record BookViewModel
{
    public string AuthorName { get; init; }
    public string Genre { get; init; }
    public string Title { get; init; }
    public int Edition { get; init; }
}