using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace janog_reception_ui
{
    public record class Participant
    {

        [JsonPropertyName("id")]
        public required string ID { get; set; }

        [JsonPropertyName("full_name")]
        public required string FullName { get; set; }

        [JsonPropertyName("organization")]
        public required string Organization { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("program")]
        public required string Program { get; set; }

    }

    public record class AcceptResponse
    {
        [JsonPropertyName("participant")]
        public required Participant Participant { get; set; }

        [JsonPropertyName("gate")]
        public required string Gate { get; set; }
    }

    public record class AcceptRequest
    {
        [JsonPropertyName("gate")]
        public required string Gate { get; set; }
        [JsonPropertyName("method")]
        public required string Method { get; set; }
    }


    internal class Client
    {
        private string _username;
        private string _password;
        public string BaseURL;

        public Client(string baseUrl, string username, string password)
        {
            BaseURL = baseUrl;
            _username = username;
            _password = password;
        }

        private HttpResponseMessage DoRequest(HttpRequestMessage request)
        {
            try
            {
                // Basic認証ヘッダを付与する
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _username, _password))));

                // リクエストの送信
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = httpClient.Send(request);
                    Debug.WriteLine(response);
                    return response;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            // ユニコードのレンジ指定で日本語も正しく表示、インデントされるように指定
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            return options;
        }


        public AcceptResponse AcceptParticipant(string id, string gate, string method) { 
            // Payload
            var payload = new AcceptRequest
            {
                Gate = gate,
                Method = method,
            };

            // リクエストの生成
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(BaseURL + "/api/v1/participants/" + id + "/accept/")
            };
            request.Content = new StringContent(
                JsonSerializer.Serialize(payload, GetJsonSerializerOptions()),
                Encoding.UTF8,
                "application/json"
            );

            var response = DoRequest(request);

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                Debug.Write(json);
                try
                {
                    AcceptResponse? ret = JsonSerializer.Deserialize<AcceptResponse>(json, GetJsonSerializerOptions());
                    if (ret == null)
                    {
                        throw new Exception("参加者情報の取得に失敗しました");
                    }
                    return ret;
                }
                catch (JsonException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("認証情報が間違っています");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("参加者情報が見つかりません");
            }
            throw new Exception("参加者の受付に失敗しました");
        }
    }
}
