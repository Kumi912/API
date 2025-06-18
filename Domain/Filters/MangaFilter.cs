using JaveragesLibrary.Domain;

namespace JaveragesLibrary.Domain.Filters;

public class MangaFilter
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public int? YearPublished { get; set; }
    public string Id { get; set; } = Guid.NewGuid().ToString();


    public Func<MangaFilter, bool> BuildFilter()
    {
        Func<MangaFilter, bool> filter = m => true; // Filtro inicial que acepta todos los mangas

        if (!string.IsNullOrEmpty(Title))
        {
            filter = filter.And(m => m.Title.Contains(Title, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(Genre))
        {
            filter = filter.And(m => m.Genre.Contains(Genre, StringComparison.OrdinalIgnoreCase));
        }

        if (YearPublished.HasValue)
        {
            filter = filter.And(m => m.YearPublished == YearPublished.Value);
        }

        return filter;
    }
}