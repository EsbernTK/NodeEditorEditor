using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EmptyFieldElement : AbstractFieldElement {
    public override void OnEnable()
    {
        base.OnEnable();
        localElementRect = new Rect(0, 0, 100, 100);
    }
    public EmptyFieldElement()
    {
        localElementRect = new Rect(0, 0, 100, 100);
    }
    public override void Show(Vector2 pos)
    {
        base.Show(pos);
    }
}
