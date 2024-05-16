using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;


public class MatchnmunchLogic : MonoBehaviour
{

    // visuals mostly
    public GameObject[] LifeDisplay;
    public Sprite[] Foods;

    // boring formatting shit
    private bool Playable = false;
    public GameObject InputBlock;
    public ProgressBar ProgressBar;

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
    public int level = 1;

    // logic
    public GameObject[] GamePieces;
    public List<GameObject> ClickedFoods;
    public List<GameObject> RevealedFoods;
    public int TotalBananas;
    public int TotalCakes;
    public int TotalCarrots;
    public int TotalCheese;
    public int TotalFish;
    public int[] TotalFoodCounts = new int[5];
    public MatchnmunchLogic thislogic;
    public int[] RightEdge = new int[] { 6, 13, 20, 27, 34, 41, 48 };
    public int[] LeftEdge = new int[] { 0, 7, 14, 21, 28, 35, 42 };
    public int[] TopEdge = new int[] { 0, 1, 2, 3, 4, 5, 6 };
    public int[] BottomEdge = new int[] { 42, 43, 44, 45, 46, 47, 48 };

    private int flatCount = 0;
    private int nicefindCount = 0;
    private int gotthemallCount = 0;
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

    public GameObject textPopupParent;
    public TextPopup textPopup;
    
    // Start is called before the first frame update
    void Start()
    {
        HighscoreText1.SetText($"{GameDataManager.Instance.mnmhighscore:n0}");
        HighscoreText2.SetText($"{GameDataManager.Instance.mnmhighscore:n0}");
        textPopup = textPopupParent.GetComponent<TextPopup>();
        for (int i  = 0; i < GamePieces.Length; i++)
        {
            GamePieces[i].GetComponent<MunchItem>().lid = GamePieces[i].transform.Find("lid").gameObject;
            if (!GamePieces[i].transform.Find("lid").gameObject.activeSelf)
            {
                GamePieces[i].transform.Find("lid").gameObject.SetActive(true);
            }
        }
    }

    public void PressedPlay()
    {
        startMenu.SetActive(false);
        scoreScreen.SetActive(false);
        StartCoroutine(PopupTexts());
    }
    public IEnumerator PopupTexts()
    {
        textPopup.SpawnText(2.3f, 0f, $"Level {level}", textPopup.Bluestone, textPopup.BSPink, 120, 2.5f, 2f, 2.5f, 3f);
        yield return new WaitForSeconds(3f);
        textPopup.SpawnText(2.3f, 0f, "3", textPopup.ArialBlack, textPopup.ABGreen, 36, 0.6f, 0.8f, 1.5f, 3f);
        yield return new WaitForSeconds(1);
        textPopup.SpawnText(2.3f, 0f, "2", textPopup.ArialBlack, textPopup.ABGreen, 36, 0.6f, 0.8f, 1.5f, 3f);
        yield return new WaitForSeconds(1);
        textPopup.SpawnText(2.3f, 0f, "1", textPopup.ArialBlack, textPopup.ABGreen, 36, 0.6f, 0.8f, 1.5f, 3f);
        yield return new WaitForSeconds(1);
        textPopup.SpawnText(2.3f, 0f, "Go!", textPopup.Bluestone, textPopup.BSPink, 36, 0.6f, 0.8f, 1.5f, 3f);
        yield return new WaitForSeconds(1);
        StartGame();
        yield break;
    }


    public void StartGame()
    {
        Lives = 6;
        GoalInt = 8 + (level * 4);
        foodRemainingTxt.SetText(GoalInt.ToString());
        Playable = false;
        InputBlock.SetActive(true);
        ProgressBar.SetProgress(0);
        TotalBananas = 0;
        TotalCakes = 0;
        TotalCarrots = 0;
        TotalCheese = 0;
        TotalFish = 0;
        TotalFoodFound = 0;
        flatCount = 0;
        nicefindCount = 0;
        gotthemallCount = 0;
        FoodFound = 0;
        FoodLeft = GoalInt;
        ClickedFoods.Clear();
        RevealedFoods.Clear();

        for (int r = 0; r < GamePieces.Length; r++)
        {
            for (int i = 0; i < RightEdge.Length; i++)
            {
                if (r == RightEdge[i])
                {
                    GamePieces[r].GetComponent<MunchItem>().REdge = true;
                }
                else if (r == LeftEdge[i])
                {
                    GamePieces[r].GetComponent<MunchItem>().LEdge = true;
                }
                if (r == TopEdge[i])
                {
                    GamePieces[r].GetComponent<MunchItem>().TEdge = true;
                }
                else if (r == BottomEdge[i])
                {
                    GamePieces[r].GetComponent<MunchItem>().BEdge = true;
                }
            }
        }

        thislogic = GetComponent<MatchnmunchLogic>();
        startMenu.SetActive(false);
        scoreScreen.SetActive(false);
        for (int i = 0; i < LifeDisplay.Length; i++)
        {
            LifeDisplay[i].SetActive(true);
        };
        for (int i = 0; i < GamePieces.Length; i++)
        {
            RevealedFoods.Add(GamePieces[i]);
            GamePieces[i].GetComponent<MunchItem>().revealed = false;
            GamePieces[i].GetComponent<MunchItem>().selected = false;
        }
        UnityEngine.Cursor.visible = false;

        ShuffleBoxes();
        StartCoroutine(RevealBoxes());
        Playable = true;
        StopCoroutine(RevealBoxes());
    }

