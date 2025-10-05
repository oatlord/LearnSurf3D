using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerStatusManager playerStatusManager;
    public Timer gameTimer;

    void Start()
    {
        // playerStatusManager = FindObjectOfType<PlayerStatusManager>();
        // gameTimer = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStatusManager.playerLives == 0 || gameTimer.remainingTime <= 0)
        {
            Debug.Log("Game Over");
            // Implement game over logic here (e.g., stop the game, show game over screen, etc.)
        }
    }
}
