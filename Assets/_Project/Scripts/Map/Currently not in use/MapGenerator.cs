using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform boardContainer;
    [SerializeField] private List<PointOfEvents> pointOfEventsPrefabs;  // Should be UI components with RectTransform
    [SerializeField] private Image pathPrefab;
    [SerializeField] private int numOfStartingPoints = 4;
    [SerializeField] private int mapLenght = 10;
    [SerializeField] private int maxWidth = 5;
    [SerializeField] private float maxXSize;
    [SerializeField] private float yPadding;
    [SerializeField] private bool isCrissCrossing;
    [Range(0.1f, 1f), SerializeField] private float chanceofMiddlePath;
    [Range(0f, 1f), SerializeField] private float chanceofSidePath;
    [Range(0.9f, 5f), SerializeField] private float mSpaceBetweenLines = 2.5f;
    [Range(1f, 5.5f), SerializeField] private float MinimumConnections = 3f;

    private PointOfEvents[][] _poePerFloor;
    private readonly List<PointOfEvents> POE = new();
    private int numOfConnections = 0;

    void Start()
    {
        ReCreateBoard();
    }

    public void ReCreateBoard()
    {
        DestroyImmediateAllChildren(boardContainer);
        numOfConnections = 0;
        GenerateRandomSeed();
        POE.Clear();
        _poePerFloor = new PointOfEvents[mapLenght][];
        for (int i = 0; i < _poePerFloor.Length; i++)
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
        float spaceBetweenLevels = 250f; // UI pixel spacing
        float xPos = spaceBetweenLevels * floorN;
        float yPos = -(yPadding * xNum);

        xPos += Random.Range(-xSize / 4f, xSize / 4f);
        yPos += Random.Range(-yPadding / 4f, yPadding / 4f);

        PointOfEvents instancePrefab = pointOfEventsPrefabs[Random.Range(0, pointOfEventsPrefabs.Count)];
        PointOfEvents instantce = Instantiate(instancePrefab, boardContainer);
        POE.Add(instantce);

        instantce.rt.anchoredPosition = new Vector2(xPos, yPos); // UI anchored position
        _poePerFloor[floorN][xNum] = instantce;

        int created = 0;

        void InstatiateNextPoint(int i, int j)
        {
            PointOfEvents nextPOE = InstatiatePointofEvents(i, j);
            AddLineBetweenPoints(instantce, nextPOE);
            instantce.NextPointsOfEvents.Add(nextPOE);
            created++;
            numOfConnections++;
        }

        while (created == 0 && floorN < mapLenght - 1)
        {
            if (xNum > 0 && Random.Range(0f, 1f) < chanceofSidePath)
            {
                if (isCrissCrossing || _poePerFloor[floorN + 1][xNum - 1] == null)
                {
                    InstatiateNextPoint(floorN + 1, xNum - 1);
                }
            }
            if (xNum < maxWidth - 1 && Random.Range(0f, 1f) < chanceofSidePath)
            {
                if (isCrissCrossing || _poePerFloor[floorN + 1][xNum + 1] == null)
                {
                    InstatiateNextPoint(floorN + 1, xNum + 1);
                }
            }
            if (Random.Range(0f, 1f) < chanceofMiddlePath)
            {
                InstatiateNextPoint(floorN + 1, xNum);
            }
        }
        return instantce;
    }

    private void CreateMap()
    {
        List<int> positions = GetRandomIndexes(numOfStartingPoints);
        foreach (int j in positions)
        {
            _ = InstatiatePointofEvents(0, j);
        }

        if (numOfConnections <= mapLenght * MinimumConnections)
        {
            Debug.Log($"Recreating board with {numOfConnections} connections");
            ReCreateBoard();
            return;
        }

        Debug.Log($"Created board with {numOfConnections} connections");
        Debug.Log($"Created board with {POE.Count} points");
    }

    private void AddLineBetweenPoints(PointOfEvents thisPoint, PointOfEvents nextPoint)
    {
        Vector2 startingPoints = thisPoint.rt.anchoredPosition;
        Vector2 endPoint = nextPoint.rt.anchoredPosition;
        Vector2 direction = (endPoint - startingPoints).normalized;
        float distance = Vector2.Distance(startingPoints, endPoint);

        float pathWidth = pathPrefab.rectTransform.sizeDelta.y;
        if (pathWidth <= 0f)
        {
            Debug.LogWarning("Path prefab height is 0 or less");
            return;
        }

        int numOfSegments = Mathf.Max(1, (int)(distance / (pathWidth * mSpaceBetweenLines)));
        float padding = (distance - (numOfSegments * pathWidth)) / (numOfSegments + 1);
        Vector2 currentPos = startingPoints + direction * (padding + (pathWidth / 2f));

        for (int i = 0; i < numOfSegments; i++)
        {
            Image pathSegment = Instantiate(pathPrefab, boardContainer);
            pathSegment.rectTransform.anchoredPosition = currentPos;
            pathSegment.rectTransform.localScale = Vector3.one;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            pathSegment.rectTransform.localEulerAngles = new Vector3(0, 0, angle);
            currentPos += direction * (pathWidth + padding);
        }

        Canvas.ForceUpdateCanvases();
    }

    private List<int> GetRandomIndexes(int n)
    {
        List<int> indexes = new();
        if (n > maxWidth)
        {
            throw new System.Exception("You have to many starting points!");
        }

        while (indexes.Count < n)
        {
            int randomNum = Random.Range(0, maxWidth);
            if (!indexes.Contains(randomNum))
            {
                indexes.Add(randomNum);
            }
        }
        return indexes;
    }

    private void DestroyImmediateAllChildren(RectTransform transform)
    {
        List<RectTransform> toRemove = new();
        foreach (RectTransform child in transform)
        {
            toRemove.Add(child);
        }
        for (int i = toRemove.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(toRemove[i].gameObject);
        }
    }

}
