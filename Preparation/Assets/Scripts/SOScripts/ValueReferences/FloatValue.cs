using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheriting from ScriptableObject, it can be attached above the scene. Meaning the object will not reset 

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;

    //serialization is a restart for the whole program (this resets the scripts held in the memory ScriptableObjects folder)

        // will reset all FloatValues back everytime play is pressed
    [HideInInspector]
    public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}
