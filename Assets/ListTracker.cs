using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ListTracker : MonoBehaviour
{
    public static ListTracker instance;
    [System.Serializable]
    public class Grocery
    {
        public GameObject groceryItem;
        public string itemName;
        public int quantity;
        public int scannedAmount;
    }
    public UnityEvent UpdateQuestOverlay;
    public int maxQuantity = 3;
    public List<GameObject> groceries;
    public int numberOfPickedItems = 3;

    [HideInInspector]
    public List<Grocery> objectiveList = new List<Grocery>();


    public void GenerateObjectiveList()
    {
        Debug.Log("List Created");
        objectiveList.Clear();
        List<GameObject> tempGroceryList = new List<GameObject>(groceries);
        for(int i = 0; i<numberOfPickedItems; i++)
        {
            int randIndex = Random.Range(0, tempGroceryList.Count);
            int itemQuantity = Random.Range(1, maxQuantity);
            GameObject selected = tempGroceryList[randIndex];
            tempGroceryList.RemoveAt(randIndex);
            objectiveList.Add(new Grocery { groceryItem = selected, quantity = itemQuantity, scannedAmount = 0, itemName = selected.GetComponent<GroceryProperties>().itemName }) ;
        }

    }

    public void MarkScanned(GameObject scannedItem)
    {
        var instance = scannedItem.GetComponent<GroceryProperties>();
        string scannedId = instance.itemId;
        foreach(var item in objectiveList)
        {
            if(item.groceryItem.GetComponent<GroceryProperties>().itemId == scannedId && item.scannedAmount != item.quantity)
            {
                item.scannedAmount++;
                instance.isScanned = true;
                UpdateQuestOverlay.Invoke();
                return;
            }
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else instance = this;
        if (SceneManager.GetSceneByName("PickGroceries").name == "PickGroceries")
        {
            GenerateObjectiveList();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
