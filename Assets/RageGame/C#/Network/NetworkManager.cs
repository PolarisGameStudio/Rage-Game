using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Steamworks;

public class NetworkManager : Photon.MonoBehaviour {

    const string VERSION = "v0.0.1";
    public string playerPrefabName = "Player";
    public Transform SpawnPoint;
    public GameObject Nome;
    private string nome;

    public GameObject player;

	// Use this for initialization

    void Start()
    {
        if (PlayerPrefs.HasKey("VolumeMusica"))
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("VolumeMusica");
        }

        if (PhotonNetwork.inRoom)PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        //PhotonNetwork.ConnectUsingSettings(VERSION);
        if (PhotonNetwork.inRoom) OnJoinedRoom();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnJoinedLobby()
    {
        //RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
        //PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        //OnJoinedRoom();
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.automaticallySyncScene = true;
        if (PhotonNetwork.room.open)
        {
            //PhotonNetwork.LoadLevel("RageGame");
            Debug.Log("Sono dentro il server!");
            //PhotonNetwork.room.customProperties["map"] = (Application.loadedLevelName);

            if (PhotonNetwork.isMasterClient)
            {
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.isVisible = true;
                roomOptions.maxPlayers = 4;
                roomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable();
                roomOptions.customRoomProperties["map"] = (Application.loadedLevelName);
                string[] lobbyProps = new string[1];
                lobbyProps[0] = "map";
                roomOptions.customRoomPropertiesForLobby = lobbyProps;


                PhotonNetwork.room.SetCustomProperties(roomOptions.customRoomProperties);
            }
            Debug.Log("Mappa: " + PhotonNetwork.room.customProperties["map"].ToString());
            player = PhotonNetwork.Instantiate(playerPrefabName, SpawnPoint.position, SpawnPoint.rotation, 0);
        }
        //PhotonNetwork.playerName = nome;
    }

    void OnPhotonJoinRoomFailed()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }

    public void Disconnetti()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }


  
    void OnGUI()
    {

        //Show Detail of connection to master server
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        GUILayout.Label("Ping: " + PhotonNetwork.GetPing());

        //GUILayout.Label (PhotonNetwork.GetPing ().ToString ());

        //Connection to master server lobby if joined
        /*if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
        {

        }*/


        //If I'm connected and inside lobby
        /*if (PhotonNetwork.insideLobby == true)
        {

            //Display the lobby connection list and room creation.
            GUI.Box(new Rect(Screen.width / 2.5f, Screen.height / 3, 400, 550), "");
            GUILayout.BeginArea(new Rect(Screen.width / 2.5f, Screen.height / 3f, 400, 500));
            GUI.color = Color.red;
            GUILayout.Box("Lobby");
            GUI.color = Color.white;

            GUILayout.Label("Room Name:");
            roomName = GUILayout.TextField(roomName,20); //For network room name ask and recieve

            if (GUILayout.Button("Create Room"))
            {

                if (roomName != "") // if the room name has a name and max players are larger then 0
                {
                    RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
                    PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default); // then create a photon room visible , and open with the maxplayers provide by user.

                }
            }

            GUILayout.Space(20);
            GUI.color = Color.red;
            GUILayout.Box("Game Rooms");
            GUI.color = Color.white;
            GUILayout.Space(20);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Width(400), GUILayout.Height(300));

            foreach (RoomInfo game in PhotonNetwork.GetRoomList()) // Each RoomInfo "game" in the amount of games created "rooms" display the fallowing.
            {

                GUI.color = Color.green;
                GUILayout.Box(game.name + " " + game.playerCount + "/" + game.maxPlayers); //Thus we are in a for loop of games rooms display the game.name provide assigned above, playercount, and max players provided. EX 2/20
                GUI.color = Color.white;

                if (GUILayout.Button("Join Room"))
                {

                    PhotonNetwork.JoinRoom(game.name); // Next to each room there is a button to join the listed game.name in the current loop.
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();


        }*/
    }
}
