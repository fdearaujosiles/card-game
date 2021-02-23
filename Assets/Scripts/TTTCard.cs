using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TTTCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    public int cardId;
    public bool myCard;
    public int effect;
    public bool visible;

    private GameObject _playerArea;
    private TTTGameEvents _gE;

    [SerializeField] private Canvas canvas;
    private RectTransform _rect;
    private Image _img;
    private CanvasGroup _canvasGroup;
    private Vector2 _oldPosition;
    private float _scaleFactor;
    void Awake()
    {
        cardId = Random.Range(0, 3);
        visible = false;
        _rect = GetComponent<RectTransform>();
        _img = GetComponent<Image>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _playerArea = GameObject.Find("PlayerArea");
        _gE = GameObject.Find("GameEvents").GetComponent<TTTGameEvents>();

        _scaleFactor = _gE._scaleFactor;
        
        myCard = transform.parent == _playerArea.transform;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void ShowImage()
    {
        string spr = "";
        switch (cardId)
        {
            case 0: spr = "R";
                break;
            case 1: spr = "P";
                break;
            case 2: spr = "S";
                break;
        }
        _img.sprite = Resources.Load<Sprite>($"Cards/{spr}");
        visible = true;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!myCard) return;
        _oldPosition = _rect.anchoredPosition;
        _canvasGroup.alpha = .4f;
        _canvasGroup.blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!myCard) return;
        _rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1;
        if (myCard) { _rect.anchoredPosition = _oldPosition; }
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (myCard && (!_gE.cardHighlighted))
        {
            _rect.localScale = new Vector3(_scaleFactor, _scaleFactor, 0);
            _rect.anchoredPosition = new Vector3(_rect.anchoredPosition.x, (_rect.rect.height * (_scaleFactor - 1)) / 2 ,0);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (myCard && (!_gE.cardHighlighted))
        {
            _rect.localScale = new Vector3(1 , 1, 0);
            _rect.anchoredPosition = new Vector3(_rect.anchoredPosition.x, 0 ,0);
        }
    }
}
