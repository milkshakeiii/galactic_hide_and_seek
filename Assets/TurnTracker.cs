using UnityEngine;
using System.Collections;

public class TurnTracker : MonoBehaviour {
	
	public delegate void NextTurn(int turnNumber);
	public static event NextTurn onNextTurn;

	private static int turnNumber = 0;
	private static int actionsTaken = 0;

	private static TurnTracker singletonInstance;

	public UnityEngine.UI.Text turnNotificationText;

	void Start()
	{
		InvokeNextTurn ();
	}

	private static TurnTracker GetSingletonInstance()
	{
		if (singletonInstance == null)
			singletonInstance = FindObjectOfType<TurnTracker> ();
		if (singletonInstance == null)
			throw new UnityException ("There is no TurnTracker instance!");
		return singletonInstance;
	}

	public void InstanceInvokeNextTurn()
	{
		TurnTracker.InvokeNextTurn ();
	}

	public static void InvokeNextTurn ()
	{
		turnNumber++;
		actionsTaken = 0;

		GetSingletonInstance ().UpdateNotificationText ();

		if (onNextTurn != null)
			onNextTurn (turnNumber);
	}

	public static void ActionTaken()
	{
		actionsTaken++;

		int actionsAllowed;
		if (HiderTurn())
		{
			actionsAllowed = 2;
		}
		else
		{
			actionsAllowed = 1;
		}

		if (actionsTaken >= actionsAllowed)
		{
			InvokeNextTurn ();
		}
	}

	private void UpdateNotificationText()
	{
		if (HiderTurn())
			turnNotificationText.text = "Hider's turn";
		else
			turnNotificationText.text = "Seeker's turn";
	}

	public static bool HiderTurn()
	{
		return turnNumber % 2 == 1;
	}

	public static bool SeekerTurn()
	{
		return !HiderTurn ();
	}
}
