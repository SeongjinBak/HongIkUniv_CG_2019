/*
 * 작성자 : 백성진
 * 
 * 운석 충돌시 카메라를 흔드는 스크립트 입니다
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    // 카메라의 트랜스폼
    public Transform tr;

    // 초기 좌표와 회전값을 저장
    private Vector3 originPos;
    private Quaternion originRot;


    private void Start()
    {
        tr = GetComponent<Transform>();
        originPos = tr.position;
        originRot = tr.rotation;
    }

    public IEnumerator ShakeCamera(float duration = 0.05f, float magnitudePos = 0.03f, float magnitudeRot = 0.1f) 
    {
        // 지속시간 체크용 변수
        float passTime = 0f;

        while(passTime < duration)
        {
            // 단위 원 내의 위치 랜덤하게 받음
            Vector3 shakePos = Random.insideUnitSphere;
            // 흔들림 크기대로 카메라 위치 변경
            tr.position = shakePos * magnitudePos;

            // 불규칙한 회전 추가
            Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0f));
            tr.rotation = Quaternion.Euler(shakeRot);

            passTime += Time.deltaTime;
            yield return null;
        }
    }

}
