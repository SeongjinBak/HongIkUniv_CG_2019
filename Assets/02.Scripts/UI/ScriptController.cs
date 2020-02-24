/*
 * 작성자 : 백성진
 * 
 * 유니티 타임라인에서 사용될 자막을 관리하는 스크립트 입니다.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptController : MonoBehaviour
{
    // 스크립트 번호
    public int index;
    // 자막 표시 부분
    private Text caption;
    // 안전하게 자막 실행을 위한 부울변수
    public bool isTurnOff;

    // Start is called before the first frame update
    void Start()
    {
        index = -1;
        caption = GetComponent<Text>();
        caption.text = "";
        isTurnOff = false;
        // 자막실행
        StartCoroutine(CaptionPlay());
    }

    // 자막 재생 코루틴
    IEnumerator CaptionPlay()
    {
        while (true)
        {
            // 자막에 아무것도 표시하지 않는다.
            if (index == -1)
            {
                caption.text = "";
            }
            if (isTurnOff == false)
            {
                if (index == 0)
                {
                    caption.text = "..2079년 12월 5일 Sombrero Galaxy 근처";
                }
                else if (index == 1)
                {
                    caption.text = "- 뭐하냐 괴물아~. 좌표 설정은 다 했고?.";
                }
                else if (index == 2)
                {
                    caption.text = "- 좌표 설정은 다 했어?.";
                }
                else if (index == 3)
                {
                    caption.text = "- 야 네가 마지막에 컴퓨터 만졌냐?.";
                }
                else if (index == 4)
                {
                    caption.text = "- 엉 왜?.";
                }
                else if (index == 5)
                {
                    caption.text = "- 뭐가 왜야, 이거 컴퓨터 고장 났는데?.";
                }
                else if (index == 6)
                {
                    caption.text = "- 뭐라고?.";
                }
                else if (index == 7)
                {
                    caption.text = "- 어 그럼 지구에 어떻게 가?.";
                }
                else if (index == 8)
                {
                    caption.text = "- 엥 우리 화성으로 가는 거 아니었냐?.";
                }
                else if (index == 9)
                {
                    caption.text = "- 무슨 소리야, 지구로 가자고 했잖아.";
                }
                else if (index == 10)
                {
                    caption.text = "- 아니 화성으로 가기로 했잖아, 무슨 소리야.";
                }
                else if (index == 11)
                {
                    caption.text = "- 아 몰라 일단 우주선에 타라. 빨리 집 가야 해.";
                }
                else if (index == 12)
                {
                    caption.text = "- 이런 애랑 어떻게 같은 팀이 돼서….";
                }
                else if (index == 13)
                {
                    caption.text = "- 그래, 일단 타긴 탈 건데, 가는 건 화성으로 간다~.";
                }
                else if (index == 14)
                {
                    caption.text = "(원래 출발할 때 화성으로 가기로 하긴 했지만…….)";
                }
                else if (index == 15)
                {
                    caption.text = "(... 생각할수록 빡치넹; 왜 성질이야?)";
                }
                else if (index == 16)
                {
                    caption.text = "- 야 지구로 갈 거야, 안갈 거야\n- 억!!!!";
                }
                else if (index == 17)
                {
                    caption.text = "- 쳤냐? 너 눕히고 나 혼자 화성으로 간다.";
                }
                else if (index == 18)
                {
                    caption.text = "- 어후…. 좀 치네?";
                }
                else if(index == 19)
                {
                    index++;
                    // 씬 전환
                    SceneChange();

                }
            }
            //1프레임 대기
            yield return null;
        }
      
    }


    private void SceneChange()
    {
        SceneManager.LoadScene("GameScene");
    }

    // 스킵 버튼
    public void SkipBtn()
    {
        SceneManager.LoadScene("GameScene");
    }
}
