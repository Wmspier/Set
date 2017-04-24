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

    public void AddCardToProposedSet(int cardId)
    {
        if(!IsCardInProposedSet(cardId))
        {
            m_proposedSet.Add(cardId);

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
                    Debug.LogWarning("PlayerHandController::IsProposedSetCorrect -- proposed set has a NULL card's id");
                    break;
                }
            }

            if (cards.Count == SET_MAX_CARD_NUM)
            {
                //  and now for the inevitable check all attributes loop
                int attributeSuccessCount = 0;
                List<int> attributeContainer = new List<int>();
                for (int a = 0; a < (int)(CardModel.eAttribute.QUANTITY); ++a)
                {
                    attributeContainer.Add(cards[0].GetCardAttributeValue((CardModel.eAttribute)(a)));
                    attributeContainer.Add(cards[1].GetCardAttributeValue((CardModel.eAttribute)(a)));
                    bool cardsMatch = attributeContainer[0] == attributeContainer[1];

                    bool streakBroken = false;
                    for (int c = 2; c < SET_MAX_CARD_NUM; ++c)
                    {
                        int attributeValue = cards[c].GetCardAttributeValue((CardModel.eAttribute)(a));
                        
                        if ( cardsMatch ? 
                            attributeContainer[0] == attributeValue 
                            : !attributeContainer.Contains(attributeValue) )
                        {
                            attributeContainer.Add(attributeValue);
                        }
                        else
                        {
                            streakBroken = true;
                            break;
                        }
                    }

                    attributeContainer.Clear();

                    if (streakBroken)
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
            //  Add to score

            //  Remove Cards from board
            //  Check/restock board 
            foreach (int id in m_proposedSet)
            {
                GameObject cardObj = BoardController.instance.GetCardObject(id);
                CardController cardControl = cardObj.GetComponent<CardController>();
                cardControl.RandomizeCardAttributes();
                cardControl.UpdateCardSymbols();
            }

            Debug.Log("YOU GOT ONE");
        }
        else
        {
            //   lose animation or whatever here
            Debug.Log("UGH NO. IDIOT.");
        }

        ClearProposedSet();
    }

    public void ClearProposedSet()
    {
        foreach(int id in m_proposedSet)
        {
            GameObject cardObj = BoardController.instance.GetCardObject(id);
            CardController cardControl = cardObj.GetComponent<CardController>();
            cardControl.ChangeCardColor(cardControl._DEBUG_UNSELECTED_COLOR);
        }
        m_proposedSet.Clear();
    }

    public void RemoveCardFromProposedSet(int cardID)
    {
        if (IsCardInProposedSet(cardID))
        {
            m_proposedSet.Remove(cardID);
        }
    }

    public bool IsCardInProposedSet(int cardID)
    {
        return m_proposedSet.Contains(cardID);
    }


}
