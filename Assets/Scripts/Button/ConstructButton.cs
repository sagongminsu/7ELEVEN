using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructButton : MonoBehaviour//건설하기 버튼
{

    public void OnClick()
    {
        BuildingManager.instance.ConstructionMode();//건설 버튼을 눌렀을 경우 건설모드 ON
    }
}
