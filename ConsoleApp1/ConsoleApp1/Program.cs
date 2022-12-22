using System.IO.Hashing;
using System.Text;

HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
HttpClient clientReq = new HttpClient(clientHandler);

var good = 0;
var bad = 0;

while (true)
{
    var rndStr = GenerateRandomString(240);
    var link = "https://localhost:7143/?link=https://127.0.0.1:7143/" + rndStr;

    string req = await clientReq.GetStringAsync(link);
    var token1 = req.Split(' ')[2];


    HttpClient clientResp = new HttpClient(clientHandler);
    link = $"https://127.0.0.1:7143/{token1}";
    var resp = await clientResp.GetStringAsync(link);

    if (rndStr == resp)
    {
        good++;
    }
    else
    {
        bad++;
    }
    Console.WriteLine($"Good: {good}    |   Bad: {bad}");
}

static string GenerateRandomString(int length)
{
Random random = new Random();
const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
return new string(Enumerable.Repeat(chars, length)
    .Select(s => s[random.Next(s.Length)]).ToArray());
}