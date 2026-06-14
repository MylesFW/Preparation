using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneLibrary))]
public class SceneLibraryEditer : UnityEditor.Editor
{
    SceneLibrary sceneLibrary;
    int indexedScene = 0;
    string sceneName = "null scene";
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        sceneLibrary = SceneLibrary.instance;
        sceneName = sceneLibrary.sceneNames[indexedScene];
        GUILayout.Label("Scene Select: " + sceneName, GUILayout.Width(200));

        if (GUILayout.Button(text: "Load Scene"))
        {
            sceneLibrary.sceneService.GoToScene(sceneName);
        }
        if (GUILayout.Button(text: "Jump to Scene"))
        {
            sceneLibrary.sceneService.JumpToScene(sceneName);
        }    
        if (GUILayout.Button(text: "Next Scene"))
        {
            incementIndex(1);
        }
        if (GUILayout.Button(text: "Previous Scene"))
        {
            incementIndex(1);
        }
    }
    public void UpdateSceneName(int sceneIndex)
    {
        sceneName = sceneLibrary.sceneNames[indexedScene];
    }
    public void incementIndex(int amount)
    {
        indexedScene += amount;
        if (indexedScene < 0)
        {
            indexedScene = sceneLibrary.sceneNames.Count - 1;
        }
        else if (indexedScene >= sceneLibrary.sceneNames.Count)
        {
            indexedScene = 0;
        }
        UpdateSceneName(indexedScene);
    }
}
