using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform boardContainer;
    [SerializeField] private List<PointOfEvents> pointOfEventsPrefabs;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private int numOfStartingPoints = 4;
    [SerializeField] private int mapLenght = 10;
    [SerializeField] private int maxWidth = 5;
    [SerializeField] private float maxXSize;
    [SerializeField] private float maxYSize;
    [SerializeField] private bool isCrissCrossing;
    [Range(0.1f, 1f), SerializeField] private float chanceofMiddlePath;
    [Range(0f, 1f), SerializeField] private float chanceofSidePath;
    [Range(0.9f, 5f), SerializeField] private float mSpaceBetweenLines = 2.5f;
    [Range(1f,5.5f),  SerializeField] private float MinimumConnections = 3f;

    private PointOfEvents[][] _poePerFloor;
    private readonly List<PointOfEvents> POE= new();
    private int numOfConnections = 0;
    private float lineLegnth;
    private float lineHeigth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReCreateBoard();
    }

    public void ReCreateBoard()
    {
        lineLegnth = pathPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * pathPrefab.transform.localScale.z;
        lineHeigth = pathPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * pathPrefab.transform.localScale.y;
        DestroyImmediateAllChildren(boardContainer);
        numOfConnections = 0;
        GenerateRandomSeed();
        POE.Clear();
        _poePerFloor = new PointOfEvents[mapLenght][];
        for(int i = 0; i < _poePerFloor.Length; i++)
        {
            _poePerFloor[i] = new PointOfEvents[maxWidth];
        }
        CreateMap();
    }

    private void GenerateRandomSeed()
    {
        int tempSeed = (int)System.DateTime.Now.Ticks;
        Random.InitState(tempSeed);
    }

    private PointOfEvents InstatiatePointofEvents(int floorN, int xNum)
    {
        if (_poePerFloor[floorN][xNum] != null)
        {
            return _poePerFloor[floorN][xNum];
        }
        float xSize = maxXSize / maxWidth;
        float xPos = (xSize * xNum) + (xSize / 2f);
        float yPos = maxYSize * floorN;

        xPos += Random.Range(-xSize / 4f, xSize / 4f);
        yPos += Random.Range(-maxYSize / 4f, maxYSize / 4f);

        Vector3 pos = new Vector3(xPos, 0, yPos);
        PointOfEvents randomPOE = pointOfEventsPrefabs[Random.Range(0, pointOfEventsPrefabs.Count)];
        PointOfEvents instantce = Instantiate(randomPOE, boardContainer);
        POE.Add(instantce);

        instantce.transform.localPosition = pos;
        _poePerFloor[floorN][xNum] = instantce;
        int created = 0;

        void InstatiateNextPoint(int index_i, int index_j)
        {
            PointOfEvents nextPOE = InstatiatePointofEvents(index_i, index_j);
            AddLineBetweenPoints(instantce, nextPOE);
            instantce.NextPointsOfEvents.Add(nextPOE);
            created++;
            numOfConnections++;
        }

        while (created == 0 && floorN < mapLenght - 1)
        {
            if(xNum > 0 && Random.Range(0f, 1f) < chanceofSidePath)
            {
                if(isCrissCrossing || _poePerFloor[floorN + 1][xNum] == null)
                {
                    InstatiateNextPoint(floorN + 1, xNum - 1);
                }
            }
            if(xNum < maxWidth - 1 && Random.Range(0f,1f) < chanceofSidePath)
            {
                if(isCrissCrossing || _poePerFloor[floorN + 1][xNum] == null)
                {
                    InstatiateNextPoint(floorN + 1, xNum + 1);
                }
            }
            if(Random.Range(0f,1f) < chanceofSidePath)
            {
                InstatiateNextPoint(floorN + 1, xNum);
            }
        }
        return instantce;
    }

    private void CreateMap()
    {
        List<int> positions = GetRandomIndexes (numOfStartingPoints);
        foreach (int j in positions)
        {
            _ = InstatiatePointofEvents(0, j);
        }

        if(numOfConnections <= mapLenght * MinimumConnections)
        {
            Debug.Log($"Recreating board with {numOfConnections} connections");
            ReCreateBoard();
            return;
        }

        Debug.Log($"Created board with {numOfConnections} connections");
        Debug.Log($"Created board with {POE} points");
    }

    private void AddLineBetweenPoints(PointOfEvents thisPoint, PointOfEvents nextPoint)
    {
        float len = lineLegnth;
        float height = lineHeigth;

        Vector3 dir = (nextPoint.transform.position - thisPoint.transform.position);

        float dist = Vector3.Distance(thisPoint.transform.position, nextPoint.transform.position);

        int num = (int)(dist / (len * mSpaceBetweenLines));

        float pad = (dist - (num * len)) / (num + 1);

        Vector3 pos_i = thisPoint.transform.position + (dir * (pad + (len / 2f)));

        for(int i = 0; i < num; i++)
        {
            Vector3 pos = pos_i + ((len + pad) * i * dir);
            GameObject lineCreated = Instantiate(pathPrefab, pos, Quaternion.identity, boardContainer);
            lineCreated.transform.LookAt(nextPoint.transform);
            lineCreated.transform.position -= Vector3.up * (height / 2f);
        }
    }

    private List<int> GetRandomIndexes(int n)
    {
        List<int> indexes = new List<int>();
        if(n > maxWidth)
        {
            throw new System.Exception("You have to many starting points!");
        }

        while (indexes.Count < n)
        {
            int randomNum = Random.Range(0, maxWidth);
            if(!indexes.Contains(randomNum))
            {
                indexes.Add(randomNum);
            }
        }
        return indexes;
    }

    private void DestroyImmediateAllChildren(Transform transform)
    {
        List<Transform> toRemove = new();
        foreach (Transform child in transform)
        {
            toRemove.Add(child);
        }
        for (int i = toRemove.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(toRemove[i].gameObject);
        }
    }

}
