using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform currentRespawn;
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
        if (other.CompareTag("Checkpoint"))
        {
            Debug.Log("Player reached a respawn point!");
            currentRespawn = other.transform;
        }
    }

    void PlayerRespawnToCheckpoint()
    {
        transform.position = currentRespawn.position;
    }
}
