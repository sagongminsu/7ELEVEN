using System;

public enum EquipmentType
{
    Sword,
    Axe,
    Pick,
    Helmet,
    Armor,
    Boots,
    Gloves
}

[Serializable]
public class EquipmentItem : Item
{
    public float durability;
    public EquipmentType equipmentType;
    public bool isEquipment;
}
