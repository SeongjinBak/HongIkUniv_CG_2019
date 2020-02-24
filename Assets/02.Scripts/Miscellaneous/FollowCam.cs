/*
 * 작성자 : 백성진
 * 
 * 게임씬 카메라 스크립트 입니다
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    // 이동속도 제어 변수
    public float moveDamping = 1f;
    // 회전 제어 변수
    public float rotateDamping = 5.0f;
    public float distance = 30f;
    public float height = 30f;
    public float targetOffset = 3f;

    private Transform tr;

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    // 카메라 위치 조정
    private void LateUpdate()
    {
        var camPos = target.position - (target.forward * distance) + (-target.up * height);
        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);
        tr.LookAt(target.position + target.up * targetOffset);
    }
}
