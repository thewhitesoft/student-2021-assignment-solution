using System.Text.Json;

using static System.Text.Json.JsonSerializer;

namespace WsEntryDataGenerator
{
    internal static class Generator
    {
        static List<ReplacementData> InitReplacements()
        {
            return new List<ReplacementData>(new[]
            {
                new ReplacementData{Replacement="Ha-haaa, hacked you",Source="I doubted if I should ever come back."},
                new ReplacementData{Replacement="sdshdjdskfm sfjsdif jfjfidjf",Source="Somewhere ages and ages hence:"},

                new ReplacementData{Replacement="d12324344rgg6f5g6gdf2ddjf",Source="wood"},
                new ReplacementData{Replacement="Random text, yeeep",Source="took"},
                new ReplacementData{Replacement="1",Source="l"},
                new ReplacementData{Replacement="Bla-bla-bla-blaaa, just some RANDOM tExT",Source=null},
                new ReplacementData{Replacement="Same line",Source="the better claim"},
                new ReplacementData{Replacement="parentheses - that is a smart word",Source="the better"},

                new ReplacementData{Replacement="sdshdjdskfm sfjsdif jfjfidjf",Source="Somewhere ages and ages hence:"},
                new ReplacementData{Replacement="Same line",Source="the better claim"},
                new ReplacementData{Replacement="sdshdjdskfm sfjsdif jfjfidjf",Source="Somewhere ages and ages hence:"},
                new ReplacementData{Replacement="An other text",Source=null},

            });
        }

        public static void GenerateReplacementData()
        {
            File.WriteAllText(Variables.ProjectDir + Variables.ReplacementsJsonName,
                              Serialize(InitReplacements()));
        }

        public static void GenerateInitialData()
        {
            var sourceData = File.ReadAllText(Variables.ProjectDir + Variables.RawSourceName)
                                 .Split(Environment.NewLine)
                                 .Where(x => !string.Equals(x, string.Empty))
                                 .Select(x => x.Trim())
                                 .ToArray();

            File.WriteAllText(Variables.ProjectDir + Variables.JsonSourceName,
                              JsonSerializer.Serialize(sourceData));
        }
    }
}
