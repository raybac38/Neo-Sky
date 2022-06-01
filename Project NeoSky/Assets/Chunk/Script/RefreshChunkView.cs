using System.Collections.Generic;
using UnityEngine;

public class RefreshChunkView : MonoBehaviour
{
    public GameObject sphere;
    List<Transform> chunkLoad = new List<Transform>();
    public Vector2Int actualChunk;
    public int renderDistance = 5;
    private void Start()
    {
        actualChunk = new Vector2Int(-22, -22);
        FirtsChunkLoad();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * 16);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * 16);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * 16);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * 16);
        }


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

    private Vector2Int CalculeChunk()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.z);
        Vector2Int chunkPosition = new Vector2Int(Mathf.RoundToInt((position.x) / 16), Mathf.RoundToInt(position.y / 16));
        return chunkPosition;
    }

    private void ChunkLoadMove()
    {
        int xMin = actualChunk.x - renderDistance;
        int xMax = actualChunk.x + renderDistance;
        int yMin = actualChunk.y - renderDistance;
        int yMax = actualChunk.y + renderDistance;


        for (int i = 0; i < chunkLoad.Count; i++)
        {
            Vector2Int chunk = new Vector2Int(Mathf.RoundToInt(chunkLoad[i].transform.position.x / 16), Mathf.RoundToInt(chunkLoad[i].transform.position.z / 16));
            if (chunk.x > xMax)
            {
                chunkLoad[i].transform.position += 16 * (1 + 2 * renderDistance) * Vector3Int.left;
                chunkLoad[i].GetComponent<ChunkShow>().Refresh();

            }
            else if (chunk.x < xMin)
            {
                chunkLoad[i].transform.position += 16 * (1 + 2 * renderDistance) * Vector3Int.right;
                chunkLoad[i].GetComponent<ChunkShow>().Refresh();

            }
            else if (chunk.y > yMax)
            {
                chunkLoad[i].transform.position += 16 * (1 + 2 * renderDistance) * Vector3Int.back;
                chunkLoad[i].GetComponent<ChunkShow>().Refresh();
            }
            else if (chunk.y < yMin)
            {
                chunkLoad[i].transform.position += 16 * (1 + 2 * renderDistance) * Vector3Int.forward;
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
        bornInf = actualChunk - (Vector2Int.one * renderDistance);
        for (int x = 0; x < renderDistance * 2 + 1; x++)
        {
            for (int y = 0; y < renderDistance * 2 + 1; y++)
            {
                GameObject gameObject = Instantiate(sphere);
                gameObject.transform.position = new Vector3(bornInf.x * 16 + (x * 16), 0, bornInf.y * 16 + (y * 16));
                chunkLoad.Add(gameObject.transform);
            }
        }

    }


}
