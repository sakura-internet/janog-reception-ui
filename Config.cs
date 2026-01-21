using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Collections.Generic;
using System.Management;

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
        // 文字列から enum へ安全に変換（許容: 大文字小文字無視）
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

        // 必須環境取得（不正時に例外）
        public static EnvironmentKind Parse(string value)
        {
            if (TryParse(value, out var k)) return k;
            throw new ArgumentException($"Invalid environment value: {value}", nameof(value));
        }
    }
    internal class AuthConfig
    {
        public required string BaseUrl { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    internal class EnvironmentConfig
    {
        public required EnvironmentKind Environment { get; set; }
        public required AuthConfig Develop { get; set; }
        public required AuthConfig Production { get; set; }

    }

    internal class Reader
    {
        public required string Port { get; set; }
        public required string Serial { get; set; }
    }
    
    internal class Config
    {
        public required EnvironmentConfig Environment { get; set; }
        public required string Gate {  get; set; }
        public required string Printer { get; set; }
        public required Reader Reader { get; set; }

        // 実行ファイルのディレクトリを返す関数
        private static string GetAppDir()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
        private static string ConfigPath()
        {
            // ディレクトリとファイル名を結合し、絶対パスを生成する
            return System.IO.Path.Combine(GetAppDir(), "config.yaml");
        }
        public static Config LoadYAML()
        {
            string filename = ConfigPath();
            if (!File.Exists(filename))
            {

                Config cfg = new Config
                {
                    Gate = "default",
                    Printer = "",
                    Reader = new Reader
                    {
                      Port = "",  
                      Serial = "",
                    },
                    Environment = new EnvironmentConfig
                    {
                        Environment = EnvironmentKind.Develop,
                        Develop = new AuthConfig { BaseUrl = "https://register.janog57-dev.sakuraha.jp", Username = "username", Password = "password" },
                        Production = new AuthConfig { BaseUrl = "https://register.janog57.sakuraha.jp", Username = "username", Password = "password" },
                    },

                };
                cfg.SaveYAML();
            }

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

            return deserializer.Deserialize<Config>(System.IO.File.ReadAllText(filename));
        }
        public AuthConfig Auth()
        {
            return Environment.Environment == EnvironmentKind.Production ? Environment.Production : Environment.Develop;
        }

        public void SaveYAML()
        {
            string filename = ConfigPath();
            var serializer = new SerializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

            System.IO.File.WriteAllText(filename, serializer.Serialize(this));
        }
    }
}
