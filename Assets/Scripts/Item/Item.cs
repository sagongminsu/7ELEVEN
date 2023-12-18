using System;
using UnityEngine;

public enum ItemType
    {
        Food,
        Resource,
        Medicines,
        Equipment
    }

    public enum ConsumableType
    {
        Hunger,
        Health
    }

    [System.Serializable]
public class ItemConsumable
    {
        public ConsumableType type;
        public float value;
    }

    [Serializable]
    public class Item
    {
        [Header("info")]
        public int index;
        public string name;
        public string text;
        public int count;
        public int point;
        public ItemType itemType;
        public Sprite itemIcon;
        public GameObject dropObject;

        [Header("Stacking")]
        public bool canStack;
        public int maxStackAmount;

        [Header("ItemConsumable")]
        public ItemConsumable[] consumables;
    }