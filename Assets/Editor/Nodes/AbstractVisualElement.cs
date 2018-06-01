using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
public abstract class AbstractVisualElement : ScriptableObject, IVisualElement, IClickable {
    [SerializeField]
    public Rect localElementRect;
    [SerializeField]
    public Rect windowElementRect;
    [SerializeField]
    public Rect worldElementRect;
    [SerializeField]
    public List<AbstractVisualElement> elements = new List<AbstractVisualElement>();
    public AbstractVisualElement parentElement;
    public int id;
    public bool drawElement = true;
    public float topMargin;
    public float botMargin { get; set; }
    public float leftMargin { get; set; }
    public float rightMargin { get; set; }

    public void SetLeftMargin(float val)
    {
        leftMargin = val;
    }

    public string elementName;

    public virtual void OnEnable()
    {
        if(elements == null)
        {
            elements = new List<AbstractVisualElement>();
        }
        //elementRect = CalculateRect();

    }

    public virtual AbstractVisualElement ClickedOn(Vector2 mousePos)
    {
        //Debug.Log(worldElementRect.Contains(mousePos) + " " + GetType());
        if (worldElementRect.Contains(mousePos))
        {
            foreach (AbstractVisualElement field in elements)
            {
                AbstractVisualElement el = field.ClickedOn(mousePos);
                if (el != null)
                {
                    return el;
                }
            }
            return this;
        }
        return null;
    }

    public virtual Rect GetElementRect()
    {
        return localElementRect;
    }

    public virtual void SetElementRect(Rect newRect)
    {
        localElementRect = newRect;
    }

    public virtual void IncrementElementRect(Vector2 inc)
    {
        localElementRect.position += inc;
        Debug.Log("calling that");
    }

    public virtual Rect CalculateRect()
    {
        Rect temp = localElementRect;
        if (elements.Count > 0)
        {
            Rect record = elements[0].CalculateRect(); //new Rect(0,0,0,0);
            foreach (AbstractVisualElement el in elements)
            {
                el.localElementRect = el.CalculateRect();
                Vector2 pos = el.localElementRect.position;
                Vector2 siz = el.localElementRect.size;
                /*
                if (pos.x < record.x)
                    record.x = pos.x;
                if (pos.y < record.y)
                    record.y = pos.y;
                    */
                if (pos.x + siz.x > record.width)
                    record.width = pos.x + siz.x;
                if (pos.y + siz.y > record.height)
                    record.height = pos.y + siz.y;
            }
            record.height += topMargin + botMargin;
            record.width += leftMargin + rightMargin;
            temp = new Rect(localElementRect.position,record.size);
        }
        return temp;
    }
    public virtual void CalculateElementRects(Vector2 position)
    {
        localElementRect = CalculateRect();
        
        if (parentElement is BaseNode)
        {
            localElementRect.position = position;
            if(localElementRect.width < parentElement.windowElementRect.width)
                localElementRect.width = parentElement.windowElementRect.width;
        }
        localElementRect.position = Vector2.Max(position, localElementRect.position);
        windowElementRect = new Rect(parentElement.windowElementRect.position + localElementRect.position, localElementRect.size);
        worldElementRect = new Rect(parentElement.worldElementRect.position + localElementRect.position, localElementRect.size);
    }
    public virtual void Show(Vector2 position)
    {
        CalculateElementRects(position);
        //Debug.Log(elementRect + " " + GetType() + " "+ worldElementRect + " " + position);
        foreach (AbstractVisualElement field in elements)
        {

            if (field.drawElement)
            {
                field.Show(new Vector2(leftMargin,topMargin));
            }
        }
        Handles.color = Color.grey;
        Handles.DrawWireCube(windowElementRect.position+ localElementRect.size / 2, localElementRect.size);
    }
    public virtual void Show()
    {
        localElementRect = CalculateRect();
        foreach (AbstractVisualElement field in elements)
        {

            if (field.drawElement)
            {
                field.Show();
            }
        }
    }

    public virtual void AddField(AbstractVisualElement field)
    {
        elements.Add(field);
        field.parentElement = this;
        field.id = elements.Count - 1;

    }
    public virtual void DestroyThis()
    {
        if (elements.Count != 0)
        {
                for (int i = elements.Count - 1; i > 0; i--)
                {
                    elements[i].DestroyThis();
                }
            
        }
        DestroyImmediate(this);
    }
    public virtual BaseNode GetBaseNode()
    {
        return parentElement.GetBaseNode();
    }
}
