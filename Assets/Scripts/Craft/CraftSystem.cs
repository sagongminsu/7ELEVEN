using System.Collections;
using TMPro;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    public Recipe recipe;
    Item item1;
    Item item2;
    Item item3;
    Item resultItem;

    public TextMeshProUGUI craftTxt;

    public GameObject craftTxtBox;

    private void Start()
    {
        item1 = recipe.FindItemByIndex(recipe.need1);
        item2 = recipe.FindItemByIndex(recipe.need2);
        item3 = recipe.FindItemByIndex(recipe.need3);
        resultItem = recipe.FindItemByIndex(recipe.resultItem);
        recipe = GetComponent<RecipeData>().recipe;
    }

    public void Crafting()
    {
        if (HasItemCheck())
        {
            for (int i = 0; i < recipe.resultCount; i++)
            {
                Inventory.instance.AddItem(resultItem);
            }
            craftTxt.text = $"Completed {resultItem.name} Acount {recipe.resultCount}";
            StartCoroutine(FadeOutCraftTxt());
            Debug.Log("성공");
        }
        else
        {
            craftTxt.text = "There are not enough items in the inventory";
            StartCoroutine(FadeOutCraftTxt());
            Debug.Log("자원없음");
        }
    }

    private bool HasItemCheck()
    {
        if (recipe.need1 != 0 && recipe.need2 != 0 && recipe.need3 != 0)
        {
            return Inventory.instance.HasItems(item1, recipe.count1)
                   && Inventory.instance.HasItems(item2, recipe.count2)
                   && Inventory.instance.HasItems(item3, recipe.count3);
        }
        else if (recipe.need1 != 0 && recipe.need2 != 0 && recipe.need3 == 0)
        {
            return Inventory.instance.HasItems(item1, recipe.count1)
                   && Inventory.instance.HasItems(item2, recipe.count2);
        }
        else
        {
            return Inventory.instance.HasItems(item1, recipe.count1);
        }
    }



    IEnumerator FadeOutCraftTxt()
    {
        craftTxtBox.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        craftTxtBox.SetActive(false);
        craftTxt.text = string.Empty;
    }

}
