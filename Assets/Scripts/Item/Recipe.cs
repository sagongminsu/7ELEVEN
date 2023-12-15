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
}