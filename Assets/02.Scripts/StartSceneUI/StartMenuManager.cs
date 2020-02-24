/*
 * 작성자 : 백성진
 * 
 * 메인 메뉴 씬 버튼 및 UI 관리 스크립트 입니다.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    private void Awake()
    {
        //Screen.SetResolution(1920, 1080, true);

        //출처: https://samablog.tistory.com/143 [사마사마의 IT이야기]


    }
    // 크레딧 창
    public GameObject credit;
    // 버튼클릭 소리
    public AudioSource _audio;
    // 크레딧창 열기
    public void CreditOpen()
    {
        // 버튼 클릭 사운드 재생
        _audio.PlayOneShot(Resources.Load<AudioClip>("PushButton"));
        credit.SetActive(true);
    }

    // 크레딧창 닫기
    public void CreditClose()
    {
        // 버튼 클릭 사운드 재생
        _audio.PlayOneShot(Resources.Load<AudioClip>("PushButton"));
        credit.SetActive(false);
    }
    
    // 게임시작 버튼
    public void GameStart()
    {
        // 버튼 클릭 사운드 재생
        _audio.PlayOneShot(Resources.Load<AudioClip>("PushButton"));
        SceneManager.LoadScene("GameOpening");
    }
}
