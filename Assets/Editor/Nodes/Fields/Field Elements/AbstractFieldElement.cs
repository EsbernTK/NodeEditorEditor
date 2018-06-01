using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[System.Serializable]
public abstract class AbstractFieldElement : AbstractVisualElement, IFieldElement, IHasCustomMenu {
    public Rect fieldElementWorldRect;
    public AbstractField parent;

    public override void OnEnable()
    {
        base.OnEnable();
        localElementRect = CalculateRect();
    }

    public override void Show()
    {
        
        Event e = Event.current;
        base.Show();


        if (e.type == EventType.Repaint)
        {
            if(elements.Count > 0) {
                localElementRect = CalculateRect();
            }
            else
            {

                //elementRect = GUILayoutUtility.GetLastRect();
                localElementRect = new Rect(0, 0, 100, 100);
            }
        }
        GUI.Box(windowElementRect, "");
    }
    public override void Show(Vector2 pos)
    {
        Event e = Event.current;
        base.Show(pos);
        //Debug.Log(fieldElementWorldRect);
        GUI.Box(windowElementRect, "");
    }
    public virtual void ShowConnectionsToSubElements()
    {
        foreach(AbstractFieldElement element in elements)
        {
            NodeEditorEditorWindow.DrawNodeCurve(localElementRect, element.localElementRect);
        }
    }


    public virtual void AddItemsToMenu(GenericMenu menu)
    {
        
        throw new System.NotImplementedException();
    }
}
