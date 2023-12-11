using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{

    [Header("Location when running in game")]
    public Vector2 initialValue;

    [Header("Location by default when starting")]
    public Vector2 defaultValue;

    public void OnAfterDeserialize() { initialValue = defaultValue; }
    public void OnBeforeSerialize() { }

}
