using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineLocate : MonoBehaviour {

    private int[] enginepos = new int[24];
    Vector3[] positionArray = new Vector3[24];
    Vector3[] positionRotation = new Vector3[24];
    bool[] islocate = new bool[12];

    //     Instantiate(Resource,load("dpswls"));
    GameObject Engine1st;
    GameObject Engine2nd;
    GameObject Engine3rd;
   

    void Start () {
        // 게임 시작시 대화창 출력 및 큐에 저장
        MessageQueue.instance.Push(new Message(PlayerNumber.P1, Circumstance.START_1, .5f));
        MessageQueue.instance.Push(new Message(PlayerNumber.P2, Circumstance.START_2, 1.5f));
        MessageQueue.instance.Push(new Message(PlayerNumber.P1, Circumstance.START_3, 2.51f));
        MessageQueue.instance.Push(new Message(PlayerNumber.P2, Circumstance.START_4, 3.05f));

        //우주선 동쪽, 위로 발사
        positionArray[0] = new Vector3 (0.0f, -0.6f, -0.64f);
        positionArray[1] = new Vector3 (0.0f, -0.047f, -0.64f);
        positionArray[2] = new Vector3 (0.0f, 0.5f, -0.64f); 
        // 우주선 서쪽, 위로 발사
        positionArray[3] = new Vector3 (1.34f, -0.6f, -0.64f);
        positionArray[4] = new Vector3 (1.34f, -0.047f, -0.64f);
        positionArray[5] = new Vector3 (1.34f, 0.5f, -0.64f); 
        // 우주선 남쪽, 우측 발사
        positionArray[6] = new Vector3 (-0.7f, -0.7f, -1.32f); // 우주선 남쪽, 상, 우측 발사
        positionArray[7] = new Vector3 (-0.7f, -0.15f, -1.32f); // 우주선 남쪽, 중, 우측 발사
        positionArray[8] = new Vector3 (-0.7f, 0.31f, -1.32f); //  우주선 남쪽, 하, 우측 발사
        // 우주선 북쪽, 우측 발사
        positionArray[9] = new Vector3 (-0.7f, -0.7f, 0.0f); // 우주선 북쪽, 상, 우측 발사
        positionArray[10] = new Vector3 (-0.7f, -0.15f, 0.0f); // 우주선 북쪽, 중, 우측 발사
        positionArray[11] = new Vector3 (-0.7f, 0.31f, 0.0f); // 우주선 북쪽, 하, 우측 발사

        // 우주선 서쪽, 아래 발사
        positionArray[12] = new Vector3 (1.34f, -0.42f, 0.54f);
        positionArray[13] = new Vector3 (1.34f, 0.06f, 0.54f);
        positionArray[14] = new Vector3 (1.34f, 0.52f, 0.54f);

        // 우주선 동쪽, 아래 발사 
        positionArray[15] = new Vector3 (0.0f, -0.42f, 0.54f);
        positionArray[16] = new Vector3 (0.0f, 0.06f, 0.54f);
        positionArray[17] = new Vector3 (0.0f, 0.52f, 0.54f);
        // 우주선 남쪽, 좌측 발사
        positionArray[18] = new Vector3 (-0.66f, 0.59f, 0.02f);
        positionArray[19] = new Vector3 (-0.66f, 0.21f, 0.02f);
        positionArray[20] = new Vector3 (-0.66f, -0.26f, 0.02f);

        // 우주선 북쪽, 좌측 발사
        positionArray[21] = new Vector3 (-0.66f, 0.59f, 1.34f);
        positionArray[22] = new Vector3 (-0.66f, 0.21f, 1.34f);
        positionArray[23] = new Vector3 (-0.66f, -0.26f, 1.34f);

        //위치에 맞는 각도
        positionRotation[0] = new Vector3 (0.0f, 0.0f, 0.0f);
        positionRotation[1] = new Vector3 (0.0f, 0.0f, 0.0f);
        positionRotation[2] = new Vector3 (0.0f, 0.0f, 0.0f);
        positionRotation[3] = new Vector3 (0.0f, 0.0f, 0.0f);
        positionRotation[4] = new Vector3 (0.0f, 0.0f, 0.0f);
        positionRotation[5] = new Vector3 (0.0f, 0.0f, 0.0f);
        positionRotation[6] = new Vector3 (0.0f, 90.0f, 0.0f);
        positionRotation[7] = new Vector3 (0.0f, 90.0f, 0.0f);
        positionRotation[8] = new Vector3 (0.0f, 90.0f, 0.0f);
        positionRotation[9] = new Vector3 (0.0f, 90.0f, 0.0f);
        positionRotation[10] = new Vector3 (0.0f, 90.0f, 0.0f);
        positionRotation[11] = new Vector3 (0.0f, 90.0f, 0.0f);

        positionRotation[12] = new Vector3 (180.0f, 0.0f, 0.0f);
        positionRotation[13] = new Vector3 (180.0f, 0.0f, 0.0f);
        positionRotation[14] = new Vector3 (180.0f, 0.0f, 0.0f);

        positionRotation[15] = new Vector3 (180.0f, 0.0f, 0.0f);
        positionRotation[16] = new Vector3 (180.0f, 0.0f, 0.0f);
        positionRotation[17] = new Vector3 (180.0f, 0.0f, 0.0f);

        positionRotation[18] = new Vector3 (180.0f, -90.0f, 0.0f);
        positionRotation[19] = new Vector3 (180.0f, -90.0f, 0.0f);
        positionRotation[20] = new Vector3 (180.0f, -90.0f, 0.0f);
        
        positionRotation[21] = new Vector3 (180.0f, -90.0f, 0.0f);
        positionRotation[22] = new Vector3 (180.0f, -90.0f, 0.0f);
        positionRotation[23] = new Vector3 (180.0f, -90.0f, 0.0f);

        
        Engine1st = this.gameObject.transform.GetChild (1).gameObject;
        Engine2nd = this.gameObject.transform.GetChild (2).gameObject;
        Engine3rd = this.gameObject.transform.GetChild (3).gameObject;

    }

    // Update is called once per frame
    void Update () {

    }

    public void RandomNumber () {
        // 엔진 위치 바뀌었을 경우 애니메이션, 사운드 출력
        PlayerUIManager.instance.EngineLocationChanged ();

        // 엔진이 중복되서 위치되지 않기 위한 islocate 변수 초기화
        islocate[enginepos[0]%12] = false;
        islocate[enginepos[1]%12] = false;
        islocate[enginepos[2]%12] = false;

        // 엔진 위치를 랜덤으로 생성
        enginepos[0] = Random.Range (0, 24);

        islocate[enginepos[0]%12] = true;
        // 엔진 위치가 중복되지 않도록, 중복되지 않은 수가 나올때까지 랜덤으로 수 생성
        while (true) {
             // 엔진 위치를 랜덤으로 생성
            enginepos[1] = Random.Range (0, 24);
            if (islocate[enginepos[1]%12] == false) {
                islocate[enginepos[1]%12] = true;
                break;
            }
        }
        // 엔진 위치가 중복되지 않도록, 중복되지 않은 수가 나올때까지 랜덤으로 수 생성
        while (true) {
             // 엔진 위치를 랜덤으로 생성
            enginepos[2] = Random.Range (0, 12);
            if (islocate[enginepos[2]%12] == false) {
                islocate[enginepos[2]%12] = true;
                break;
            }
        }

        //각도 조정
        Engine1st.transform.localPosition = positionArray[enginepos[0]];
        Engine1st.transform.localRotation = Quaternion.Euler (positionRotation[enginepos[0]]);
        Engine2nd.transform.localPosition = positionArray[enginepos[1]];
        Engine2nd.transform.localRotation = Quaternion.Euler (positionRotation[enginepos[1]]);
        Engine3rd.transform.localPosition = positionArray[enginepos[2]];
        Engine3rd.transform.localRotation = Quaternion.Euler (positionRotation[enginepos[2]]);

    }
}