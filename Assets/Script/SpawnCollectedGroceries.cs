using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectedGroceries : MonoBehaviour
{
    public GameObject spawnPoint;
    private void SpawnGroceries()
    {
        foreach(var item in ListTracker.instance.objectiveList)
        {
            for (int i = 0; i < item.quantity; i++)
            {
                GameObject Grocery = Instantiate(item.groceryItem, spawnPoint.transform.position, Quaternion.identity);
            }
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnGroceries();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
