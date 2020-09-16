using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour
{
    [Header("Required Fields")]
    public Image energy;
    public Image cooldown;
    #region Setup
    PlayerMovement player;
    private void Start()
    {
        player = PlayerMovement.instance;
    }
    #endregion Setup

    // Update is called once per frame
    void Update()
    {
        energy.fillAmount = player.sprintTimer / 1.0f;
        cooldown.fillAmount = player.sprintCooldown / 1.0f;
    }
}
