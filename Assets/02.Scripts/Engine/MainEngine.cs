using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainEngine : MonoBehaviour {

    Rigidbody rb;

    //엔진 사용하고 있는지
    public int[] engineOn = { 0, 0, 0 };
    public int[] engineOn2p = { 0, 0, 0 };

    //로켓의 속도
    public float rocketSpeed;
    //연기와 불꽃 파티클
    GameObject flame;
    GameObject smoke;
    //사용한 연료량

    int[] dup = { 0, 0, 0 };

    Vector3 forceDir;

    //엔진1 연료 남은 양 이미지
    public Image EnginefuelLeft;

    // 전체 연료량
    public float EnginewaitTime = 1000.0f;

    // 유저가 어떤 버튼을 눌렀는지 확인하는 배열
    public int[] btnClick = new int[3] { -1, -1, -1 };


    void Start () {
        //rb = this.gameObject
        rb = this.gameObject.GetComponentInParent<Rigidbody> ();
        rocketSpeed = 2f;
        //  forceDir = new Vector3 (0, 10000.0f * rocketSpeed, 0);
        // rb.AddRelativeForce (forceDir);

        // 불꽃과 연기 파티클을 받아온다
        flame = this.gameObject.transform.GetChild (4).gameObject;
        smoke = this.gameObject.transform.GetChild(5).gameObject;
    }

    // Update is called once per frame
    void Update () {

        // 1st Fueltank
        if (this.gameObject.name == "1st_FuelTanker") {

            if (EnginefuelLeft.fillAmount == 0) {
                // 만약 연료를 다 사용하면 연기를 키고, 불꽃을 삭제한다.
                flame.SetActive (false);
                smoke.SetActive (true);
                if(dup[0] == 0)
                {
                    // 메시징 추가
                    MessageQueue.instance.Push(new Message(PlayerNumber.P2, Circumstance.ENGINE_1OFF, .5f));
                    dup[0] = 1;
                }

                // 엔진을 다 꺼준다.
                engineOn[0] = 0;
                engineOn2p[0] = 0;
                GameManager.instance.player1Button = 0;
                GameManager.instance.player2Button = 0;
            } else {
                //1p의 버튼 Q
                if (Input.GetKeyDown (KeyCode.Q) && GameManager.instance.player1Button == 0) {

                    // 엔진을 사용중으로 바꾼다.
                    engineOn[0] = 1;
                    GameManager.instance.player1Button = 1;
                    // 1p로 사운드, 애니메이션 코드 실행
                    PlayerUIManager.instance.PushButton (PlayerNumber.P1);
                } else if (Input.GetKeyUp (KeyCode.Q) && engineOn[0] == 1) {
                    //엔진을 미사용중으로 바꾼다.
                    engineOn[0] = 0;
                    GameManager.instance.player1Button = 0;
                }

                //2p의 버튼 I
                if (Input.GetKeyDown (KeyCode.I) && GameManager.instance.player2Button == 0) {
                    //엔진을 사용중으로 바꾼다.
                    engineOn2p[0] = 1;
                    GameManager.instance.player2Button = 1;
                    // 1p로 사운드, 애니메이션 코드 실행
                    PlayerUIManager.instance.PushButton (PlayerNumber.P2);
                } else if (Input.GetKeyUp (KeyCode.I) && engineOn2p[0] == 1) {
                    // 엔진을 미사용중으로 바꾼다.
                    engineOn2p[0] = 0;
                    GameManager.instance.player2Button = 0;
                }

                if (engineOn[0] == 1 || engineOn2p[0] == 1) {
                    Debug.Log ("Engine1 is On");
                    //만약 같은 엔진이 눌려있을 경우
                    flame.SetActive (true);
                    // 연료 이미지를 빨갛게 바꾼다.
                    EnginefuelLeft.color = Color.red;
                    // 동일 버튼 클릭시
                    if (engineOn[0] == 1 && engineOn2p[0] == 1 && btnClick[0] == -1)
                    {
                        //메세징 조건문
                        btnClick[0]++;
                        // 메시징 추가
                        MessageQueue.instance.Push(new Message(PlayerNumber.ALL, Circumstance.SAMEBUTTON_1, 0f));
                    }

                    // 연료 사용량 반영
                    EnginefuelLeft.fillAmount -= 20 / EnginewaitTime * Time.deltaTime;
                    GameManager.instance.usedFuel += 20.0f;


                    // 연료 위치에 따라 힘을 주는 벡터 방향 바꿈
                    if (this.gameObject.transform.localPosition == new Vector3 (0, 0, 0)) {
                        forceDir = new Vector3 (0, rocketSpeed, 0);
                    } else if (this.gameObject.transform.localPosition.x == -0.7f) {
                        
                        if (this.gameObject.transform.localPosition.z == -1.32f)
                            forceDir = new Vector3 (rocketSpeed, 0, 0.5f * rocketSpeed);
                        else if (this.gameObject.transform.localPosition.z == 0.0f)
                            forceDir = new Vector3 (rocketSpeed, 0, 0.5f * rocketSpeed);

                    } else if (this.gameObject.transform.localPosition.z == -0.64f) {

                        if (this.gameObject.transform.localPosition.x == 0.0f)
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, rocketSpeed);
                        else if (this.gameObject.transform.localPosition.x == 1.34f)
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, rocketSpeed);
                    }
                    else if (this.gameObject.transform.localPosition.z == 0.54f) {

                        if (this.gameObject.transform.localPosition.x == 0.0f) {
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, -1.0f*rocketSpeed);
                        } else if (this.gameObject.transform.localPosition.x == 1.34f) {
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, -1.0f*rocketSpeed);
                        }
                    }
                    else if (this.gameObject.transform.localPosition.x == -0.66f) {

                        if (this.gameObject.transform.localPosition.z == 1.34f)
                            forceDir = new Vector3 (-1.0f*rocketSpeed, 0, 0.5f * rocketSpeed);
                        else if (this.gameObject.transform.localPosition.z == 0.02f)
                            forceDir = new Vector3 (-1.0f*rocketSpeed, 0, 0.5f * rocketSpeed);
                    }

                    rb.AddRelativeForce (forceDir);
                } else {
                    //연료 사용량 색 변화
                    EnginefuelLeft.color = Color.cyan;
                    //엔진 불꽃 꺼버린다.
                    flame.SetActive (false);
                    //메세징 조건문
                    btnClick[0] = -1;
                }
            }
        }

        if (this.gameObject.name == "2nd_FuelTanker") {
            //연료 다 사용할 시

            if (EnginefuelLeft.fillAmount == 0) {
                //불꽃 삭제 및 연기 발생
                flame.SetActive (false);
                smoke.SetActive (true);
                // 메시징 추가
                if (dup[1] == 0)
                {
                    dup[1] = -1;
                    MessageQueue.instance.Push(new Message(PlayerNumber.P1, Circumstance.ENGINE_2OFF, .5f));
                }
                // 엔진 사용 초기화
                engineOn[1] = 0;
                engineOn2p[1] = 0;
                GameManager.instance.player1Button = 0;
                GameManager.instance.player2Button = 0;
            } else {

                //1p의 버튼 W
                if (Input.GetKeyDown (KeyCode.W) && GameManager.instance.player1Button == 0) {
                    //엔진 사용중으로 바꿈
                    engineOn[1] = 1;

                    GameManager.instance.player1Button = 1;
                    // 임시로 1p로 사운드, 애니메이션 코드 넣어놓음.
                    PlayerUIManager.instance.PushButton (PlayerNumber.P1);
                } else if (Input.GetKeyUp (KeyCode.W) && engineOn[1] == 1) {
                    // 엔진 미사용중으로 바꿈
                    engineOn[1] = 0;

                    GameManager.instance.player1Button = 0;
                }
                //2p의 버튼 O
                if (Input.GetKeyDown (KeyCode.O) && GameManager.instance.player2Button == 0) {
                    //엔진 사용중으로 바꿈.
                    engineOn2p[1] = 1;

                    GameManager.instance.player2Button = 1;
                    // 임시로 1p로 사운드, 애니메이션 코드 넣어놓음.
                    PlayerUIManager.instance.PushButton (PlayerNumber.P2);
                } else if (Input.GetKeyUp (KeyCode.O) && engineOn2p[1] == 1) {
                    //엔진 미사용중으로 바꿈
                    engineOn2p[1] = 0;

                    GameManager.instance.player2Button = 0;
                }
                

                if (engineOn[1] == 1 || engineOn2p[1] == 1) {
                    //엔진 불꽃 킨다.
                    flame.SetActive (true);

                    Debug.Log ("W is On");
                    //엔진 색 바꾼다.
                    EnginefuelLeft.color = Color.red;

                    if (engineOn[1] == 1 && engineOn2p[1] == 1 && btnClick[1] == -1)
                    {
                        //메세징 조건
                        btnClick[1] = 0;
                        // 메시징 추가
                        MessageQueue.instance.Push(new Message(PlayerNumber.ALL, Circumstance.SAMEBUTTON_2, 0f));
                    }
                   

                    // 연료 사용량 반영

                    EnginefuelLeft.fillAmount -= 20 / EnginewaitTime * Time.deltaTime;
                    GameManager.instance.usedFuel += 20.0f;


                    //연료통 위치 따라 방향 정해준다.
                    if (this.gameObject.transform.localPosition == new Vector3 (0, 0, 0)) {
                        forceDir = new Vector3 (0, rocketSpeed, 0);
                    } else if (this.gameObject.transform.localPosition.x == -0.7f) {

                        if (this.gameObject.transform.localPosition.z == -1.32f)
                            forceDir = new Vector3 (rocketSpeed, 0, 0.5f * rocketSpeed);
                        else if (this.gameObject.transform.localPosition.z == 0.0f)
                            forceDir = new Vector3 (rocketSpeed, 0, 0.5f * rocketSpeed);

                        //forceDir = new Vector3 (rocketSpeed, 0, 0);

                    } else if (this.gameObject.transform.localPosition.z == -0.64f) {

                        if (this.gameObject.transform.localPosition.x == 0.0f)
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, rocketSpeed);
                        else if (this.gameObject.transform.localPosition.x == 1.34f)
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, rocketSpeed);
                    }

                    else if (this.gameObject.transform.localPosition.z == 0.54f) {

                        if (this.gameObject.transform.localPosition.x == 0.0f) {
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, -1.0f*rocketSpeed);
                        } else if (this.gameObject.transform.localPosition.x == 1.34f) {
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, -1.0f*rocketSpeed);
                        }
                    }
                    //  forceDir = new Vector3 (0, 0, (-1) * rocketSpeed);
                    else if (this.gameObject.transform.localPosition.x == -0.66f) {

                        if (this.gameObject.transform.localPosition.z == 1.34f)
                            forceDir = new Vector3 (-1.0f*rocketSpeed, 0, 0.5f * rocketSpeed);
                        else if (this.gameObject.transform.localPosition.z == 0.02f)
                            forceDir = new Vector3 (-1.0f*rocketSpeed, 0, 0.5f * rocketSpeed);
                    }

                    rb.AddRelativeForce (forceDir);
                } else {
                    //연료 사용량 색 변화
                    EnginefuelLeft.color = Color.cyan;
                     //엔진 불꽃 꺼버린다.
                    flame.SetActive (false);
                    //메세징 조건
                        btnClick[1] = -1;
                }
            }
        }

        if (this.gameObject.name == "3rd_FuelTanker") {
            
            if (EnginefuelLeft.fillAmount == 0) {
                // 연료 다 쓰면 연기나게
                flame.SetActive (false);
                smoke.SetActive (true);
                if(dup[2] == 0)
                {
                    // 메시징 추가
                    MessageQueue.instance.Push(new Message(PlayerNumber.P2, Circumstance.ENGINE_3OFF, .5f));
                    dup[2] = 1;
                }
                //엔진 사용 꺼버리기
                engineOn[2] = 0;
                engineOn2p[2] = 0;
                GameManager.instance.player1Button = 0;
                GameManager.instance.player2Button = 0;
            } else {

                //1p의 버튼 E
                if (Input.GetKeyDown (KeyCode.E) && GameManager.instance.player1Button == 0) {
                    engineOn[2] = 1;
                    GameManager.instance.player1Button = 1;
                    // 임시로 2p로 사운드, 애니메이션 코드 넣어놓음.
                    PlayerUIManager.instance.PushButton (PlayerNumber.P1);
                } else if (Input.GetKeyUp (KeyCode.E) && engineOn[2] == 1) {
                    engineOn[2] = 0;
                    GameManager.instance.player1Button = 0;
                }
                //2p의 버튼 P
                if (Input.GetKeyDown (KeyCode.P) && GameManager.instance.player2Button == 0) {
                    engineOn2p[2] = 1;

                    GameManager.instance.player2Button = 1;
                    // 임시로 1p로 사운드, 애니메이션 코드 넣어놓음.
                    PlayerUIManager.instance.PushButton (PlayerNumber.P2);
                } else if (Input.GetKeyUp (KeyCode.P) && engineOn2p[2] == 1) {
                    engineOn2p[2] = 0;
                    GameManager.instance.player2Button = 0;
                }

                if (engineOn[2] == 1 || engineOn2p[2] == 1) {
                    //엔진 불꽃 킨다.
                    flame.SetActive (true);
                    Debug.Log ("E is On");

                    EnginefuelLeft.color = Color.red;
                    // 둘다 같은 엔진 사용할 경우
                    if (engineOn[2] == 1 && engineOn2p[2] == 1 && btnClick[2] == -1) {
                        btnClick[2] = 0;
                        // 메시징 추가
                        MessageQueue.instance.Push(new Message(PlayerNumber.ALL, Circumstance.SAMEBUTTON_3,0f));
                    }

                    // 연료 사용량 반영
                    EnginefuelLeft.fillAmount -= 20 / EnginewaitTime * Time.deltaTime;
                    GameManager.instance.usedFuel += 20.0f;


                    if (this.gameObject.transform.localPosition == new Vector3 (0, 0, 0)) {
                        forceDir = new Vector3 (0, rocketSpeed, 0);
                    } else if (this.gameObject.transform.localPosition.x == -0.7f) {

                        if (this.gameObject.transform.localPosition.z == -1.32f)
                            forceDir = new Vector3 (rocketSpeed, 0, 0.5f * rocketSpeed);
                        else if (this.gameObject.transform.localPosition.z == 0.0f)
                            forceDir = new Vector3 (rocketSpeed, 0, 0.5f * rocketSpeed);

                        //forceDir = new Vector3 (rocketSpeed, 0, 0);

                    } else if (this.gameObject.transform.localPosition.z == -0.64f) {

                        if (this.gameObject.transform.localPosition.x == 0.0f)
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, rocketSpeed);
                        else if (this.gameObject.transform.localPosition.x == 1.34f)
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, rocketSpeed);

                    }

                    //  forceDir = new Vector3 (0, 0, rocketSpeed);
                    else if (this.gameObject.transform.localPosition.z == 0.54f) {

                        if (this.gameObject.transform.localPosition.x == 0.0f) {
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, -1.0f*rocketSpeed);
                        } else if (this.gameObject.transform.localPosition.x == 1.34f) {
                            forceDir = new Vector3 (0.5f * rocketSpeed, 0, -1.0f*rocketSpeed);
                        }
                    }
                    //  forceDir = new Vector3 (0, 0, (-1) * rocketSpeed);
                    else if (this.gameObject.transform.localPosition.x == -0.66f) {

                        if (this.gameObject.transform.localPosition.z == 1.34f)
                            forceDir = new Vector3 (-1.0f*rocketSpeed, 0, 0.5f * rocketSpeed);
                        else if (this.gameObject.transform.localPosition.z == 0.02f)
                            forceDir = new Vector3 (-1.0f*rocketSpeed, 0, 0.5f * rocketSpeed);

                        //forceDir = new Vector3 ((-1) * rocketSpeed, 0, 0);
                    }

                    rb.AddRelativeForce (forceDir);
                } else {
                     //연료 사용량 색 변화
                    EnginefuelLeft.color = Color.cyan;
                     //엔진 불꽃 꺼버린다.
                    flame.SetActive (false);
                    btnClick[2] = -1;
                }
            }
        } else {
            forceDir = new Vector3 (0, 0.1f * rocketSpeed, 0);
            rb.AddRelativeForce (forceDir);
        }

       // GameManager.instance.usedFuel = rocketSpeed;
        //    Debug.Log(forceDir.x+" "+forceDir.y+" "+forceDir.z);
        //    Debug.Log("*******");
    }
}