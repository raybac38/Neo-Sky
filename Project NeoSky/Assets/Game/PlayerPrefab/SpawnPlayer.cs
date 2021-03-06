using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private RefreshChunkView refreshChunk;
    private SpawnTower spawn;
    private GameObject player;

    private void Start()
    {
        refreshChunk = GetComponentInChildren<RefreshChunkView>();
        refreshChunk.load = false;
        spawn = GetComponentInChildren<SpawnTower>();
        player = GetComponentInChildren<PlayerMovement>().gameObject;
        StartCoroutine(SpawnZone());
    }
    IEnumerator SpawnZone()
    {
        yield return new WaitForSeconds(1f);
        spawn.RequestTeleportNearestTower(player);

    }
}
