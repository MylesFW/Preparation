using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavable
{
    GameData SaveInstance(GameData data);
    void LoadInstance(GameData data);
    void NewGame();
}