using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighSpeed : MonoBehaviour
{
    public static HighSpeed Instance;

    [Range(1,10)]
    public float MoveSpeed;

    public float CurrentTime;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        CurrentTime = 0;
    }

    void Update()
    {
        CurrentTime += Time.deltaTime;
    }
}
