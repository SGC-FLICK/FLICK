using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDecisionEffectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _effect = null;
    [SerializeField] private int _createCount = 0;
    [SerializeField] private GameObject _decisionLine = null;

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
        for (int i = 0; i < _createCount; i++)
        {
            Instantiate(_effect, new Vector3(e.note.transform.position.x, _decisionLine.transform.position.y), Quaternion.identity);
        }
    }
}
