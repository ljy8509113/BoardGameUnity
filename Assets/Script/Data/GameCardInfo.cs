using System;
using System.Collections.Generic;

[Serializable]
public class GameCardInfo {
    public List<UserGameData> arrayUser;
	public Dictionary<int, NumberCard> mapFieldCards;
}
