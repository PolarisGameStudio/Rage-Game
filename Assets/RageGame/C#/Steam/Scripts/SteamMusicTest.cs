using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamMusicTest : MonoBehaviour {
	protected Callback<PlaybackStatusHasChanged_t> m_PlaybackStatusHasChanged;
	protected Callback<VolumeHasChanged_t> m_VolumeHasChanged;
	
	public void OnEnable() {
		m_PlaybackStatusHasChanged = Callback<PlaybackStatusHasChanged_t>.Create(OnPlaybackStatusHasChanged);
		m_VolumeHasChanged = Callback<VolumeHasChanged_t>.Create(OnVolumeHasChanged);
	}

	public void RenderOnGUI() {
		GUILayout.Label("SteamMusic.BIsEnabled() : " + SteamMusic.BIsEnabled());
		GUILayout.Label("SteamMusic.BIsPlaying() : " + SteamMusic.BIsPlaying());
		GUILayout.Label("SteamMusic.GetPlaybackStatus() : " + SteamMusic.GetPlaybackStatus());

		if (GUILayout.Button("Play()")) {
			SteamMusic.Play();
			print("SteamMusic.Play()");
		}

		if (GUILayout.Button("Pause()")) {
			SteamMusic.Pause();
			print("SteamMusic.Pause()");
		}

		if (GUILayout.Button("PlayPrevious()")) {
			SteamMusic.PlayPrevious();
			print("SteamMusic.PlayPrevious()");
		}

		if (GUILayout.Button("PlayNext()")) {
			SteamMusic.PlayNext();
			print("SteamMusic.PlayNext()");
		}

		if (GUILayout.Button("SetVolume(1.0f)")) {
			SteamMusic.SetVolume(1.0f);
			print("SteamMusic.SetVolume(1.0f)");
		}
		
		GUILayout.Label("SteamMusic.GetVolume() : " + SteamMusic.GetVolume());
	}

	void OnPlaybackStatusHasChanged(PlaybackStatusHasChanged_t pCallback) {
		Debug.Log("[" + PlaybackStatusHasChanged_t.k_iCallback + " - PlaybackStatusHasChanged]");
	}

	void OnVolumeHasChanged(VolumeHasChanged_t pCallback) {
		Debug.Log("[" + VolumeHasChanged_t.k_iCallback + " - VolumeHasChanged] - " + pCallback.m_flNewVolume);
	}
}
