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

    public TextMeshProUGUI CraftTxt;

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
            CraftTxt.text = $"Completed {resultItem.name} Acount {recipe.resultCount}";
            FadeOutCraftTxt();
        }
        else
        {
            CraftTxt.text = "There are not enough items in the inventory";
            FadeOutCraftTxt();
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



    private IEnumerator FadeOutCraftTxt()
    {
        float duration = 4f;
        float timer = 0f;

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);
            CraftTxt.color = new Color(CraftTxt.color.r, CraftTxt.color.g, CraftTxt.color.b, alpha);

            timer += Time.deltaTime;
            yield return null;
        }
        CraftTxt.text = string.Empty;
    }
}