    void SetScore()
    {
        GotAllText.SetText($"{gotthemallCount*1000:n0} Pts");
        NiceFindText.SetText($"{nicefindCount*200:n0} Pts");
        ScoreText.SetText($"{flatCount*100:n0} Pts");
        totalScore += (flatCount*100) + (nicefindCount*200) + (gotthemallCount*1000);
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
        HighscoreText1.SetText($"{GameDataManager.Instance.mnmhighscore:n0}");
        HighscoreText2.SetText($"{GameDataManager.Instance.mnmhighscore:n0}");
    }

    void UpdateScore()
    {
        ShuffleBoxes();
        HideBoxes();

        if (FoodFound > 1)
        {
            FoodLeft -= FoodFound;
            TotalFoodFound += FoodFound;
        }

        if (FoodLeft < 0)
        {
            FoodLeft = 0;
            FoodFound += FoodGoal;
        }

        string collectedTreats = "";
        if(FoodFound > 4)
        {
            if (FoodFound > 9)
            {
                collectedTreats += "Fantastic! ";
            }
            else
            {
                collectedTreats += "Great! ";
            }
        }
        collectedTreats += $"{FoodFound} Treats Collected!";
        textPopup.SpawnText(-4f, -4f, collectedTreats, textPopup.ArialBlack, textPopup.ABGreen, 36, 0.3f, 0.2f, 1f, 3f);

        currentScoreTxt.SetText($"{TotalFoodFound*100:n0}");
        foodRemainingTxt.SetText(FoodLeft.ToString());

        float progressamt = (float)(GoalInt-FoodLeft) / (float)GoalInt;
        ProgressBar.SetProgress(progressamt);
        Debug.Log($"{TotalFoodFound} food collected out of {GoalInt}");

        winningFood = -1;
        FoodFound = 0;
        FoodGoal = 0;
        
        if (TotalFoodFound >= GoalInt)
        {
            EndLevel();
        }
    }
    
