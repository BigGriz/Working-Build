using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer = 61.0f;
    TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 1.0f)
        {
            timer -= Time.deltaTime;
        }
        text.SetText(Mathf.FloorToInt(timer).ToString());

        // Call EndGame Here
    }
}
