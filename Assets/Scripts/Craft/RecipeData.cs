using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RecipeData : MonoBehaviour
{
    public int recipeNum;
    public Recipe recipe;

    public GameObject result;
    public GameObject need1Img;
    public GameObject need2Img;
    public GameObject need3Img;

    private Image imageComponentResult;
    private Image imageComponent1;
    private Image imageComponent2;
    private Image imageComponent3;

    public TextMeshProUGUI count1;
    public TextMeshProUGUI count2;
    public TextMeshProUGUI count3;
    public TextMeshProUGUI countResult;

    private void Awake()
    {
        imageComponentResult = GetComponent<Image>();
        imageComponent1 = need1Img.GetComponent<Image>();
        imageComponent2 = need2Img.GetComponent<Image>();
        imageComponent3 = need3Img.GetComponent<Image>();
        recipe = FindRecipeByNumber(recipeNum);
    }

    private void Start()
    {
        SetNeedSprite(recipe.need1, recipe.need2, recipe.need3, recipe.resultItem);
        count1.text = recipe.count1.ToString();
        count2.text = recipe.count2.ToString();
        count3.text = recipe.count3.ToString();
        countResult.text = recipe.resultCount.ToString();
    }

    private Recipe FindRecipeByNumber(int recipeNumber)
    {
        foreach (Recipe recipe in ItemDataManager.Instance.recipes)
        {
            if (recipe.index == recipeNumber)
            {
                return recipe;
            }
        }
        return null;
    }

    private void SetNeedSprite(int itemIndex1, int itemIndex2, int itemIndex3, int itemIndex4)
    {
        Item item1 = recipe.FindItemByIndex(itemIndex1);
        Item item2 = recipe.FindItemByIndex(itemIndex2);
        Item item3 = recipe.FindItemByIndex(itemIndex3);
        Item item4 = recipe.FindItemByIndex(itemIndex4);

        if (item1 != null)
        {
            imageComponent1.sprite = item1.itemIcon;
            count1.GetComponent<CanvasRenderer>().SetAlpha(1f);
        }
        else
        {
            imageComponent1.sprite = Resources.Load<Sprite>("Icon/X");
            count1.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }

        if (item2 != null)
        {
            imageComponent2.sprite = item2.itemIcon;
            count2.GetComponent<CanvasRenderer>().SetAlpha(1f);
        }
        else
        {
            imageComponent2.sprite = Resources.Load<Sprite>("Icon/X");
            count2.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }

        if (item3 != null)
        {
            imageComponent3.sprite = item3.itemIcon;
            count3.GetComponent<CanvasRenderer>().SetAlpha(1f);
        }
        else
        {
            imageComponent3.sprite = Resources.Load<Sprite>("Icon/X");
            count3.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }

        if (item4 != null)
        {
            imageComponentResult.sprite = item4.itemIcon;
            countResult.GetComponent<CanvasRenderer>().SetAlpha(1f);
        }
        else
        {
            imageComponentResult.sprite = Resources.Load<Sprite>("Icon/X");
            countResult.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
    }
}
