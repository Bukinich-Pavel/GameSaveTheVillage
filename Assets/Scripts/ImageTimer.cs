using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTimer : MonoBehaviour
{
    public float MaxTime;
    public bool Tick;

    private Image img;
    private float currentTime;
    void Start()
    {
        img = GetComponent<Image>();
        currentTime = 0;
    }

    
    void Update()
    {
        Tick = false;
        currentTime += Time.deltaTime;

        if (currentTime >= MaxTime)
        {
            Tick = true;
            currentTime = 0;
        }

        img.fillAmount = currentTime / MaxTime;
    }
}
