using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongProgress : MonoBehaviour
{
    private Image progress;
    private float maxTime = 15f;

    void Start()
    {
        progress = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        progress.fillAmount = HighSpeed.Instance.CurrentTime / maxTime; 
    }
}
