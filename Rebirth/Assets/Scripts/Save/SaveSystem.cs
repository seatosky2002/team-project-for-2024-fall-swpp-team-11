using System.IO;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SaveSystem
{
    private static string InventorySavePath => Application.persistentDataPath + "/inventory.json";
   
   #region Inventory
    public static void SaveInventoryData(InventoryData inventoryData)
    {
        List<string> itemNames = new List<string>();
        foreach (var item in inventoryData.Items)
        {
            itemNames.Add(item.itemName);
        }

        string json = JsonConvert.SerializeObject(itemNames, Formatting.Indented);
        Debug.Log("Items in inventory:" + json);

        File.WriteAllText(InventorySavePath, json);
        Debug.Log("인벤토리 아이템들이 파일로 저장되었습니다.");
    }
    
    public static InventoryData LoadInventoryData()
    {
        if (!File.Exists(InventorySavePath))
        {
            Debug.Log("Inventory JSON 파일이 존재하지 않습니다.");
            return null;
        }

        string json = File.ReadAllText(InventorySavePath);
        List<string> itemNames = JsonConvert.DeserializeObject<List<string>>(json);


        ItemData[] allItems = Resources.LoadAll<ItemData>("ScriptableItem");
        List<ItemData> loadedItems = new List<ItemData>();
        foreach (var itemName in itemNames)
        {
            ItemData foundItem = System.Array.Find(allItems, item => item.itemName == itemName);
            if (foundItem != null)
            {
                loadedItems.Add(foundItem);
            }
            else
            {
                Debug.LogWarning($"아이템 '{itemName}'을(를) 찾을 수 없습니다.");
            }
        }
        InventoryData inventoryData = new InventoryData(loadedItems);

        return inventoryData;
    }
    #endregion

}