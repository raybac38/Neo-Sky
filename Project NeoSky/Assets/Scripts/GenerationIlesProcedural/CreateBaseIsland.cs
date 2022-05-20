using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBaseIsland : MonoBehaviour
{
    public RawImage raw;
    public Texture2D texture2D;
    public int width;
    public int height;
    public Vector2 maxValue;

    private int[] permutationTable = new int[] {99, 1, 15, 170, 197, 5, 11, 227, 142, 161, 80, 19, 110, 207, 14, 60, 223, 111, 121, 227, 138, 22, 9, 19, 25, 39, 23, 195, 28, 29, 232, 33, 232, 138, 14, 51, 121, 63, 5, 51, 22, 40, 42, 144, 185, 39, 43, 9, 254, 144, 203, 227, 102, 132, 55, 56, 92, 149, 58, 60, 77, 249, 62, 14, 65, 58, 56, 81, 170, 40, 68, 80, 149, 237, 180, 158, 80, 84, 42, 95, 142, 98, 38, 94, 185, 192, 102, 216, 142, 234, 105, 22, 234, 9, 107, 163, 110, 111,
109, 80, 9, 120, 125, 166, 9, 113, 217, 55, 227, 227, 227, 177, 80, 21, 92, 208, 128, 220, 67, 132, 58, 134, 135, 166, 49, 217, 19, 202, 254, 138, 140, 81, 141, 80, 142, 144, 47, 80, 172, 80, 158, 9, 19, 109, 149, 157, 180, 158, 39, 142, 160, 99, 244, 200, 60, 9, 170, 80, 172, 173, 255, 7, 178, 81, 247, 203, 177, 73, 178, 73, 180, 182, 9, 19, 77, 208, 186, 158, 213, 102, 7, 68, 55, 19, 237, 189, 77, 73, 195, 185, 216, 39, 196, 197, 222, 202, 213, 177, 80, 227, 80, 80, 203, 202, 80, 203, 84, 254, 9, 213, 227, 60, 214, 216, 158, 84, 110, 227, 222, 202, 113, 195, 68, 138, 135, 223, 221, 158, 227, 230, 138, 237, 7, 11, 248, 138, 240, 242, 244,
208, 216, 137, 196, 247, 111, 68, 158, 251, 47, 39, 84, 77, 134, 29, 185, 163};

    private void Start()
    {
        GenerateNoiseTexture();
    }
    public void GenerateNoiseTexture()
    {
        texture2D = new Texture2D(width, height, TextureFormat.RGB24, true);
        texture2D.name = "perlin noise";
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float color = CalculeCaseValue(i, j);
                texture2D.SetPixel(i, j, Color.yellow);
            }
        }
        GetComponent<Renderer>().material.mainTexture = texture2D;

    }

    public float CalculeCaseValue(int x, int y)
    {
        float value = 0;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Vector2 distance = new Vector2((x + 0.5f) - (x + i), (y + 0.5f) - (y + i));
                Vector2 gradient = new Vector2(permutationTable[(x + i) % 256], permutationTable[(y + j) % 256]);
                distance = distance.normalized;
                gradient = gradient.normalized;
                value += Vector2.Dot(distance, gradient);

            }
        }
        Debug.Log(value);
        //permet de mettre la valeur du pixel entre 0 et 1
        if(value < maxValue.x)
        {
            maxValue = new Vector2(value, maxValue.y);
        }
        if (value > maxValue.y)
        {
            maxValue = new Vector2(maxValue.x, value);
        }
         
        return value;
    }

}
