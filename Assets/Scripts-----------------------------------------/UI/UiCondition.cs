using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UiCondition : MonoBehaviour
{

        public Condition health;
        public Condition hunger;
        public Condition stamina;
        public Condition AddSpeed;
    




    // Start is called before the first frame update
    void Start()
    {
        ChharacterManager.Instance.Player.condition.uiCondition = this;
    }


}
