using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TTTCardDrop : MonoBehaviour, IDropHandler
{

    [SerializeField] public Canvas canvas;
    public TTTGameEvents gameEvents;
    public int cardId;
    public void OnDrop(PointerEventData eventData)
    {
        if ((!transform.parent.GetComponent<TTTPlayer>().AI) && eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<TTTCard>().myCard)
        {
            cardId = eventData.pointerDrag.GetComponent<TTTCard>().cardId;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            eventData.pointerDrag.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>());
            transform.parent.GetComponent<TTTPlayer>().OrderCards();
            eventData.pointerDrag.GetComponent<TTTCard>().myCard = false;
            gameEvents.EndTurn();
        }
    }
}
