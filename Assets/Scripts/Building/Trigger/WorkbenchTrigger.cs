using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")//�浹ü�� �÷��̾��� ���
        {
            PlayerController.instance.isInBlacksmithArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.gameObject.tag == "Player")//�浹ü�� �÷��̾��� ���
        {
            PlayerController.instance.isInBlacksmithArea = false;
        }
    }
}
