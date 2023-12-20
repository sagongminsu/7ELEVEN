using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureButton : MonoBehaviour//건설 UI 안, 스크롤 뷰에 있는 버튼입니다.
{
    public StructureData m_Data;//건물 정보(ScriptableObject)

    public void Start()
    {
        gameObject.GetComponent<Image>().sprite = m_Data.m_Icon;//건물 아이콘으로 이미지 변경
    }

    public void OnClick()//버튼을 눌렀을 때, BuildingManager에 건축물 정보 전달
    {
        BuildingManager.instance.SelectBuilding(ref m_Data);
    }
}
