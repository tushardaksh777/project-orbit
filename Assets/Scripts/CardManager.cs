using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject pool;
    public List<CardView> cardViews = new List<CardView>();

    private Dictionary<int , List<GameObject>> cards = new Dictionary<int, List<GameObject>>();
    public int cardBuffer = 6;

    void Start()
    {
        StartPooling();
    }

    public void StartPooling()
    {
        for (int i = 0; i < cardViews.Count; i++)
        {
            List<GameObject> c = new List<GameObject>();
            for (int j = 0; j < cardBuffer; j++)
            {
                GameObject card = Instantiate(cardViews[i].gameObject, Vector3.zero , Quaternion.identity , pool.transform);
                c.Add(card);
            }
            cards.Add(cardViews[i].cardId, c);
        }
    }

    public CardView GetCardById(int id)
    {
        List<GameObject> cardList = new List<GameObject>();
        cards.TryGetValue(id, out cardList);

        if(cardList.Count > 0)
        {
            GameObject cardObj = cardList[0];
            cards[id].RemoveAt(0);
            return cardObj.GetComponent<CardView>();
        }
        else
        {
            Debug.LogWarning("No more Cards available Increase buffer size");
            return null;
        }
    }

    public void ReturnToThePool(CardView card , int cardId)
    {
        card.gameObject.SetActive(false);
        card.transform.SetParent(pool.transform, false);
        cards[cardId].Add(card.gameObject);
    }
}
