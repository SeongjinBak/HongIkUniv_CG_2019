/*
 * 작성자 : 백성진
 * 운석, 소행성 등의 오브젝트 풀링 매니징 클래스 입니다.
 * 이 클래스에서 오브젝트 풀을 생성 합니다.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolingManager : MonoBehaviour
{
    [Header("Asteroid Pool")]
    // 운석 프리팹
    public GameObject [] asteroidPrefab;
    // 운석의 MAX 개수
    public int maxPool = 30;
    // 운석 풀
    public List<GameObject> asteroidPool;
  

    // 싱글턴
    public static PoolingManager instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        // 운석 오브젝트 풀링 생성함수 호출
        CreatePooling();
        // 운석 개체 생성 코루틴 호출
        StartCoroutine(CreateAsteroid());
    }

    // 오브젝트 풀에서 운석 가져오는 함수
    public GameObject GetAsteroid()
    {
        // 운석의 갯수만큼 루프
        for(int i = 0; i < asteroidPool.Count; i++)
        {
            // 꺼져있는 운석 발견시 가져옴.
            if(asteroidPool[i].activeSelf == false)
            {
                return asteroidPool[i];
            }
        }
        return null;
    }

    // 오브젝트 풀 생성
    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("Asteroids");

        for(int i = 0; i < maxPool; i++)
        {
            // 운석 0~2 하나 선택
            int index = Random.Range(0, 3);
           
            var obj = Instantiate<GameObject>(asteroidPrefab[Random.Range(0,asteroidPrefab.Length)], objectPools.transform);
            obj.name = "Asteroid_" + i.ToString("00");
            obj.SetActive(false);
            asteroidPool.Add(obj);
        }
    }
    
    // 운석체 생성
    private IEnumerator CreateAsteroid()
    {
        WaitForSeconds ws = new WaitForSeconds(0.02f);
        Transform target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        while (true)
        {
            // 게임씬이 아니라면 종료.
            if (SceneManager.GetActiveScene().name != "GameScene")
            {
                break;
            }

            yield return ws;
            // 게임씬이 아니라면 종료.
            if (SceneManager.GetActiveScene().name != "GameScene")
            {
                break;
            }
            // 풀에 가용한 행성이 있다면 가져온다.
            var _asteroid = GetAsteroid();
            if (_asteroid != null)
            {
                // 위치는 랜덤.
                _asteroid.transform.position = (target.position + new Vector3(Random.Range(-0.99f, 0.99f) * Random.Range(40f, 70f), Random.Range(-0.99f, 0.99f) * Random.Range(40f, 70f), Random.Range(-0.99f, 0.99f) * Random.Range(40f, 70f)));
                
                _asteroid.transform.rotation = Quaternion.identity;
                _asteroid.SetActive(true);
            }
        }
    }

    // 안전한 종료를 위해 생성한 함수
    public IEnumerator SetActiveFalse(GameObject gameObject)
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}

    


