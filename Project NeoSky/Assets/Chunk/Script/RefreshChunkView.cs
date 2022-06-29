using System;
using System.Collections.Generic;
using UnityEngine;

public class RefreshChunkView : MonoBehaviour
{
    public GameObject chunk;
    public GameObject chunkGameObjetStorage;
    [Flags] public enum loadProsesse { NormalLoad, HightSpeedLoad };
    ///List<Transform> chunkLoad = new List<Transform>();
    public loadProsesse actualLoadProsesse;

    public int rapiditerChargement = 0;
    ///
    public List<Vector2Int> chunkToLoad = new List<Vector2Int>();
    public List<ChunkShow> chunkLoad = new List<ChunkShow>();

    public Vector2Int actualChunk;
    public int renderDistance = 15;
    private int offSetChunk = 5;
    private void Start()
    {
        actualLoadProsesse = loadProsesse.NormalLoad;
        actualChunk = CalculeMyChunk();
        ///FirtsChunkLoad();
        if (renderDistance < 1)
        {
            renderDistance = 1;
        }
    }


    private void RefreshChunkListe()
    {
        //ajout des nouveau chunk a chager
        Vector2Int recurence = new Vector2Int(Mathf.RoundToInt(actualChunk.x), Mathf.RoundToInt(actualChunk.y));
        chunkToLoad.Clear();
        actualChunk = CalculeMyChunk();
        chunkToLoad.Add(actualChunk);
        for (int render = 1; render < renderDistance + 1; render++)
        {

            recurence -= Vector2Int.one;
            for (int i = 0; i < render * 2; i++)
            {
                chunkToLoad.Add(recurence);
                recurence += Vector2Int.up;
            }
            for (int i = 0; i < render * 2; i++)
            {
                chunkToLoad.Add(recurence);
                recurence += Vector2Int.right;
            }
            for (int i = 0; i < render * 2; i++)
            {
                chunkToLoad.Add(recurence);
                recurence += Vector2Int.down;
            }
            for (int i = 0; i < (render * 2); i++)
            {
                chunkToLoad.Add(recurence);
                recurence += Vector2Int.left;
            }
        }


        //enleve les doublons
        for (int i = chunkLoad.Count - 1; i != -1; i--)
        {
            if (chunkToLoad.Contains(chunkLoad[i].myChunk))
            {
                int ram = chunkToLoad.IndexOf(chunkLoad[i].myChunk);
                chunkToLoad.RemoveAt(ram);
            }
        }

        ///supression des chunks qui sont trop loins
        if(chunkLoad.Count != 0)
        {
            for (int i = chunkLoad.Count - 1; i != 0; i--)
            {
        
                if (chunkLoad[i].myChunk.x > actualChunk.x + renderDistance + offSetChunk)
                {
                    Destroy(chunkLoad[i].gameObject);
                    chunkLoad.RemoveAt(i);
                }
                if (chunkLoad[i].myChunk.x < actualChunk.x - renderDistance - offSetChunk)
                {
                    Destroy(chunkLoad[i].gameObject);
                    chunkLoad.RemoveAt(i);
                }
                if (chunkLoad[i].myChunk.y > actualChunk.y + renderDistance + offSetChunk)
                {
                    Destroy(chunkLoad[i].gameObject);
                    chunkLoad.RemoveAt(i);
                }
                if (chunkLoad[i].myChunk.y < actualChunk.y - renderDistance - offSetChunk)
                {
                    Destroy(chunkLoad[i].gameObject);
                    chunkLoad.RemoveAt(i);
                }
            }

        }

    }

    private void Update()
    {
        actualChunk = CalculeMyChunk();
        RefreshChunkListe();
        //prendre le premier chunk, et le charger
        for (int i = 0; i < rapiditerChargement; i++)
        {
            if (chunkToLoad.Count != 0)
            {
                GameObject obj = Instantiate(chunk);
                obj.transform.SetParent(chunkGameObjetStorage.transform);
                obj.transform.position = new Vector3(chunkToLoad[0].x * 16, 1, chunkToLoad[0].y * 16);
                chunkLoad.Add(obj.GetComponent<ChunkShow>());
                chunkLoad[chunkLoad.Count - 1].Refresh();
                chunkToLoad.RemoveAt(0);
            }
        }
        
    }

    private Vector2Int CalculeMyChunk()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.z);
        Vector2Int chunkPosition = new Vector2Int(Mathf.RoundToInt((position.x) / 16), Mathf.RoundToInt(position.y / 16));
        return chunkPosition;
    }



}
