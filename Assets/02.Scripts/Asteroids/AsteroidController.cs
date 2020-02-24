/*
 * 작성자 : 백성진
 * 
 * 플레이어를 방해하는 오브젝트인 운석을 컨트롤하는 클래스 입니다
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidController : MonoBehaviour
{
    Transform target;

    
    private void OnEnable()
    {
        //collideWithShip.SetActive(false);
        target = GameObject.Find("SpaceShip").GetComponent<Transform>();
        float distance = Vector3.Distance(target.position, transform.position);
        
         AsteroidRotate();
         StartCoroutine(AsteroidMoving());

    }
    
    // 운석 회전
    private void AsteroidRotate()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Random.Range(0.1f, 0.99f);
    }

    // 운석 이동
    private IEnumerator AsteroidMoving()
    {
        WaitForSeconds ws = new WaitForSeconds(0.02f);
        // 운석 속도 지정
        float moveSpeed = Random.Range(1f, 2.5f);
        while (true)
        {
            
            yield return ws;

            // 카메라와 거리 멀어지면 비활성화
            if(Vector3.Distance(target.position, transform.position) > 75f)
            {
                gameObject.SetActive(false);
            }
            // 이동
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 우주선과 충돌
        if (collision.transform.CompareTag("Player"))
        {

            Collide();
            gameObject.SetActive(false);
        }

    }

    public void Collide()
    {
        // 충돌시 UI 시행
        StartCoroutine(PlayerUIManager.instance.CollideWithAsteroid());
        // 화면 흔들림 추가
        StartCoroutine(GameObject.Find("Main Camera").GetComponent<ShakeCam>().ShakeCamera(.3f, 3f, 0.4f));
        // 파티클 시행
        GameObject g = Resources.Load<GameObject>("CollideShipAndAsteroid");
        var obj = Instantiate<GameObject>(g, transform.position, Quaternion.identity);
        PoolingManager.instance.StartCoroutine(PoolingManager.instance.SetActiveFalse(obj));

         // 우주선 체력, 충돌시 우주선 체력이 줄어든다.
        GameManager.instance.spaceShipHPImg.fillAmount -= 0.2f;
        GameManager.instance.spaceShipHP -= 0.2f;
        GameManager.instance.collisionCount++;
         
    }
 
}
