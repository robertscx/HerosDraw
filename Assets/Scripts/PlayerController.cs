using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public int health;
    public int mana;
    public int maxMana;

    public List<CardObject> deck;
    public List<GameObject> hand;
    public List<GameObject> discardPile;
    public GameObject completeCard;
    public int handSizeStart;
    public int handSize;
    public float cardGap;
    public float cardSpeed;

    public Canvas canvas;
    public GameObject deckObject;
    public GameObject handHolder;

    private float cardWidth;

    // Start is called before the first frame update
    void Start()
    {
        handSize = handSizeStart;
        cardWidth = completeCard.GetComponent<RectTransform>().sizeDelta.x * completeCard.GetComponent<RectTransform>().localScale.x;
        
        shuffle();
        for (int i = 0; i < handSize; i++)
        {
            drawCard();
        }
        shiftHand(cardSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        if (handSize != hand.Count)
        {
            handSize = hand.Count;
            shiftHand(cardSpeed / 4);
        }
    }

    public void shuffle()
    {
        deck = deck.OrderBy(i => Guid.NewGuid()).ToList();
    }

    public virtual void shiftCard(RectTransform rt, int i, float cardSpeed)
    {
        if (handSize % 2 == 0)
        {
            rt.DOAnchorPos(new Vector2((-cardWidth / 2 - cardWidth * (handSize / 2 - 1) - cardGap / 2 - cardGap * (handSize / 2 - 1) + cardWidth * i + cardGap * i), -1080/2.1f), cardSpeed);
        }
        else
        {
            rt.DOAnchorPos(new Vector2((-cardWidth * (handSize - 1) / 2 - cardGap * (handSize - 1) / 2 + cardWidth * i + cardGap * i), -1080/ 2.1f), cardSpeed);
        }
    }

    public void shiftHand(float cardSpeed)
    {
        for (int i = 0; i < hand.Count; i++)
        {
            shiftCard(hand[i].GetComponent<RectTransform>(), i, cardSpeed);
        }
    }

    public virtual void drawCard()
    {
        if (deck.Count > 0)
        {
            CardObject drawnCardIdentity = deck[0];
            deck.RemoveAt(0);
            GameObject drawnCard = Instantiate(completeCard, canvas.transform);
            drawnCard.transform.SetParent(handHolder.transform);
            drawnCard.GetComponent<CardBehavior>().cardIdentity = drawnCardIdentity;
            drawnCard.GetComponent<CardBehavior>().isEnemy = false;
            hand.Add(drawnCard);
            RectTransform rt = drawnCard.GetComponent<RectTransform>();
            rt.anchoredPosition = deckObject.GetComponent<RectTransform>().anchoredPosition;
        }
        
    }

}
