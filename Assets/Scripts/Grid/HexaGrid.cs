using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObject/HexGridSystem", order = 1, fileName = "HexGrid")]
public class HexaGrid : ScriptableObject
{
    [SerializeField] GameObject Tile;
    [SerializeField] int Width = 5;
    [SerializeField] int Height = 5;
    [SerializeField] List<Sprite> TilesSprites;
    [SerializeField] AdSystem AdSystem;
    [SerializeField] TwoButtonPopupSO TwoButtonPopupSO;
    int maxHexes = 25;
    float xOffset = 1f;
    float yOffset = 0.866f;
    float xPos;
    float yPos;
    int index=0;
    bool once = false;
    GameObject tileGenerated;
    List<HexData> HexesList = new List<HexData>();

    List<HexData> checkedHexes = new List<HexData>();
    Queue<HexData> InqueueHexes = new Queue<HexData>();
    List<HexData> neighbours = new List<HexData>();

    HexData tempPlacement;
    int tempTier;

    public Dictionary<Vector2, HexData> HexAtPosition = new Dictionary<Vector2, HexData>(); //all the hexes in grid
    public Dictionary<HexData, List<HexData>> NeighboursOfHexes = new Dictionary<HexData, List<HexData>>(); //all the hexes in grid with their neighbours
    Dictionary<HexData, List<HexData>> trackOfPlacement = new Dictionary<HexData, List<HexData>>();

    private static readonly Vector2 left = new Vector2(-1, 0);
    private static readonly Vector2 right = new Vector2(1, 0);
    private static readonly Vector2 topLeft = new Vector2(-0.5f, 0.866f);
    private static readonly Vector2 topRight = new Vector2(0.5f, 0.866f);
    private static readonly Vector2 bottomLeft = new Vector2(-0.5f, -0.866f);
    private static readonly Vector2 bottomRight = new Vector2(0.5f, -0.866f);

    public static Vector2 Left => left;
    public static Vector2 Right => right;
    public static Vector2 TopLeft => topLeft;
    public static Vector2 TopRight => topRight;
    public static Vector2 BottomLeft => bottomLeft;
    public static Vector2 BottomRight => bottomRight;

    private static List<Vector2> directions = null;
    public static List<Vector2> Directions
    {
        get
        {
            if (directions == null)
            {
                directions = new List<Vector2>();

                directions.Add(left);
                directions.Add(right);
                directions.Add(topLeft);
                directions.Add(topRight);
                directions.Add(bottomLeft);
                directions.Add(bottomRight);
            }

            return directions;
        }
    }
    //List<Vector2> HexPointedTop = Neighbours2.get();
    //public Dictionary<>


