using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Opzioni : MonoBehaviour {

    //AUDIO
	public Slider sliderVolumeMusica;
	public Slider sliderVolumeFX;

	public GameObject Audio;


    //VIDEO
    public Dropdown Resolution;
    public Dropdown Quality;

    public Toggle Fullscreen;
    public Toggle TripleBuffering;
    public Toggle Anisotropicfiltering;


    Resolution[] resolutions;


    string resolutionText; //testo che indica le risoluzioni trovate
    int value = 0;         //Indice della risoluzione di quando apriamo il gioco

    // Use this for initialization
    void Start () {
   		if (PlayerPrefs.HasKey ("VolumeMusica")) {
			Audio.GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat("VolumeMusica");
			sliderVolumeMusica.value = PlayerPrefs.GetFloat("VolumeMusica");
            sliderVolumeFX.value = PlayerPrefs.GetFloat("VolumeFX");
		}

        Resolution.options.Clear();


        resolutions = Screen.resolutions;
        System.Array.Reverse(resolutions);
        foreach (Resolution res in resolutions)
        {
            resolutionText = res.width + "x" + res.height;
            Resolution.options.Add(new Dropdown.OptionData() { text = resolutionText });

            /*if (resolutionText != Screen.width + "x" + Screen.height)
                value++;*/
        }






        /*Debug.Log(value);
        Resolution.value = value;*/
        Resolution.Select();
        Resolution.RefreshShownValue();
        //Resolution.transform.FindChild("Label").GetComponent<Text>().text = Screen.width+"x"+Screen.height;

        LoadSettings();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SalvaAudio(){
		PlayerPrefs.SetFloat ("VolumeMusica", sliderVolumeMusica.value);
		PlayerPrefs.SetFloat ("VolumeFX", sliderVolumeFX.value);
		Audio.GetComponent<AudioSource> ().volume = sliderVolumeMusica.value;
	}


    public void UpdateResolution()
    {
        string resolution = Resolution.transform.FindChild("Label").GetComponent<Text>().text;
        string[] tmp = resolution.Split('x');

        Screen.SetResolution(int.Parse(tmp[0]), int.Parse(tmp[1]), Fullscreen.isOn);
        PlayerPrefs.SetInt("resWidth", int.Parse(tmp[0]));
        PlayerPrefs.SetInt("resHeight", int.Parse(tmp[1]));
        PlayerPrefs.SetInt("resValue", Resolution.value);
    }

    public void UpdateQuality()
    {

        switch (Quality.transform.FindChild("Label").GetComponent<Text>().text) {
            case "High":
                QualitySettings.SetQualityLevel(2,true);
            break;
            case "Medium":
                QualitySettings.SetQualityLevel(1, true);
            break;
            case "Low":
                QualitySettings.SetQualityLevel(0, true);
            break;
        }

        PlayerPrefs.SetInt("Quality", QualitySettings.GetQualityLevel());

    }

    public void UpdateWindowed()
    {
        if (Fullscreen.isOn)
        {
            Screen.fullScreen = true;
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            Screen.fullScreen = false;
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        

    }


    public void UpdateTripleBuffering()
    {
        if (TripleBuffering.isOn)
            QualitySettings.maxQueuedFrames = 3;
        else
            QualitySettings.maxQueuedFrames = 0;


        PlayerPrefs.SetInt("TripleBuffering", QualitySettings.maxQueuedFrames);
    }

    public void UpdateAnisotropicFiltering()
    {
        if (Anisotropicfiltering.isOn)
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
            PlayerPrefs.SetInt("AnisotropicFiltering", 1);
        }
        else
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            PlayerPrefs.SetInt("AnisotropicFiltering", 0);
        }
    }


    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("resWidth") && PlayerPrefs.HasKey("resHeight"))
        {
            Resolution.value = PlayerPrefs.GetInt("resValue");
        }


        Debug.Log(PlayerPrefs.GetInt("Fullscreen"));
        if (PlayerPrefs.HasKey("Fullscreen"))
            if (PlayerPrefs.GetInt("Fullscreen") == 1)
                Fullscreen.isOn = true;
            else
                Fullscreen.isOn = false;


        if (PlayerPrefs.HasKey("Quality"))
        {
            switch (PlayerPrefs.GetInt("Quality")) {
                case 2:
                    Quality.value = 0;//Quality.transform.FindChild("Label").GetComponent<Text>().text = "High";
                    break;

                case 1:
                    Quality.value = 1;//Quality.transform.FindChild("Label").GetComponent<Text>().text = "Medium";
                    break;

                case 0:
                    Quality.value = 2;
                    //Quality.transform.FindChild("Label").GetComponent<Text>().text = "Low";
                    break;
            }
        }


        if (PlayerPrefs.HasKey("TripleBuffering"))
        {
            if(PlayerPrefs.GetInt("TripleBuffering") == 3)
                TripleBuffering.isOn = true;
            else
                TripleBuffering.isOn = false;
        }


        if (PlayerPrefs.HasKey("AnisotropicFiltering"))
        {
            if (PlayerPrefs.GetInt("AnisotropicFiltering") == 1)
                Anisotropicfiltering.isOn = true;
            else
                Anisotropicfiltering.isOn = false;
        }

    }

}
