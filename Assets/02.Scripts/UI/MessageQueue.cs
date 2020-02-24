/*
 * 작성자 : 백성진
 * 
 * 우선순위 큐에 메시지를 저장한 후 발송 및 보관을 관리하는 스크립트 입니다.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageQueue : MonoBehaviour
{
    public static MessageQueue instance = null; 

    // 싱글턴 패턴
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    // 시간 float 값으로 정렬된 메시지 딕셔너리.
    public List<Message> messageQueue = new List<Message>();


    // 오름차순 정렬
    public void Sort()
    {
        messageQueue.Sort(delegate (Message A, Message B)
        {
            if (A.dispatchTime > B.dispatchTime)
            {
                return 1;
            }
            else if (A.dispatchTime < B.dispatchTime)
            {
                return -1;
            }
            return 0;
        });
    }

    // 큐에 메시지 삽입
    public void Push(Message message)
    {
        message.dispatchTime = Time.time + message.dispatchTime;
        messageQueue.Add(message);
        Debug.Log("ADD : " + message.circumstance);
        Sort();
    }

    // TOP 획득
    public Message Peek()
    {
        if (messageQueue.Count == 0)
        {
            return new Message(PlayerNumber.ALL, Circumstance.DUMMY, Time.time + 1f);
        }
        else
        {
            return messageQueue[0];
        }
    }

    // TOP 획득 (Peek과 동일)
    public Message Top()
    {
        if (messageQueue.Count == 0)
        {
            return new Message(PlayerNumber.ALL, Circumstance.DUMMY, Time.time + 1f);
        }
        else
        {
            return messageQueue[0];
        }
    }

    // 큐에서 POP
    private void Pop()
    {
        if(messageQueue.Count != 0)
        {
            messageQueue.RemoveAt(0);
        }
    }

    // 보존시간(3초)지난 메시지는 큐에서 삭제함.
    private void RemoveCandidateElement() {
        if (messageQueue.Count != 0)
        {
            if (Top().dispatchTime + 3f < Time.time || Top().circumstance == Circumstance.DUMMY)
            {
                Pop();
            }
        }
    }

    // 큐의 사이즈
    public int Size()
    {
        return messageQueue.Count;
    }

    // 메시지 발송.
    public Message Dispatch()
    {
        // 보낼 만한 메시지 있는 경우, 메시지 발송
        if (Time.time >= Top().dispatchTime && Top().circumstance != Circumstance.DUMMY)
        {
            Message msg = Top();
            Pop();
            Debug.Log("REMOVED : " + msg.circumstance);
            return msg;
        }
        // 보낼만한 메시지 없는경우 DUMMY 메시지 발송
        else
        {
            return new Message(PlayerNumber.ALL, Circumstance.DUMMY, Time.time + 2f);
        }
    }
    private void Update()
    {
        // 시간이 지나면 자동으로 큐에서 제거한다.
        RemoveCandidateElement();
    }

}
