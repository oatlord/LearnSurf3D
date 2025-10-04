using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject hpPointsArray;

    private Rigidbody playerRb;

    public float nitroForce = 10f;
    public int playerLives = 3;

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
        PlayerLoseLives();
        player.SendMessage("PlayerRespawnToCheckpoint");
    }

    public void PlayerLoseLives()
    {
        playerLives -= 1;
        Debug.Log("Player Lives: " + playerLives);
        if (playerLives < 0)
        {
            Debug.Log("Game Over");
        }
        else
        {
            // Deactivate one of the HP points in the UI
            if (hpPointsArray != null && hpPointsArray.transform.childCount >= playerLives + 1)
            {
                hpPointsArray.transform.GetChild(playerLives).gameObject.SetActive(false);
            }
        }
    }
}
