using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CaricaOpzioni : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

        //RISOLUZIONE - FULLSCREEN
        /*if (PlayerPrefs.HasKey("resWidth") && PlayerPrefs.HasKey("resHeight"))
        {
            if (!PlayerPrefs.HasKey("Fullscreen"))
                Screen.SetResolution(PlayerPrefs.GetInt("resWidth"), PlayerPrefs.GetInt("resHeight"), true);
            else
                if(PlayerPrefs.GetInt("Fullscreen") == 1)
                    Screen.SetResolution(PlayerPrefs.GetInt("resWidth"), PlayerPrefs.GetInt("resHeight"), true);
                else Screen.SetResolution(PlayerPrefs.GetInt("resWidth"), PlayerPrefs.GetInt("resHeight"), false);
        }
        else
        {
            if (!PlayerPrefs.HasKey("Fullscreen"))
                Screen.SetResolution(Screen.width, Screen.height, true);
            else
                if (PlayerPrefs.GetInt("Fullscreen") == 1)
                    Screen.SetResolution(Screen.width, Screen.height, true);
                else
                    Screen.SetResolution(Screen.width, Screen.height, false);
        }*/


        //IN-GAME QUALITY
        if(PlayerPrefs.HasKey("Quality"))
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"), true);
        else
            QualitySettings.SetQualityLevel(2, true);


        //TRIPLE BUFFERING
        if (PlayerPrefs.HasKey("TripleBuffering"))
            QualitySettings.maxQueuedFrames = PlayerPrefs.GetInt("TripleBuffering");
        else
            QualitySettings.maxQueuedFrames = 3;


        //ANISOTROPIC FILTERING
        if (PlayerPrefs.HasKey("AnisotropicFiltering"))
            if(PlayerPrefs.GetInt("AnisotropicFiltering") == 1)
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
            else
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        else
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;





        SceneManager.LoadScene("Menu");




    }
	
}
