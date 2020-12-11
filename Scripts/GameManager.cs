using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;
    public bool playerTurn;
    public static int stage = 1;        // 스테이지 관리를 위한 배열

    public int playerHP = 200;
    public int playerMP = 100;
    public int initialPlayerHP;
    public int initialPlayerMP;

    public int playerPower = 15;
    public int playerArmor = 5;
    public int initialPlayerPower;
    public int initialPlayerArmor;

    [HideInInspector] public int enemyHP = 100;
    [HideInInspector] public int enemyPower = 10;
    [HideInInspector] public int enemyArmor = 3;


    public int level = 1;
    [HideInInspector] public int allEnemyCount = 2;
    //private float restartLevelDelay = 1.5f;
    private float hideLevelDelay = 1f;
    public Text levelText;
    private GameObject levelImage;
    public bool initialPlayerStats = true;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        //InitGame();
    }

    private void OnLevelWasLoaded(int Index)
    {
        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "Cave_Map" || SceneManager.GetActiveScene().name == "Forest_Map" || SceneManager.GetActiveScene().name == "Tutorial_Map")
        {
            level++;
            enemyHP += 10;
            enemyPower += 3;
            enemyArmor += 1;
            //StartCoroutine("NextStage");
            InitGame();
        }
    }

    /*IEnumerator NextStage()
    {
        yield return new WaitForSeconds(restartLevelDelay);
        InitGame();
    }*/

    void InitGame()
    {
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Stage " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", hideLevelDelay);

        playerTurn = true;
        boardScript.SetupScene(level);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit()
#endif
    }
}
