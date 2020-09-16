using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    [Header("Required Fields")]
    public Fader fader;
    public GameObject thanks;
    public GameObject endNotice;
    int index = 0;

    private void Start()
    {
        endNotice.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioController.instance.PlaySFX(1);
            NextScreen();
        }
    }

    public void NextScreen()
    {
        index++;

        if (index == 1)
        {
            thanks.SetActive(false);
            endNotice.SetActive(true);
        }
        else if (index == 2)
        {
            fader.ChangeLevel("Main");
        }
    }
}
