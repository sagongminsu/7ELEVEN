using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructButton : MonoBehaviour//�Ǽ��ϱ� ��ư
{

    public void OnClick()
    {
        BuildingManager.instance.ConstructionMode();//�Ǽ� ��ư�� ������ ��� �Ǽ���� ON
    }
}
