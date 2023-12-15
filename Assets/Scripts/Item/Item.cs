using System;
using UnityEngine;

public enum ItemType
    {
        Food,
        Resource,
        Medicines,
        Equipment
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

    }