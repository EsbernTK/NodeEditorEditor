using UnityEngine;
public interface IVisualElement {
    Rect GetElementRect();
    void SetElementRect(Rect newRect);
    void Show();
    void Show(Vector2 pos);
}
