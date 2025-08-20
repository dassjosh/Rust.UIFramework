using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiPlayerAvatars : BaseUiFrameworkLibrary, ISingleton
{
    private readonly AvatarData _data = AvatarData.Instance;
    private readonly UiSteamConfig _config = UiFrameworkConfig.Instance.Steam;
    private readonly HttpClient _httpClient;
    private readonly IUiLogger<UiPlayerAvatars> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiPlayerAvatars>();
    
    private UiPlayerAvatars()
    {
        if (!string.IsNullOrEmpty(_config.ApiKey))
        {
            HttpClientHandler handler = new()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = false
            };
            _httpClient = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(30),
                BaseAddress = new Uri($"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={_config.ApiKey}&steamids=")
            };
        }
    }

    public string GetAvatarUrl(ulong steamId, AvatarType type)
    {
        return _data.GetAvatar(steamId, type);
    }

    protected override void OnPlayerConnected(BasePlayer player)
    {
        if (_httpClient != null)
        {
#pragma warning disable EPC13
            Task.Run(() => GetPlayerAvatarAsync(player.userID));
#pragma warning restore EPC13
        }
    }

    private async Task GetPlayerAvatarAsync(ulong steamId)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(StringCache<ulong>.ToString(steamId));
            if (!response.IsSuccessStatusCode)
            {
                _logger.Error("An error occured getting player avatar. SteamId: {0} Status Code: {1} Message: {2}", steamId, response.StatusCode, await response.Content.ReadAsStringAsync());
                return;
            }
        
            string json = await response.Content.ReadAsStringAsync();
            Root root = JsonConvert.DeserializeObject<Root>(json);
            _data.AddAvatar(steamId, root.Response.Players[0].AvatarHash);
        }
        catch (Exception ex)
        {
            _logger.Exception("An error occured getting player avatar. SteamId: {0}", steamId, ex);
        }
    }
    
    private sealed class Player
    {
        [JsonProperty("avatarhash")]
        public string AvatarHash { get; set; }
    }

    private sealed class Response
    {
        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }

    private sealed class Root
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }
}