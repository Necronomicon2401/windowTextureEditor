using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MyMenues : MonoBehaviour
{
    [MenuItem("My Tools/Hello Menu %g")]
    public static void HelloMenu()
    {
        Debug.Log("HelloMenu");
    }
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
