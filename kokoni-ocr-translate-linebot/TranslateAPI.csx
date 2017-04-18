#r "Newtonsoft.Json"
#r "System.Configuration"

using System;
using System.Net;
using System.Net.Http.Headers;
using System.Configuration;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

/// <summary>
/// 翻訳APIにリクエストする
/// </summary>
/// <returns>string</returns>
static async Task<string> GetTranslateWord(string transWord,TraceWriter log)
{
    string translated = string.Empty;
    string from = "en";
    string to = "ja";
    string uri = $"http://api.microsofttranslator.com/v2/Http.svc/Translate?text={WebUtility.UrlEncode(transWord)}&from={from}&to={to}";
    string AccessToken = await GetAccessToken();
    log.Info(uri);

    var res = new HttpResponseMessage();
    using (var getTranslateWord = new HttpClient())
    {
        //　AccessTokenをヘッダーに追加
        getTranslateWord.DefaultRequestHeaders.Add("Authorization", AccessToken);

        // 非同期でPOST
        res = await getTranslateWord.GetAsync(uri);
        translated = await res.Content.ReadAsStringAsync();

        log.Info(translated);
    }

    // <string>XXXXXX</string> 形式で取得されるので XDocumentでパース
    XDocument xdoc = XDocument.Parse(translated);

    // 翻訳された文字を返却
    return xdoc.Descendants().First().Value;
}


/// <summary>
/// TranslateApiにアクセスするためのAccessTokenを取得する
/// </summary>
/// <returns>Stream</returns>
static async Task<string> GetAccessToken()
{    
    // AccessToken取得
    Uri ServiceUrl = new Uri("https://api.cognitive.microsoft.com/sts/v1.0/issueToken");
    string OcpApimSubscriptionKeyHeader = "Ocp-Apim-Subscription-Key";
    TimeSpan TokenCacheDuration = new TimeSpan(0, 5, 0);
    string storedTokenValue = string.Empty;
    DateTime storedTokenTime = DateTime.MinValue;

    if ((DateTime.Now - storedTokenTime) < TokenCacheDuration)
    {
        return storedTokenValue;
    }

    using (var client = new HttpClient())
    {
        var request = new HttpRequestMessage();
        request.Method = HttpMethod.Post;
        request.RequestUri = ServiceUrl;
        request.Content = new StringContent(string.Empty);
        request.Headers.TryAddWithoutValidation(OcpApimSubscriptionKeyHeader, ConfigurationManager.AppSettings["AuthToken"]);
        client.Timeout = TimeSpan.FromSeconds(2);

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var token = await response.Content.ReadAsStringAsync();
        storedTokenTime = DateTime.Now;
        storedTokenValue = "Bearer " + token;
    }

    return storedTokenValue;
}
