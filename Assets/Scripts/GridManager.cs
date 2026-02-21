using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public CardManager cardManager;
    public GameObject gridContent;
    public GridLayoutGroup gridLayout;

    public int gridx = 0;
    public int gridy = 4;

    public int minOffsetSize = 140;
    public int maxgridSize = 6;
    public float minScaleoffset = 0.25f;

    private List<CardView> totalSpawnedCards = new List<CardView>();

    private void Awake()
    {
        GameManager.Instance.onGameStarted += ArrangeGridSize;
        GameManager.Instance.onRestartGame += ResetOnRestart;
    }

    public void ArrangeGridSize(int x , int y)
    {
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = y;

        float gridSize = minOffsetSize * (1 + (maxgridSize - y) * minScaleoffset);
        
        gridLayout.cellSize = new Vector2(gridSize , gridSize);

        GenerateCards(x , y);
    }

    protected void GenerateCards(int x , int y)
    {
        int totalCardsCount = x * y;
        int totalCardsInOneGroup = 2;
        int cardPair = totalCardsCount / totalCardsInOneGroup;
        List<int> uniqueIds = GenrateUniqueIds(cardPair);

        
        List<int> finalCardIds = new List<int>();

        for (int i = 0; i < uniqueIds.Count; i++)
        {
            finalCardIds.Add(uniqueIds[i]);
            finalCardIds.Add(uniqueIds[i]);
        }

        List<int> uniqueCards = Shuffle(finalCardIds);
        
        for (int i = 0; i < uniqueCards.Count; i++)
        {
            CardView card = cardManager.GetCardById(uniqueCards[i]);
            card.transform.SetParent(gridContent.transform, false);
            card.transform.localScale = Vector3.one;
            card.gameObject.SetActive(true);
            card.ResetObject();
            card.WaitForFake(1.5f);
            totalSpawnedCards.Add(card);
        }
    }

    protected List<int> GenrateUniqueIds(int uniquePair)
    {
        if(uniquePair > cardManager.cardViews.Count)
        {
            Debug.LogWarning("Cards can be repeat as you have low count");
        }
        
        List<int> uniqueIds = new List<int>();

        for (int i = 0; i < uniquePair; i++)
        {
            uniqueIds.Add(GenrateRandomIds(uniqueIds));
        }

        return uniqueIds;
    }

    protected int GenrateRandomIds(List<int> ids)
    {
        int id = Random.Range(0, cardManager.cardViews.Count);
        if (ids.Contains(id) && ids.Count < cardManager.cardViews.Count)
        {
            return GenrateRandomIds(ids);
        }
        return id;
    }

    public List<int> Shuffle(List<int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0 , n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    protected void ResetOnRestart()
    {
        for (int i = totalSpawnedCards.Count - 1; i >= 0; i--)
        {
            cardManager.ReturnToThePool(totalSpawnedCards[i], totalSpawnedCards[i].cardId);
            totalSpawnedCards.RemoveAt(i);
        }
    }
}
