using System.Text.Json.Serialization;

namespace WsEntryDataGenerator
{
    internal class ReplacementData : IEquatable<ReplacementData>
    {
        // по умолчанию, парсятся регистрозависимые имена
        [JsonPropertyName("replacement")]
        public string Replacement { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        // нужен
        public bool Equals(ReplacementData other)
        {
            if (other is null)
                return false;

            return Replacement.Equals(other.Replacement) && Source.Equals(other.Source);
        }

        public override bool Equals(object obj)
        {
            if (obj is ReplacementData replacementData)
                return Equals(replacementData);

            return false;
        }

        // нужен для работы HashSet
        public override int GetHashCode()
        {
            int sourcaHash = Source is null ? 0 : Source.GetHashCode();
            return sourcaHash ^ Replacement.GetHashCode();
        }

        // для убодства дебага
        public override string ToString()
        {
            return $"Source: ${Source};Replacement: {Replacement}";
        }
    }
}
