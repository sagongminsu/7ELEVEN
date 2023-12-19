using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Resource : MonoBehaviour
{
    public Item itemToGive;
    public int quantityPerHit = 1;
    public int capacity;

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacity <= 0) { break; }

            capacity -= 1;
            Instantiate(itemToGive.dropObject, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up)); // 충돌이 일어난 곳에서 나를 바라보는 방향으로 생성한다는 의미
        }

        if (capacity <= 0)
        {
            Destroy(gameObject);
        }
    }
}
