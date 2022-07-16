using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RefreshChunkView))]
public class SpawnTower : MonoBehaviour
{
    //script servant uniquement a faire apparaitre les tours de spawn
    public GameObject towerSpawnPrefab;
    private RefreshChunkView refreshChunkView;
    private ChunkShow chunkShow;
    private int scaleAffectionZone;
    public int regionScale = 5;


    public GameObject tower;
    private void Start()
    {
        refreshChunkView = GetComponent<RefreshChunkView>();
        chunkShow = refreshChunkView.chunk.GetComponent<ChunkShow>();
        scaleAffectionZone = chunkShow.AffectiongScaleIsland;
        tower = Instantiate(towerSpawnPrefab);
        tower.transform.position += Vector3.up * 300;
        refreshChunkView.load = true;
        InvokeRepeating("RefreshTowerPosition", 0.8f, 1f);
        
    }

    private void RefreshTowerPosition()
    {
        Vector2Int myAffectionZone = new Vector2Int(Mathf.RoundToInt(transform.position.x / (regionScale * 16 * scaleAffectionZone)),Mathf.RoundToInt( transform.position.z / (regionScale * 16 * scaleAffectionZone)));
        float hauteur = 300;
        RaycastHit raycastHit;
        if(Physics.Raycast(new Vector3(myAffectionZone.x, 300, myAffectionZone.y), Vector3.down, out raycastHit))
        {
            if(raycastHit.point.y + 25 < tower.transform.position.y)
            {
                hauteur = raycastHit.point.y + 25;
                tower.transform.position = new Vector3(myAffectionZone.x * 16 * scaleAffectionZone * regionScale, hauteur, myAffectionZone.y * 16 * scaleAffectionZone * regionScale);
            }
        }
        else
        {
            tower.transform.position = new Vector3(myAffectionZone.x * 16 * scaleAffectionZone * regionScale, 400, myAffectionZone.y * 16 * scaleAffectionZone * regionScale);
        }
    }

    public void RequestTeleportNearestTower(GameObject player)
    {
        player.transform.position = tower.transform.position + new Vector3(0.8f, 10f, 2.1f);
        refreshChunkView.rapiditerChargement = 1;
    }

    
}
