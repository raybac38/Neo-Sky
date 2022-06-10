using System;
using System.Collections.Generic;
using UnityEngine;

public class RefreshChunkView : MonoBehaviour
{
    public GameObject sphere;
    [Flags] public enum loadProsesse { NormalLoad, HightSpeedLoad };
    ///List<Transform> chunkLoad = new List<Transform>();
    public loadProsesse actualLoadProsesse;

    ///
    public List<Vector2Int> chunkToLoad = new List<Vector2Int>();
    public List<ChunkShow> chunkLoad = new List<ChunkShow>();

    public Vector2Int actualChunk;
    public int renderDistance = 5;
    private int offSetChunk = 3;
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
        int x = 0;
        Vector2Int recurence = Vector2Int.zero;
        chunkToLoad.Clear();
        chunkToLoad.Add(actualChunk);
        for (int render = 0; render < renderDistance; render++)
        {
            x--;
            recurence = actualChunk - Vector2Int.one * x;
            chunkToLoad.Add(recurence);

            for (int i = 0; i < render * 2; i++)
            {
                recurence += Vector2Int.up;
                chunkToLoad.Add(recurence);
            }
            for (int i = 0; i < render * 2; i++)
            {
                recurence += Vector2Int.right;
                chunkToLoad.Add(recurence);
            }
            for (int i = 0; i < render * 2; i++)
            {
                recurence += Vector2Int.down;
                chunkToLoad.Add(recurence);
            }
            for (int i = 0; i < render * 2; i++)
            {
                recurence += Vector2Int.left;
                chunkToLoad.Add(recurence);
            }

        }

        int decalage = 0;
        //enleve les doublons
        for (int i = 0; i < chunkLoad.Count; i++)
        {
            if (chunkToLoad.Contains(chunkLoad[i - decalage].myChunk))
            {
                chunkToLoad.RemoveAt(i - decalage);
                decalage++;
            }
        }

        ///supression des chunks qui sont trop loins
        for (int i = 0; i < chunkLoad.Count; i++)
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

    private void Update()
    {
        actualChunk = CalculeMyChunk();
        RefreshChunkListe();
        //prendre le premier chunk, et le charger
        if(chunkToLoad.Count != 0)
        {
            GameObject obj = Instantiate(sphere);
            obj.transform.position = new Vector3(chunkToLoad[0].x * 16, 1, chunkToLoad[0].y * 16);
            chunkLoad.Add(obj.GetComponent<ChunkShow>());
            chunkLoad[chunkLoad.Count - 1].Refresh();
            chunkToLoad.RemoveAt(0);

        }


    }
    ///private void Update()
    ///{
    ///    UpdateChunkLoadListe();
    ///    if (actualChunk != CalculeMyChunk())
    ///    {
    ///        if(Vector2.Distance(actualChunk, CalculeMyChunk()) > 3)
    ///        {
    ///            for (int i = 0; i < chunkLoad.Count; i++)
    ///            {
    ///                FirtsChunkLoad();
    ///            }
    ///        }
    ///        else
    ///        {
    ///            actualChunk = CalculeMyChunk();
    ///
    ///            ChunkLoadMove();
    ///        }
    ///        actualChunk = CalculeMyChunk();
    ///
    ///    }
    ///}
    ///private void UpdateChunkLoadListe()
    ///{
    ///    for (int i = 0; i < chunkLoadListe.Count; i++)
    ///    {
    ///        ///a avancer
    ///        ///faire en sorte que les chunks se charge de maniere calme (ici, enlever les chunk trop loins qui n'ont pas eu le temps d'etre charger 
    ///        ///on a la render distance qui dit a partir de ou le chunk sera charger, et l'offset qui fait un surcie pour les chunks deja charger.)
    ///        ///chunk load liste contient tout les chunks non charger
    ///        ///et chunkLaodListe contient tout les chunks charger (remplacer l'actuelle chunk liste )
    ///        
    ///    }
    ///}
    ///
    private Vector2Int CalculeMyChunk()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.z);
        Vector2Int chunkPosition = new Vector2Int(Mathf.RoundToInt((position.x) / 16), Mathf.RoundToInt(position.y / 16));
        return chunkPosition;
    }
    ///
    ///private void ChunkLoadMove()
    ///{
    ///    int xMin = actualChunk.x - maxLoadChunkDistance;
    ///    int xMax = actualChunk.x + maxLoadChunkDistance;
    ///    int yMin = actualChunk.y - maxLoadChunkDistance;
    ///    int yMax = actualChunk.y + maxLoadChunkDistance;
    ///
    ///
    ///    for (int i = 0; i < chunkLoad.Count; i++)
    ///    {
    ///        Vector2Int chunk = new Vector2Int(Mathf.RoundToInt(chunkLoad[i].transform.position.x / 16), Mathf.RoundToInt(chunkLoad[i].transform.position.z / 16));
    ///        if (chunk.x > xMax)
    ///        {
    ///            chunkLoad[i].transform.position += 16 * (1 + 2 * maxLoadChunkDistance) * Vector3Int.left;
    ///            chunkLoad[i].GetComponent<ChunkShow>().Refresh();
    ///
    ///        }
    ///        else if (chunk.x < xMin)
    ///        {
    ///            chunkLoad[i].transform.position += 16 * (1 + 2 * maxLoadChunkDistance) * Vector3Int.right;
    ///            chunkLoad[i].GetComponent<ChunkShow>().Refresh();
    ///
    ///        }
    ///        else if (chunk.y > yMax)
    ///        {
    ///            chunkLoad[i].transform.position += 16 * (1 + 2 * maxLoadChunkDistance) * Vector3Int.back;
    ///            chunkLoad[i].GetComponent<ChunkShow>().Refresh();
    ///        }
    ///        else if (chunk.y < yMin)
    ///        {
    ///            chunkLoad[i].transform.position += 16 * (1 + 2 * maxLoadChunkDistance) * Vector3Int.forward;
    ///            chunkLoad[i].GetComponent<ChunkShow>().Refresh();
    ///
    ///        }
    ///    }
    ///}
    ///private void FirtsChunkLoad()
    ///{
    ///    for (int i = 0; i < chunkLoad.Count; i++)
    ///    {
    ///        Destroy(chunkLoad[i].gameObject);
    ///
    ///    }
    ///    chunkLoad = new List<Transform>();
    ///
    ///    Vector2Int bornInf;
    ///    bornInf = actualChunk - (Vector2Int.one * maxLoadChunkDistance);
    ///    for (int x = 0; x < maxLoadChunkDistance * 2 + 1; x++)
    ///    {
    ///        for (int y = 0; y < maxLoadChunkDistance * 2 + 1; y++)
    ///        {
    ///            GameObject gameObject = Instantiate(sphere);
    ///            gameObject.transform.position = new Vector3(bornInf.x * 16 + (x * 16), 0, bornInf.y * 16 + (y * 16));
    ///            chunkLoad.Add(gameObject.transform);
    ///        }
    ///    }
    ///
    ///}


}
