using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyField : AbstractField
{

    public override void OnEnable()
    {
        base.OnEnable();
    }
    public EmptyField(BaseNode owner) : base(owner)
    {
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
