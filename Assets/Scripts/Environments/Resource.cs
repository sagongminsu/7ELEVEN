using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Resource : MonoBehaviour
{
    public int quantityPerHit = 1;
    public int capacity;
    public int[] acount;
    public int size;
    public int getChance;
    public int[] itemSpwanChance;
    public int[] randomMin;
    public int[] randomMax;

    public List<GameObject> dropObj = new List<GameObject>();


    private void Start()
    {
        size = dropObj.Count;
        int randomInt = Random.Range(2, 5);    
        acount = new int[randomInt];
    }

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {

        if (capacity <= 0)
        {

            for (int j = 0; j < size; j++)
            {
                getChance = Random.Range(1, 100);
                acount[j] = Random.Range(randomMin[j], randomMax[j]);

                for (int i = 0; i < acount[j]; i++)
                {
                    
                    if (itemSpwanChance[j] >= getChance)
                    {
                        Instantiate(dropObj[j], hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up)); // 충돌이 일어난 곳에서 나를 바라보는 방향으로 생성한다는 의미
                    }                  
                }
            }

                       
            Destroy(gameObject);
        }
    }
}
