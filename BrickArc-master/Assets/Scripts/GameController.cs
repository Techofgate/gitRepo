using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Text gameText;
    private bool gameState;

    public Text levelDescriptionText;
    public string[] levelDescription;
    
    public GameObject ballPrefab;
    public GameObject[] levelMaps;
    public int[] levelMapsBrickN;

    private GameObject ball;
    private GameObject levelMap;
    private int level;
    private int brickN;


    // Use this for initialization
    void Start()
    {
        if (levelMaps.Length == 0)
            Debug.LogError("No level map.");
        if (levelMaps.Length != levelMapsBrickN.Length)
            Debug.LogError("The number of levelMaps and the number of levelMapsBrickN isn't equal.");

        gameText.text = "摁任意键开始游戏\nEsc键退出";
        levelDescriptionText.text = "";
        gameState = false;

        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
            return;
        }
        if (gameState)
        {
            if (brickN <= 0)
            {
                if (!EnterNextLevel())
                {
                    EndGame();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && level > 0)
            {
                ReStartLevel();
            }
            else if (Input.anyKeyDown)
            {
                StartGame();
            }
        }
    }

    public void BrickNumberReduce()
    {
        brickN--;
    }

    bool EnterNextLevel()
    {
        if (level == levelMaps.Length)
        {
            level++;
            return false;
        }

        DestroyLevel();

        level++;
        LoadLevel();

        if (levelDescription.Length >= level)
            levelDescriptionText.text = levelDescription[level - 1];
        else
            levelDescriptionText.text = "";

        return true;
    }

    void StartGame()
    {
        level = 1;
        LoadLevel();

        gameText.text = "";
        if (levelDescription.Length >= 1)
            levelDescriptionText.text = levelDescription[0];
        else
            levelDescriptionText.text = "";
        gameState = true;
    }

    public void EndGame()
    {
        DestroyLevel();
        if (level > levelMaps.Length)
        {
            gameText.text = "恭喜你闯过了最后一关!\n摁任意键重新游戏\nEsc键退出";
            level = 0;
        }
        else
            gameText.text = "你死在了第 " + level.ToString() + " 关\n鼠标左键重新本关\n摁其它任意键重新游戏\nEsc键退出";
        levelDescriptionText.text = "";
        gameState = false;
    }

    void ReStartLevel()
    {
        LoadLevel();

        gameText.text = "";
        if (levelDescription.Length >= level)
            levelDescriptionText.text = levelDescription[level - 1];
        else
            levelDescriptionText.text = "";
        gameState = true;
    }

    void LoadLevel()
    {
        if (level == 0)
            return;
        ball = Instantiate(ballPrefab);
        levelMap = Instantiate(levelMaps[level - 1]);
        levelMap.SetActive(true);
        brickN = levelMapsBrickN[level - 1];
    }

    void DestroyLevel()
    {
        ball.GetComponent<Ball>().Active = false;
        Destroy(ball);
        Destroy(levelMap);
    }

    // For Button
    public void LoadLevel(int _level)
    {
        if (gameState)
            DestroyLevel();

        level = _level;
        LoadLevel();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
}
