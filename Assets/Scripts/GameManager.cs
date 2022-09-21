using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    TileController TileControllerRef;
    [SerializeField] LifeSystem life;
    [SerializeField] AdSystem AdSystem;
    //[SerializeField] RewardGranted RewardGranted;
    InputController InputControllerRef;
    [SerializeField] HexaGrid HexaGrifRef;
    TileSpawner tileSpawner;
    GameObject newTileRef;
    // Start is called before the first frame update
    void Start()
    {
        life.testingtime();
        TileControllerRef = this.GetComponent<TileController>();
        InputControllerRef = this.GetComponent<InputController>();
        tileSpawner = this.GetComponent<TileSpawner>();
        tileSpawner.Spawn();
        InputControllerRef.InitializeInputController(TileControllerRef);
        HexaGrifRef.GenerateGrid();
        //HexaGrifRef.PrintNeighbours();
        //TileControllerRef.InitializatingGrid(HexaGrifRef);
        TileControllerRef.InitializingTiles();
       // RewardGranted.ReadRewards();
    }
    void SkipTile()
    {
       // AdSystem.O
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
