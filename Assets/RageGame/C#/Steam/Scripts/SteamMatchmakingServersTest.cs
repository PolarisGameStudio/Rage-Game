using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamMatchmakingServersTest : MonoBehaviour {
	private HServerListRequest m_ServerListRequest = HServerListRequest.Invalid;
	private HServerQuery m_ServerQuery = HServerQuery.Invalid;

	private ISteamMatchmakingServerListResponse m_ServerListResponse;
	private ISteamMatchmakingPingResponse m_PingResponse;
	private ISteamMatchmakingPlayersResponse m_PlayersResponse;
	private ISteamMatchmakingRulesResponse m_RulesResponse;

	public void OnEnable() {
		m_ServerListResponse = new ISteamMatchmakingServerListResponse(OnServerResponded, OnServerFailedToRespond, OnRefreshComplete);
		m_PingResponse = new ISteamMatchmakingPingResponse(OnServerResponded, OnServerFailedToRespond);
		m_PlayersResponse = new ISteamMatchmakingPlayersResponse(OnAddPlayerToList, OnPlayersFailedToRespond, OnPlayersRefreshComplete);
		m_RulesResponse = new ISteamMatchmakingRulesResponse(OnRulesResponded, OnRulesFailedToRespond, OnRulesRefreshComplete);
	}

	private void OnDisable() {
		ReleaseRequest();
		CancelServerQuery();
	}

	public void RenderOnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width - 120, 0, 120, Screen.height));
		GUILayout.Label("Variables:");
		GUILayout.Label("m_ServerListRequest: " + m_ServerListRequest);
		GUILayout.Label("m_ServerQuery: " + m_ServerQuery);
		GUILayout.EndArea();

		if (GUILayout.Button("RequestInternetServerList(new AppId_t(440), filters, (uint)filters.Length, m_ServerListResponse)")) {
			ReleaseRequest();

			MatchMakingKeyValuePair_t[] filters = {
				new MatchMakingKeyValuePair_t { m_szKey = "appid", m_szValue = "440" },
				new MatchMakingKeyValuePair_t { m_szKey = "gamedir", m_szValue = "tf" },
				new MatchMakingKeyValuePair_t { m_szKey = "gametagsand", m_szValue = "beta" },
			};

			m_ServerListRequest = SteamMatchmakingServers.RequestInternetServerList(new AppId_t(440), filters, (uint)filters.Length, m_ServerListResponse);
			print("SteamMatchmakingServers.RequestInternetServerList(new AppId_t(440), filters, (uint)filters.Length, m_ServerListResponse) : " + m_ServerListRequest);
		}

		if (GUILayout.Button("RequestLANServerList(new AppId_t(440), m_ServerListResponse)")) {
			ReleaseRequest();
			m_ServerListRequest = SteamMatchmakingServers.RequestLANServerList(new AppId_t(440), m_ServerListResponse); ;
			print("SteamMatchmakingServers.RequestLANServerList(" + new AppId_t(440) + ", m_ServerListResponse) : " + m_ServerListRequest);
		}

		if (GUILayout.Button("RequestFriendsServerList(new AppId_t(440), null, 0, m_ServerListResponse)")) {
			ReleaseRequest();
			m_ServerListRequest = SteamMatchmakingServers.RequestFriendsServerList(new AppId_t(440), null, 0, m_ServerListResponse);
			print("SteamMatchmakingServers.RequestFriendsServerList(" + new AppId_t(440) + ", null, 0, m_ServerListResponse) : " + m_ServerListRequest);
		}

		if (GUILayout.Button("RequestFavoritesServerList(new AppId_t(440), null, 0, m_ServerListResponse)")) {
			ReleaseRequest();
			m_ServerListRequest = SteamMatchmakingServers.RequestFavoritesServerList(new AppId_t(440), null, 0, m_ServerListResponse);
			print("SteamMatchmakingServers.RequestFavoritesServerList(" + new AppId_t(440) + ", null, 0, m_ServerListResponse) : " + m_ServerListRequest);
		}

		if (GUILayout.Button("RequestHistoryServerList(new AppId_t(440), null, 0, m_ServerListResponse)")) {
			ReleaseRequest();
			m_ServerListRequest = SteamMatchmakingServers.RequestHistoryServerList(new AppId_t(440), null, 0, m_ServerListResponse);
			print("SteamMatchmakingServers.RequestHistoryServerList(" + new AppId_t(440) + ", null, 0, m_ServerListResponse) : " + m_ServerListRequest);
		}

		if (GUILayout.Button("RequestSpectatorServerList(new AppId_t(440), null, 0, m_ServerListResponse)")) {
			ReleaseRequest();
			m_ServerListRequest = SteamMatchmakingServers.RequestSpectatorServerList(new AppId_t(440), null, 0, m_ServerListResponse);
			print("SteamMatchmakingServers.RequestSpectatorServerList(" + new AppId_t(440) + ", null, 0, m_ServerListResponse) : " + m_ServerListRequest);
		}

		if (GUILayout.Button("ReleaseRequest(m_ServerListRequest)")) {
			ReleaseRequest(); // We do this instead, because we want to make sure that m_ServerListRequested gets set to Invalid after releasing.
		}

		if (GUILayout.Button("GetServerDetails(m_ServerListRequest, 0)")) {
			gameserveritem_t gsi = SteamMatchmakingServers.GetServerDetails(m_ServerListRequest, 0);
			print("SteamMatchmakingServers.GetServerDetails(" + m_ServerListRequest + ", 0) : " + gsi + "\n" + GameServerItemFormattedString(gsi));
		}

		if (GUILayout.Button("CancelQuery(m_ServerListRequest)")) {
			SteamMatchmakingServers.CancelQuery(m_ServerListRequest);
			print("SteamMatchmakingServers.CancelQuery(" + m_ServerListRequest + ")");
		}

		if (GUILayout.Button("RefreshQuery(m_ServerListRequest)")) {
			SteamMatchmakingServers.RefreshQuery(m_ServerListRequest);
			print("SteamMatchmakingServers.RefreshQuery(" + m_ServerListRequest + ")");
		}

		GUILayout.Label("SteamMatchmakingServers.IsRefreshing(m_ServerListRequest) : " + SteamMatchmakingServers.IsRefreshing(m_ServerListRequest));

		GUILayout.Label("SteamMatchmakingServers.GetServerCount(m_ServerListRequest) : " + SteamMatchmakingServers.GetServerCount(m_ServerListRequest));


		if (GUILayout.Button("RefreshServer(m_ServerListRequest, 0)")) {
			SteamMatchmakingServers.RefreshServer(m_ServerListRequest, 0);
			print("SteamMatchmakingServers.RefreshServer(" + m_ServerListRequest + ", 0)");
		}

		// 3494815209 = 208.78.165.233 = Valve Payload Server (Virginia srcds150 #1)
		if (GUILayout.Button("PingServer(3494815209, 27015, m_PingResponse)")) {
			CancelServerQuery();
			m_ServerQuery = SteamMatchmakingServers.PingServer(3494815209, 27015, m_PingResponse);
			print("SteamMatchmakingServers.PingServer(3494815209, 27015, m_PingResponse) : " + m_ServerQuery);
		}

		if (GUILayout.Button("PlayerDetails(3494815209, 27015, m_PlayersResponse)")) {
			CancelServerQuery();
			m_ServerQuery = SteamMatchmakingServers.PlayerDetails(3494815209, 27015, m_PlayersResponse);
			print("SteamMatchmakingServers.PlayerDetails(3494815209, 27015, m_PlayersResponse) : " + m_ServerQuery);
		}

		if (GUILayout.Button("ServerRules(3494815209, 27015, m_RulesResponse)")) {
			CancelServerQuery();
			m_ServerQuery = SteamMatchmakingServers.ServerRules(3494815209, 27015, m_RulesResponse);
			print("SteamMatchmakingServers.ServerRules(3494815209, 27015, m_RulesResponse) : " + m_ServerQuery);
		}

		if (GUILayout.Button("CancelServerQuery(m_ServerQuery)")) {
			CancelServerQuery(); // We do this instead, because we want to make sure that m_ServerListRequested gets set to Invalid after releasing, and we call it from a number of places.
		}
	}

	private void ReleaseRequest() {
		if (m_ServerListRequest != HServerListRequest.Invalid) {
			SteamMatchmakingServers.ReleaseRequest(m_ServerListRequest);
			m_ServerListRequest = HServerListRequest.Invalid;
			print("SteamMatchmakingServers.ReleaseRequest(m_ServerListRequest)");
		}
	}

	private void CancelServerQuery() {
		if (m_ServerQuery != HServerQuery.Invalid) {
			SteamMatchmakingServers.CancelServerQuery(m_ServerQuery);
			m_ServerQuery = HServerQuery.Invalid;
			print("SteamMatchmakingServers.CancelServerQuery(m_ServerQuery)");
		}
	}

	private string GameServerItemFormattedString(gameserveritem_t gsi) {
		return	"m_NetAdr: " + gsi.m_NetAdr.GetConnectionAddressString() + "\n" +
				"m_nPing: " + gsi.m_nPing + "\n" +
				"m_bHadSuccessfulResponse: " + gsi.m_bHadSuccessfulResponse + "\n" +
				"m_bDoNotRefresh: " + gsi.m_bDoNotRefresh + "\n" +
				"m_szGameDir: " + gsi.GetGameDir() + "\n" +
				"m_szMap: " + gsi.GetMap() + "\n" +
				"m_szGameDescription: " + gsi.GetGameDescription() + "\n" +
				"m_nAppID: " + gsi.m_nAppID + "\n" +
				"m_nPlayers: " + gsi.m_nPlayers + "\n" +
				"m_nMaxPlayers: " + gsi.m_nMaxPlayers + "\n" +
				"m_nBotPlayers: " + gsi.m_nBotPlayers + "\n" +
				"m_bPassword: " + gsi.m_bPassword + "\n" +
				"m_bSecure: " + gsi.m_bSecure + "\n" +
				"m_ulTimeLastPlayed: " + gsi.m_ulTimeLastPlayed + "\n" +
				"m_nServerVersion: " + gsi.m_nServerVersion + "\n" +
				"m_szServerName: " + gsi.GetServerName() + "\n" +
				"m_szGameTags: " + gsi.GetGameTags() + "\n" +
				"m_steamID: " + gsi.m_steamID + "\n";
	}

	// ISteamMatchmakingServerListResponse
	private void OnServerResponded(HServerListRequest hRequest, int iServer) {
		Debug.Log("OnServerResponded: " + hRequest + " - " + iServer);
	}

	private void OnServerFailedToRespond(HServerListRequest hRequest, int iServer) {
		Debug.Log("OnServerFailedToRespond: " + hRequest + " - " + iServer);
	}

	private void OnRefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response) {
		Debug.Log("OnRefreshComplete: " + hRequest + " - " + response);
	}

	// ISteamMatchmakingPingResponse
	private void OnServerResponded(gameserveritem_t gsi) {
		Debug.Log("OnServerResponded: " + gsi + "\n" + GameServerItemFormattedString(gsi));
	}

	private void OnServerFailedToRespond() {
		Debug.Log("OnServerFailedToRespond");
	}

	// ISteamMatchmakingPlayersResponse
	private void OnAddPlayerToList(string pchName, int nScore, float flTimePlayed) {
		Debug.Log("OnAddPlayerToList: " + pchName + " - " + nScore + " - " + flTimePlayed);
	}

	private void OnPlayersFailedToRespond() {
		Debug.Log("OnPlayersFailedToRespond");
	}

	private void OnPlayersRefreshComplete() {
		Debug.Log("OnPlayersRefreshComplete");
	}

	// ISteamMatchmakingRulesResponse
	private void OnRulesResponded(string pchRule, string pchValue) {
		Debug.Log("OnRulesResponded: " + pchRule + " - " + pchValue);
	}

	private void OnRulesFailedToRespond() {
		Debug.Log("OnRulesFailedToRespond");
	}

	private void OnRulesRefreshComplete() {
		Debug.Log("OnRulesRefreshComplete");
	}
}
