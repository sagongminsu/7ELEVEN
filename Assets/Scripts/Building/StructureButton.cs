using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureButton : MonoBehaviour
{
    public StructureData m_Data;//�ǹ� ����(ScriptableObject)

    public void Start()
    {
        gameObject.GetComponent<Image>().sprite = m_Data.m_Icon;//�ǹ� ���������� �̹��� ����
    }

    public void OnClick()//��ư�� ������ ��, BuildingManager�� ���๰ ���� ����
    {
        BuildingManager.instance.SelectBuilding(ref m_Data);
    }
}
