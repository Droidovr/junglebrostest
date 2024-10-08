using UnityEngine;
using UnityEngine.UI;

public class MainMenu_StartMenu : MonoBehaviour
{
    public static bool boolSoundOn = true;
    public Image soundImage;
    public Sprite soundOn;
    public Sprite soundOff;

    SoundManager soundManager;

   
    void Start()
    {
        soundManager = SoundManager.Instance;
        setSound(PlayerPrefs.GetInt("soundOn", 1) == 1);
    }



    public void TurnSound()
    {

        setSound(!boolSoundOn);

    }

    private void setSound(bool sound)
    {
        SoundManager.PlaySfx(soundManager.soundClick);
        boolSoundOn = !boolSoundOn;

        if (sound)
        {
            soundImage.sprite = soundOn;
            PlayerPrefs.SetInt("soundOn", 1);
            AudioListener.volume = 1;
        }

        else
        {
            soundImage.sprite = soundOff;
            PlayerPrefs.SetInt("soundOn", 0);
            AudioListener.volume = 0;
        }
    }
}