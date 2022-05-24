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
    }
    private void Update()
    {
        if (actualChunk != CalculeChunk())
        {
            actualChunk = CalculeChunk();
            ChangeChunkLoad();
        }
    }

    private Vector2Int CalculeChunk()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.z);
        Vector2Int chunkPosition = new Vector2Int(Mathf.RoundToInt((position.x) / 10), Mathf.RoundToInt((position.y) / 10));
        return chunkPosition;
    }
    private void ChangeChunkLoad()
    {
        for (int i = 0; i < chunkLoad.Count; i++)
        {
            Destroy(chunkLoad[i].gameObject);

        }
        chunkLoad = new List<Transform>();

        Vector2Int bornInf;
        bornInf = actualChunk - (Vector2Int.one * renderDistance * 10);
        for (int x = 0; x < renderDistance *2 + 1; x++)
        {
            for (int y = 0; y < renderDistance * 2 + 1; y++)
            {
                GameObject gameObject = Instantiate(sphere);
                gameObject.transform.position = new Vector3(bornInf.x + (x * 10), 0, bornInf.y + (y * 10));
                chunkLoad.Add(gameObject.transform);
            }
        }

    }
}
