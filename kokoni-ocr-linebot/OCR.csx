#r "Newtonsoft.Json"
#r "System.Configuration"
#r "Microsoft.WindowsAzure.Storage"

using System;
using System.Net;
using System.Net.Http.Headers;
using System.Configuration;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

/// <summary>
/// 画像から文字を取得する
/// </summary>
/// <returns>Stream</returns>
static async Task<HttpResponseMessage> GetOCRData(Stream stream)
{
    var OCRResponse = new HttpResponseMessage();
    
    // Computer vision APIのOCRにリクエスト
    using (var getOCRDataClient = new HttpClient())
    {
        // リクエストパラメータ作成、検知タイプは英文
        string language = "unk";
        string detectOrientation = "true";
        var uri = $"https://westus.api.cognitive.microsoft.com/vision/v1.0/ocr?language={language}&detectOrientation={detectOrientation}";

        // ComputerVisionAPIへのリクエスト情報を作成
        HttpRequestMessage OCRRequest = new HttpRequestMessage(HttpMethod.Post, uri);
        OCRRequest.Content = new StreamContent(stream);
        OCRRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        // リクエストヘッダーの作成
        getOCRDataClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{ConfigurationManager.AppSettings["SubscriptionKey"]}");

        // ComputerVisionAPIにリクエスト
        OCRResponse = await getOCRDataClient.SendAsync(OCRRequest);
    }

    return OCRResponse;
}

/// <summary>
/// ComputerVisionAPIのレスポンスメッセージをパース
/// </summary>
/// <returns>Stream</returns>
static async Task<string> GetParseString(HttpResponseMessage OCRResponse, TraceWriter log)
{
    // ComputerVisionAPIのレスポンスをパースして文章に
    var jsonContent = await OCRResponse.Content.ReadAsStringAsync();
    log.Info(jsonContent);
    OCR_Response ocr_data = JsonConvert.DeserializeObject<OCR_Response>(jsonContent);

    string words= String.Empty;

    if(ocr_data.regions.Any())
    {
        // OCRで取得した文字列をパースして文章にする。
        foreach(var regions in ocr_data.regions)
        {
            foreach(var line in regions.lines)
            {
                foreach(var word in line.words)
                {
                    words = words + word.text + " ";
                }
                words = words + Environment.NewLine;
            }
        }
    }
    else
    {
        words = "There is no words!!";
    }
    
    return words;
}


// OCRから返却されたデータ
public class OCR_Response
{
    public string language { get; set; }
    public double textAngle { get; set; }
    public string orientation { get; set; }
    public List<Region> regions { get; set; }
}
public class Region
{
    public string boundingBox { get; set; }
    public List<Line> lines { get; set; }
}
public class Line
{
    public string boundingBox { get; set; }
    public List<Word> words { get; set; }
}
public class Word
{
    public string boundingBox { get; set; }
    public string text { get; set; }
}