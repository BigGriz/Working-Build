using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    Animator animator;
    string level;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("FadeIn");
    }

    public void ChangeLevel(string _level)
    {
        animator.SetTrigger("FadeOut");
        level = _level;
    }
    public void LevelTransition()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }
}
