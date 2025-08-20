using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace StringLib;

public static class TextUtil
{
    public static List<string> SplitIntoWords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return [];
        }

        // Регулярное выражение для поиска слов:
        // - Слово начинается и заканчивается на букву.
        // - Может содержать апострофы и дефисы внутри.
        // - Не содержит чисел или знаков препинания.
        const string pattern = @"\p{L}+(?:[\-\']\p{L}+)*";
        Regex regex = new(pattern, RegexOptions.Compiled);

        return regex.Matches(text)
            .Select(match => match.Value)
            .ToList();
    }

    public static string CapitalizeWords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        StringBuilder result = new(text.Length);
        Regex wordRegex = new(@"\p{L}+(?:[\-']\p{L}+)*", RegexOptions.Compiled);

        int lastIndex = 0;
        foreach (Match match in wordRegex.Matches(text))
        {
            // Копируем текст до текущего слова без изменений
            result.Append(text.Substring(lastIndex, match.Index - lastIndex));

            string word = match.Value;
            if (word.Length > 0)
            {
                string capitalized = char.ToUpper(word[0], CultureInfo.CurrentCulture) + word.Substring(1);
                result.Append(capitalized);
            }

            lastIndex = match.Index + match.Length;
        }

        // Добавляем оставшуюся часть текста (если есть)
        if (lastIndex < text.Length)
        {
            result.Append(text.Substring(lastIndex));
        }

        return result.ToString();
    }
}