using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTemp : MonoBehaviour
{
    private float speed;
    
    public float ArrivalTime;

    void Start()
    {
        speed = HighSpeed.Instance.MoveSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position.y = (ArrivalTime - HighSpeed.Instance.CurrentTime) * HighSpeed.Instance.MoveSpeed;
        transform.position = position;
    }
}
