using System.Net.Http.Json;
using System.Text.Json;
using ReolinkNVR.DTO;

namespace ReolinkNVR;

public class ReolinkNVR
{
    private readonly HttpClient _httpClient;
    private readonly string _nvrIp;
    private readonly string _username;
    private readonly string _password;


    public ReolinkNVR(HttpClient httpClient, string nvrIp, string username, string password)
    {
        _httpClient = httpClient;
        _nvrIp = nvrIp;
        _username = username;
        _password = password;
    }

    private async Task<string> GetToken()
    {
        var request = new LoginRequest
        {
            cmd = "Login",
            param = new LoginParam
            {
                User = new User
                {
                    password = _password,
                    userName = _username,
                    Version = "0"
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync<LoginRequest[]>($"https://{_nvrIp}/api.cgi?cmd=Login", [request]);

        if (response.IsSuccessStatusCode)
        {
            var loginResponse = await JsonSerializer.DeserializeAsync<LoginResponse[]>(await response.Content.ReadAsStreamAsync());
            return loginResponse[0].value.Token.name;
        }

        return "";
    }

    //this is prepared to save multiple files at once but afair Edge device supports single file downloads anyway...
    public async Task<SaveStreamResponse> SaveStream(DateTime startTime, DateTime endTime, int channelId)
    {
        var token = await GetToken();
        var request = new SaveStreamRequest
        {
            action = 1,
            cmd = "NvrDownload",
            param = new SaveStreamParam
            {
                NvrDownload = new Nvrdownload
                {
                    channel = channelId,
                    StartTime = new Time(startTime),
                    EndTime = new Time(endTime),
                    streamType = "main"
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync<SaveStreamRequest[]>($"https://{_nvrIp}/api.cgi?cmd=NvrDownload&token={token}", [request]);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"couldn't save stream {startTime.ToString("o")} - {endTime.ToString("o")} - channel {channelId} - {await response.Content.ReadAsStringAsync()}");
        }

        var saveStreamResponse = await JsonSerializer.DeserializeAsync<SaveStreamResponse[]>(await response.Content.ReadAsStreamAsync());
        return saveStreamResponse[0];
    }

    // i highly doubt that this is the correct method to use but well that's all that's in the docs
    // also some libaries on github use the same method: https://github.com/starkillerOG/reolink_aio/blob/31f75f890f5276a06f6685a3608a6095937929ba/reolink_aio/api.py#L3331C41-L3331C42
    public async Task DownloadVOD(string filename, string outputFilePath)
    {
        var token = await GetToken();
        var stream = await _httpClient.GetStreamAsync($"https://{_nvrIp}/cgi-bin/api.cgi?cmd=Download&source={filename}&output={filename}&token={token}");
        using FileStream fs = new FileStream(outputFilePath, FileMode.CreateNew);
        await stream.CopyToAsync(fs);
    }
}
