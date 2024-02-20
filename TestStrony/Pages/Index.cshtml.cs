using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using TestStrony.PostgreSQL;
using TestStrony.Services;
using TestStrony.Helpers;
using TestStrony.Models;

public class IndexModel : PageModel
{
    [BindProperty]
    public required string FirstName { get; set; }

    [BindProperty]
    public required string LastName { get; set; }

    public required string SearchResult { get; set; }

    public void OnGet()
    {
        // Pocz¹tkowe ustawienia strony
    }

    public required List<WynikModel> SearchResults { get; set; }

    public void OnPostSearch()
    {
        string url = LinkGettingSystem.Link(TextCrawler.SpecialCharacterConverter(FirstName.ToString()), TextCrawler.SpecialCharacterConverter(LastName.ToString()));
        Dictionary<string, double> records = DictionaryBuilder.CalculatePointsFromShortCourseToLongCourseTime(DictionaryBuilder.AthleteRecords(url), url);
        Console.WriteLine("Zakoñczono zbieranie rekordów");
        List<string> queries = PostgreSQLQueryBuilder.GetRudolphPointsQuery(records, url);
        Console.WriteLine("zakoñczono tworzenie query");
        SearchResults = [];

        int adder = 0;
        foreach (var (key, value) in records)
        {
            int odp = Convert.ToInt32(SqlDataManager.DataBaseConnection(queries[adder]));
            SearchResults.Add(new WynikModel
            {
                Dystans = $"{DataFormat.TranslateStrokeBack(key)}",
                Wynik = (odp >= 1) ? $"{odp}pkt" : "<1pkt"
            });
            adder++;
        }
        ViewData["SearchResults"] = SearchResults;
    }

    public class WynikModel
    {
        public required string Dystans { get; set; }
        public required string Wynik { get; set; }
    }

}