using System;

public enum RecipeType
{
    Normal,
    Cook
}

[Serializable]
public class Recipe
{
    public int index;
    public int need1;
    public int count1;
    public int need2;
    public int count2;
    public int need3;
    public int count3;
    public RecipeType recipeType;
    public int resultItem;
    public int resultCount;

    public Item FindItemByIndex(int itemIndex)
    {
        foreach (Item item in ItemDataManager.Instance.resourceList)
        {
            if (item.index == itemIndex)
            {
                return item;
            }
        }
        foreach (Item item in ItemDataManager.Instance.medicinesList)
        {
            if (item.index == itemIndex)
            {
                return item;
            }
        }
        foreach (Item item in ItemDataManager.Instance.foodList)
        {
            if (item.index == itemIndex)
            {
                return item;
            }
        }
        foreach (Item item in ItemDataManager.Instance.equipmentItemItemList)
        {
            if (item.index == itemIndex)
            {
                return item;
            }
        }
        return null;
    }
}