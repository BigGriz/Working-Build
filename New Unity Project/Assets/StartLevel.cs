using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();                      
    }

    private void Start()
    {
        animator.SetTrigger("Start");
    }

    public void StartUp()
    {
        CallbackHandler.instance.globalInfo.gamePaused = false;
    }
}
