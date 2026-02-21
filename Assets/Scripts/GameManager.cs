using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public Action<int, int> onGameStarted;
	public Action<CardView> onCardSelected;
	public Action onRestartGame;
	public Action cardsGotMatched;
	public Action<int, int> updateScores;
    public Action onGameEnded;

    List<CardView> selectedCards = new List<CardView>();

	bool isCheckingMatching = false;
	private int totalMatches = 0;
	private int totalTurns = 0;

	public int targetMatches = 0;

	private void Awake()
	{
        if (Instance != null)
        {
            Instance = null;
        }
        Instance = this;

        onGameStarted += StartTheGame;
		onCardSelected += CardSelected;
		onRestartGame += RestartTheGame;
		onGameEnded += GameEnded;
    }

	private void StartTheGame(int x, int y)
	{
        targetMatches = (x * y) / 2;
		UpdateScore();
    }

	public void CardSelected(CardView cardView)
	{
		if (isCheckingMatching)
			return;

		if (selectedCards.Count == 0)
		{
			selectedCards.Add(cardView);
			cardView.FlipToReal();
		}
		else
		{
			isCheckingMatching = true;
			StartCoroutine(CheckForMatch(cardView));
		}
	}

	IEnumerator CheckForMatch(CardView cardView)
	{
		if (selectedCards[selectedCards.Count - 1].cardId == cardView.cardId)
		{
			//Card matched
			selectedCards.Add(cardView);
			cardView.FlipToReal();
			yield return new WaitForSeconds(1f);

			for (int i = 0; i < selectedCards.Count; i++)
			{
				selectedCards[i].DisableObject();
			}
			totalMatches++;
			totalTurns++;
			selectedCards = new List<CardView>();
			isCheckingMatching = false;
		}
		else
		{
            //Card not matched
            cardView.FlipToReal();
            yield return new WaitForSeconds(1f);
			for (int i = 0; i < selectedCards.Count; i++)
			{
				selectedCards[i].FlipToFake();
			}
			cardView.FlipToFake();
			selectedCards = new List<CardView>();
            isCheckingMatching = false;
			totalTurns++;
        }

		UpdateScore();

        if (totalMatches == targetMatches)
		{
			onGameEnded.Invoke();
        }
	}
    
	protected void RestartTheGame()
	{
		totalTurns = 0;
		totalMatches = 0;
		selectedCards = new List<CardView>();
		UpdateScore();
    }
    protected void GameEnded()
	{

	}

    public List<CardView> GetSelectedCards()
	{
		return selectedCards;
	}

	public void UpdateScore()
	{
        updateScores.Invoke(totalTurns, totalMatches);
    }
}
