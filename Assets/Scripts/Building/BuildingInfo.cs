using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultBuildingInfo", menuName = "TopDownController/BuildingInfo/Default", order = 0)]

public class BuildingInfo : ScriptableObject
{
    public string m_BuildingName;
    public string m_Desc;
    public Sprite m_Icon;
    public GameObject m_Prefab;
}
