/*
 * 작성자 : 백성진
 * 
 * Message 형식 클래스 입니다.
 * 대화창 메시징에 쓰이는 클래스 입니다
 */

public class Message
{
    // 누가 애니메이팅 돼야 하는지
    public PlayerNumber player;
    // 어떤 상황인지
    public Circumstance circumstance;
    // 전송해야 할 시간
    public float dispatchTime;
    public bool isCalled = false;

    // 생성자
    public Message(PlayerNumber playerNum, Circumstance circumstanceInfo, float inputTime)
    {
        player = playerNum;
        circumstance = circumstanceInfo;
        dispatchTime = inputTime;
    }
   
}