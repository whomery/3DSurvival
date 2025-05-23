using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData ItemData;
    public Action addItem;

    public Transform dropPosition;



    private void Awake()
    {
        ChharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();

}
}
