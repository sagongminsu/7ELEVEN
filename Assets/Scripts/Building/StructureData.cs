using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStructureData", menuName = "MakeStructureData/StructureData/Default", order = 0)]

public class StructureData : ScriptableObject
{
    public string m_StructureName;//�ǹ� �̸�
    public string m_Desc;//�ǹ� ����
    public Sprite m_Icon;//�ǹ� ������ �̹���
    public GameObject m_Prefab;//���๰ ������
    public GameObject m_Prefab_B;//û���� ������

    public StructureRecipe[] items;//�Ǽ��� �ʿ��� ������ �䱸 ���� �迭 (�׽�Ʈ��)
}
