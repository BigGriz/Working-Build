using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public Fader fader;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioController.instance.PlaySFX(1);
            fader.ChangeLevel("Main");
        }
    }
}
