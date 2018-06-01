using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestField : AbstractField {
    public TestField(BaseNode owner) : base(owner)
    {
    }

    public override void OnEnable()
    {

        if (elements.Count == 0)
        {
            localElementRect = new Rect(0, 0, 0, 0);
            AddField(new EmptyFieldElement());
            AddField(new EmptyFieldElement());
            AddField(new EmptyFieldElement());
        }
        //topMargin = 0;
        leftMargin = 10;
        base.OnEnable();
    }
    public override void Show()
    {
        base.Show();
    }
    public override void Show(Vector2 pos)
    {
        base.Show(pos);
    }
}
