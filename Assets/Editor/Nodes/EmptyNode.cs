using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EmptyNode : BaseNode {

    public override void OnEnable()
    {
        base.OnEnable();
        if (elements.Count == 0)
        {
            //elementRect = new Rect(0, 0, 200, 200);
            AddField(new TestField(this));
            AddField(new TestField(this));

        }

        
    }
    public EmptyNode()
    {
        //elementRect = new Rect(0, 0, 200, 200);
        //base.OnEnable();
        //AddField(new EmptyField(this));
        //AddField(new EmptyField(this));
        //AddField(CreateInstance<EmptyField>());
    }
    /*
    public override void OnEnable()
    {

        elementRect = new Rect(0, 0, 200, 200);
        //base.OnEnable();
        AddField(new EmptyField(this));
        AddField(new EmptyField(this));
    }
    */
}
