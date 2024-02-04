using System.Text.RegularExpressions;

namespace Tryzoob
{
    public class MediaFormats
    {
        public static bool IsValidImageFormat(string url)
        {
            return Regex.IsMatch(url, @"\.(jpg|jpeg|png)$", RegexOptions.IgnoreCase);
        }

        public static bool IsValidVideoFormat(string url)
        {
            return Regex.IsMatch(url, @"\.(mp4|avi|mov)$", RegexOptions.IgnoreCase);
        }
    }
}
