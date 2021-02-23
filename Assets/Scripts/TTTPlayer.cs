using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTTPlayer : MonoBehaviour
{
    public bool AI;
    
    public GameObject playedCard;
    public GameObject area;
    public TTTGameEvents gE;

    public int score = 0;
    void Awake()
    {
        playedCard = transform.GetChild(0).gameObject;
        area = transform.GetChild(1).gameObject;
        gE = GameObject.Find("GameEvents").gameObject.GetComponent<TTTGameEvents>();
    }
    
    public void DrawCard()
    {
        GameObject newCard = Instantiate(gE.card, new Vector3(0, 0,0), Quaternion.identity, area.transform);
        OrderCards();
        if(!AI) { newCard.GetComponent<TTTCard>().ShowImage(); }
    }
    
    public void Play()
    {
        try { Destroy(playedCard.transform.GetChild(0).gameObject); } catch{}

        Transform card = area.transform.GetChild(Random.Range(0, area.transform.childCount));
        card.GetComponent<RectTransform>().SetParent(playedCard.GetComponent<RectTransform>());
        OrderCards();
        playedCard.GetComponent<TTTCardDrop>().cardId = card.GetComponent<TTTCard>().cardId;
        card.GetComponent<TTTCard>().ShowImage();
        gE.EndTurn();
    }

    public void OrderCards()
    {
        int posX = (area.transform.childCount - 1) * (-80);
        foreach (Transform card in area.transform)
        {
            card.GetComponent<RectTransform>().anchoredPosition = new Vector3(posX, 0, 0);
            posX += 160;
        }
    }
}
