using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    int index = 0;
    [Header("Required Fields")]
    public GameObject mainMenuImage;
    public GameObject controlsMenuImage;
    public Fader fader;
    public GlobalInfo globalInfo;

    private void Start()
    {
        controlsMenuImage.SetActive(false);
        globalInfo.lockdownLevel = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextScreen();
            AudioController.instance.PlaySFX(1);
        }
    }

    public void NextScreen()
    {
        index++;

        if (index == 1)
        {
            mainMenuImage.SetActive(false);
            controlsMenuImage.SetActive(true);
        }
        else if (index == 2)
        {
            fader.ChangeLevel("Level1");  
        }
    } 
}
