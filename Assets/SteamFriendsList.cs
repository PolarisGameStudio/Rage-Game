using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using Steamworks;


public class SteamFriendsList : MonoBehaviour {

    private Vector2 scrollPosition;

    private List<GameObject> friendsList = new List<GameObject>();
    private int friendsAmount = 0;
    private Room[] game;
    public GameObject FriendListButton;
    public GameObject FriendListPanel;
    private static int contatoreFriends = 0;
    private GameObject[] arrayLista;

    public GameObject textNumeroGiocatori;
    public GameObject textNumeroDead;
    public GameObject textNumeroLivelliCompletati;

    struct FriendStatusID
    {
        public CSteamID id;
        public int status;
        public int game;
    };

    private CallResult<NumberOfCurrentPlayers_t> m_NumberOfCurrentPlayers;
    FriendStatusID[] FriendStatus;

	// Use this for initialization
	void Start () {
        textNumeroDead.GetComponent<Text>().text = "DEAD: " + PlayerPrefs.GetInt("NumeroDead");
        textNumeroLivelliCompletati.GetComponent<Text>().text = "LEVELS COMPLETED: " + PlayerPrefs.GetInt("NumeroLivelliCompletati");
        Cancella();
        ContaGiocatori();
        ListFriends();
	}
	
	// Update is called once per frame

    public void ListFriends()
    {
        int n = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
        FriendStatus = new FriendStatusID[n];

        for (int i = 0; i < n; i++)
        {
            FriendStatus[i].id = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
            FriendStatus[i].status = (int)SteamFriends.GetFriendPersonaState(SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate));

            var fgi = new FriendGameInfo_t();
            bool StoGiocando = SteamFriends.GetFriendGamePlayed(SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate), out fgi);

            FriendStatus[i].game = StoGiocando ? 1 : 0;
        }

        FriendStatus = FriendStatus.OrderByDescending(go => go.game).ThenByDescending(go => go.status).ToArray();



        for (int i = 0; i < n; i++) // Each RoomInfo "game" in the amount of games created "rooms" display the fallowing.
            {
                
                friendsList.Add((GameObject)Instantiate(FriendListButton));
                friendsList[friendsAmount].transform.SetParent(FriendListPanel.transform);
                friendsList[friendsAmount].GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);



                friendsList[friendsAmount].GetComponent<FriendButton>().m_Friend = FriendStatus[i].id;
                friendsAmount++;

            }
        contatoreFriends = 0;

    }

    public void Cancella()
    {
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
        FriendStatus = null;
        arrayLista = null;
        arrayLista = GameObject.FindGameObjectsWithTag("Friend");
        for (int i = 0; i < arrayLista.Length; i++)
        {
           DestroyImmediate(arrayLista[i].gameObject);
        }
        friendsList.Clear();
        friendsAmount = 0;
    }

    private void OnEnable()
    {
        if (SteamManager.Initialized)
        {
            m_NumberOfCurrentPlayers = CallResult<NumberOfCurrentPlayers_t>.Create(OnNumberOfCurrentPlayers);
        }
    }

    public void ContaGiocatori()
    {
        SteamAPICall_t handle = SteamUserStats.GetNumberOfCurrentPlayers();
        m_NumberOfCurrentPlayers.Set(handle);
    }

    private void OnNumberOfCurrentPlayers(NumberOfCurrentPlayers_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_bSuccess != 1 || bIOFailure)
        {
            Debug.Log("There was an error retrieving the NumberOfCurrentPlayers.");
        }
        else
        {
            textNumeroGiocatori.GetComponent<Text>().text = pCallback.m_cPlayers.ToString();
            Debug.Log("The number of players playing your game: " + pCallback.m_cPlayers);
        }
    }

}
