using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TTTEnemy : MonoBehaviour
{
    public GameObject playedCard;
    public GameObject area;
    public TTTGameEvents gE;
    
    private List<TTTCard> _cards;

    private void Awake()
    {
        playedCard = transform.GetChild(0).gameObject;
        area = transform.GetChild(1).gameObject;
        gE = GameObject.Find("GameEvents").gameObject.GetComponent<TTTGameEvents>();
    }

    
}
