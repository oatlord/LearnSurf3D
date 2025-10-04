using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroCollectableBehavior : MonoBehaviour
{
    public PlayerStatusManager playerStatusManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Nitro Collected");
            playerStatusManager.SendMessage("NitroBoost");
            Destroy(gameObject);
        }
    }
}
