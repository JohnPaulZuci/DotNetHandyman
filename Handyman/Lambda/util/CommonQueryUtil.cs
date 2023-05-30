

namespace CommonQueryUtilNS
{

    public class CommonQueryUtil
    {

        internal static List<string> GetFormattedQuery(string givenQuery)
        {
             var strings = givenQuery.Split(';').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            return strings;
        }
    }
}