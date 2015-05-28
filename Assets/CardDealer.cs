using UnityEngine;
using System.Collections;

public class CardDealer : MonoBehaviour 
{
	public GameObject[] hiderCards;
	public GameObject[] seekerCards;
	public UnityEngine.UI.Text cardCountDisplay;
	public float cardXSpacing;

	private Queue hiderDeck = new Queue();
	private Queue seekerDeck = new Queue();
	private GameObject[] hiderDeckContents;
	private GameObject[] seekerDeckContents;

	private ArrayList activeCards = new ArrayList();

	void OnEnable()
	{
		TurnTracker.onNextTurn += OnNextTurn;
	}
	
	void OnDisable()
	{
		TurnTracker.onNextTurn -= OnNextTurn;
	}

	void Awake()
	{
		InitializeDecks ();
	}

	private void InitializeDecks()
	{
		FillWithRandomCards (ref hiderDeckContents, hiderCards, 4);
		FillWithRandomCards (ref seekerDeckContents, seekerCards, 4);

		Reshuffle (ref hiderDeck, hiderDeckContents);
		Reshuffle (ref seekerDeck, seekerDeckContents);
	}

	private void FillWithRandomCards(ref GameObject[] fillMe, GameObject[] randomCards, int howManyCards)
	{
		fillMe = new GameObject[howManyCards];
		for (int i = 0; i < fillMe.Length; i++)
		{
			GameObject randomCard = randomCards[UnityEngine.Random.Range(0, randomCards.Length)];
			fillMe[i] = Instantiate(randomCard) as GameObject;
			fillMe[i].transform.parent = gameObject.transform;
			fillMe[i].SetActive(false);
		}
	}

	private void Reshuffle(ref Queue deck, GameObject[] contents)
	{
		ArrayList deckList = new ArrayList();
		for (int i = 0; i < contents.Length; i++)
		{
			if (!contents[i].activeSelf)
				deckList.Add(contents[i]);
		}

		System.Random rng = new System.Random();  
		int n = deckList.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			GameObject value = deckList[k] as GameObject;
			deckList[k] = deckList[n];
			deckList[n] = value;
		}

		deck = new Queue (deckList);
	}

	private void OnNextTurn(int turnNumber)
	{
		ClearActiveCards ();

		DrawCard ();
		DrawCard ();

		UpdateCardCountDisplay ();
	}

	private void ClearActiveCards()
	{
		foreach(GameObject card in activeCards)
		{
			card.SetActive(false);
		}
		activeCards.Clear ();
	}

	private void DrawCard()
	{

		if (TurnTracker.HiderTurn())
		{
			if (hiderDeck.Count == 0)
				Reshuffle (ref hiderDeck, hiderDeckContents);
			activeCards.Add(hiderDeck.Dequeue());
		}
		else
		{
			if (seekerDeck.Count == 0)
				Reshuffle (ref seekerDeck, seekerDeckContents);
			activeCards.Add(seekerDeck.Dequeue());
		}


		PositionActiveCards ();
	}

	private void PositionActiveCards()
	{
		for (int i = 0; i < activeCards.Count; i++)
		{
			GameObject activeCard = activeCards[i] as GameObject;
			activeCard.SetActive(true);
			activeCard.transform.localPosition = new Vector2(i * cardXSpacing, 0);
		}
	}

	private void UpdateCardCountDisplay()
	{
		if (TurnTracker.HiderTurn ())
			cardCountDisplay.text = hiderDeck.Count.ToString ();
		else
			cardCountDisplay.text = seekerDeck.Count.ToString ();
	}
}