using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructButton : MonoBehaviour//건설하기 버튼
{

    public void OnClick()
    {
        bool result;

        result = BuildingManager.instance.RecipeCheck();//충분한 아이템을 갖고 있을 경우, 참

        if (result)
        {
            BuildingManager.instance.ConstructionMode();//건설 버튼을 눌렀을 경우 건설모드 ON
        }else
        {
            Debug.Log("재료 부족");
        }
    }
}
