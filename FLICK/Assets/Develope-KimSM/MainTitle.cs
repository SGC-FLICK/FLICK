﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTitle : MonoBehaviour
{
    public void TouchTitle()
    {
        SceneManager.LoadScene("SelectMusicScene");
    }
}
