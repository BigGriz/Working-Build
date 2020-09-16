using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour
{
    public Image energy;
    public Image cooldown;
    PlayerMovement player;

    private void Start()
    {
        player = PlayerMovement.instance;
    }

    // Update is called once per frame
    void Update()
    {
        energy.fillAmount = player.sprintTimer / 1.0f;
        cooldown.fillAmount = player.sprintCooldown / 1.0f;
    }
}
