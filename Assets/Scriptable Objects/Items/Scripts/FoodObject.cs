using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]

public class FoodObject : ItemObject
{
    // Start is called before the first frame update
    public void Awake()
    {
        type = ItemType.Food;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