    public void ClickBox(int box)
    {
        MunchItem clickedbox = GamePieces[box].GetComponent<MunchItem>();
        selectedFood = clickedbox.ItemNumber;
        
        if (winningFood == -1)
        {
            winningFood = selectedFood;
        }
        if (selectedFood == winningFood)
        {
            if (!clickedbox.selected)
            {
                FoodFound += 1; flatCount += 1;

                ClickedFoods.Add(GamePieces[box]);
                clickedbox.selected = true;
                if (ClickedFoods.Count > 1)
                {
                    if (!clickedbox.revealed)
                    {
                        textPopup.SpawnText(clickedbox.transform.position.x, clickedbox.transform.position.y, "Nice find!", textPopup.ArialBlack, textPopup.ABGreen, 36, 0.3f, 0.2f, 1f, 3f);
                        nicefindCount += 1;
                    }
                }
                clickedbox.revealed = true;
                RevealedFoods.Add(GamePieces[box]);

                // if tile is not on an edge
                if (!clickedbox.LEdge && !clickedbox.REdge && !clickedbox.TEdge && !clickedbox.BEdge)
                {
                    GamePieces[box + 1].GetComponent<MunchItem>().revealed = true;
                    GamePieces[box + 6].GetComponent<MunchItem>().revealed = true;
                    GamePieces[box + 7].GetComponent<MunchItem>().revealed = true;
                    GamePieces[box + 8].GetComponent<MunchItem>().revealed = true;
                    GamePieces[box - 1].GetComponent<MunchItem>().revealed = true;
                    GamePieces[box - 6].GetComponent<MunchItem>().revealed = true;
                    GamePieces[box - 7].GetComponent<MunchItem>().revealed = true;
                    GamePieces[box - 8].GetComponent<MunchItem>().revealed = true;

                    RevealedFoods.Add(GamePieces[box + 1]);
                    RevealedFoods.Add(GamePieces[box + 6]);
                    RevealedFoods.Add(GamePieces[box + 7]);
                    RevealedFoods.Add(GamePieces[box + 8]);
                    RevealedFoods.Add(GamePieces[box - 1]);
                    RevealedFoods.Add(GamePieces[box - 6]);
                    RevealedFoods.Add(GamePieces[box - 7]);
                    RevealedFoods.Add(GamePieces[box - 8]);
                }
                else
                {
                    if (clickedbox.REdge)
                    {
                        GamePieces[box - 1].GetComponent<MunchItem>().revealed = true;

                        RevealedFoods.Add(GamePieces[box - 1]);

                        if (!clickedbox.TEdge)
                        {
                            GamePieces[box - 7].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box - 8].GetComponent<MunchItem>().revealed = true;

                            RevealedFoods.Add(GamePieces[box - 7]);
                            RevealedFoods.Add(GamePieces[box - 8]);

                            if (!clickedbox.BEdge)
                            {
                                GamePieces[box + 7].GetComponent<MunchItem>().revealed = true;
                                GamePieces[box + 6].GetComponent<MunchItem>().revealed = true;

                                RevealedFoods.Add(GamePieces[box + 7]);
                                RevealedFoods.Add(GamePieces[box + 6]);
                            }
                        }
                        else
                        {
                            GamePieces[box + 6].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box + 7].GetComponent<MunchItem>().revealed = true;

                            RevealedFoods.Add(GamePieces[box + 6]);
                            RevealedFoods.Add(GamePieces[box + 7]);
                        }
                    }
                    else if (clickedbox.LEdge)
                    {
                        GamePieces[box + 1].GetComponent<MunchItem>().revealed = true;

                        RevealedFoods.Add(GamePieces[box + 1]);

                        if (!clickedbox.TEdge)
                        {
                            GamePieces[box - 7].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box - 6].GetComponent<MunchItem>().revealed = true;

                            RevealedFoods.Add(GamePieces[box - 7]);
                            RevealedFoods.Add(GamePieces[box - 6]);

                            if (!clickedbox.BEdge)
                            {
                                GamePieces[box + 7].GetComponent<MunchItem>().revealed = true;
                                GamePieces[box + 8].GetComponent<MunchItem>().revealed = true;

                                RevealedFoods.Add(GamePieces[box + 7]);
                                RevealedFoods.Add(GamePieces[box + 8]);
                            }
                        }
                        else
                        {
                            GamePieces[box + 8].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box + 7].GetComponent<MunchItem>().revealed = true;

                            RevealedFoods.Add(GamePieces[box + 8]);
                            RevealedFoods.Add(GamePieces[box + 7]);
                        }
                    }
                    else
                    {
                        if (clickedbox.TEdge)
                        {
                            GamePieces[box + 1].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box + 6].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box + 7].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box + 8].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box - 1].GetComponent<MunchItem>().revealed = true;

                            RevealedFoods.Add(GamePieces[box + 1]);
                            RevealedFoods.Add(GamePieces[box + 6]);
                            RevealedFoods.Add(GamePieces[box + 7]);
                            RevealedFoods.Add(GamePieces[box + 8]);
                            RevealedFoods.Add(GamePieces[box - 1]);
                        }
                        else if (clickedbox.BEdge)
                        {
                            GamePieces[box + 1].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box - 1].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box - 6].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box - 7].GetComponent<MunchItem>().revealed = true;
                            GamePieces[box - 8].GetComponent<MunchItem>().revealed = true;

                            RevealedFoods.Add(GamePieces[box + 1]);
                            RevealedFoods.Add(GamePieces[box - 1]);
                            RevealedFoods.Add(GamePieces[box - 6]);
                            RevealedFoods.Add(GamePieces[box - 7]);
                            RevealedFoods.Add(GamePieces[box - 8]);
                        }
                    }
                }
                for (int i = 0; i < RevealedFoods.Count; i++)
                {
                    RevealedFoods[i].transform.Find("lid").gameObject.SetActive(false);
                }

                // got em all
                if (FoodFound == TotalFoodCounts[winningFood])
                { 
                    gotthemallCount += 1; UpdateScore();
                    textPopup.SpawnText(0f, 0f, "Got 'Em All!", textPopup.ArialBlack, textPopup.ABGreen, 36, 0.3f, 0.2f, 1f, 3f);
                }
            }
        }
        else
        {
            UpdateScore();
            Lives -= 1;
            if (Lives < 0)
            {
                EndLevel();
            }
            else
            {
                LifeDisplay[Lives + 1].SetActive(false);
            }
        }
    }

    private IEnumerator RevealBoxes()
    {
        while(true)
        {
            Debug.Log("Starting Coroutine");
            for (int i = 0; i < RevealedFoods.Count; i++)
            {
                RevealedFoods[i].transform.Find("lid").gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(4);
            Debug.Log("Ending Coroutine");
            HideBoxes();
            InputBlock.SetActive(false);
            yield break;
        }
    }

    public void HideBoxes()
    {
        for (int i = 0; i < RevealedFoods.Count; i++)
        {
            RevealedFoods[i].transform.Find("lid").gameObject.SetActive(true);
        }
        for (int i = 0; i < ClickedFoods.Count; i++)
        {
            ClickedFoods[i].GetComponent<MunchItem>().selected = false;
        }
        RevealedFoods.Clear();
        ClickedFoods.Clear();
    }

    void ShuffleBoxes()
    {
        // initial shuffle
        if (!Playable)
        {
            for (int x = 0; x < GamePieces.Length; x++)
            {
                int random = Random.Range(0, Foods.Length);
                GamePieces[x].transform.Find("food").GetComponent<UnityEngine.UI.Image>().sprite = Foods[random];
                GamePieces[x].GetComponent<MunchItem>().ItemNumber = random;
                if (GamePieces[x].GetComponent<MunchItem>().ItemNumber == 0)
                {
                    TotalBananas += 1;
                }
                else if (GamePieces[x].GetComponent<MunchItem>().ItemNumber == 1)
                {
                    TotalCakes += 1;
                }
                else if (GamePieces[x].GetComponent<MunchItem>().ItemNumber == 2)
                {
                    TotalCarrots += 1;
                }
                else if (GamePieces[x].GetComponent<MunchItem>().ItemNumber == 3)
                {
                    TotalCheese += 1;
                }
                else
                {
                    TotalFish += 1;
                }
            }
        }
        // shuffle after finishing batch/making mistake
        else
        {
            for (int x = 0; x < ClickedFoods.Count; x++)
            {
                if (ClickedFoods[x].GetComponent<MunchItem>().ItemNumber == 0)
                {
                    TotalBananas -= 1;
                }
                else if (ClickedFoods[x].GetComponent<MunchItem>().ItemNumber == 1)
                {
                    TotalCakes -= 1;
                }
                else if (ClickedFoods[x].GetComponent<MunchItem>().ItemNumber == 2)
                {
                    TotalCarrots -= 1;
                }
                else if (ClickedFoods[x].GetComponent<MunchItem>().ItemNumber == 3)
                {
                    TotalCheese -= 1;
                }
                else
                {
                    TotalFish -= 1;
                }
                int random = Random.Range(0, Foods.Length);
                ClickedFoods[x].transform.Find("food").GetComponent<UnityEngine.UI.Image>().sprite = Foods[random];
                ClickedFoods[x].GetComponent<MunchItem>().ItemNumber = random;
                if (ClickedFoods[x].GetComponent<MunchItem>().ItemNumber == 0)
                {
                    TotalBananas += 1;
                }
                else if (ClickedFoods[x].GetComponent<MunchItem>().ItemNumber == 1)
                {
                    TotalCakes += 1;
                }
                else if (ClickedFoods[x].GetComponent<MunchItem>().ItemNumber == 2)
                {
                    TotalCarrots += 1;
                }
                else if (ClickedFoods[x].GetComponent<MunchItem>().ItemNumber == 3)
                {
                    TotalCheese += 1;
                }
                else
                {
                    TotalFish += 1;
                }
            }
            for (int y = 0; y < RevealedFoods.Count; y++)
            {
                RevealedFoods[y].transform.Find("lid").gameObject.SetActive(true);
                RevealedFoods[y].GetComponent<MunchItem>().revealed = false;
            }
        }
        ResetTotalFoodCounts();
    }

    void ResetTotalFoodCounts()
    {
        TotalFoodCounts[0] = TotalBananas;
        TotalFoodCounts[1] = TotalCakes;
        TotalFoodCounts[2] = TotalCarrots;
        TotalFoodCounts[3] = TotalCheese;
        TotalFoodCounts[4] = TotalFish;
    }

    void EndLevel()
    {
        Playable = false;
        scoreScreen.SetActive(true);
        ProgressBar.SetProgress(0);
        SetScore();
    }
}
