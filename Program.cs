using static System.Text.Json.JsonSerializer;

using WsEntryDataGenerator;

/// <summary>
/// Получение данных с API
/// </summary>
List<string> FetchData()
{
    const string URL = @"https://raw.githubusercontent.com/thewhitesoft/student-2022-assignment/main/data.json";
    HttpClient httpClient = new HttpClient();
    var result = httpClient.GetAsync(URL).Result;
    return Deserialize<List<string>>(result.Content.ReadAsStream());
}

/// <summary>
/// Получение данных из файла .json
/// </summary>
List<string> ReadData()
{
    return Deserialize<List<string>>(
            File.ReadAllText(Variables.ProjectDir + Variables.DataSourceName)
        );
}

List<string> data = FetchData();
// используем HashSet, чтобы сразу выбрать только уникальные объекты
HashSet<ReplacementData> replacementsSource = Deserialize<HashSet<ReplacementData>>(
        File.ReadAllText(Variables.ProjectDir + Variables.ReplacementsJsonName)
    );

// помещаем значения, которые должны быть заменены актуальным
Dictionary<string, string> replacements = new Dictionary<string, string>();

// помещаем значения, которых быть не должно
HashSet<string> toBeRemoved = new HashSet<string>();
foreach (var repl in replacementsSource)
{
    // так как значение сначала могло быть null, а потом нет, то мы проверяем добавлено ли оно уже на удаление
    if (repl.Source is null)
    {
        toBeRemoved.Add(repl.Replacement);
        continue;
    }
    else
        toBeRemoved.Remove(repl.Replacement);

    replacements[repl.Replacement] = repl.Source;
}

data.RemoveAll(x => toBeRemoved.Contains(x));

// мы начинаем с бОльших замен, так как не можем быть уверены, что более маленькие не входят в большие. В этом случае они могут быть "испорчены"
var orderedReplacements = replacements.OrderByDescending(x => x.Key.Length);

for (int i = 0; i < data.Count; i++)
{
    // сохраняем все совпадения от большего к меньшему
    foreach (var replacement in orderedReplacements)
    {
        data[i] = data[i].Replace(replacement.Key, replacement.Value);
    }
}

File.WriteAllText(Variables.ProjectDir + "result.json", Serialize(data));
