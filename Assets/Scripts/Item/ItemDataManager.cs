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
    public List<Recipe> recipes;
    

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
        resourceList = ReadCSVFile<Item>("Assets/CSV/ResourceItem.csv");
        foodList = ReadCSVFile<Item>("Assets/CSV/FoodItem.csv");
        medicinesList = ReadCSVFile<Item>("Assets/CSV/RecoveryItem.csv");
        equipmentItemItemList = ReadCSVFile<EquipmentItem>("Assets/CSV/EquipmentItem.csv");
        recipes = ReadCSVFile<Recipe>("Assets/CSV/RecipeData.csv");
    }

    List<T> ReadCSVFile<T>(string path)
    {
        List<T> result = new List<T>();

        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                reader.ReadLine(); // Skip header

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    if (typeof(T) == typeof(Item) && values.Length == 6)
                    {
                        Item newData = new Item
                        {
                            index = int.Parse(values[0]),
                            name = values[1],
                            text = values[2],
                            count = int.Parse(values[3]),
                            point = int.Parse(values[4]),
                            itemType = (ItemType)Enum.Parse(typeof(ItemType), values[5]),
                            itemIcon = Resources.Load<Sprite>("ItemIcon/Item/" + values[0]),
                        };
                        result.Add((T)(object)newData);
                    }
                    else if (typeof(T) == typeof(EquipmentItem) && values.Length == 8)
                    {
                        EquipmentItem newData = new EquipmentItem
                        {
                            index = int.Parse(values[0]),
                            name = values[1],
                            text = values[2],
                            count = int.Parse(values[3]),
                            point = int.Parse(values[4]),
                            itemType = (ItemType)Enum.Parse(typeof(ItemType), values[5]),
                            durability = float.Parse(values[6]),
                            equipmentType = (EquipmentType)Enum.Parse(typeof(EquipmentType), values[7]),
                            itemIcon = Resources.Load<Sprite>("ItemIcon/Equipment/" + values[0])
                        };
                        result.Add((T)(object)newData);
                    }
                    else if (typeof(T) == typeof(Recipe) && values.Length >= 10)
                    {
                        Recipe newData = new Recipe
                        {
                            index = int.Parse(values[0]),
                            need1 = int.Parse(values[1]),
                            count1 = int.Parse(values[2]),
                            need2 = int.Parse(values[3]),
                            count2 = int.Parse(values[4]),
                            need3 = int.Parse(values[5]),
                            count3 = int.Parse(values[6]),
                            recipeType = (RecipeType)Enum.Parse(typeof(RecipeType), values[7]),
                            resultItem = int.Parse(values[8]),
                            resultCount = int.Parse(values[9])
                        };
                        result.Add((T)(object)newData);
                    }
                    else
                    {
                        Debug.LogError($"Error reading CSV file '{path}': Incorrect number of columns or invalid type");
                    }
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error reading the CSV file '{path}': {e.Message}");
        }

        return result;
    }

}
