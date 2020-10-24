using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDecisionEvent : GameEvent
{
    public DecisionType decisionType = DecisionType.Miss;
    public Note note = null;

    public NoteDecisionEvent(DecisionType type, Note note)
    {
        this.decisionType = type;
        this.note = note;
    }
}