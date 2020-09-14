using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Steamworks;

public class FriendButton : MonoBehaviour {

    public CSteamID m_Friend;
    private GameObject Nickname;
    private GameObject Bordo;
    private GameObject Immagine;
    private GameObject Status;
	// Use this for initialization

    
    void Start () {

        Nickname = transform.Find("FriendNickname").gameObject;
        Status = transform.Find("Status").gameObject;
        Bordo = transform.Find("Image").gameObject;
        Immagine = Bordo.transform.Find("FriendImage").gameObject;


        transform.GetComponent<Button>().onClick.AddListener(() => { OpenChat(); });

        int FriendAvatar = SteamFriends.GetMediumFriendAvatar(m_Friend);

        uint ImageWidth;
        uint ImageHeight;
        bool ret = SteamUtils.GetImageSize(FriendAvatar, out ImageWidth, out ImageHeight);

        if (ret && ImageWidth > 0 && ImageHeight > 0)
        {
            byte[] Image = new byte[ImageWidth * ImageHeight * 4];

            ret = SteamUtils.GetImageRGBA(FriendAvatar, Image, (int)(ImageWidth * ImageHeight * 4));

            Texture2D m_SmallAvatar;
            m_SmallAvatar = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
            m_SmallAvatar.LoadRawTextureData(Image); // The image is upside down! "@ares_p: in Unity all texture data starts from "bottom" (OpenGL convention)"
            m_SmallAvatar.Apply();
            Immagine.GetComponent<Image>().sprite = Sprite.Create(m_SmallAvatar, new Rect(0, 0, m_SmallAvatar.width, m_SmallAvatar.height), new Vector2(0.5f, 0.5f));
        }



        if (SteamFriends.GetFriendPersonaState(m_Friend).ToString() == "k_EPersonaStateOffline")
        {
            Nickname.GetComponent<Text>().color = new Color32(255, 255, 255, 100);
            Status.GetComponent<Text>().color = new Color32(255, 255, 255, 100);
            Status.GetComponent<Text>().text = "Offline";
            Bordo.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
        else if (SteamFriends.GetFriendPersonaState(m_Friend).ToString() == "k_EPersonaStateSnooze")
        {
            Nickname.GetComponent<Text>().color = new Color32(77, 148, 176, 255);
            Status.GetComponent<Text>().color = new Color32(77, 148, 176, 255);
            Status.GetComponent<Text>().text = "Snooze";
            Bordo.GetComponent<Image>().color = new Color32(77, 148, 176, 255);
        }

        else{
            var fgi = new FriendGameInfo_t();
            bool StoGiocando = SteamFriends.GetFriendGamePlayed(m_Friend, out fgi);
            if (StoGiocando)
            {
                Bordo.GetComponent<Image>().color = new Color32(126, 161, 54, 255);
                Nickname.GetComponent<Text>().color = new Color32(126, 161, 54, 255);
                Status.GetComponent<Text>().color = new Color32(126, 161, 54, 255);
                if(fgi.m_gameID.ToString() != "480")
                    Status.GetComponent<Text>().text = "Playing another game";
                else
                {
                    Status.GetComponent<Text>().text = "Playing RageGame";
                }
            }
            else
            {
                Nickname.GetComponent<Text>().color = new Color32(77, 148, 176, 255);
                Status.GetComponent<Text>().color = new Color32(77, 148, 176, 255);
                Status.GetComponent<Text>().text = "Online";
                Bordo.GetComponent<Image>().color = new Color32(77, 148, 176, 255);
            }
        }

        Nickname.GetComponent<Text>().text = SteamFriends.GetFriendPersonaName(m_Friend);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OpenChat()
    {
        SteamFriends.ActivateGameOverlayToUser("chat", m_Friend); 
    }
}
