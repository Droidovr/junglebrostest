﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu_ChracterChoose : MonoBehaviour
{

    [Tooltip("The unique character ID")]
    public int characterID;
    public int price;
    public GameObject CharacterPrefab;


    public bool unlockDefault = false;

    //	public GameObject Locked;
    public GameObject UnlockButton;

    public Text pricetxt;
    public Text state;

    [HideInInspector]
    public bool isUnlock;
    SoundManager soundManager;
    private ChangeHero changeHero;
    private bool clickAllowed;
    // Use this for initialization
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();

        if (unlockDefault)
            isUnlock = true;
        else
            isUnlock = PlayerPrefs.GetInt(GlobalValue.Character + characterID, 0) == 1 ? true : false;

        //		Locked.SetActive (!isUnlock);
        UnlockButton.SetActive(!isUnlock);

        pricetxt.text = price.ToString();

        state.text = Localisation.GetString("CharacterStateLocked");
    }

    void Update()
    {
        if (!isUnlock)
        {
            return;
        }


        if (PlayerPrefs.GetInt(GlobalValue.ChoosenCharacterID, 1) == characterID)
        {
            state.text = Localisation.GetString("CharacterStatePicked1");
        }

        else
        {
            state.text = Localisation.GetString("CharacterStateChoose1");
        }

        if (isUnlock)
        {
            clickAllowed = true;
        }

    }


    public void Unlock()
    {
        SoundManager.PlaySfx(soundManager.soundClick);

        var coins = PlayerPrefs.GetInt(GlobalValue.Coins, 0);
        if (coins >= price)
        {
            coins -= price;
            PlayerPrefs.SetInt(GlobalValue.Coins, coins);
            //Unlock
            PlayerPrefs.SetInt(GlobalValue.Character + characterID, 1);
            isUnlock = true;
            //			Locked.SetActive (false);
            UnlockButton.SetActive(false);
        }
        else
            NotEnoughCoins.Instance.ShowUp();
    }

    public void Pick()
    {
        SoundManager.PlaySfx(soundManager.soundClick);

        if (clickAllowed)
        {
            Debug.Log(333);
            PlayerPrefs.SetInt(GlobalValue.ChoosenCharacterID, characterID);
            PlayerPrefs.SetInt(GlobalValue.ChoosenCharacterInstanceID, CharacterPrefab.GetInstanceID());

            CharacterHolder.Instance.CharacterPicked = CharacterPrefab;
        }

    }
}
