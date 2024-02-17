using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using TestStrony.Main;
using System;
using TestStrony.PostgreSQL;
using TestStrony.Services;
using TestStrony.Helpers;
using TestStrony.Models;

public class IndexModel : PageModel
{
    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    public string SearchResult { get; set; }

    public void OnGet()
    {
        // Pocz¹tkowe ustawienia strony
    }

    public List<WynikModel> SearchResults { get; set; }

    public void OnPostSearch()
    {
        string firstName = FirstName;
        string lastName = LastName;
        string url = LinkGettingSystem.Link(firstName, lastName);
        Dictionary<string, double> records = DictionaryBuilder.CalculatePointsFromShortCourseToLongCourseTime(DictionaryBuilder.AthleteRecords(url), url);
        List<string> queries = PostgreSQLQueryBuilder.GetRudolphPointsQuery(records, url);

        SearchResults = new List<WynikModel>();

        int adder = 0;
        foreach (var (key, value) in records)
        {
            int odp = Convert.ToInt32(SqlDataManager.DataBaseConnection(queries[adder], key));
            Console.WriteLine(DataFormat.TranslateStrokeBack(key));
            Console.WriteLine(odp);
            SearchResults.Add(new WynikModel
            {
                Dystans = $"Na dystansie {DataFormat.TranslateStrokeBack(key)}",
                Wynik = (odp >= 1) ? $"uzyska³eœ {odp}pkt w skali Rudolpha" : "uzyska³eœ/aœ mniej ni¿ 1 pkt"
            });
            adder++;
        }
    }

    public class WynikModel
    {
        public string Dystans { get; set; }
        public string Wynik { get; set; }
    }

}