namespace WsEntryDataGenerator
{
    internal static class Variables
    {
        /// папка, куда помещается исполняемый файл всегда одна
        /// Используется .NET 6, если используется иная версия, то папка будет называться иначе
        public const string ExeDir = @"bin\Debug\net6.0";
        public const string RawSourceName = "source.txt";
        public const string JsonSourceName = "initial-data.json";
        public const string DataSourceName = "data.json";
        public const string ReplacementsJsonName = "replacement.json";
        public readonly static string ProjectDir = Directory.GetCurrentDirectory()
                                                            .Replace(ExeDir, "");
    }
}
