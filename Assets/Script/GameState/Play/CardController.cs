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

	Dictionary<int, NumberCard> dicFieldCards = new Dictionary<int, NumberCard>();
    Dictionary<int, UserGameData> dicUser = new Dictionary<int, UserGameData>();

    public void setCardInfo(GameCardInfo info)
    {
        foreach(UserGameData data in info.arrayUser)
        {
            dicUser.Add(data.no, data);
        }
        
        dicFieldCards = info.mapFieldCards;
    }

    public Dictionary<int, NumberCard> getFieldCards()
    {
        return dicFieldCards;
    }

    public UserGameData getUserGameData(int no)
    {
        return dicUser[no];
    }

}
