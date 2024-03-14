using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TMP_Text pandaTalk;
    public TMP_Text InputName;
    public GameObject InputField;
    public GameObject saveButton;
    public GameObject menu;
    public static Player player;
    private void Start()
    {
        player = new Player();
        player.initPlayer();
        //save the last day the user opened the app
        PlayerPrefs.SetString("lastOpened", DateTime.Today.ToString());


        if (!PlayerPrefs.HasKey("playername"))
        {
            askPlayerName();
        }
        else
        {
            player.setName(PlayerPrefs.GetString("playername"));
            pandaTalk.text = "Hi " + player.getName() + ":) Are you ready to learn ??";
        }
        if (!PlayerPrefs.HasKey("wordCount"))
        {
            PlayerPrefs.SetFloat("wordCount", 0f);
        }
    }
    void askPlayerName()
    {
        saveButton.SetActive(true);
        menu.SetActive(false);
        InputField.SetActive(true);
        pandaTalk.text = "Hey! What's your name?";

    }

    public void savePlayerName()
    {
        saveButton.SetActive(false);
        InputField.SetActive(false);
        menu.SetActive(true);
        pandaTalk.text = "Hi " + PlayerPrefs.GetString("playername") + ":) Are you ready to learn ??";
        player.setName(InputName.text);
    }

    public void wantToQuit(GameObject objectA)
    {
        objectA.SetActive(true);
    }
    public void exit()
    {
        Application.Quit();
    }
}
