using JaveragesLibrary.Domain;
using JaveragesLibrary.Domain.Filters;
using System.Text.Json;

namespace JaveragesLibrary.Infrastructure;

public class MangaRepository
{
    private readonly List<MangaFilter> _mangas;
    private readonly string _filePath;

    public MangaRepository(IConfiguration configuration)
    {
        _filePath = configuration.GetValue<string>("dataBank") ?? "javerage.library.data.json";
        _mangas = LoadData();
    }

    private string GetCurrentFilePath()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var currentFilePath = Path.Combine(currentDirectory, _filePath);
        return currentFilePath;
    }

    private List<MangaFilter> LoadData()
    {
        var currentFilePath = GetCurrentFilePath();
        if (File.Exists(currentFilePath))
        {
            
        }
        return [];
    }

    public IEnumerable<MangaFilter> GetAllMangas()
    {
        return _mangas;
    }

    public MangaFilter? GetMangaById(string id)
    {
        return _mangas.Find(m => m.Id == id);
    }

    public void AddManga(MangaFilter manga)
    {
        _mangas.Add(manga);
        var currentFilePath = GetCurrentFilePath();
        if (!File.Exists(currentFilePath))
        {
            File.Create(currentFilePath).Close();
        }
        File.WriteAllText(currentFilePath, JsonSerializer.Serialize(_mangas, new JsonSerializerOptions { WriteIndented = true }));
    }

    public void UpdateManga(string id, MangaFilter updatedManga)
    {
        var manga = GetMangaById(id);
        if (manga != null)
        {
            manga.Title = updatedManga.Title;
            manga.Genre = updatedManga.Genre;
            manga.YearPublished = updatedManga.YearPublished;
            // Después de actualizar, guarda los cambios en el archivo
            File.WriteAllText(GetCurrentFilePath(), JsonSerializer.Serialize(_mangas, new JsonSerializerOptions { WriteIndented = true }));
        }
    }

    public void DeleteManga(string id)
    {
        var manga = GetMangaById(id);
        if (manga != null)
        {
            _mangas.Remove(manga);
            // Después de eliminar, guarda los cambios en el archivo
            File.WriteAllText(GetCurrentFilePath(), JsonSerializer.Serialize(_mangas, new JsonSerializerOptions { WriteIndented = true }));
        }
    }

    public IEnumerable<MangaFilter> SearchMangas(MangaFilter filter)
    {
        return [.. _mangas.Where(filter.BuildFilter())];

        // // Forma manual
        // IQueryable<Manga> query = _mangas.AsQueryable();

        // if (!string.IsNullOrEmpty(filter.Title))
        // {
        //     query = query.Where(m => m.Title.Contains(filter.Title, StringComparison.OrdinalIgnoreCase));
        // }

        // if (!string.IsNullOrEmpty(filter.Author))
        // {
        //     query = query.Where(m => m.Author.Contains(filter.Author, StringComparison.OrdinalIgnoreCase));
        // }

        // if (!string.IsNullOrEmpty(filter.Genre))
        // {
        //     query = query.Where(m => m.Genre.Contains(filter.Genre, StringComparison.OrdinalIgnoreCase));
        // }

        // return [.. query];
    }
}
