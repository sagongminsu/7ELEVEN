using UnityEngine;

public class CampfireTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")//충돌체가 플레이어일 경우
        {
            PlayerController.instance.isInCookArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")//충돌체가 플레이어일 경우
        {
            PlayerController.instance.isInCookArea = false;
        }
    }
}
