using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{

    private int m_card_id;
    public int CardID { get { return m_card_id; } }

    public Color _DEBUG_SELECTED_COLOR = Color.gray;
    public Color _DEBUG_UNSELECTED_COLOR = Color.white;

    private void Awake()
    {
        m_card_id = gameObject.GetInstanceID();
    }

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
        CardView view = GetCardView();
        CardModel data = gameObject.GetComponent<CardModel>();
        view.GenerateSymbols(data.Attributes);
    }

    public void BTN_OnClick()
    {
        ToggleSelectColor();
    }

    public void ToggleSelectColor()
    {
        if (PlayerHandController.instance.IsCardInProposedSet(CardID))
        {
            ChangeCardColor(_DEBUG_UNSELECTED_COLOR);
            PlayerHandController.instance.RemoveCardFromProposedSet(CardID);
        }
        else
        {
            ChangeCardColor(_DEBUG_SELECTED_COLOR);
            PlayerHandController.instance.AddCardToProposedSet(CardID);
        }
    }

    public void ChangeCardColor(Color colValue)
    {
        GetCardView().ChangeCardColor(colValue);
    }

    protected CardView GetCardView()
    {
        CardView view = this.gameObject.GetComponent<CardView>();
        if(view == null)
        {
            Debug.LogError("CardController::GetCardView -- no view component!");
        }
        return view;
    }

    protected CardModel GetCardModel()
    {
        CardModel model = this.gameObject.GetComponent<CardModel>();
        if (model == null)
        {
            Debug.LogError("CardController::GetCardModel -- no model component!");
        }
        return model;
    }

    public int GetCardAttributeValue(CardModel.eAttribute attribute)
    {
        return GetCardModel().Attributes[(int)attribute];
    }

}
