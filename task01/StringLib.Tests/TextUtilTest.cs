using StringLib;

namespace StringLib.Tests;

public class TextUtilTest
{
    [Theory]
    [MemberData(nameof(SplitIntoWordParams))]
    public void Can_split_into_words(string input, string[] expected)
    {
        List<string> result = TextUtil.SplitIntoWords(input);
        Assert.Equal(expected, result);
    }

    public static TheoryData<string, string[]> SplitIntoWordParams()
    {
        return new TheoryData<string, string[]>
        {
            // Апостроф считается частью слова
            { "Can't do that", ["Can't", "do", "that"] },

            // Буква "Ё" считается частью слова
            { "Ёжик в тумане", ["Ёжик", "в", "тумане"] },
            { "Уж замуж невтерпёж", ["Уж", "замуж", "невтерпёж"] },

            // Дефис в середине считается частью слова
            { "Что-нибудь хорошее", ["Что-нибудь", "хорошее"] },
            { "mother-in-law's", ["mother-in-law's"] },
            { "up-to-date", ["up-to-date"] },
            { "Привет-пока", ["Привет-пока"] },

            // Слова из одной буквы допускаются
            { "Ну и о чём речь?", ["Ну", "и", "о", "чём", "речь"] },

            // Смена регистра не мешает разделению на слова
            { "HeLLo WoRLd", ["HeLLo", "WoRLd"] },
            { "UpperCamelCase or lowerCamelCase?", ["UpperCamelCase", "or", "lowerCamelCase"] },

            // Цифры не считаются частью слова
            { "word123", ["word"] },
            { "123word", ["word"] },
            { "word123abc", ["word", "abc"] },

            // Знаки препинания не считаются частью слова
            { "C# is awesome", ["C", "is", "awesome"] },
            { "Hello, мир!", ["Hello", "мир"] },
            { "Много   пробелов", ["Много", "пробелов"] },

            // Пустые строки, пробелы, знаки препинания
            { null!, [] },
            { "", [] },
            { "   \t\n", [] },
            { "!@#$%^&*() 12345", [] },
            { "\"", [] },

            // Пограничные случаи с апострофами и дефисами
            { "-привет", ["привет"] },
            { "привет-", ["привет"] },
            { "'hello", ["hello"] },
            { "hello'", ["hello"] },
            { "--привет--", ["привет"] },
            { "''hello''", ["hello"] },
            { "'a-b'", ["a-b"] },
            { "--", [] },
            { "'", [] },
        };
    }

    [Theory]
    [MemberData(nameof(CapitalizeWordsParams))]
    public void Can_capitalize_words(string input, string expected)
    {
        string result = TextUtil.CapitalizeWords(input);
        Assert.Equal(expected, result);
    }

    public static TheoryData<string, string> CapitalizeWordsParams()
    {
        return new TheoryData<string, string>
        {
            // Основные сценарии
            { "The quick brown fox jumps over the lazy dog", "The Quick Brown Fox Jumps Over The Lazy Dog" },
            { "Съешь же ещё этих мягких французских булок, да выпей чаю.", "Съешь Же Ещё Этих Мягких Французских Булок, Да Выпей Чаю." },
            { "hello-world", "Hello-world" },
            { "can't stop", "Can't Stop" },
            { "какой-нибудь", "Какой-нибудь" },

            // Пограничные случаи
            { "123 hello", "123 Hello" },
            { " hello", " Hello" },
            { "", "" },
            { null!, null! },
            { "привет!", "Привет!" },
            { " зелёный,чёрный", " Зелёный,Чёрный" },
            { "--привет--", "--Привет--" },
            { "''hello''", "''Hello''" },

            // Смешанные языки
            { "hello, мир!", "Hello, Мир!" },
            { "it's уже-утро", "It's Уже-утро" },
        };
    }
}