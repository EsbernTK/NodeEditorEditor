using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[System.Serializable]
public class BaseNode : AbstractVisualElement, IHasCustomMenu {
    [SerializeField]
    public Dictionary<string, AbstractVisualElement> fieldDict = new Dictionary<string, AbstractVisualElement>();
    public string windowName;

    public override void OnEnable()
    {
        
        base.OnEnable();
        topMargin = 16;
        leftMargin = 15;

        rightMargin = 10;
        botMargin = 10;
        //elementRect = CalculateRect();
        //elementRect = new Rect(elementRect.x, elementRect.y, 200, 200);
    }
    public BaseNode()
    {

        //topMargin = 10f;
        //elementRect = CalculateRect();
    }
    
    public override void Show()
    {
        localElementRect = CalculateRect();
        windowElementRect = new Rect(new Vector2(0,0),localElementRect.size);
        worldElementRect = localElementRect;
        Vector2 curPos = new Vector2(0, topMargin);
        foreach(AbstractVisualElement field in elements)
        {

            if (field.drawElement)
            {
                field.Show(curPos);
                
                curPos.y += field.localElementRect.height;
            }
        }
    }
    
    public override void AddField(AbstractVisualElement field)
    {
        EmptyField newField = new EmptyField(this)
        {
            leftMargin = leftMargin,
            rightMargin = rightMargin
        };
        field.parentElement = newField;
        newField.AddField(field);
        base.AddField(newField);
        if (fieldDict.ContainsKey(field.name))
            fieldDict[field.name] = field;
        else
            fieldDict.Add(field.name, field);

    }

    public virtual void AddItemsToMenu(GenericMenu menu)
    {
        
    }
    public override Rect CalculateRect()
    {
        Rect temp = localElementRect;
        
        Vector2 tempPos = Vector2.zero;
        if (elements.Count > 0)
        {
            Rect record = new Rect(0, 0, 0, 0);

            foreach (AbstractVisualElement el in elements)
            {
                el.localElementRect = el.CalculateRect();
                if(el.localElementRect.position.x + el.localElementRect.width > record.width)
                    record.width = el.localElementRect.position.x + el.localElementRect.width;
                tempPos.y += el.localElementRect.height;
            }

            temp = new Rect(temp.position,new Vector2(record.width,tempPos.y + topMargin + botMargin));

        }
        return temp;
    }
    public override BaseNode GetBaseNode()
    {
        return this;
    }
}
