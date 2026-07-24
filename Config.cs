using System.Runtime.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace janog_reception_ui
{
    [Serializable]
    public enum EnvironmentKind
    {
        [EnumMember(Value = "develop")]
        Develop,
        [EnumMember(Value = "production")]
        Production
    }

    public static class EnvironmentKindExtensions
    {
        public static bool TryParse(string? value, out EnvironmentKind kind)
        {
            kind = default;
            if (string.IsNullOrWhiteSpace(value)) return false;

            switch (value.Trim().ToLowerInvariant())
            {
                case "develop":
                case "dev":
                    kind = EnvironmentKind.Develop;
                    return true;
                case "production":
                case "prod":
                    kind = EnvironmentKind.Production;
                    return true;
                default:
                    return false;
            }
        }

        public static EnvironmentKind Parse(string value)
        {
            if (TryParse(value, out var kind)) return kind;
            throw new ArgumentException($"Invalid environment value: {value}", nameof(value));
        }
    }

    internal class AuthConfig
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    internal class EnvironmentConfig
    {
        public EnvironmentKind Environment { get; set; }
        public AuthConfig Develop { get; set; } = new();
        public AuthConfig Production { get; set; } = new();
    }

    internal class Reader
    {
        public string Port { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;

        public Reader Clone() => new() { Port = Port ?? string.Empty, Serial = Serial ?? string.Empty };
    }

    internal class ReceptionSetConfig
    {
        public string Gate { get; set; } = string.Empty;
        public string Printer { get; set; } = string.Empty;
        public Reader Reader { get; set; } = new();

        public ReceptionSetConfig Clone() => new()
        {
            Gate = Gate ?? string.Empty,
            Printer = Printer ?? string.Empty,
            Reader = (Reader ?? new Reader()).Clone(),
        };
    }

    internal class Config
    {
        public EnvironmentConfig Environment { get; set; } = CreateDefaultEnvironment();
        public List<ReceptionSetConfig> ReceptionSets { get; set; } = new();
        public bool AudioEnabled { get; set; } = true;

        // Compatibility-only aliases for the former single-set YAML shape.
        // Normalize() clears these so SaveYAML() writes only reception_sets.
        [YamlMember(Alias = "gate")]
        public string? LegacyGate { get; set; }

        [YamlMember(Alias = "printer")]
        public string? LegacyPrinter { get; set; }

        [YamlMember(Alias = "reader")]
        public Reader? LegacyReader { get; set; }

        private static string GetAppDir()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                ?? AppContext.BaseDirectory;
        }

        private static string ConfigPath()
        {
            return Path.Combine(GetAppDir(), "config.yaml");
        }

        private static EnvironmentConfig CreateDefaultEnvironment() => new()
        {
            Environment = EnvironmentKind.Develop,
            Develop = new AuthConfig
            {
                BaseUrl = "https://register.janog57-dev.example.com",
                Username = "username",
                Password = "password",
            },
            Production = new AuthConfig
            {
                BaseUrl = "https://register.janog57.example.com",
                Username = "username",
                Password = "password",
            },
        };

        private static Config CreateDefault() => new()
        {
            Environment = CreateDefaultEnvironment(),
            ReceptionSets = new List<ReceptionSetConfig>
            {
                new() { Gate = "default" },
            },
            AudioEnabled = true,
        };

        public static Config LoadYAML()
        {
            string filename = ConfigPath();
            if (!File.Exists(filename))
            {
                var defaultConfig = CreateDefault();
                defaultConfig.SaveYAML();
                return defaultConfig;
            }

            var config = DeserializeYAML(File.ReadAllText(filename));
            return config;
        }

        internal static Config DeserializeYAML(string yaml)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            var config = deserializer.Deserialize<Config>(yaml) ?? CreateDefault();
            config.Normalize();
            return config;
        }

        public void Normalize()
        {
            Environment ??= CreateDefaultEnvironment();
            Environment.Develop ??= new AuthConfig();
            Environment.Production ??= new AuthConfig();

            ReceptionSets ??= new List<ReceptionSetConfig>();
            if (ReceptionSets.Count == 0)
            {
                ReceptionSets.Add(new ReceptionSetConfig
                {
                    Gate = LegacyGate ?? string.Empty,
                    Printer = LegacyPrinter ?? string.Empty,
                    Reader = (LegacyReader ?? new Reader()).Clone(),
                });
            }

            for (var i = 0; i < ReceptionSets.Count; i++)
            {
                ReceptionSets[i] ??= new ReceptionSetConfig();
                ReceptionSets[i].Gate ??= string.Empty;
                ReceptionSets[i].Printer ??= string.Empty;
                ReceptionSets[i].Reader ??= new Reader();
                ReceptionSets[i].Reader.Port ??= string.Empty;
                ReceptionSets[i].Reader.Serial ??= string.Empty;
            }

            LegacyGate = null;
            LegacyPrinter = null;
            LegacyReader = null;
        }

        public AuthConfig Auth()
        {
            return Environment.Environment == EnvironmentKind.Production
                ? Environment.Production
                : Environment.Develop;
        }

        public void SaveYAML()
        {
            File.WriteAllText(ConfigPath(), SerializeYAML());
        }

        internal string SerializeYAML()
        {
            Normalize();
            var serializer = new SerializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
                .Build();

            return serializer.Serialize(this);
        }
    }
}
