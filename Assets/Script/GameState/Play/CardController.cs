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
	List<NumberCard> arrayFieldCards = new List<NumberCard>();

	public void setCardInfo(List<UserGameData> users, List<NumberCard> cards)
    {
        foreach(UserGameData data in users)
        {
            dicUser.Add(data.no, data);
        }
        
		arrayFieldCards = cards;
    }

	public List<NumberCard> getFieldCards()
    {
		return arrayFieldCards;
    }

    public UserGameData getUserGameData(int no)
    {
        return dicUser[no];
    }

}
