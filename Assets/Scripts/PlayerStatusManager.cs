using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Rigidbody playerRb;
    public float nitroForce = 10f;
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -10f)
        {
            PlayerFall();
        }

    }

    public void NitroBoost()
    {
        Debug.Log("Nitro Boost Activated");
        playerRb.AddForce(player.transform.forward * nitroForce, ForceMode.Impulse);
    }

    public void PlayerFall()
    {
        Debug.Log("Player fell off of map");
    }
}
