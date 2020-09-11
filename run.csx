#r "Newtonsoft.Json"

using System.Net;
using System.Text;
using Newtonsoft.Json;
using  Newtonsoft.Json.Linq;

public static async Task<HttpResponseMessage> Run(HttpRequest req, ILogger log)
{
    string url = req.Query["url"];
    string originalHost = req.Headers["x-original-host"];
    string[] parts = url.Split("/".ToCharArray());
    string host = parts[2];
    //log.LogInformation("url=" + url);
    //log.LogInformation("originalHost=" + originalHost);
    //log.LogInformation("host=" + host);

    HttpClient client = new HttpClient();
    HttpResponseMessage res = client.GetAsync(url).Result;
    var contents = await res.Content.ReadAsStringAsync();
    client.Dispose();
    //log.LogInformation("HttpStatusCode=" + res.StatusCode.ToString());
    //log.LogInformation("Content=" + contents);

    if ( res.StatusCode == HttpStatusCode.OK ) {
        contents = contents.Replace(host, originalHost);
    }
    var content = new StringContent(contents, System.Text.Encoding.UTF8, "application/json");
    content.Headers.Add("Access-Control-Allow-Origin", "*");
    return new HttpResponseMessage(res.StatusCode) {Content = content};
}
