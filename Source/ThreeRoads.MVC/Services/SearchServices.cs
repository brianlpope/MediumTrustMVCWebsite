using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeRoads.Data.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace ThreeRoads.MVC.Services
{
    public class SearchServices
    {
        public List<SearchResult> FormatSearchResults(string query, List<SearchResult> searchResults)
        {
            // initialize output
            StringBuilder sb = new StringBuilder();
            List<SearchResult> items = new List<SearchResult>();

            // foreach returned content item
            // create a new search result
            foreach (SearchResult item in searchResults)
            {
                int matchCount = 0;
                sb.Clear();
                // remove markup before processing
                string searchSource = Regex.Replace(item.Value, "<[^>]*>", "");

                // within content item
                // retrieve and format the specific result text
                Regex regex = new Regex(string.Concat(@"(?'entire'.{0,60}(?'criteria'", query, @").{0,60})"), RegexOptions.Multiline | RegexOptions.IgnoreCase);
                MatchCollection matches = regex.Matches(searchSource);
                for (int i = 0; i < matches.Count; i++)
                {
                    matchCount++;
                    string resultString = matches[i].Groups["entire"].Value;
                    string matchedString = matches[i].Groups["criteria"].Value;
                    sb.AppendFormat("...{0}", resultString.Replace(matchedString, string.Concat("<b>", matchedString, "</b>")));
                    sb.AppendLine();
                }
                // finalize the formatting and add to result collection
                if (matchCount > 0)
                {
                    sb.Append("...");
                    item.Value = sb.ToString();
                    item.MatchCount = matchCount;
                    items.Add(item);
                }
            }
            return items;
        }
    }
}