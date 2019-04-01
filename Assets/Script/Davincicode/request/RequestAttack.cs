public class RequestAttack : RequestBase {
    public string selectUser;
    public int selectIndex;
    public int attackValue;

	public RequestAttack(int roomNo, string selectUser, int selectIndex, int attackValue) : base(DavinciCommon.IDENTIFIER_ATTACK, DavinciCommon.GAME_CODE, roomNo)
    {
        this.selectUser = selectUser;
        this.selectIndex = selectIndex;
        this.attackValue = attackValue;
    }
}
