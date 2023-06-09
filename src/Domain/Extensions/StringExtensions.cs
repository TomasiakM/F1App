using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Domain.Extensions;
public static class StringExtensions
{
    public static string ToUrlFriendly(this string str)
    {
        var normalizedString = str.Normalize(NormalizationForm.FormD);
        normalizedString = normalizedString.Replace('ł', 'l').Replace('Ł', 'L');

        var stringBuilder = new StringBuilder(capacity: normalizedString.Length);
        for (int i = 0; i < normalizedString.Length; i++)
        {
            char c = normalizedString[i];
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        var text = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

        return Regex.Replace(text, @"[^A-Za-z0-9_~]+", "-").ToLower();
    }
}
