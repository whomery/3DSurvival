using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    List<IDamagaIbe> things = new List<IDamagaIbe>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DealDamage",0,damageRate);
    }

    // Update is called once per frame
void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhySicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagaIbe damagaIbe))
        {
            things.Add(damagaIbe);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IDamagaIbe damagaIbe))
        {
            things.Remove(damagaIbe);
        }
    }
}
