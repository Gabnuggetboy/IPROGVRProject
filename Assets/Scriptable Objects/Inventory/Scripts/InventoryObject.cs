using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();
    public ItemObject emptyItem; // Reference to the EmptyItem asset

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Database"); 
#endif
        // Initialize with 4 empty slots if Container is empty
        if (Container.Count == 0)
        {
            InitializeEmptySlots();
        }
    }

    private void InitializeEmptySlots()
    {
        Container.Clear();
        for (int i = 0; i < 4; i++)
        {
            Container.Add(new InventorySlot(database.GetId[emptyItem], emptyItem, 0));
        }
    }

    public void AddItem(ItemObject _item, int _amount)
    {
        // Check for empty slots (amount == 0) to replace
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == emptyItem && Container[i].amount == 0)
            {
                Container[i] = new InventorySlot(database.GetId[_item], _item, _amount);
                return;
            }
            else if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        // If no empty slots and item not found, optionally expand or ignore
        // For now, we'll assume only 4 slots are allowed
        Debug.LogWarning("Inventory is full!");
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
        else
        {
            // If no save file exists, initialize with empty slots
            InitializeEmptySlots();
        }
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item = database.GetItem[Container[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {
    }
}