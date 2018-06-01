using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class NodeEditorEditorWindow : EditorWindow {

    public static NodeEditorEditorWindow editor;
    public NodeEditorSaver saver;

    private Vector2 mousePos;

    List<BaseNode> nodes = new List<BaseNode>();
    public string pathName = "Assets/Noise Assets/";
    public string saverName = "NodeEditorWindow";
    public bool makingConnection;
    AbstractVisualElement selectedElement;
    private void Awake()
    {
        
        pathName = pathName + saverName + ".asset";
        try
        {

            saver = (NodeEditorSaver)AssetDatabase.LoadAssetAtPath(pathName, typeof(NodeEditorSaver));
            Debug.Log("Loading Asset");
            nodes = saver.nodes;
        }
        catch { }
        if (!saver)
        {
            Debug.Log("creating asset");
            saver = new NodeEditorSaver();
            saver.name = saverName;
            saver.nodes = new List<BaseNode>();
            nodes.Add(new EmptyNode());
            AssetDatabase.CreateAsset(saver, pathName);
        }

    }
    [MenuItem("Window/Noise Editor")]
    static void ShowEditor()
    {
        editor = GetWindow<NodeEditorEditorWindow>();
    }
    private void OnGUI()
    {
        Event e = Event.current;
        mousePos = e.mousePosition;
        if (e.type == EventType.MouseDown)
        {
            BaseNode tempNode = ClickedOnANode();
            if (e.button == 1)
            {

                if (tempNode == null)
                {
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Create Node"), false, AddNode, typeof(EmptyNode));
                    menu.ShowAsContext();
                    e.Use();
                }
                else
                {
                    GUI.BringWindowToFront(tempNode.id);
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Delete Node"), false, DeleteNode, tempNode);
                    menu.ShowAsContext();
                    e.Use();
                }
            }
            else if (e.button == 0)
            {
                if (tempNode != null)
                {
                    selectedElement = tempNode.ClickedOn(mousePos); //!= tempNode ? tempNode.ClickedOn(mousePos) : null;
                    if (selectedElement) { }
                        //Debug.Log(selectedElement.GetType());
                }
                
            }
        }
        if (e.type == EventType.mouseDrag)
        {
            Debug.Log("calling this");
            if (selectedElement != null)//&& !(selectedElement is BaseNode))
            {
                selectedElement.IncrementElementRect(e.delta);
                //selectedElement.GetBaseNode().CalculateRect();
                Event.current.Use();
            }
        }
        if(e.type == EventType.mouseUp)
        {
            selectedElement = null;
        }
        BeginWindows();
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].localElementRect = GUI.Window(i, nodes[i].localElementRect, showNode, nodes[i].windowName);
            
        }
        EndWindows();
    }

    private void showNode(int id)
    {
        nodes[id].id = id;
        nodes[id].Show();
        //GUI.DragWindow();

    }
    public void OnDestroy()
    {
        saver.nodes = nodes;
        AssetDatabase.SaveAssets();
    }
    public void AddNode(object type)
    {

        System.Type clb = (System.Type)type;
        ConstructorInfo constructor = clb.GetConstructor(System.Type.EmptyTypes);
        BaseNode node = null;
        try
        {
            node = (BaseNode)constructor.Invoke(null);
            node.localElementRect.position = mousePos;
            nodes.Add(node);
        }
        catch
        {
            Debug.Log("Warning: node type must inherit from BaseNode");
        }

    }
    public void DeleteNode(object node)
    {
        //if (EditorUtility.DisplayDialog("Delete Elements?", "This element has one or more sub-elements, are you sure you want to delete them?", "Okay", "Cancel"))
        BaseNode curNode = (BaseNode)node;
        nodes.Remove(curNode);
        curNode.DestroyThis();

    }
    private BaseNode ClickedOnANode()
    {
        BaseNode tempNode = null;
        foreach (BaseNode node in nodes)
        {
            if (node.localElementRect.Contains(mousePos))
            {
                if (tempNode == null)
                    tempNode = node;
                
                return node;
            }

        }
        return null;
    }

    public static void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x + end.width / 2, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++)
        {
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }
}
