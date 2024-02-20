using Microsoft.Extensions.Primitives;
using System.Text;
using System.Text.RegularExpressions;

namespace TestStrony.Helpers
{
    public class TextCrawler
    {
        public static string GetAge(string url)
        {
            int date = DateTime.Now.Year - Convert.ToInt32(GetNumericValue(Scraper.Loader(url).DocumentNode.SelectSingleNode("//div[@id='name']").InnerText));
            return (date <= 18) ? Convert.ToString(date) : "open"; //this function return the Age of athlete or return open if an age is above 18
        }
        public static string GetNumericValue(string input)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(input);
            return (match.Success) ? match.Value : string.Empty; //this is helper function responsible for getting rid of non numeric strings
        }
        public static string GetTableName(string url) => (GetAge(url) == "open") ? $"rudolphtableopen{CheckGender(url)}" : $"rudolphtable{GetAge(url)}yearsold{CheckGender(url)}";
        public static string CheckGender(string url) => Scraper.Loader(url).DocumentNode.SelectSingleNode("//div[@id='name']").InnerHtml.Contains("gender1.png") ? "boys" : "girls";
        public static string SpecialCharacterConverter(string input)
        {
            Dictionary<char, char> characterMap = new Dictionary<char, char>
            {
                {'ą', 'a'}, {'ć', 'c'}, {'ę', 'e'},
                {'ł', 'l'}, {'ń', 'n'}, {'ó', 'o'},
                {'ś', 's'}, {'ź', 'z'}, {'ż', 'z'}
            };
            StringBuilder resultString = new StringBuilder(input.Length);
            foreach (char singleCharacter in input) { resultString.Append(characterMap.ContainsKey(singleCharacter) ? characterMap[singleCharacter] : singleCharacter); }
            return resultString.ToString();
        }
    }
}
