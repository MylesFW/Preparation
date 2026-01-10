using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemSet : MonoBehaviour
{

    public ObjectContext playerContext;

    public Item iOne;
    public Item iTwo;
    public Item iThree;
    public Item iFour;
    public Item iFive;
    public Item iSix;
    public Item iSeven;

    private void Start()
    {
        iOne = new PotatoFoodItem(playerContext);
    }




}
