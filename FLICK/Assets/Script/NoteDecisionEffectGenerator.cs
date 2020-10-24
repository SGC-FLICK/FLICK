using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDecisionEffectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _effect = null;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.AddListener<NoteDecisionEvent>(NoteDecisionEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NoteDecisionEvent(NoteDecisionEvent e)
    {
        
    }
}
