using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")//충돌체가 플레이어일 경우
        {
            PlayerController.instance.isInBlacksmithArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.gameObject.tag == "Player")//충돌체가 플레이어일 경우
        {
            PlayerController.instance.isInBlacksmithArea = false;
        }
    }
}
