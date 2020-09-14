using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Steamworks;

public class NetworkManagerMenu : MonoBehaviour {

    const string VERSION = "v0.0.1";
    private string roomName = "";
    private string mapName = "RG01";
    public string playerPrefabName = "Player";
    public Transform SpawnPoint;
    public GameObject Nome;
    public GameObject inputFieldNickname;
    private string nome;

    public GameObject player;

    private Vector2 scrollPosition;

    private List<GameObject> currentRoom = new List<GameObject>();
    private int roomAmount = 0;
    private Room[] game;
    public GameObject roomListButton;
    public GameObject RoomListPanel;
    private static int contatorestanze = 0;
    private GameObject[] arrayLista;

    private bool flagNomeStanza;                //Flag per vedere se il nome della stanza è già in uso

    public GameObject[] bottoniColori;

    private CSteamID m_Friend;


    // Use this for initialization
    void Start()
    {
        bottoniColori[PlayerPrefs.GetInt("IDColor")].GetComponent<Image>().enabled = true;
        if (SteamManager.Initialized)
        {
            PhotonNetwork.playerName = SteamFriends.GetPersonaName();
        }
        //inputFieldNickname.GetComponent<InputField>().text = PlayerPrefs.GetString("Nickname");
        //if (PlayerPrefs.HasKey("Nickname")) PhotonNetwork.playerName = PlayerPrefs.GetString("Nickname"); ;
        PhotonNetwork.ConnectUsingSettings(VERSION);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerPrefs.GetString("Nickname"));
    }

    void OnJoinedLobby()
    {
        /*RoomOptions roomOptions = new RoomOptions();
        roomOptions.isVisible = true;
        roomOptions.maxPlayers = 2;

        roomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOptions.customRoomProperties["race"] = "RageGame";
        string[] lobbyProps = new string[1];
        lobbyProps[0] = "map";
        roomOptions.customRoomPropertiesForLobby = lobbyProps;


        PhotonNetwork.CreateRoom(roomName, roomOptions, null);*/
        //PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        /*Debug.Log("Sono dentro il server!");
        player = PhotonNetwork.Instantiate(playerPrefabName, SpawnPoint.position, SpawnPoint.rotation, 0);
        PhotonNetwork.playerName = nome;*/
    }

    /*public void CaricaNome(GameObject nickname)
    {
        nome = nickname.GetComponent<Text>().text;
        PhotonNetwork.playerName = nome;
        PlayerPrefs.SetString("Nickname", nome);
        Debug.Log(nome);
    }*/


    void OnConnectToGameServer()
    {
        PhotonNetwork.ConnectUsingSettings(VERSION);
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

	public void ColorePersonaggio(int i){
		PlayerPrefs.SetInt("IDColor",i);
	}

    public void HostName(GameObject inputFieldName)
    {
        flagNomeStanza = false;
        foreach (RoomInfo game in PhotonNetwork.GetRoomList())
        {
            if (game.name == inputFieldName.GetComponent<InputField>().text)
            {
                flagNomeStanza = true;
            }
        }
        if(!flagNomeStanza)roomName = inputFieldName.GetComponent<InputField>().text;
        else inputFieldName.GetComponent<InputField>().text = "";

        //Debug.Log(roomName.ToString());

    }

    public void MapName(GameObject inputFieldName)
    {
        mapName = inputFieldName.GetComponent<Text>().text;

        //Debug.Log(mapName.ToString());

    }

    public void HostPlayer(GameObject inputFieldPlayers)
    {
        /*maxPlayer = int.Parse(inputFieldPlayers.GetComponent<InputField>().text);

        Debug.Log(maxPlayer.ToString());*/
    }

    public void CreateButton(GameObject button)
    {
        //Create The Room with the given values
        //if the room name is not equal to null also players are greater then 0
        if (roomName != "" && mapName!= "" && PhotonNetwork.insideLobby)
        {
            //RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.isVisible = true;
            roomOptions.maxPlayers = 4;

            roomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable();
            roomOptions.customRoomProperties["map"] = mapName;
            string[] lobbyProps = new string[1];
            lobbyProps[0] = "map";
            roomOptions.customRoomPropertiesForLobby = lobbyProps;

			//PhotonNetwork.offlineMode = true;
            PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);

            Destroy(GameObject.FindGameObjectWithTag("LobbyCanvas").gameObject);
            //Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
            PhotonNetwork.LoadLevel(mapName);
        }

    }

    void OnPhotonJoinRoomFailed()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void ListRooms()
    {
        arrayLista = GameObject.FindGameObjectsWithTag("Server");
        for (int i = 0; i < arrayLista.Length; i++)
        {
            Destroy(arrayLista[i].gameObject);
        }
        roomAmount = 0;
        currentRoom.Clear();
        //Vector3 startPosition = new Vector3 (roomOrigionObject.position.x,roomOrigionObject.position.y,roomOrigionObject.position.z);

        /*
        for (int i =0; i < 10; i++) {
 
 
                        currentRoom.Add( (GameObject) Instantiate(roomListButton));
                        currentRoom[i].transform.SetParent(RoomListPanel.transform);
                        currentRoom[i].GetComponent<RectTransform>().sizeDelta = new Vector3(1,1,1);
                        currentRoom[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-6,117 - (i * 30));
 
                        }
        */
        /*foreach (RoomInfo game in PhotonNetwork.GetRoomList().)
        {
            contatorestanze++;
        }*/

        for (int i = 0; i < PhotonNetwork.GetRoomList().Length; i++)
        {
            contatorestanze++;
        }

        foreach (RoomInfo game in PhotonNetwork.GetRoomList()) // Each RoomInfo "game" in the amount of games created "rooms" display the fallowing.
        {
            Debug.Log(contatorestanze);
            if (roomAmount < contatorestanze)
            {
                //GUILayout.Box(game.name + " " + game.playerCount + "/" + game.maxPlayers); //Thus we are in a for loop of games rooms display the game.name provide assigned above, playercount, and max players provided. EX 2/20


                currentRoom.Add((GameObject)Instantiate(roomListButton));
                //GameObject currentRoom = Instantiate(roomListButton,startPosition,Quaternion.identity) as GameObject;
                currentRoom[roomAmount].transform.SetParent(RoomListPanel.transform);
                currentRoom[roomAmount].GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
                //currentRoom[roomAmount].GetComponent<RectTransform>().localPosition = new Vector3(-6, 200 - (roomAmount * 120), 0);




                //RoomButton roomButton = currentRoom.GetComponent<RoomButton>();
                currentRoom[roomAmount].GetComponent<RoomButton>().nome = game.name;
                currentRoom[roomAmount].GetComponent<RoomButton>().playercount = game.playerCount;
                currentRoom[roomAmount].GetComponent<RoomButton>().players = game.maxPlayers;
                currentRoom[roomAmount].GetComponent<RoomButton>().map = game.customProperties["map"].ToString();
                currentRoom[roomAmount].GetComponent<RoomButton>().game = game;


                roomAmount++;

                Debug.Log(currentRoom.Count.ToString());
                //roomButton.name = game.name;

                //PhotonNetwork.JoinRoom(game.name); // Next to each room there is a button to join the listed game.name in the current loop.

            }
            else
            {
                currentRoom.Clear();
            }
        }
        contatorestanze = 0;

    }
}
