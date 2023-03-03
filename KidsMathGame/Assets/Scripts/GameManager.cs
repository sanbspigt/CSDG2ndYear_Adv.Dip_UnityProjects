using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    GameObject []bottomBarObjs = new GameObject[3];
    [SerializeField]
    GameObject mainMenu, gamePlayScreen;

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

        result = value1 + value2;
    }

    void SetValuesToUIElements()
    {
        bottomBarObjs[0].GetComponent<DraggableItem>().SetValueToText(value1);
        bottomBarObjs[1].GetComponent<DraggableItem>().SetValueToText(value2);
        bottomBarObjs[2].GetComponent<DraggableItem>().SetValueToText(result);
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

    public void SetCurrGameState(int currGS)
    {
        currGameplayState = (GameplayState)currGS;
        mainMenu.SetActive(false);
        SetTheCurrentArithematicOperator();
        gamePlayScreen.SetActive(true);
        Debug.Log("Curr Game State: "+currGameplayState);
    }

    int GetCurrUserLevel()
    {
        return PlayerPrefs.GetInt("CURR_USER_LEVEL",0);
    }

    void GenerateArthValues()
    { 
        
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