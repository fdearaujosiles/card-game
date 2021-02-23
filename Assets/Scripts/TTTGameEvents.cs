using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTTGameEvents : MonoBehaviour
{
    public GameObject card;
    public TTTPlayer[] players;
    public GameObject[] slots;
    public GameObject text;
    public GameObject button;
    
    public int currentPlayer;
    public int playerScore;
    public int enemyScore;
    public float _scaleFactor;

    public bool cardHighlighted;
    private void Start()
    {
        NewGame();
        cardHighlighted = false;
    }
    
    public void NewGame()
    {
        foreach (TTTPlayer player in players)
        {
            for (int i = 0; i < 5; i++) { player.DrawCard(); }
        }
        
        button.SetActive(false);
        foreach (GameObject slot in slots)
        {
            foreach (Transform child in slot.transform) {
                Destroy(child.gameObject);
            }
        }
    }

    public void ResolveMatch()
    {
        int card1 = slots[0].GetComponent<TTTCardDrop>().cardId;
        int card2 = slots[1].GetComponent<TTTCardDrop>().cardId;

        Debug.Log($"card1: {card1} card2: {card2}");
        
        if (card1 == 0)
        {
            playerScore += card2 == 2 ? 1 : 0;
            enemyScore += card2 == 1 ? 1 : 0;
        }
        else if (card1 == 1)
        {
            playerScore += card2 == 0 ? 1 : 0;
            enemyScore += card2 == 2 ? 1 : 0;
        }
        else
        {
            playerScore += card2 == 1 ? 1 : 0;
            enemyScore += card2 == 0 ? 1 : 0;
        }

        text.GetComponent<Text>().text = $"Player: {playerScore}\nEnemy: {enemyScore}";
        if (players[0].area.transform.childCount == 0)
        {
            button.SetActive(true);
        } ;
    }
    
    public void EndTurn()
    {
        
        currentPlayer = currentPlayer == players.Length - 1 ? 0 : currentPlayer + 1;
        
        if (players[currentPlayer].AI)
        {
            players[currentPlayer].GetComponent<TTTPlayer>().Play();
        }
        else
        {
            ResolveMatch();
        }

    }
    
}
