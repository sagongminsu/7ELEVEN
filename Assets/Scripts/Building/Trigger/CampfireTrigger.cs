using UnityEngine;

public class CampfireTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")//�浹ü�� �÷��̾��� ���
        {
            PlayerController.instance.isInCookArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")//�浹ü�� �÷��̾��� ���
        {
            PlayerController.instance.isInCookArea = false;
        }
    }
}
