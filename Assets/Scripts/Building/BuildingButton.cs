using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    public BuildingInfo BuildingInfo;


    public void Start()
    {
        gameObject.GetComponent<Image>().sprite = BuildingInfo.m_Icon;//건축물 아이콘으로 이미지 변경
    }

    public void OnClick()//버튼을 눌렀을 때, BuildingManager에 건축물 정보 전달
    {
        BuildingManager.instance.SelectBuilding(ref BuildingInfo);
    }
}
