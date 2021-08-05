using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    // Start is called before the first frame update
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateData()
    {
        //다이알로그 데이터
        talkData.Add(10000, new string[] {
            "...문이 열리지 않는다.",
            "유니콘 뿔을 충분히 모으지 못한 것 같다."
        });
        talkData.Add(1000, new string[] {
            "안녕! 난 유니콘이야!\n넌 여기가 처음이구나!",
            "정말 반가워!\n초면에 미안한데 혹시...",
            "내게 유니콘 뿔을 찾아줄 수 있겠니?",
            "정말 정말 중요한 물건인데 이 세상 어딘가에 숨겨져있어...!",
            "지금의 나는 다리를 다쳐서 이 앞의 떠다니는 섬을 건널 수 없지만,\n너라면 가능할 거야!",
            "...그리고 혹시라도 토끼 인형이 있다면 바로...",
            "없애버려",
            "꼭 유니콘 뿔은 나에게 줘야해!!!",
            "그럼 행운을 빌어!"
        });
        talkData.Add(1001, new string[] {
            "유니콘 뿔은 떠있는 섬 위에 어딘가에 있어!",
            "꼭 샅샅이 찾아보고 유니콘 뿔을 찾게 되면 꼭 나에게 돌아와줘!"
        });
        talkData.Add(1002, new string[] {
            "뿔을 갖고 왔구나!!",
            "...",
            "이거 하나 찾아놓고 나한테 온거야...?",
            "잘 들어 이 세계에는 뿔이 총 10개가 있어.",
            "뿔을 모두 모으면 당장 나한테 가져와",
            "딴길로 새지 말고"
        });
        talkData.Add(1003, new string[] {
            "뿔을 갖고 왔구나!!",
            "...",
            "이거 조금 찾아놓고 나한태 온거야...?",
            "잘 들어 이 세계에는 뿔이 총 10개가 있어.",
            "뿔을 모두 모으면 당장 나한테 가져와",
            "딴길로 새지 말고"
        });
        talkData.Add(1004, new string[] {
            "그래 뿔을 갖고 왔구나.",
            "나머지 뿔들은 저 성 안에 있어.",
            "내 친구들도 있으니까 인사 전해주고...!"
        });
        talkData.Add(1005, new string[] {
            "인형을 찢어버리라고 했잖아!!!!!!!!!!",
            "인형을 찢어!!!!!!",
            "인형을 찢어!!!!!! 인형을 찢어!!!!!!!!"
        });


        talkData.Add(100, new string[] { "상자이다.", "물약이 들어있다!" });
        talkData.Add(101, new string[] { "상자이다.", "유니콘 뿔이 들어있다!" });
        talkData.Add(500, new string[] { "토끼 인형이다.",
            "...유니콘이 없애라고 했는데...",
            "인형을 들었다.",
            "인형 밑에 노트가 있다!",
            "[뿔은 열쇠다. 절대 놓지지마라. \n뿔달린 짐승을 믿지 마라\n\t\t\t-익명-]"
        });
        talkData.Add(501, new string[] { "토끼 인형이다.",
            "....없애긿촗싈ㅈ",
            "머리를 뜯어버려!!!!!!!!!!!!!!!!!!!!!!",
            "핥봃굻늑힎쌓젫",
            "인형을 들었다.",
            "인형 밑에 노트가 있다!",
            "[마우스 클릭으로 공격.]",
            "[열쇠를 모두 모아야 문이 열린다. \n뿔 달린 짐승을 믿지 마라]",
            "[뿔 달린 짐승을 믿지 마라. 뿔 달린 짐승을 믿지 마라. \n" +
            "뿔 달린 짐승을 믿지 마라. 뿔 달린 짐승을 믿지 마라. \n" +
            "뿔 달린 짐승을 믿지 마라. 뿔 달린 짐승을 믿지 마라. \n-익명-]"
        });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }
}
