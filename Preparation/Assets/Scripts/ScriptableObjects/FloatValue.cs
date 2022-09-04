using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheriting from ScriptableObject, it can be attached above the scene. Meaning the object will not reset 

[CreateAssetMenu]
public class FloatValue : ScriptableObject
{
    public float initialValue;
}
