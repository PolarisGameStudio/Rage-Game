using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamClientTest : MonoBehaviour {
	private HSteamPipe m_Pipe;
	private HSteamUser m_GlobalUser;
	private HSteamPipe m_LocalPipe;
	private HSteamUser m_LocalUser;
	
	public void RenderOnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width - 120, 0, 120, Screen.height));
		GUILayout.Label("Variables:");
		GUILayout.Label("m_Pipe: " + m_Pipe);
		GUILayout.Label("m_GlobalUser: " + m_GlobalUser);
		GUILayout.Label("m_LocalPipe: " + m_LocalPipe);
		GUILayout.Label("m_LocalUser: " + m_LocalUser);
		GUILayout.EndArea();

		GUILayout.Label("DON'T TOUCH THESE IF YOU DO NOT KNOW WHAT THEY DO, YOU COULD CRASH YOUR STEAM CLIENT");

		if (GUILayout.Button("SteamClient.CreateSteamPipe()")) {
			m_Pipe = SteamClient.CreateSteamPipe();
			print("SteamClient.CreateSteamPipe() : " + m_Pipe);
		}

		if (GUILayout.Button("SteamClient.BReleaseSteamPipe(m_Pipe)")) {
			print("SteamClient.BReleaseSteamPipe(" + m_Pipe + ") : " + SteamClient.BReleaseSteamPipe(m_Pipe));
		}

		if (GUILayout.Button("SteamClient.ConnectToGlobalUser(m_Pipe)")) {
			m_GlobalUser = SteamClient.ConnectToGlobalUser(m_Pipe);
			print("SteamClient.ConnectToGlobalUser(" + m_Pipe + ") : " + m_GlobalUser);
		}
		
		if (GUILayout.Button("SteamClient.CreateLocalUser(out m_LocalPipe, EAccountType.k_EAccountTypeGameServer)")) {
			m_LocalUser = SteamClient.CreateLocalUser(out m_LocalPipe, EAccountType.k_EAccountTypeGameServer);
			print("SteamClient.CreateLocalUser(out m_LocalPipe, EAccountType.k_EAccountTypeGameServer) : " + m_LocalUser + " -- " + m_LocalPipe);
		}

		if (GUILayout.Button("SteamClient.ReleaseUser(m_LocalPipe, m_LocalUser)")) {
			SteamClient.ReleaseUser(m_LocalPipe, m_LocalUser);
			print("SteamClient.ReleaseUser(" + m_LocalPipe + ", " + m_LocalUser + ")");
		}

		if (GUILayout.Button("SteamClient.GetISteamUser(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMUSER_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamUser(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMUSER_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamUser(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMUSER_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamGameServer(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMGAMESERVER_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamGameServer(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMGAMESERVER_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamGameServer(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMGAMESERVER_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.SetLocalIPBinding(127.0.0.1, 27015)")) {
			SteamClient.SetLocalIPBinding(2130706433, 27015);
			print("SteamClient.SetLocalIPBinding(2130706433, 27015)");
		}

		if (GUILayout.Button("SteamClient.GetISteamFriends(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMFRIENDS_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamFriends(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMFRIENDS_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamFriends(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMFRIENDS_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamUtils(SteamAPI.GetHSteamPipe(), " + Constants.STEAMUTILS_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamUtils(" + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMUTILS_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamUtils(SteamAPI.GetHSteamPipe(), Constants.STEAMUTILS_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamMatchmaking(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMMATCHMAKING_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamMatchmaking(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMMATCHMAKING_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamMatchmaking(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMMATCHMAKING_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamMatchmakingServers(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMMATCHMAKINGSERVERS_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamMatchmakingServers(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMMATCHMAKINGSERVERS_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamMatchmakingServers(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMMATCHMAKINGSERVERS_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamGenericInterface(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMAPPTICKET_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamGenericInterface(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMAPPTICKET_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamGenericInterface(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMAPPTICKET_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamUserStats(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMUSERSTATS_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamUserStats(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMUSERSTATS_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamUserStats(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMUSERSTATS_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamGameServerStats(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMGAMESERVERSTATS_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamGameServerStats(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMGAMESERVERSTATS_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamGameServerStats(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMGAMESERVERSTATS_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamApps(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMAPPS_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamApps(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMAPPS_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamApps(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMAPPS_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamNetworking(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMNETWORKING_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamNetworking(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMNETWORKING_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamNetworking(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMNETWORKING_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamRemoteStorage(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMREMOTESTORAGE_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamRemoteStorage(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMREMOTESTORAGE_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamRemoteStorage(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMREMOTESTORAGE_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamScreenshots(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMSCREENSHOTS_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamScreenshots(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMSCREENSHOTS_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamScreenshots(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMSCREENSHOTS_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.RunFrame()")) {
			SteamClient.RunFrame();
			print("SteamClient.RunFrame()");
		}

		GUILayout.Label("SteamClient.GetIPCCallCount : " + SteamClient.GetIPCCallCount());

		//GUILayout.Label("SteamClient.SetWarningMessageHook : " + SteamClient.SetWarningMessageHook()); // N/A - Check out SteamTest.cs for example usage.

		if (GUILayout.Button("SteamClient.BShutdownIfAllPipesClosed()")) {
			print("SteamClient.BShutdownIfAllPipesClosed() : " + SteamClient.BShutdownIfAllPipesClosed());
		}
#if _PS3
		if (GUILayout.Button("SteamClient.GetISteamPS3OverlayRender()")) {
			print("SteamClient.GetISteamPS3OverlayRender() : " + SteamClient.GetISteamPS3OverlayRender());
		}
#endif
		if (GUILayout.Button("SteamClient.GetISteamHTTP(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMHTTP_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamHTTP(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMHTTP_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamHTTP(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMHTTP_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamUnifiedMessages(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMUNIFIEDMESSAGES_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamUnifiedMessages(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMUNIFIEDMESSAGES_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamUnifiedMessages(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMUNIFIEDMESSAGES_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamController(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMCONTROLLER_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamController(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMCONTROLLER_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamController(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMCONTROLLER_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamUGC(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMUGC_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamUGC(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMUGC_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamUGC(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMUGC_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamAppList(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMAPPLIST_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamAppList(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMAPPLIST_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamAppList(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMAPPLIST_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamMusic(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMMUSIC_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamMusic(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMMUSIC_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamMusic(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMMUSIC_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamMusicRemote(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMMUSICREMOTE_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamMusicRemote(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMMUSICREMOTE_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamMusicRemote(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMMUSICREMOTE_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamHTMLSurface(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMHTMLSURFACE_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamHTMLSurface(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMHTMLSURFACE_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamHTMLSurface(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMHTMLSURFACE_INTERFACE_VERSION));
		}

#if DISABLED
		// Helper functions for internal Steam usage
		if (GUILayout.Button("SteamClient.Set_SteamAPI_CPostAPIResultInProcess()")) {
			print("SteamClient.Set_SteamAPI_CPostAPIResultInProcess() : " + SteamClient.Set_SteamAPI_CPostAPIResultInProcess());
		}

		if (GUILayout.Button("SteamClient.Remove_SteamAPI_CPostAPIResultInProcess()")) {
			print("SteamClient.Remove_SteamAPI_CPostAPIResultInProcess() : " + SteamClient.Remove_SteamAPI_CPostAPIResultInProcess());
		}

		if (GUILayout.Button("SteamClient.Set_SteamAPI_CCheckCallbackRegisteredInProcess()")) {
			print("SteamClient.Set_SteamAPI_CCheckCallbackRegisteredInProcess() : " + SteamClient.Set_SteamAPI_CCheckCallbackRegisteredInProcess());
		}
#endif

		if (GUILayout.Button("SteamClient.GetISteamInventory(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMINVENTORY_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamInventory(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMINVENTORY_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamInventory(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMINVENTORY_INTERFACE_VERSION));
		}

		if (GUILayout.Button("SteamClient.GetISteamVideo(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), " + Constants.STEAMVIDEO_INTERFACE_VERSION + ")")) {
			print("SteamClient.GetISteamVideo(" + SteamAPI.GetHSteamUser() + ", " + SteamAPI.GetHSteamPipe() + ", " + Constants.STEAMVIDEO_INTERFACE_VERSION + ") : " +
				SteamClient.GetISteamVideo(SteamAPI.GetHSteamUser(), SteamAPI.GetHSteamPipe(), Constants.STEAMVIDEO_INTERFACE_VERSION));
		}
	}
}
