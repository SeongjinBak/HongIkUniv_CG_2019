using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //버튼 중복을 막기위한 변수
    public int player1Button = 0;
    public int player2Button = 0;
    // 화살표 방향을 위한 벡터
    Vector3 dirToTarget;
    // 화살표 오브젝트
    public GameObject pointer1;
    // 카메라 오브젝트
    public GameObject mainCam;

    // 거리를 보여주기 위한 text
    public Text Playe1LeftDistance;
    public Text Playe2LeftDistance;

    public float gameTime = 68.0f;
    // 남은 시간
    public Text LeftTime;

    //연료의 남은 양
    public Image fuelLeft;
    //우주선 체력
    public Image spaceShipHPImg;

    public bool coolingDown;
    // 전체 연료량
    public float waitTime = 1000.0f;
    //사용한 연료량
    public float usedFuel;

    // 우주선 선체 HP
    public float spaceShipHP = 100f;
    // 엔진 소모량
    public float[] spaceShipEngines = new float[3] { 0f, 0f, 0f };
    // 행성까지의 거리
    public float[] remainingDistances = new float[2];
    // 운석 충돌 횟수
    public float collisionCount = 0f;

    // 1p의 목적지
    public GameObject planet1;
    // 2p의 목적지
    public GameObject planet2;
    // 우주선의 좌표
    public GameObject spaceShip;
    // 1p 목적지까지 남은 거리
    public float distance1;
    // 2p 목적지까지 남은 거리
    public float distance2;

    // 승리자
    public PlayerNumber winner;

    // 싱글턴
    public static GameManager instance = null;
    private void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy (this.gameObject);
        DontDestroyOnLoad (this.gameObject);

    }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        //  if (coolingDown == true) {
        //     fuelLeft.fillAmount -= usedFuel * 100 / waitTime * Time.deltaTime;
        // }

        if (SceneManager.GetActiveScene().name != "GameScene")
        {
            return;
        }
        if(distance1 > distance2)
        {
            winner = PlayerNumber.P2;
        }
        else
        {
            winner = PlayerNumber.P1;
        }
        dirToTarget = planet1.transform.position - mainCam.transform.position;
            pointer1.transform.forward = dirToTarget.normalized;

            Vector3 tmp = new Vector3(30f, 0f, 0f);

            //행성 1까지 남은 거리
            distance1 = Vector3.Distance(planet1.transform.position, spaceShip.transform.position);
            //행성 2까지 남은 거리
            distance2 = Vector3.Distance(planet2.transform.position, spaceShip.transform.position);
            // 시간을 계속 계산해준다.
            gameTime -= Time.deltaTime;
            // 남은 시간 화면에 표시
            int intTime = (int)gameTime;
            LeftTime.text = intTime.ToString();

            if (intTime % 10 == 0 && intTime > 0)
            {
                GameObject.Find("SpaceShip").GetComponent<EngineLocate>().RandomNumber();
            }
            // 시간 종료되면 게임 종료
            if (((int)gameTime) <= 0)
            {
                SceneManager.LoadScene("GameEnding");
            }
            // 남은 거리를 string으로 출력
            Playe1LeftDistance.text = distance1.ToString();
            Playe2LeftDistance.text = distance2.ToString();
        
        

    }

}