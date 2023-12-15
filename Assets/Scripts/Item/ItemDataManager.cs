using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    private static ItemDataManager instance;
    public List<Item> resourceList;
    public List<Item> foodList;
    public List<Item> medicinesList;
    public List<EquipmentItem> equipmentItemItemList;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        resourceList = ReadCSVFile("Assets/CSV/ResourceItem.csv");
        foodList = ReadCSVFile("Assets/CSV/FoodItem.csv");
        medicinesList = ReadCSVFile("Assets/CSV/RecoveryItem.csv");
        equipmentItemItemList = EReadCSVFile("Assets/CSV/EquipmentItem.csv");

    }

    List<Item> ReadCSVFile(string path)
    {
        List<Item> result = new List<Item>();

        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    Item newItem = new Item
                    {
                        index = int.Parse(values[0]),
                        name = values[1],
                        text = values[2],
                        count = int.Parse(values[3]),
                        point = int.Parse(values[4]),
                        itemType = (ItemType)Enum.Parse(typeof(ItemType), values[5])
                    };

                    result.Add(newItem);
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Error reading the CSV file: " + e.Message);
        }

        return result;
    }
    List<EquipmentItem> EReadCSVFile(string path)
    {
        List<EquipmentItem> result = new List<EquipmentItem>();

        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    EquipmentItem newItem = new EquipmentItem
                    {
                        index = int.Parse(values[0]),
                        name = values[1],
                        text = values[2],
                        count = int.Parse(values[3]),
                        point = int.Parse(values[4]),
                        itemType = (ItemType)Enum.Parse(typeof(ItemType), values[5]),
                        durability = float.Parse(values[6]),
                        equipmentType = (EquipmentType)Enum.Parse(typeof(EquipmentType), values[7])
                    };

                    result.Add(newItem);
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Error reading the CSV file: " + e.Message);
        }

        return result;
    }
}
