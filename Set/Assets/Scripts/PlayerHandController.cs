using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    //  FUTURE: PlayerHand is a GLOBAL reference, unless we implement an event system. (cards --> hand)
    public static PlayerHandController instance = null;

    //  FUTURE: if we "unstatic" this, we can probably have an adjustable difficulty
    public static int SET_MAX_CARD_NUM = 3;

    private List<int> m_proposedSet;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyObject(this.gameObject);
        }

        m_proposedSet = new List<int>();
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddCardToProposedSet(int hashCode)
    {
        if(!IsCardInProposedSet(hashCode))
        {
            m_proposedSet.Add(hashCode);

            //  FUTURE : Unless we have a "check cards" button, autocheck proposed set after 3 card draws
            if(m_proposedSet.Count >= SET_MAX_CARD_NUM)
            {
                CheckProposedSet();
            }
        }
    }

    private bool IsProposedSetCorrect()
    {
        if(m_proposedSet.Count >= SET_MAX_CARD_NUM)
        {
            List<CardController> cards = new List<CardController>();

            for(int i = 0; i < SET_MAX_CARD_NUM; ++i)
            {
                GameObject cardObj = BoardController.instance.GetCardObject(m_proposedSet[i]);
                if(cardObj != null && cardObj.GetComponent<CardController>() != null)
                {
                    cards.Add(cardObj.GetComponent<CardController>());
                }
                else
                {
                    Debug.LogWarning("PlayerHandController::IsProposedSetCorrect -- proposed set has a NULL obj's hashcode");
                    break;
                }
            }

            if(cards.Count == SET_MAX_CARD_NUM)
            {
                //  and now for the inevitable check all attributes loop
                int attributeSuccessCount = 0;
                for(int a = 0; a < (int)(CardController.eAttribute.ATTRIBUTE_COUNT); ++a)
                {
                    bool cardsMatch = cards[0].GetAttribute((CardController.eAttribute)(a)) == cards[1].GetAttribute((CardController.eAttribute)(a));
                    int lastCardValue = -1;

                    bool streakBroken = false;
                    for(int c = 2; c < SET_MAX_CARD_NUM; ++c)
                    {
                        int attributeValue = cards[c].GetAttribute((CardController.eAttribute)(a)); 
                        if( (cardsMatch ? lastCardValue == attributeValue : lastCardValue != attributeValue ) )
                        {
                            lastCardValue = attributeValue;
                        }
                        else
                        {
                            streakBroken = true;
                            break;
                        }
                    }

                    if(streakBroken)
                    {
                        break;
                    }
                    else
                    {
                        attributeSuccessCount++;
                    }

                }
                return (attributeSuccessCount == 3);    //  TODO: this '3' should be defined somewhere and NOT hardcoded ._.
            }
        }

        return false;
    }

    //  Handle success/fail cases of IsProposedSetCorrect
    public void CheckProposedSet()
    {
        if(IsProposedSetCorrect())
        {
            //  Remove Cards from board

            //  Add to score

            //  Check/restock board || send event for new cards
        }
        else
        {
           //   lose animation or whatever here
        }

        m_proposedSet.Clear();
    }

    public void RemoveAddFromProposedSet(int hashCode)
    {
        if (IsCardInProposedSet(hashCode))
        {
            m_proposedSet.Remove(hashCode);
        }
    }

    private bool IsCardInProposedSet(int hashCode)
    {
        return m_proposedSet.Contains(hashCode);
    }


}
