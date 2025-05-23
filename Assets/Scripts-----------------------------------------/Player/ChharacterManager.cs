using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChharacterManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static ChharacterManager _instance;
    public static ChharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<ChharacterManager>();
            }

            return _instance;
        }

    }
    public Player _player;

    public Player Player
    {
        get { return _player; }

        set { _player = value; }
    }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);


        }
        else
        {
            if(Instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}