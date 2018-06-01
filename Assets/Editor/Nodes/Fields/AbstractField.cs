using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[System.Serializable]
public abstract class AbstractField: AbstractVisualElement, IField,IHasCustomMenu {
    public FieldType fieldType;
    public BaseNode owner;
    public object value;
    public bool drawField;
    public override void OnEnable()
    {
        base.OnEnable();
        localElementRect = CalculateRect();
    }
    public AbstractField(BaseNode owner,string name = "Place holder field", FieldType fieldType = FieldType.NoInput, bool drawField = true) : base()
    {
        this.fieldType = fieldType;
        this.name = name;
        this.drawField = drawField;
        this.owner = owner;
        this.localElementRect = CalculateRect();
    }
    public override void Show(Vector2 position)
    {
        EventType e = Event.current.type;
        base.Show(position);
    }
    public override void Show()
    {
        EventType e = Event.current.type;
        base.Show();
    }

    public virtual void AddItemsToMenu(GenericMenu menu)
    {
        throw new System.NotImplementedException();
    }
}
