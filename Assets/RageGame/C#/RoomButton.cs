using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Steamworks;

public class RoomButton : MonoBehaviour
{

    public string nome;
    public int players;
    public int playercount;
    public int ping;
    public string map;
    public RoomInfo game;
    public GameObject uiCanvas;

    void Start()
    {
        //int currentPlayers = PhotonNetwork.countOfPlayersInRooms;

        transform.GetChild(0).GetComponent<Text>().text = nome + "                        " + playercount.ToString() + "/" + players.ToString()+"          "+map;
        uiCanvas = GameObject.FindGameObjectWithTag("LobbyCanvas").gameObject;
    }

    public void JoinGame()
    {
        if (playercount < 4)
        {
            PhotonNetwork.JoinRoom(nome);
            PhotonNetwork.LoadLevel(map);
            Destroy(uiCanvas.gameObject);
        }

    }
}
