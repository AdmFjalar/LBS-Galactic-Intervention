using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public int stage = 1;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Stage", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Stage") == stage)
        {
            Destroy(gameObject);
        }
    }
}