    //[SerializeField] List<Vector2> hexGridPos = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        PrintNeighbours();
    }
    public Dictionary<Vector2,HexData> GetHexAtPosition()
    {
        return HexAtPosition;
    }
    public Dictionary<HexData, List<HexData>> GetNeighboursOfHexes()
    {
        return NeighboursOfHexes;
    }
    public void GenerateGrid()
    { 
        HexAtPosition.Clear();
        NeighboursOfHexes.Clear();
        //Vector2 tempvec = new Vector2();
        //HexData temp = new HexData();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Vector2 tempvec = new Vector2();
                HexData temp = new HexData();
                if (y % 2 == 0)
                {
                    xPos = x * xOffset;
                    yPos = y * yOffset;
                    tempvec.x = xPos;
                    tempvec.y = yPos;
                    if (!once)
                    {
                        tileGenerated = (GameObject)Instantiate(Tile, tempvec, Quaternion.identity);
                    }
                    else
                    {
                        tileGenerated = (GameObject)Instantiate(tileGenerated, tempvec, Quaternion.identity);
                    }
                    tileGenerated.name = "Hex_" + tempvec.x + "_" + tempvec.y;
                    //tileGenerated.transform.SetParent(this.transform);
                    temp.Hex = tileGenerated;
                    temp.Id = tileGenerated.name;
                    HexAtPosition[tempvec]= temp;
                    //PopulateNeighbours(tempvec, temp);
                }
                else if (x < Width - 1)
                {
                    xPos = x * xOffset;
                    xPos += xOffset / 2f;
                    yPos = y * yOffset;
                    tempvec.x = xPos;
                    tempvec.y = yPos;
                    tileGenerated = (GameObject)Instantiate(tileGenerated, tempvec, Quaternion.identity);
                    tileGenerated.name = "Hex_" + tempvec.x + "_" + tempvec.y;
                    //tileGenerated.transform.SetParent(this.transform);
                    temp.Hex = tileGenerated;
                    temp.Id = tileGenerated.name;
                    HexAtPosition[tempvec] = temp;
                    //
                }
            }
        }

        PopulateNeighbours();
    }
    public void IsGridFill()
    {
        bool notOccupied = false;
        foreach (var pair in HexAtPosition)
        {
            if(!pair.Value.Occupied)
            {
                notOccupied = true;
                break;
            }
        }
        if(!notOccupied)
        {
            AdSystem.OnQuitAd();
            Debug.Log("fail");
            TwoButtonPopupSO.FailYes += ResetGrid;
            TwoButtonPopupSO.Fail.Invoke();
        }
    }
    public void ResetGrid()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TwoButtonPopupSO.Hidepopup.Invoke();
    }
    public HexData GetHexAtPosition(Vector2 position)
    {
        foreach (var otherPair in HexAtPosition)
        {
            if (otherPair.Key == position)
            {
                return otherPair.Value;
            }
        }

        return null;
    }

    public void PopulateNeighbours()
    {
        foreach (var pair in HexAtPosition)
        {
            Vector2 tempvec = pair.Key;
            HexData tempHex = pair.Value;

            var list = new List<HexData>();
            for (int i = 0; i < Directions.Count; i++)
            {
                var neighbourPos = Directions[i] + tempvec;
                var neighbourHex = GetHexAtPosition(neighbourPos);
                if (neighbourHex != null)
                {
                    list.Add(neighbourHex);
                }
            }

            NeighboursOfHexes.Add(tempHex, list);
        }
        
    }
    public void PrintNeighbours()
    {
        foreach (KeyValuePair<Vector2, HexData> kvp in HexAtPosition)
        {
            Debug.Log("Key " + kvp.Key + " value "+kvp.Value.Id);
           
        }
        foreach (KeyValuePair<HexData, List<HexData>> kvp in NeighboursOfHexes)
        {
            Debug.Log("Key "+ kvp.Key.Id +" values " + kvp.Value.Count);
            //for(int i=0; i<kvp.Value.Count; i++)
            //{
            //    Debug.Log("neighbour # " + i + " is " + kvp.Value[i].Id);
            //}
        }
    }
    public HexData GetNearestPositionFromPoint(Vector3 position)
    {
        float distance;
        foreach (var pair in HexAtPosition)
        {
             distance = Vector3.Distance(pair.Value.Hex.transform.position, position);
            if (distance <= 0.5f && pair.Value.Hex.transform.childCount==0)
            {
                return pair.Value;
            }
        }

        return null;
    }
    public void AfterSnap(HexData hex)
    {
        HexData bestPlacement=new HexData();
        matchedNeighbours.Clear();
        //matchedNeighbours.Add(hex);
        Match(hex);
        List<HexData> matched = DeepCopyList(matchedNeighbours);
        List<HexData> matched2 = DeepCopyList(matchedNeighbours);
        if (matched.Count >= 3)
        {
            //jjdejbestPlacement = matched[0];
            bestPlacement = BestPlacement(matched2);
            Merge(bestPlacement, matched);
        }
        //Merge(bestPlacement);
    }
    List<HexData> matchedNeighbours = new List<HexData>();
    public void Match(HexData hex)
    {
        //matchedNeighbours.Add(hex);
        List<HexData> nearestMatched = new List<HexData>();
        foreach(var pair in NeighboursOfHexes[hex])
        {
            if(pair.Occupied && hex.HexTile.Tier==pair.HexTile.Tier && !matchedNeighbours.Contains(pair))
            {
                matchedNeighbours.Add(pair);
                nearestMatched.Add(pair);
            }
        }
        //checkedMatched.Add(hex);
        foreach(var pair in nearestMatched)
        {
            Match(pair);
        }
        //return matchedNeighbours;
    }
    public HexData BestPlacement(List<HexData> matched)
    {
        //List<HexData> tempMatched = DeepCopyList(matched);
        int maxMatched = 0;
        HexData bestPlacement = matched[0];
        foreach (var pair in matched)
        {
            pair.HexTile.Tier++;
            matchedNeighbours.Clear();
            Match(pair);
            if(matchedNeighbours.Count>=maxMatched)
            {
                maxMatched = matchedNeighbours.Count;
                //pair.HexTile.Tier--;
                bestPlacement = pair;
            }
            pair.HexTile.Tier--;
        }
        return bestPlacement;
    }
    List<HexData> DeepCopyList(List<HexData> list)
    {
        List<HexData> newList = new List<HexData>();
        foreach(var pair in list)
        {
            newList.Add(pair);
        }
        return newList;
    }

    public void Merge(HexData bestPlacement, List<HexData> matched)
    {
      
        foreach (var pair in matched)
        {
            if (pair==bestPlacement && pair.HexTile.Tier!=-1)
            {
                Debug.Log(pair.HexTile.Tier);
                pair.HexTile.TileObj.GetComponent<SpriteRenderer>().sprite = TilesSprites[pair.HexTile.Tier];
                pair.HexTile.Tier = pair.HexTile.Tier + 1;
               
            }
            else
            {
                pair.Occupied = false;
                pair.Hex.transform.DetachChildren();
                pair.HexTile.TileObj.SetActive(false);
                pair.HexTile.Tier = -1;
                //Debug.Log("b"+pair.HexTile.Tier);
                // pair.HexTile.Tier = pair.HexTile.Tier+1;
                //pair.HexTile.TileObj.GetComponent<SpriteRenderer>().sprite = TilesSprites[pair.HexTile.Tier];
                // Debug.Log("a"+pair.HexTile.Tier);

            }
        }
       }


    //public void searching(HexData hex)
    //{
    //    InqueueHexes.Clear();
    //    neighbours.Clear();
    //    checkedHexes.Clear();
    //    InqueueHexes.Enqueue(hex);
    //    SearchNeighbours(hex);
    //    Debug.Log("matched neighbours are");

    //   foreach(var pair in neighbours)
    //    {
    //        Debug.Log(pair.Id);
    //    }
    //   if(neighbours.Distinct().Count()>=3)
    //    {
    //        Debug.Log("finish");
    //        Merge();
    //    }
    //    Debug.Log("finish");
    //}
    //public void SearchingNeighbours(HexData hex)
    //{
    //    GridDirections directions = new GridDirections();
    //    HexData temp;
    //    if (!checkedHexes.Contains(hex))
    //    {
    //        List<Vector2> neighbourPositions = new List<Vector2>();
    //        //neighbourPositions=directions.NeighbourPositions()
    //        for (int i=0; i<6;i++)
    //        {

    //        }
    //    }
    //}
    //public void SearchNeighbours(HexData hex)
    //{
    //    GridDirections directions=new GridDirections();
    //    HexData temp;
    //    if (!checkedHexes.Contains(hex))
    //    {
    //        if (directions.BottomLeft(hex.Index) < maxHexes && directions.BottomLeft(hex.Index) >= 0 && HexesList[directions.BottomLeft(hex.Index)].Occupied && hex.HexTile.Tier == HexesList[directions.BottomLeft(hex.Index)].HexTile.Tier)
    //        {
    //            InqueueHexes.Enqueue(HexesList[directions.BottomLeft(hex.Index)]);
    //            neighbours.Add(hex);
    //        }
    //        if (directions.BottomRight(hex.Index) < maxHexes && directions.BottomRight(hex.Index) >= 0 && HexesList[directions.BottomRight(hex.Index)].Occupied && hex.HexTile.Tier == HexesList[directions.BottomRight(hex.Index)].HexTile.Tier)
    //        {
    //            InqueueHexes.Enqueue(HexesList[directions.BottomRight(hex.Index)]);
    //            neighbours.Add(hex);
    //        }
    //        if (directions.TopLeft(hex.Index) < maxHexes && directions.TopLeft(hex.Index) >= 0 && HexesList[directions.TopLeft(hex.Index)].Occupied && hex.HexTile.Tier == HexesList[directions.TopLeft(hex.Index)].HexTile.Tier)
    //        {
    //            InqueueHexes.Enqueue(HexesList[directions.TopLeft(hex.Index)]);
    //            neighbours.Add(hex);
    //        }
    //        if (directions.TopRight(hex.Index) < maxHexes && directions.TopRight(hex.Index) >= 0 && HexesList[directions.TopRight(hex.Index)].Occupied && hex.HexTile.Tier == HexesList[directions.TopRight(hex.Index)].HexTile.Tier)
    //        {
    //            InqueueHexes.Enqueue(HexesList[directions.TopRight(hex.Index)]);
    //            neighbours.Add(hex);
    //        }
    //        if (directions.Left(hex.Index) < maxHexes && directions.Left(hex.Index) >= 0 && HexesList[directions.Left(hex.Index)].Occupied && hex.HexTile.Tier == HexesList[directions.Left(hex.Index)].HexTile.Tier)
    //        {
    //            InqueueHexes.Enqueue(HexesList[directions.Left(hex.Index)]);
    //            neighbours.Add(hex);
    //        }
    //        if (directions.Right(hex.Index) < maxHexes && directions.Right(hex.Index) >= 0 && HexesList[directions.Right(hex.Index)].Occupied && hex.HexTile.Tier == HexesList[directions.Right(hex.Index)].HexTile.Tier)  //check tier here instead of below
    //        { 
    //                InqueueHexes.Enqueue(HexesList[directions.Right(hex.Index)]);
    //                neighbours.Add(hex);
    //        }
    //        checkedHexes.Add(hex);
    //    }
    //    while(InqueueHexes.Count>0)
    //    {
    //        temp = InqueueHexes.Dequeue();
    //         SearchNeighbours(temp);
    //    }
    //    return;
    //}
    //public void Merge()
    //{
    //    bool merge = false;
    //    foreach (var pair in neighbours.Distinct())
    //    {
    //        if (!merge)
    //        { 
    //            merge = true;
    //            Debug.Log("b" + pair.HexTile.Tier);
    //            pair.HexTile.TileObj.GetComponent<SpriteRenderer>().sprite = TilesSprites[pair.HexTile.Tier];
    //            pair.HexTile.Tier = pair.HexTile.Tier + 1;
    //            Debug.Log("a" + pair.HexTile.Tier);
    //        }
    //        else
    //        {

    //            pair.Hex.transform.DetachChildren();
    //            pair.HexTile.TileObj.SetActive(false);
    //            pair.HexTile.Tier = -1;
    //            //Debug.Log("b"+pair.HexTile.Tier);
    //            // pair.HexTile.Tier = pair.HexTile.Tier+1;
    //            //pair.HexTile.TileObj.GetComponent<SpriteRenderer>().sprite = TilesSprites[pair.HexTile.Tier];
    //            // Debug.Log("a"+pair.HexTile.Tier);

    //        }
    //    }
    //}
    Dictionary<HexData, List<HexData>> DeepCopy()
    {
        Dictionary<HexData, List<HexData>> neighboursOfHexes = new Dictionary<HexData, List<HexData>>();
        foreach (KeyValuePair<HexData, List<HexData>> kvp in NeighboursOfHexes)
        {
            foreach (var pair in kvp.Value)
            {
                NeighboursOfHexes[kvp.Key].Add(pair);
            }
        }
        return neighboursOfHexes;
    }
}
