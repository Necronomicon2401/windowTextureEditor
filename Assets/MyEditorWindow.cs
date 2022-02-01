using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyEditorWindow : EditorWindow
{
    private Color _lmbColor;
    private Color _rmbColor;
    private GameObject go;
    private List<Color> _boxesColors = new List<Color>();
    private int sideSize = 13;
    private void OnEnable()
    {
        //Texture _texture = new Texture2D(100, 100);
        Debug.Log("MyEditorWindow OnEnable");
        for (int i = 0; i < sideSize * sideSize; i++)
        {
            _boxesColors.Add(GUI.color);
        }
    }

    [MenuItem("Window/My Editor Window")]
    private static void OpenMyEditorWindow()
    {
        GetWindow<MyEditorWindow>();
    }

    void OnGUI()
    {
        Event e = Event.current;
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        _lmbColor = EditorGUILayout.ColorField(_lmbColor);
        _rmbColor = EditorGUILayout.ColorField(_rmbColor);
        if (GUILayout.Button("Fill all pixel with first color")) FillAllPixels();

        go = (GameObject) EditorGUILayout.ObjectField("Object to fill", go,
            typeof(GameObject));
        if (GUILayout.Button("Apply")) Apply();
        GUILayout.EndVertical();

        for (int i = 0; i < sideSize; i++)
        {
            GUILayout.BeginVertical();
            for (int j = 0; j < sideSize; j++)
            {
                GUI.color = _boxesColors[sideSize * i + j];
                GUILayout.Box(GUIContent.none,GUILayout.Width(50), GUILayout.Height(50));

                if (e.button == 0 && e.isMouse && GUILayoutUtility.GetLastRect().Contains(e.mousePosition) && e.type == EventType.MouseDown)
                {
                    Event.current.Use();
                    _boxesColors[sideSize * i + j] = _lmbColor;
                    Debug.Log($"Left Click on {sideSize * i + j}");
                }
                else if (e.button == 1 && e.isMouse && GUILayoutUtility.GetLastRect().Contains(e.mousePosition) && e.type == EventType.MouseDown)
                {
                    Event.current.Use();
                    _boxesColors[sideSize * i + j] = _rmbColor;
                    Debug.Log($"Right Click on {sideSize * i + j}");
                }
            }
            GUILayout.EndVertical();
        }
        
        GUILayout.EndHorizontal();
    }

    void Apply()
    {
        Debug.Log("Applied");
        Texture2D texture = new Texture2D(sideSize, sideSize);
        go.GetComponent<Renderer>().material.mainTexture = texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = _boxesColors[(sideSize - 1 - y) + (sideSize * x)];
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
    }
    void FillAllPixels()
    {
        Debug.Log("All pixels filled");
        for (int i = 0; i < _boxesColors.Count; i++)
        {
            _boxesColors[i] = _lmbColor;
        }
    }
}
