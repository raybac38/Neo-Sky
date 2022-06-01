using System.Collections.Generic;
using UnityEngine;
public class ChunkShow : MonoBehaviour
{
    Vector2Int myChunk;
    public Vector2Int myAffectingZone;
    public int AffectiongScaleIsland = 8; //scale par rapport a 16

    public GameObject cubePreview;
    public GameObject islandPreview;
    List<Transform> previewThinks = new List<Transform>();


    private int[] table = { 1, 72, 95, 4, 92, 89, 74, 18, 18, 9, 10, 11, 43, 78, 89, 63, 15, 15, 7, 30, 74, 75, 31, 95, 61, 57, 48, 76, 48, 18, 89, 76, 10, 30, 18, 75, 31, 32, 63, 74, 15, 92, 81, 70, 39, 48, 95, 42, 84, 89, 15, 39, 33, 53, 46, 76, 18, 33, 57, 76, 46, 43,
 10, 30, 59, 7, 89, 63, 64, 89, 75, 84, 89, 53, 76, 30, 31, 33, 89, 5, 57, 81, 81, 89, 30, 30, 84, 86, 18, 7, 76, 95, 7, 63, 39, 97, 48, 89};
    public Vector2[] affectingZonePoint = new Vector2[9];
    private float[,] baseMap = new float[16, 16];
    private void Awake()
    {

    }
    private void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        myAffectingZone = myChunk = new Vector2Int(Mathf.FloorToInt(transform.position.x / (16 * AffectiongScaleIsland)) , Mathf.FloorToInt(transform.position.z / (16 * AffectiongScaleIsland))) ;
        myChunk = new Vector2Int(Mathf.FloorToInt(transform.position.x / 16), Mathf.FloorToInt(transform.position.z / 16));
        CalculateAffectingZonePoint();

        CalculateBaseMap();
        GeneratePreview();
    }
    public void CalculateAffectingZonePoint()
    {

        int i = 0;
        for (float x = -1f; x < 2f; x++)
        {
            for (float y = -1f; y < 2f; y++)
            {
                affectingZonePoint[i] = (16 * AffectiongScaleIsland) * (myAffectingZone + new Vector2(x, y));
                affectingZonePoint[i] = affectingZonePoint[i] + GenerationPseudoAleatoire((new Vector2(x,y) + myAffectingZone) * AffectiongScaleIsland * 16f);
                i++;

            }
        }
    }
    ///private Vector2 PseudoRandomeNum(Vector2 position)
    ///{
    ///    float nombre = (position.x + position.y) + 2f * position.x - 0.5f * position.y;
    ///    nombre = Mathf.Pow(nombre, 6);
    ///    nombre = nombre % 100000;
    ///    nombre = nombre / 100;
    ///    nombre = Mathf.Floor(nombre);
    ///    
    ///    float witdh = nombre % 98;
    ///    float height = Mathf.FloorToInt(nombre / 98) % 98;
    ///
    ///    return new Vector2(witdh / 1000f + 1f, height / 1000f + 1f) * AffectiongScaleIsland * 16f;
    ///}
    public void CalculateBaseMap()
    {
        Vector2 position = myChunk * 16;
        for (int x = 0; x < baseMap.GetLength(0); x++)
        {
            for (int y = 0; y < baseMap.GetLength(1); y++)
            {
                float firstValue = ClosestPoint(new Vector2(position.x + x, position.y + y));
                baseMap[x, y] = SecondeClosestPoint(firstValue, new Vector2(position.x + x, position.y + y)) - firstValue;

            }
        }
    }
    private float SecondeClosestPoint(float closestDistance, Vector2 position)
    {
        float distance = 50000f;
        for (int i = 0; i < affectingZonePoint.Length; i++)
        {
            if(Vector2.Distance(position, affectingZonePoint[i]) == closestDistance)
            {

            }else if (Vector2.Distance(position, affectingZonePoint[i]) < distance)
            {
                distance = Vector2.Distance(position, affectingZonePoint[i]);
            }
        }
        return distance;
    }
    private float ClosestPoint(Vector2 position)
    {
        float distance = 50000f;
        for (int i = 0; i < affectingZonePoint.Length; i++)
        {
            if(Vector2.Distance(position, affectingZonePoint[i]) < distance)
            {
                distance = Vector2.Distance(position, affectingZonePoint[i]);
            }
        }
        return distance;
    }
    public void GeneratePreview()
    {
        for (int i = 0; i < previewThinks.Count; i++)
        {
            Destroy(previewThinks[i].gameObject);
        }
        previewThinks.Clear();
        for (int x = 0; x < baseMap.GetLength(0); x++)
        {
            for (int y = 0; y < baseMap.GetLength(1); y++)
            {
                if(baseMap[x,y] > 60f) //metre a 60 en temps normal voir plus

                {
                    GameObject obj = Instantiate(cubePreview, transform);

                    obj.transform.position = new Vector3(myChunk.x * 16f + x, baseMap[x,y], myChunk.y * 16f + y);
                    previewThinks.Add(obj.transform);

                }

                
            }
        }
    }

    public Vector2 GenerationPseudoAleatoire(Vector2 position)
    {

        float x = position.x;
        float y = position.y;
        Debug.Log(y);
        if (Mathf.CeilToInt(x % 96) < 0)
        {
            x = x % 96;
            x = 96 - x;
            x = x % 96;
            x = table[Mathf.CeilToInt(x)];

        }
        else
        {
            x = x % 96;
            x = table[Mathf.CeilToInt(x)];

        }
        if (Mathf.CeilToInt(y % 96) < 0)
        {
            y = Mathf.CeilToInt(y);
            y = y % 96;
            y = y % 2;
            y = 96 - y;
            Debug.Log(y);
            y = table[Mathf.CeilToInt(y)];

        }
        else
        {
            y = y % 96;
            y = table[Mathf.CeilToInt(y)];

        }

        float value = x + y + Mathf.Sqrt(x * x + y * y);
        x = value % 90;
        y = (value / 90) % 90;
        x /= 120;
        y /= 120;
        return new Vector2(x, y) * AffectiongScaleIsland * 16f;
    }
}
