using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    int index = 0;
    public GameObject mainMenuImage;
    public GameObject controlsMenuImage;

    private void Start()
    {
        controlsMenuImage.SetActive(false);
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
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("WorkingScene");
        }
    }
}
