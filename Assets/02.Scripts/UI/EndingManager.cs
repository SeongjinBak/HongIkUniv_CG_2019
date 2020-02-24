/*
 * 작성자 : 백성진
 * 
 * 게임 엔딩(결과창) 매니징 클래스 입니다.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    Text result;
    // 결과 문자열 입니다.
    string resultString = "";
    AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        result = GameObject.Find("ResultText").GetComponent<Text>();
        _audio = GetComponent<AudioSource>();
        // BGM 재생 (0.8크기)
        _audio.PlayOneShot(Resources.Load<AudioClip>("pursuit_1_FULL"), 0.8f);
        resultString = 
          
            "사용 연료량 : " + GameManager.instance.usedFuel +
            "\n잔여 연료량 : " + (58937.0f*3f - GameManager.instance.usedFuel).ToString() +
            "\n\n운석 충돌 횟수 : " + GameManager.instance.collisionCount +
            "\n선체 파손도 : " + (1.0f - GameManager.instance.spaceShipHP).ToString() +
            "\n\n1P의 목적지까지 남은 거리 : " + GameManager.instance.distance1 +
            "\n2P의 목적지까지 남은 거리 : " + GameManager.instance.distance2 +
            "\n\n" + (GameManager.instance.winner == PlayerNumber.P1 ? "PLAYER 1" : "PLAYER 2") +"의 목적지에 도달하였습니다."
            ;
       
        // 결과 보여주는 코루틴 호출
        StartCoroutine(ShowResult());

        


    }

    
    // Result Text 출력
    IEnumerator ShowResult()
    {
        int i = 0;
        // 100프레임 대기
        while(i++ < 100)
        {
            yield return null;
        }
        // 결과 스트링을 0.02초 간격으로 한글자씩 출력
        for (i = 0; i < resultString.Length; i++)
        {
            result.text += resultString[i];
            _audio.PlayOneShot(Resources.Load<AudioClip>("PushButton"));    
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(0.1f);
        // 승/패 애니메이션 출력
        PlayAnimation();

    }

    // 승/패 애니메이션 출력
    private void PlayAnimation()
    {
        if(GameManager.instance.winner == PlayerNumber.P1)
        {
            GameObject.Find("P1").GetComponent<Animator>().SetTrigger("Win");
            GameObject.Find("P2").GetComponent<Animator>().SetTrigger("Lose");
        }
        else
        {
            GameObject.Find("P2").GetComponent<Animator>().SetTrigger("Win");
            GameObject.Find("P1").GetComponent<Animator>().SetTrigger("Lose");
        }
    }

    // Restart 버튼
    public void Restart()
    {
        _audio.PlayOneShot(Resources.Load<AudioClip>("PushButton"));
        SceneManager.LoadScene("StartMenu");
    }
}
