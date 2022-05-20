using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate3DNoise : MonoBehaviour
{
    private int[] randomeTable = new int[500];

    public int rayon;

    [Range(0, 1)]
    public float alphaLimite;

    public GameObject cube;

    private void Awake()
    {
        randomeTable = new int[] { 28, 492, 2, 3, 302, 404, 113, 485, 275, 167, 52, 125, 411, 410, 292, 15, 22, 56, 252, 337, 290, 83, 19, 51, 243, 28, 23, 342, 485, 109, 157, 444, 499, 291, 429, 52, 37, 213, 180, 52, 41, 139, 31, 113, 83, 402, 50, 94, 40, 31, 77, 83, 259, 465, 218, 31, 14, 284, 28, 28, 246, 140, 64, 69, 419, 499, 73, 264, 257, 269, 472, 215, 93, 83, 411, 83, 442, 37, 258, 152, 472, 410, 99, 402, 37, 103, 105, 114, 438, 56, 25, 52, 93, 490, 52,
4, 31, 472, 272, 365, 155, 125, 127, 5, 477, 219, 281, 302, 165, 215, 138, 411, 127, 83, 141, 365, 302, 142, 180, 28, 180, 352, 152, 155, 231, 284, 384, 156, 466, 159, 83, 25, 444, 133, 167, 168, 33, 42, 442, 180, 183, 186, 103, 350, 164, 22, 53, 167, 140, 189, 302, 352, 52, 192, 28, 28, 263, 292, 114, 164, 411, 14, 151, 302, 485, 233, 164, 300, 189, 83, 273, 210, 465, 189, 189, 218, 203, 114, 273, 189, 222, 231, 213, 155, 28, 499, 396, 51, 443, 151, 233, 238, 138, 422, 114, 492, 52, 19, 69, 5, 66, 481, 242, 219, 300, 73, 83, 66, 438, 37, 411, 167, 203, 189, 114, 83, 481, 360, 244, 29, 393, 167, 281, 115, 83, 252, 165, 309, 258, 168, 259, 461, 490, 483, 262, 31, 77, 490, 263, 172, 330, 302, 266, 269, 273, 272, 237, 33, 402, 8, 470, 77, 142, 303, 140, 100, 69, 155, 278, 167, 13, 165, 114, 337, 69, 485, 83, 152, 175, 215, 290, 291, 31, 52, 25, 430, 52, 266, 64, 425, 297, 483, 424, 21, 180, 311, 438, 113, 440, 314, 208, 22, 330, 499, 31, 443, 180, 361, 164, 396, 183, 326, 167, 481, 142, 51, 219, 328, 251, 330, 231, 201, 192, 139, 83, 83, 189, 284, 470, 332, 175, 441, 83, 427, 264, 336, 422, 337, 338, 83, 444, 73, 342, 344, 342, 66, 192, 350, 164, 284, 183, 350, 73, 233, 189, 53, 114, 231, 355, 113, 14, 458, 443, 25, 332, 360, 402, 330, 14, 5, 364, 442, 69, 424, 214, 10, 485, 314, 370, 349, 109, 373, 96, 374, 311, 470, 67, 405, 52, 383, 499, 384, 189, 183, 411, 83, 388, 83, 350, 231, 492, 114, 429, 41, 114, 390, 483, 189, 394, 218, 5, 461, 397, 155, 444, 159, 402, 403, 404, 209, 410, 416, 417, 418, 51, 365, 479, 422, 438, 458, 297, 427, 429, 441, 183, 430, 433, 67, 436, 492, 438, 233, 25, 439, 96, 438, 284, 481, 278, 411, 300, 470, 93, 113, 328, 404, 443, 452, 352, 140, 307, 130, 455, 83, 406, 243, 140, 221, 208, 50, 411, 388, 52, 477, 167, 462, 464, 465, 490, 291, 290, 290, 356, 292, 481, 467, 470, 284, 473, 218, 477, 477, 172, 481, 481, 311, 485, 142, 394, 488, 117, 490, 411, 292, 52, 164, 311, 481, 361, 41};
    }
    private Vector3Int position;
    private void Update()
    {

    }
    private void Start()
    {
        script();

    }
    public void script()
    {
        position = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
        Vector3 bornInf = -position - new Vector3(rayon, rayon, rayon);
        for (int x = 0; x < rayon * 2 + 1; x++)
        {
            for (int y = 0; y < rayon * 2 + 1; y++)
            {
                for (int z = 0; z < rayon * 2 + 1; z++)
                {
                    float caseValue = 0;
                    caseValue = CalculeCaseValue(bornInf.x + x, bornInf.y + y, bornInf.z + z);
                    Debug.Log(caseValue);
                    caseValue += 1f;
                    if (caseValue > alphaLimite)
                    {
                        GameObject tempo = Instantiate(cube);
                        tempo.transform.position = new Vector3(bornInf.x + x, bornInf.y + y, bornInf.z + z);
                    }
                }
            }
        }
    }

    public float CalculeCaseValue(float x, float y, float z)
    {
        float value = 0;
        Vector3 sommet = Vector3.zero;
        Vector3 direction = Vector3.zero;
        for (float i = -0.5f; i < 0.5f; i++)
        {
            for (float j = -0.5f; j < 0.5f; j++)
            {
                for (float k = -0.5f; k < 0.5f; k++)
                {
                    sommet = new Vector3(Mathf.RoundToInt(x + i), Mathf.RoundToInt(y + j), Mathf.RoundToInt(z + k));
                    sommet = GetRandomeValue(sommet);
                    sommet = sommet.normalized;
                    direction = new Vector3(i, j, k);
                    value += Vector3.Dot(sommet, direction);

                }
            }
        }
        return value;


    }

    public Vector3 GetRandomeValue(Vector3 position)
    {
        Vector3Int positionInt = new Vector3Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z));
        positionInt = new Vector3Int(Mathf.Abs(positionInt.x), Mathf.Abs(positionInt.y), Mathf.Abs(positionInt.z));
        positionInt = new Vector3Int(positionInt.x % 500, positionInt.y % 500, positionInt.z % 500);
        positionInt = new Vector3Int(randomeTable[positionInt.x], randomeTable[positionInt.y], randomeTable[positionInt.z]);
        return positionInt;
    }


}