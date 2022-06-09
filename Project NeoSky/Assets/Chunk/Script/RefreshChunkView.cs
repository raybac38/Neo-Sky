using System;
using System.Collections.Generic;
using UnityEngine;

public class RefreshChunkView : MonoBehaviour
{
    public GameObject sphere;
    [Flags] public enum loadProsesse { NormalLoad, HightSpeedLoad};
    List<Transform> chunkLoad = new List<Transform>();
    List<Vector2Int> chunkLoadListe = new List<Vector2Int>();
    public loadProsesse actualLoadProsesse;

    public List<ChunkShow> chunks = new List<ChunkShow>();
    
    public Vector2Int actualChunk;
    public int maxLoadChunkDistance = 5;
    private int offSetChunk = 3;
    private void Start()
    {
        actualLoadProsesse = loadProsesse.NormalLoad;
        actualChunk = CalculeChunk();
        FirtsChunkLoad();
        
    }
    private void Update()
    {
        UpdateChunkLoadListe();
        if (actualChunk != CalculeChunk())
        {
            if(Vector2.Distance(actualChunk, CalculeChunk()) > 3)
            {
                for (int i = 0; i < chunkLoad.Count; i++)
                {
                    FirtsChunkLoad();
                }
            }
            else
            {
                actualChunk = CalculeChunk();

                ChunkLoadMove();
            }
            actualChunk = CalculeChunk();

        }
    }
    private void UpdateChunkLoadListe()
    {
        for (int i = 0; i < chunkLoadListe.Count; i++)
        {
            ///a avancer
            ///faire en sorte que les chunks se charge de maniere calm (ici, enlever les chunk trop loins qui n'ont pas eu le temps d'etre charger 
            ///on a la render distance qui dit a partir de ou le chunk sera charger, et l'offset qui fait un surcie pour les chunks deja charger.)
            ///chunk load liste contient tout les chunks non charger
            ///et chunkLaodListe contient tout les chunks charger (remplacer l'actuelle chunk liste )
            ///
        }
    }
    private Vector2Int CalculeChunk()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.z);
        Vector2Int chunkPosition = new Vector2Int(Mathf.RoundToInt((position.x) / 16), Mathf.RoundToInt(position.y / 16));
        return chunkPosition;
    }

    private void ChunkLoadMove()
    {
        int xMin = actualChunk.x - maxLoadChunkDistance;
        int xMax = actualChunk.x + maxLoadChunkDistance;
        int yMin = actualChunk.y - maxLoadChunkDistance;
        int yMax = actualChunk.y + maxLoadChunkDistance;


        for (int i = 0; i < chunkLoad.Count; i++)
        {
            Vector2Int chunk = new Vector2Int(Mathf.RoundToInt(chunkLoad[i].transform.position.x / 16), Mathf.RoundToInt(chunkLoad[i].transform.position.z / 16));
            if (chunk.x > xMax)
            {
                chunkLoad[i].transform.position += 16 * (1 + 2 * maxLoadChunkDistance) * Vector3Int.left;
                chunkLoad[i].GetComponent<ChunkShow>().Refresh();

            }
            else if (chunk.x < xMin)
            {
                chunkLoad[i].transform.position += 16 * (1 + 2 * maxLoadChunkDistance) * Vector3Int.right;
                chunkLoad[i].GetComponent<ChunkShow>().Refresh();

            }
            else if (chunk.y > yMax)
            {
                chunkLoad[i].transform.position += 16 * (1 + 2 * maxLoadChunkDistance) * Vector3Int.back;
                chunkLoad[i].GetComponent<ChunkShow>().Refresh();
            }
            else if (chunk.y < yMin)
            {
                chunkLoad[i].transform.position += 16 * (1 + 2 * maxLoadChunkDistance) * Vector3Int.forward;
                chunkLoad[i].GetComponent<ChunkShow>().Refresh();

            }
        }
    }
    private void FirtsChunkLoad()
    {
        for (int i = 0; i < chunkLoad.Count; i++)
        {
            Destroy(chunkLoad[i].gameObject);

        }
        chunkLoad = new List<Transform>();

        Vector2Int bornInf;
        bornInf = actualChunk - (Vector2Int.one * maxLoadChunkDistance);
        for (int x = 0; x < maxLoadChunkDistance * 2 + 1; x++)
        {
            for (int y = 0; y < maxLoadChunkDistance * 2 + 1; y++)
            {
                GameObject gameObject = Instantiate(sphere);
                gameObject.transform.position = new Vector3(bornInf.x * 16 + (x * 16), 0, bornInf.y * 16 + (y * 16));
                chunkLoad.Add(gameObject.transform);
            }
        }

    }


}
