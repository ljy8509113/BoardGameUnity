using System;
using System.Collections.Generic;

public class CardController {

    private static CardController instance = null;
    public static CardController Instance()
    {
        if (instance == null)
            instance = new CardController();

        return instance;
    }

	//Dictionary<int, NumberCard> dicFieldCards = new Dictionary<int, NumberCard>();
    Dictionary<int, UserGameData> dicUser = new Dictionary<int, UserGameData>();
	List<Card> arrayFieldCards = new List<Card>();

	public void setCardInfo(List<UserGameData> users, List<Card> cards)
    {
        foreach(UserGameData data in users)
        {
            dicUser.Add(data.no, data);
        }
        
		arrayFieldCards = cards;
    }

	public List<Card> getFieldCards()
    {
		return arrayFieldCards;
    }

    public UserGameData getUserGameData(int no)
    {
        return dicUser[no];
    }

}
