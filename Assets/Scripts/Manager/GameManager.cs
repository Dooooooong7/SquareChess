using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Unity.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public int totalRounds = 2;
    public int currentRound = 0;
    public int []enemiesPerRound = { 4,2,1,0,0 };
    public int []heroesPerRound = { 4,2,2,1,1 };   
    public int totalLevel = 10;
    public int currentLevel = 0;

    public StartButton startButton;
    public StartButton closeButton;
    public StartButton closeButton2;
    public BuffPane buffPane;
    public BuffPane buffPane2;

    public TMP_Text roundText;
    public TMP_Text enemyListText;
    public TMP_Text heroListText;
    private void Start()
    {
        StartCoroutine(StartNewLevel());
    }

    public IEnumerator StartNewLevel()
    {
        while (currentLevel++ <= totalLevel)
        {
            enemyListText.text = "Enemies per round:\n";
            heroListText.text = "Heroes per round:\n";
            for (int i = 0; i < totalRounds; i++)
            {
                enemyListText.text += enemiesPerRound[i] + " ";
                heroListText.text += heroesPerRound[i] + " ";
            }
            currentRound = 0;
            currentLevel++;
            HandManager.Instance.ClearHeroUsed();
            for (int i = 0; i < totalRounds; i++)
            {
                yield return StartCoroutine(StartNewRound());
                if (EnemyManager.Instance.AreAllCellsEmpty()) break;
                currentRound++;
            }
            roundText.text = "Round";
            enemyListText.text = "Enemies per round:\n";
            heroListText.text = "Heroes per round:\n";
            EnemyManager.Instance.ClearChessBoard();
            buffPane2.ShowBuffPanel();    
            yield return StartCoroutine(closeButton2.StartButtonClicked());
        }
    }
    
    private IEnumerator StartNewRound()
    {
        roundText.text = "Round " + (currentRound + 1) + " / " + totalRounds;
        EnemyManager.Instance.ProduceEnemy(enemiesPerRound[currentRound]);
        HandManager.Instance.CreateHeroesThisRound(heroesPerRound[currentRound]);
        yield return StartCoroutine(startButton.StartButtonClicked());
        yield return new WaitForSeconds(2f);
        buffPane.ShowBuffPanel();
        yield return StartCoroutine(closeButton.StartButtonClicked());
    }
    
    private void EndGame()
    {
        // 游戏结束逻辑
        Debug.Log("Game Over");
    }
}
