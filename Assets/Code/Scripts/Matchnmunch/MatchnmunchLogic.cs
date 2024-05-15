using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;


public class MatchnmunchLogic : MonoBehaviour
{

    // visuals mostly
    public GameObject[] LifeDisplay;
    public GameObject[] FoodObjects;
    public Sprite[] Foods;
    public GameObject FoodObj;
    public GameObject BoxBackground;
    public GameObject Box;
    public GameObject BoxFound;
    public Camera cam;
    public float BoxLiftSpeed = 3f;
    public GameObject HoverObj;
    public Sprite HoverGoodSprite;
    public Sprite HoverBadSprite;
    public Text FeedbackText;

    // boring formatting shit
    public int GridWidth = 7;
    public int GridHeight = 7;
    public float FoodMargin = 0.1f;
    public float FoodLeftStart = 0.3f;
    private bool GameStarted = false;
    private float aspect;
    private float worldHeight;
    private float worldWidth;
    private float LeftStart;
    public GameObject TreatParent;
    public GameObject InputBlock;
    public MatchnmunchLogic thislogic;
    public ProgressBar ProgressBar;

    private float IconWidth;
    private float IconHeight;
    private int selectedFood;
    public int winningFood;

    public GameObject startMenu;
    public GameObject scoreScreen;
    public int Lives = 6;
    private int FoodGoal = 0;
    private int FoodFound = 0;
    public int TotalFoodFound = 0;
    public int GoalInt = 12;
    public int FoodLeft = 0;
    private GameObject[,] Grid;
    private GameObject[,] GridBox;
    private GameObject HoverObjGame;
    private float[,] BoxHeightGoal;
    private float StartCountDown = 3;
    private float winningFoodTimer;
    private bool gameRunning = false;

    public List<GameObject> ClickedFoods;

    private int flatscore = 0;
    private int nicefindscore = 0;
    private int gotthemallscore = 0;
    private int totalScore = 0;

    // visual shit
    public TMP_Text LevelCount;
    public TMP_Text foodRemainingTxt;
    public TMP_Text currentScoreTxt;

    public TMP_Text ScoreText;
    public TMP_Text NiceFindText;
    public TMP_Text GotAllText;
    public TMP_Text KibbleWon;
    public TMP_Text KibbleTotal;
    public TMP_Text TotalScoreText;
    public TMP_Text HighscoreText1;
    public TMP_Text HighscoreText2;
    
    // Start is called before the first frame update
    void Start()
    {
        HighscoreText1.SetText(GameDataManager.Instance.mnmhighscore.ToString());
        HighscoreText2.SetText(GameDataManager.Instance.mnmhighscore.ToString());
    }

    public void StartGame()
    {
        gameRunning = true;
        startMenu.SetActive(false);
        for (int i = 0; i < LifeDisplay.Length; i++)
        {
            LifeDisplay[i].SetActive(true);
        };
        foodRemainingTxt.SetText(GoalInt.ToString());
        FoodLeft = GoalInt;
        ClickedFoods.Clear();

        Grid = new GameObject[GridWidth,GridHeight];
        GridBox = new GameObject[GridWidth,GridHeight];
        BoxHeightGoal = new float[GridWidth,GridHeight];

        aspect = (float)Screen.width / Screen.height;
        worldHeight = cam.orthographicSize * 2;
        worldWidth = worldHeight * aspect;
        LeftStart = worldWidth * FoodLeftStart - worldWidth/2;

        HoverObjGame = Instantiate(HoverObj, new Vector3(10,10,10), Quaternion.identity);

        Cursor.visible = false;

        IconWidth = ((worldWidth - FoodMargin - worldWidth * FoodLeftStart) / (GridWidth + 1));
        IconHeight = ((worldHeight - FoodMargin ) / (GridHeight + 1));

        // randomizes the initial boxes' foods
        for(int x = 0; x < GridWidth; x++)
        {
            for(int y = 0; y < GridHeight; y++)
            {
                GameObject Bg = Instantiate(BoxBackground, new Vector3(LeftStart + (x+1) * IconWidth,(y+1) * IconHeight - worldHeight / 2 - FoodMargin,0), Quaternion.identity);
                Bg.transform.SetParent(TreatParent.transform);
                int CurrentItem = Random.Range(0,Foods.Length-1);
                GameObject Clone = Instantiate(FoodObj, new Vector3(LeftStart + (x+1) * IconWidth,(y+1) * IconHeight - worldHeight / 2 - FoodMargin,0), Quaternion.identity);
                Clone.GetComponent<MunchItem>().logic = thislogic;
                SpriteRenderer ChildImg = Clone.GetComponentInChildren<SpriteRenderer>();
                ChildImg.sprite = Foods[CurrentItem];
                Clone.GetComponent<MunchItem>().ItemNumber = CurrentItem;
                Grid[x,y] = Clone;
                Clone.transform.SetParent(TreatParent.transform);
            }
        }

        for(int x = 0; x < GridWidth; x++)
        {
            for(int y = 0; y < GridHeight; y++)
            {
                BoxHeightGoal[x,y] = 0;
                GameObject Clone = Instantiate(Box, new Vector3(LeftStart + (x+1) * IconWidth,(y+1) * IconHeight - worldHeight / 2 - FoodMargin + BoxHeightGoal[x,y],0), Quaternion.identity);
                GridBox[x,y] = Clone;
                Clone.transform.SetParent(TreatParent.transform);
            }
        }
    }

