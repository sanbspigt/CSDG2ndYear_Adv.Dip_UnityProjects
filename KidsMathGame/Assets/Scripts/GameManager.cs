using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameplayState currGameplayState = GameplayState.NONE;
    public static int rangeOfFirstLevelUser = 9;
    public static int rangeOfSecondLevelUser = 20;
    public static int rangeOfThirdLevelUser = 40;
    public static int rangeOfFourthLevelUser = 60;
    public static string currOperator = "";

    int value1;
    int value2;
    int result;

    public int finalV1,finalV2,finalResult;

    [SerializeField]
    GameObject []bottomBarObjs = new GameObject[3];
    [SerializeField]
    GameObject mainMenu, gamePlayScreen,
        levelWin,levelFail;
    [SerializeField]
    TextMeshProUGUI operatorText;
    [SerializeField]
    Transform bottomBarPar;
    
   
    private void Start()
    {
        if (gamePlayScreen.activeInHierarchy)
        { 
            gamePlayScreen.SetActive(false);
            mainMenu.SetActive(true);
            
        }
    }

    public void CheckForLevelComplete()
    {
       
        if (bottomBarPar.childCount != 0) return;

        switch (currGameplayState)
        {
            case GameplayState.ADDITION:
                if (finalResult == result)
                {
                    if ((finalV1 == value1 && finalV2 == value2) ||
                       (finalV1 == value2 && finalV2 == value1))
                    {
                        levelWin.SetActive(true);
                    }
                }
                else
                {
                    CheckForLevelFail();
                }
                break;
            case GameplayState.SUBTRACTION:
                if (finalResult == result)
                {
                    if ((finalV1 == value1 && finalV2 == value2))
                    {
                        levelWin.SetActive(true);
                    }
                }
                else
                {
                    CheckForLevelFail();
                }
                break;
            case GameplayState.MULTIPLICATION:
                if (finalResult == result)
                {
                    if ((finalV1 == value1 && finalV2 == value2) ||
                       (finalV1 == value2 && finalV2 == value1))
                    {
                        levelWin.SetActive(true);
                    }
                }
                else
                {
                    CheckForLevelFail();
                }
                break;
            case GameplayState.DIVISION:
                if (finalResult == result)
                {
                    if ((finalV1 == value1 && finalV2 == value2))
                    {
                        levelWin.SetActive(true);
                    }
                }
                else
                {
                    CheckForLevelFail();
                }
                break;
            case GameplayState.NONE:
                break;
        }
    }

    //For Level Win screen 
    public void GoToNextLevel()
    {
        PlayerPrefs.SetInt("CURR_LEVELS_PLAYED", GetCurrLevelsPlayed() + 1);
        GoToMainMenu();
    }

    //For Level Fail Screen
    public void RestartLevel()
    {
        levelFail.SetActive(false);
        levelWin.SetActive(false);
        gamePlayScreen.SetActive(true);
        mainMenu.SetActive(false);
        SetPrevPosOfDraggables();
    }

    public void GoToMainMenu()
    {
        levelFail.SetActive(false);
        levelWin.SetActive(false);
        gamePlayScreen.SetActive(false);
        mainMenu.SetActive(true);
        SetPrevPosOfDraggables();
    }

    void SetPrevPosOfDraggables()
    {
        for (int i = 0; i < bottomBarObjs.Length; i++)
        {
            bottomBarObjs[i].
                GetComponent<DraggableItem>().ResetPostions();
        }
    }

    public void CheckForLevelFail()
    {
        if (bottomBarPar.childCount !=0) return;

        levelFail.SetActive(true);
    }

    /// <summary>
    /// Generating random values based on user level.
    /// </summary>
    void GenererateRandomValues()
    {
        int playerLevel = GetCurrUserLevel();
        switch (playerLevel)
        {
            case 0:
                value1 = Random.Range(0, rangeOfFirstLevelUser);
                value2 = Random.Range(0, rangeOfFirstLevelUser);
                
                break;
            case 1:
                value1 = Random.Range(0, rangeOfSecondLevelUser);
                value2 = Random.Range(0, rangeOfSecondLevelUser);
                break;
            case 2:
                value1 = Random.Range(0, rangeOfThirdLevelUser);
                value2 = Random.Range(0, rangeOfThirdLevelUser);
                break;
            case 3:
                value1 = Random.Range(0, rangeOfFourthLevelUser);
                value2 = Random.Range(0, rangeOfFourthLevelUser);
                break;

        }
        CalcResult();
        
      //  Debug.Log("Values: v1:"+value1+"  v2: "+value2 +"\tRes: "+result);
    }

    void CalcResult()
    {
        switch (currGameplayState)
        {
            case GameplayState.ADDITION:
                result = value1 + value2;
                break;
            case GameplayState.SUBTRACTION:
                if (value1 >= value2)
                    result = value1 - value2;
                else
                    result = value2 - value1;
                break;
            case GameplayState.MULTIPLICATION:
                result = value1 * value2;
                break;
            case GameplayState.DIVISION:
                result = value1 / value2;
                break;
            case GameplayState.NONE:
                break;
        }
    }

    void SetValuesToUIElements()
    {
        if (Random.value >= 0.5f)
        {
            bottomBarObjs[0].GetComponent<DraggableItem>().SetValueToText(value1);
            bottomBarObjs[1].GetComponent<DraggableItem>().SetValueToText(value2);
            bottomBarObjs[2].GetComponent<DraggableItem>().SetValueToText(result);
        }
        else
        {
            bottomBarObjs[2].GetComponent<DraggableItem>().SetValueToText(value1);
            bottomBarObjs[1].GetComponent<DraggableItem>().SetValueToText(value2);
            bottomBarObjs[0].GetComponent<DraggableItem>().SetValueToText(result);
        }
        operatorText.text = currOperator;
    }

    void SetTheCurrentArithematicOperator()
    {
        switch (currGameplayState)
        {
            case GameplayState.ADDITION:
                currOperator = "+";
                break;
            case GameplayState.SUBTRACTION:
                currOperator = "-";
                break;
            case GameplayState.MULTIPLICATION:
                currOperator = "*";
                break;
            case GameplayState.DIVISION:
                currOperator = "/";
                break;
            case GameplayState.NONE:
                break;
        }
    }


    //Event calls from the buttons
    public void SetCurrGameState(int currGS)
    {
        currGameplayState = (GameplayState)currGS;
        mainMenu.SetActive(false);
        GenererateRandomValues();
        SetTheCurrentArithematicOperator();
        SetValuesToUIElements();
        gamePlayScreen.SetActive(true);
        Debug.Log("Curr Game State: "+currGameplayState);
    }

    int GetCurrUserLevel()
    {
        return PlayerPrefs.GetInt("CURR_USER_LEVEL",0);
    }

    int GetCurrLevelsPlayed()
    {
        return PlayerPrefs.GetInt("CURR_LEVELS_PLAYED",0);
    }

}

public enum GameplayState
{ 
    ADDITION = 0,
    SUBTRACTION = 1,
    MULTIPLICATION =2,
    DIVISION = 3,
    NONE = 4,
}