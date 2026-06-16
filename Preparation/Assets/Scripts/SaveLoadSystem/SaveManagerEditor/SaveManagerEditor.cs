using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveSystem))]
public class SaveManagerEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        SaveSystem saveSystem = SaveSystem.instance;
        

        DrawDefaultInspector();


        if (GUILayout.Button(text: "New Game"))
        {
            saveSystem.NewGame(1);
        }


        if (GUILayout.Button(text: "Save Game"))
        {
            saveSystem.SaveGameJSON();
        }


        if (GUILayout.Button(text: "Load Game"))
        {
            saveSystem.LoadGameJSON();
        }


        if (GUILayout.Button(text: "Reload Game"))
        {
            //saveSystem.ReloadGame();
        }


        if (GUILayout.Button(text: "Delete Game"))
        {
            saveSystem.DeleteSave(saveSystem.saveSlot);
        }
    }
}
