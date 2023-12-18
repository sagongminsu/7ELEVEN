using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    // PlayerConditions 관련 스크립트에 IDamagable 이라는 인터페이스가 존재한다는 가정하에 작성함.
    //private List<IDamagable> thingsToDamage = new List<IDamagable> (); 

    //private void Start()
    //{
    //    InvokeRepeating("DealDamage", 0, damageRate); // 지연실행 방법. -- 바로 실행하는데 damageRate 주기대로 실행하겠다는 의미.
    //}

    //private void DealDamage()
    //{
    //    for (int i = 0; i < thingsToDamage.Count; i++)
    //    {
    //        thingsToDamage[i].TakePhysicalDamage(damage);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out IDamagable damagable))
    //    {
    //        thingsToDamage.Add(damagable);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out IDamagable damagable))
    //    {
    //        thingsToDamage.Add(damagable);
    //    }
    //}

}
