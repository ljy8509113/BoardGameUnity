using System.Collections;
using System.Collections.Generic;

public class RequestSelectFieldCard : ResponseBaseDavincicode {
    int index;
	public RequestSelectFieldCard(int index) : base(Common.IDENTIFIER_SELECT_FIELD_CARD)
    {
        this.index = index;
    }
}
