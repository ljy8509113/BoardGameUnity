public class RequestSelectUserCard : RequestBase {
    public string selectUser;
    public int selectIndex;

	public RequestSelectUserCard(string selectUser, int selectIndex, int roomNo) : base(DavinciCommon.IDENTIFIER_SELECT_USER_CARD, DavinciCommon.GAME_CODE, roomNo)
    {
        this.selectUser = selectUser;
        this.selectIndex = selectIndex;
    }
}
