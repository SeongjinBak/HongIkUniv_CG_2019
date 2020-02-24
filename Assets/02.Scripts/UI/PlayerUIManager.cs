/*
 * 작성자 : 백성진
 * 
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{

    // 싱글턴
    public static PlayerUIManager instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        lightAnim = GetComponent<Animator>();
        soundSource = GetComponent<AudioSource>();
        soundPlay = false;
    }

    public Animator lightAnim;
    public AudioSource soundSource;


    //p1과 p2의 애니메이터 입니다. (UI_DialogueManager 폴더의 프리팹)
    public Animator p1;
    public Animator p2;

    // 엔진 교체시 사운드 1번출력을 위한 bool 변수
    bool soundPlay;

    // 엔진 위치 바뀌었을 경우  애니메이션, 사운드 출력
    public void EngineLocationChanged()
    {
        p1.SetTrigger("Pointing");
        p2.SetTrigger("Pointing");
        // 엔진 위치 바뀌었을 경우 사운드 출력
        if (!soundPlay)
            StartCoroutine(EngineChangeCoroutine());
    }

    // 엔진 변경시, 사운드/대화창 호출
    IEnumerator EngineChangeCoroutine()
    {
        soundPlay = true;
        // 엔진 교체시 대화창 출력
        MessageQueue.instance.Push(new Message(PlayerNumber.ALL, Circumstance.ENGINECHANGED, 0f));
        StartCoroutine(PlaySound(Resources.Load<AudioClip>("COMPUTER_MALFUNCTION"), 0f));
        yield return new WaitForSeconds(1.1f);
        soundPlay = false;
    }

    // 버튼 입력시 애니메이션, 사운드 출력
    public void PushButton(PlayerNumber playerNumber)
    {
        float val = Random.Range(0.1f, 0.99f);
        if(playerNumber == PlayerNumber.P1)
        {
            if (val > 0.5f)
                p1.SetTrigger("ButtonDown");
            else
                p1.SetTrigger("Pointing");
        }
        else if(playerNumber == PlayerNumber.P2)
        {
            if (val > 0.5f)
                p2.SetTrigger("ButtonDown");
            else
                p2.SetTrigger("Pointing");
        }
        // 랜덤하게 애니메이션 실행
        if(val > 0.5f)
            StartCoroutine( PlaySound(Resources.Load<AudioClip>("PushButton"), 1.5f));
        else
            StartCoroutine(PlaySound(Resources.Load<AudioClip>("PointingButton"), 1.5f));
    }

    // sec초 후에 사운드 출력
    public IEnumerator PlaySound(AudioClip clip, float sec)
    {
        yield return new WaitForSeconds(sec);
        soundSource.PlayOneShot(clip, Random.Range(0.8f, 1f));
    }

    // 운석 충돌시 애니메이션, 메시지 출력
    public IEnumerator CollideWithAsteroid()
    {
        MessageQueue.instance.Push(new Message(PlayerNumber.ALL, Circumstance.ALERT, 0f));

        lightAnim.SetTrigger("Alert");
        StartCoroutine(CollisionAlertSound());
        yield return null;
    }

    // 운석 충돌시 사운드 출력
    public IEnumerator CollisionAlertSound()
    {
        soundSource.PlayOneShot(Resources.Load<AudioClip>("Explosion"), Random.Range(0.8f, 1f));
        soundSource.clip = (Resources.Load<AudioClip>("Siren"));
        soundSource.loop = true;
        soundSource.Play();
        yield return new WaitForSeconds(2.2f);
        soundSource.Stop();
    }


}
