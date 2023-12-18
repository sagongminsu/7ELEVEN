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
        gameObject.GetComponent<Image>().sprite = BuildingInfo.m_Icon;//���๰ ���������� �̹��� ����
    }

    public void OnClick()//��ư�� ������ ��, BuildingManager�� ���๰ ���� ����
    {
        BuildingManager.instance.SelectBuilding(ref BuildingInfo);
    }
}
