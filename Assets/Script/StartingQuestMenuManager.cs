using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartingQuestMenuManager : MonoBehaviour
{
    [System.Serializable]
    public class QuestEntry
    {
        public string itemName;
        public Image itemIcon;
        public TMP_Text statusIcon;
        public TMP_Text itemNameText;
    }

    [Header("References")]
    public GameObject questOverlay;
    public Transform head;

    [Header("Overlay Settings")]
    public Vector3 overlayOffset = new Vector3(0, 0, 0.5f);

    [Header("Quests")]
    public List<QuestEntry> questEntries = new List<QuestEntry>();

    private Dictionary<string, bool> equippedItems = new Dictionary<string, bool>();

    void Start()
    {
        foreach (var quest in questEntries)
        {
            equippedItems[quest.itemName] = false;
            quest.statusIcon.text = "<color=red>✗</color>";
            quest.itemNameText.text = quest.itemName;
        }
    }

    void Update()
    {
        if (questOverlay.activeSelf)
        {

            questOverlay.transform.position = head.position + head.TransformDirection(overlayOffset);

            questOverlay.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            questOverlay.transform.forward *= -1;
        }
    }

    public void OnItemEquipped(EquippableItem item)
    {
        if (equippedItems.ContainsKey(item.itemName))
        {
            equippedItems[item.itemName] = true;
            UpdateQuestUI(item);
        }
    }

    private void UpdateQuestUI(EquippableItem item)
    {
        foreach (var quest in questEntries)
        {
            if (quest.itemName == item.itemName)
            {
                quest.itemIcon.sprite = item.inventoryIcon;
                quest.statusIcon.text = "<color=green>✓</color>";
            }
        }
    }
}
