/*
 * 작성자 : 백성진
 * 
 * 대화창 매니징 클래스 입니다.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UI_DialogueManager : MonoBehaviour
{


    [Header("ANIMATING")]
    // 1p, 2p의 Dialogue animator 입니다.
    public Animator dialogueAnim1p;
    public Animator dialogueAnim2p;
    // 1p, 2p의 Animator입니다.
    public Animator anim2p;
    public Animator anim1p;

    // 1p, 2p의 Text 입니다.
    public Text text1p;
    public Text text2p;

    [SerializeField]
    private Circumstance currentCircumstance_1;
    private Circumstance currentCircumstance_2;

    // Start is called before the first frame update
    void Start()
    {
        currentCircumstance_1 = Circumstance.DUMMY;
        currentCircumstance_2 = Circumstance.DUMMY;
        StartCoroutine(ManageIterate());
    }

    // 매 0.02초간 반복하며 대화창 관리하는 코루틴
    IEnumerator ManageIterate()
    {
        while (true)
        {
            if (SceneManager.GetActiveScene().name != "GameScene")
            {
                break;
            }
            yield return new WaitForSeconds(0.02f);
            DialogueController();
        }
    }

    // current, 즉 메시지 타입과 우선순위에 따라 적절한 대화창 출력
    IEnumerator Animating(Message current)
    {
        bool isPlaying = false;
        Debug.Log("ANIMATING 실행");

        if(current.player == PlayerNumber.P1)
            currentCircumstance_1 = current.circumstance;
        else if(current.player == PlayerNumber.P2)
            currentCircumstance_2 = current.circumstance;
        else if(current.player == PlayerNumber.ALL)
        {
            currentCircumstance_1 = current.circumstance;
            currentCircumstance_2 = current.circumstance;
        }

        // 이미 플레이중인 애니메이션이 있다면 그 애니메이션 실행 중지
        if (current.player == PlayerNumber.P1 && dialogueAnim1p.GetBool("Moving") == true)
        {
            dialogueAnim1p.SetBool("Moving", false);
            isPlaying = true;
        }
        else if (current.player == PlayerNumber.P2 && dialogueAnim2p.GetBool("Moving") == true)
        {
            dialogueAnim2p.SetBool("Moving", false);
            isPlaying = true;
        }
        else if (current.player == PlayerNumber.ALL)
        {
            if (dialogueAnim1p.GetBool("Moving") == true && dialogueAnim2p.GetBool("Moving") == true)
            {
                dialogueAnim1p.SetBool("Moving", false);
                dialogueAnim2p.SetBool("Moving", false);
                isPlaying = true;
            }
        }

        // 만약 애니메이션이 이미 플레이 중이라면 0.5초 대기(유저에게 메시지 내용 보여주기 위함)
        if (isPlaying)
           yield return new WaitForSeconds(0.5f);

        // 운석충돌상황
        if(current.circumstance == Circumstance.ALERT)
        {
            text1p.text = "운석에 맞았어...! 꽉 잡아!";
            text2p.text = "걱정해 주는척 하지마라";

            anim1p.SetTrigger("HoldOn");
            anim2p.SetTrigger("HoldOn");
        }
        // 엔진 교체 상황
        else if(current.circumstance == Circumstance.ENGINECHANGED)
        {
            text1p.text = "엔진 위치가 변경 되었어.";
            text2p.text = "어쩌라고, 싫으면 내리던가.";
        }
        // 스타트시 메시지1
        else if (current.circumstance == Circumstance.START_1)
        {
            text1p.text = "난 화성으로 간다. 넌 지구?";
        }
        // 스타트 2
        else if (current.circumstance == Circumstance.START_2)
        {
            text2p.text = "그래, 난 지구로 갈 거야.";
        }
        // 스타트 3
        else if (current.circumstance == Circumstance.START_3)
        {
            text1p.text = "나 운전 잘하는데~";
        }
        // 스타트시 4
        else if (current.circumstance == Circumstance.START_4)
        {
            text2p.text = "내가 너보다 더 운전 잘해";
        }
        // 2번째 엔진 꺼진 경우
        else if (current.circumstance == Circumstance.ENGINE_2OFF)
        {
            text1p.text = "2번 엔진 꺼졌다!";
        }
        // 1번째 엔진 꺼진 경우
        else if (current.circumstance == Circumstance.ENGINE_1OFF)
        {
            text2p.text = "1번 엔진에서 연기난다!";
        }
        // 3번째 엔진 꺼진 경우
        else if (current.circumstance == Circumstance.ENGINE_3OFF)
        {
            text2p.text = "3번 엔진 연료 부족!";
        }
        // 같은 버튼 (1번엔진) 누른경우
        else if (current.circumstance == Circumstance.SAMEBUTTON_1)
        {
            text1p.text = "야, 1번 엔진은 내 거야";
            text2p.text = "아 비켜 비켜!";

            anim1p.SetTrigger("Evading");
            anim2p.SetTrigger("Blocking");
        }
        // 같은 버튼 (2번엔진) 누른경우
        else if (current.circumstance == Circumstance.SAMEBUTTON_2)
        {
            text2p.text = "2번 엔진 건들지마.";
            text1p.text = "나도 2번 써야 해";

            anim2p.SetTrigger("Evading");
            anim1p.SetTrigger("Blocking");
        }
        // 같은 버튼 (3번엔진) 누른경우
        else if (current.circumstance == Circumstance.SAMEBUTTON_3)
        {
            text1p.text = "3번 엔진 내 건데~";
            text2p.text = "같이 좀 쓰자";

            anim1p.SetTrigger("Evading");
            anim2p.SetTrigger("Blocking");
        }

        // 1P 메시지 인 경우
        if (current.player == PlayerNumber.P1)
        {
            dialogueAnim1p.SetBool("Moving", true);
        }
        else if (current.player == PlayerNumber.P2)
        {
            dialogueAnim2p.SetBool("Moving", true);
        }
        else if (current.player == PlayerNumber.ALL)
        {
            dialogueAnim1p.SetBool("Moving", true);
            dialogueAnim2p.SetBool("Moving", true);
        }

        // 플레이 중이 아니었던 경우
        if (!isPlaying)
            yield return new WaitForSeconds(2f);
        // 플레이 중이었어서 이전 애니메이션이 꺼졌던 경우
        else
            yield return new WaitForSeconds(2.5f);
        
        // 다시 Moving을 False로 해서 애니메이션 종료
        if (current.player == PlayerNumber.P1 && dialogueAnim1p.GetBool("Moving") == true)
        {
            dialogueAnim1p.SetBool("Moving", false);
        }
        else if (current.player == PlayerNumber.P2 && dialogueAnim2p.GetBool("Moving") == true)
        {
            dialogueAnim2p.SetBool("Moving", false);
        }
        else if (current.player == PlayerNumber.ALL)
        {
            if (dialogueAnim1p.GetBool("Moving") == true && dialogueAnim2p.GetBool("Moving") == true)
            {
                dialogueAnim1p.SetBool("Moving", false);
                dialogueAnim2p.SetBool("Moving", false);
            }
        }


        currentCircumstance_1 = Circumstance.DUMMY;
        currentCircumstance_2 = Circumstance.DUMMY;
        Debug.Log("ANIMATING 종료");
    }


    // 게임에서 축적된 데이터를 바탕으로 대화창 띄우기.
    public void DialogueController()
    {
        // Dispathch 후 우선순위가 dispatch된게 더 높다면, 아니메이터 끄고 다시 새로운거로 키기. 아니라면
        // dispatch된거를 자연적으로 종료할때까지 키기.
        Message current = MessageQueue.instance.Dispatch();
        //Debug.Log(current.circumstance);
        // 우선순위 조사
        if (current.circumstance == Circumstance.DUMMY)
            return;

        if (current.player == PlayerNumber.P1)
        {
            if ((int)current.circumstance < (int)currentCircumstance_1)
                return;
        }
        else if (current.player == PlayerNumber.P2)
        {
            if ((int)current.circumstance < (int)currentCircumstance_2)
                return;
        }
        else if (current.player == PlayerNumber.ALL)
        {
            if ((int)current.circumstance < (int)currentCircumstance_1 || ((int)current.circumstance < (int)currentCircumstance_2))
            {
                return;
            }
        }
        // 모든 조건을 통과했다면 애니메이션 시작
        StartCoroutine(Animating(current));
    }

    
    
}
