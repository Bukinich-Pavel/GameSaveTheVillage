using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_PlayStop : MonoBehaviour
{
    public GameObject Pause;

    public Button buttonPlayStopGame;
    public Sprite spriteStopGame;
    public Sprite spritePlayGame;

    public Button buttonMusic;
    public Sprite spriteOffMusic;
    public Sprite spriteOnMusic;

    public AudioSource AudioSourceBackAudio;
    public AudioSource[] AudioSourceGroupe;

    public void ClickButtonPlayPauseGame()
    {
        if (Time.timeScale == 0)
        {
            buttonPlayStopGame.image.sprite = spriteStopGame;
            AudioSourceBackAudio.Play();
            Time.timeScale = 1;
            Pause.SetActive(false);
        }
        else
        {
            buttonPlayStopGame.image.sprite = spritePlayGame;
            AudioSourceBackAudio.Pause();
            Time.timeScale = 0;
            Pause.SetActive(true);
        }
    }

    public void ClickButtonMusicOnOff()
    {

        if (AudioSourceGroupe[0].mute)
        {
            MusicOff();
        }
        else
        {
            MusicOn();
        }
    }

    private void MusicOn()
    {
        foreach (var item in AudioSourceGroupe)
        {
            item.mute = true;
        }
        AudioSourceBackAudio.mute = true;
        buttonMusic.image.sprite = spriteOffMusic;
    }

    private void MusicOff()
    {
        foreach (var item in AudioSourceGroupe)
        {
            item.mute = false;
        }
        AudioSourceBackAudio.mute = false;
        buttonMusic.image.sprite = spriteOnMusic;
    }
}
