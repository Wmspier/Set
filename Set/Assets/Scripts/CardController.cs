using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private void Start()
    {
        GenerateCard();
    }


    public void GenerateCard()
    {
        RandomizeCardAttributes();
        CreateCardView();
    }

    void RandomizeCardAttributes()
    {
        CardModel data = gameObject.GetComponent<CardModel>();
        for (int i = 0; i < data.Attributes.Length; ++i)
        {
            data.Attributes[i] = /*(CardModel.eAttribute)*/Random.Range(0, CardModel.GetAttributeQuantity(i));
        }
    }

    void CreateCardView()
    {
        CardView view = gameObject.GetComponent<CardView>();
        CardModel data = gameObject.GetComponent<CardModel>();
        view.GenerateSymbols(data.Attributes);
    }

    //  Use to select the card as part of your 'set'
    public void Select()
    {
        //  Send event to PlayerHand? w/ self as param
        PlayerHandController.instance.AddCardToProposedSet(this.gameObject.GetHashCode());
    }

    public void Deselect()
    {
        //  Send event to PlayerHand? w/ self as param
        PlayerHandController.instance.RemoveAddFromProposedSet(this.gameObject.GetHashCode());
    }

}
