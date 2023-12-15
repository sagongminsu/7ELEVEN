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
public class EquipmentItem : Item
{
    public float durability;
    public EquipmentType equipmentType;
}
