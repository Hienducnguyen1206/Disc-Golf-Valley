using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider MusicSlider;
    public AudioSource MusicSource;
    public Toggle MusicToggle;

    public Slider SFXSlider;
    public AudioSource SFXSource;
    public Toggle SFXToggle;



    public static AudioController instance;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
   
        MusicSource = transform.GetChild(0).GetComponent<AudioSource>();
        SFXSource = transform.GetChild(1).GetComponent<AudioSource>();

        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);

        MusicToggle.onValueChanged.AddListener(ToggleMusic);
       
        
        //SFXToggle.onValueChanged.AddListener(ToggleSFX);

     
        MusicToggle.isOn = MusicSource.isPlaying;
        SFXToggle.isOn = SFXSource.isPlaying;

        MusicSlider.value = MusicSource.volume;
        SFXSlider.value = SFXSource.volume;
    }







    private void SetMusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }

    private void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }

    private void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            MusicSource.Play();
        }
        else
        {
            MusicSource.Pause();
        }
    }

    /*
    private void ToggleSFX(bool isOn)
    {
        if (isOn)
        {
            SFXSource.Play();
        }
        else
        {
            SFXSource.Pause();
        }
    }*/
}