    void SetScore()
    {
        gameRunning = false;
        GotAllText.SetText($"{gotthemallscore:n0} Pts");
        NiceFindText.SetText($"{nicefindscore:n0} Pts");
        ScoreText.SetText($"{flatscore:n0} Pts");
        totalScore = flatscore + nicefindscore + gotthemallscore;
        TotalScoreText.SetText($"{totalScore:n0} Pts");

        ProgressBar.SetProgress(0);

        int kibble = totalScore/50;
        KibbleWon.SetText(kibble.ToString());
        GameDataManager.Instance.AddKibble(kibble);
        KibbleTotal.SetText(GameDataManager.Instance.kibble.ToString());

        if(totalScore> GameDataManager.Instance.mnmhighscore)
        {
            GameDataManager.Instance.mnmhighscore = totalScore;  
        }
        HighscoreText1.SetText(GameDataManager.Instance.mnmhighscore.ToString());
        HighscoreText2.SetText(GameDataManager.Instance.mnmhighscore.ToString());
    }

    void UpdateScore()
    {
        totalScore = flatscore + nicefindscore + gotthemallscore;
        currentScoreTxt.SetText($"{totalScore:n0}");
        FoodLeft -= FoodFound;
        if (FoodLeft < 0)
        {
            FoodLeft = 0;
        }
        foodRemainingTxt.SetText(FoodLeft.ToString());
        float progressamt = (float)(GoalInt-FoodLeft) / (float)GoalInt;
        ProgressBar.SetProgress(progressamt);

        FoodFound += FoodGoal;
        TotalFoodFound = GoalInt - FoodLeft;
        winningFood = 0;
        FoodFound = 0;
        FoodGoal = 0;
        Debug.Log($"Setting progress to {progressamt}\n{TotalFoodFound} food collected out of {GoalInt}");
        if (TotalFoodFound >= GoalInt)
        {
            EndLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            Vector2 MousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            if (StartCountDown > 0)
            {
                StartCountDown -= Time.deltaTime;
            }
            if (winningFoodTimer > 0)
            {
                winningFoodTimer -= Time.deltaTime;
                if (winningFoodTimer <= 0)
                {
                    FeedbackText.enabled = false;
                }
            }
            for(int x = 0; x < GridWidth; x++)
            {
                for(int y = 0; y < GridHeight; y++)
                {
                    if (GridBox[x,y])
                    {
                        GridBox[x,y].transform.position = Vector3.Lerp(GridBox[x,y].transform.position, new Vector3(LeftStart + (x+1) * IconWidth,(y+1) * IconHeight - worldHeight / 2 - FoodMargin + BoxHeightGoal[x,y],0), Time.deltaTime * BoxLiftSpeed);
                        if (GridBox[x,y].transform.position.y > (y+1) * IconHeight - worldHeight / 2 - FoodMargin + 5.5f)
                        {
                            Destroy(GridBox[x,y]);
                        }
                        if (StartCountDown <= 0 && !GameStarted)
                        {
                            BoxHeightGoal[x,y] = 6;
                        }
                    }
                }
            }
            if (StartCountDown <= 0)
            {
                if(!GameStarted && Input.GetMouseButtonDown(0))
                {
                    GameStarted = true;

                    for(int x = 0; x < GridWidth; x++)
                    {
                        for(int y = 0; y < GridHeight; y++)
                        {
                            BoxHeightGoal[x,y] = 5;
                            GameObject Clone = Instantiate(Box, new Vector3(LeftStart + (x+1) * IconWidth,(y+1) * IconHeight - worldHeight / 2 - FoodMargin + BoxHeightGoal[x,y],0), Quaternion.identity);
                            BoxHeightGoal[x,y] = 0;

                            GridBox[x,y] = Clone;
                            if (winningFood == Grid[x,y].GetComponent<MunchItem>().ItemNumber)
                            {
                                FoodGoal += 1;
                            }
                        }
                    }

                }
                if (GameStarted)
                {
                    // FoodGoalText.text = "Found " + FoodFound + " of " + FoodGoal;
                }

                int HoverX = (int)((MousePosition.x + worldWidth * FoodLeftStart - FoodMargin) / IconWidth -1.5f);
                int HoverY = (int)((MousePosition.y + worldHeight/2 - FoodMargin) / IconHeight -0.4f);

                //if (HoverX >= 0 && HoverX < GridWidth && HoverY >= 0 && HoverY < GridHeight)
                //{
                //    SpriteRenderer sprite = HoverObjGame.GetComponent<SpriteRenderer>();
                //    HoverObjGame.transform.position = new Vector3(LeftStart + (HoverX+1) * IconWidth, (HoverY+1) * IconHeight - worldHeight / 2 - FoodMargin,0);
                //    sprite.sprite = HoverGoodSprite;
                //    if (winningFood > 0 && winningFood != Grid[HoverX,HoverY].GetComponent<MunchItem>().ItemNumber)
                //    {
                //        sprite.sprite = HoverBadSprite;
                //    }
                //}
                //else
                //{
                //    HoverObjGame.transform.position = new Vector3(10,10,0);
                //}
                if(Input.GetMouseButtonDown(0))
                {
                    int SelectedX = HoverX;
                    int SelectedY = HoverY;
                    if (Grid[SelectedX,SelectedY])
                    {
                        selectedFood = Grid[SelectedX,SelectedY].GetComponent<MunchItem>().ItemNumber;
                        if (winningFood == 0)
                        {
                            winningFood = selectedFood;
                            for(int x = 0; x < GridWidth; x++)
                            {
                                for(int y = 0; y < GridHeight; y++)
                                    {
                                        if (winningFood == Grid[x,y].GetComponent<MunchItem>().ItemNumber)
                                        {
                                            FoodGoal += 1;
                                        }
                                    }
                            }
                        }
                        if (winningFood == selectedFood)
                        {
                            //Instantiate(BoxFound, new Vector3(LeftStart + (SelectedX+1) * IconWidth, (SelectedY+1) * IconHeight - worldHeight / 2 - FoodMargin,0), Quaternion.identity);
                            FoodFound += 1;
                            flatscore += 100;
                            if (GridBox[SelectedX,SelectedY])
                            {
                                FeedbackText.transform.position = cam.WorldToScreenPoint(new Vector3(LeftStart + (SelectedX+1) * IconWidth, (SelectedY+1) * IconHeight - worldHeight / 2 - FoodMargin,0));
                                FeedbackText.enabled = true;
                                winningFoodTimer = 1f;
                                nicefindscore += 100;
                            }
                            if (FoodFound == FoodGoal)
                            {
                                gotthemallscore += 1000;
                                UpdateScore();
                            }
                            if (TotalFoodFound >= GoalInt)
                            {
                                EndLevel();
                            }
                            for (int x = -1; x <= 1; x++)
                                {
                                    for (int y = -1; y <= 1; y++)
                                    {
                                        if (SelectedX + x >= 0 && SelectedX + x < GridWidth)
                                        {
                                            if (SelectedY + y >= 0 && SelectedY + y < GridHeight)
                                            {
                                                BoxHeightGoal[SelectedX + x,SelectedY + y] = 6;
                                                if (GridBox[SelectedX + x,SelectedY + y])
                                                {
                                                    SpriteRenderer sprite = GridBox[SelectedX + x,SelectedY + y].GetComponent<SpriteRenderer>();
                                                    if (sprite)
                                                    {
                                                        sprite.sortingOrder = 10;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                        }
                        else
                        {
                            if (Lives <= 0)
                            {
                                EndLevel();
                            }
                            else
                            {
                                LifeDisplay[Lives].SetActive(false);
                                Lives -= 1;
                                UpdateScore();
                            }
                        }
                        Debug.Log("SelectedFood = " + selectedFood);
                        Debug.Log("WinningFood = " + winningFood);
                    }
                }
            }
        }
    }

    void EndLevel()
    {
        gameRunning = false;
        scoreScreen.SetActive(true);
        SetScore();
    }
}
