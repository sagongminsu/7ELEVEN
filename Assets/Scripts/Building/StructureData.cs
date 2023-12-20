using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStructureData", menuName = "MakeStructureData/StructureData/Default", order = 0)]

public class StructureData : ScriptableObject
{
    public string m_StructureName;//건물 이름
    public string m_Desc;//건물 설명
    public Sprite m_Icon;//건물 아이콘 이미지
    public GameObject m_Prefab;//건축물 프리펩
    public GameObject m_Prefab_B;//청사진 프리펩

    public StructureRecipe[] items;//건설에 필요한 아이템 요구 개수 배열 (테스트용)
}
