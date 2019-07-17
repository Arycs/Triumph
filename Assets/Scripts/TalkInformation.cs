using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TalkInformation : MonoBehaviour {

    public Text TalkPreview;
    private int i = 0, j = 0, k = 0, l = 0;

    public GameObject bg;

    public string[] TalkInfo0 = new string[] {
        "梅西尔星外，一艘库丘林级战舰行驶中。",
    };

    public string[] TalkInfo1 = new string[] {
        "赵策\n瘸腿，让独眼快点，最多2个星际时后面的鬣狗们就追上来了，这条大鱼可不能落在他们手里",
        "瘸腿\n老大，好了好了，独眼锁定那家伙的位置了！",
        "赵策\n好，给老子把迁移器打开，让兄弟们都准备好，抓住这小子咱们就发达了。到时候第一时间老子就换一架伯伦希尔级，这破船我受够了！",
        "独眼\n头儿，去年你提船的时候可不是这么说的",
        "赵策\n闭嘴，独眼，给老子把大家伙预热上",
        "瘸腿\n头儿，对付个杂鱼是不是有点过儿了？那玩意开一次要吃掉我们半年的能量供给啊！"
    };

    public string[] TalkInfo2 = new string[] {
        "梅西尔星秘密工厂"
    };

    public string[] TalkInfo3 = new string[] {
        "吉米\n该死的，我的25cm口径中子光束炮呢，我放在这那么大一个机甲也不见了？",
        "莉莉丝\n吉米，存储记录中显示这些已经被您售出了",
        "吉米\n我就不该淌这趟浑水的。飞行器无法起飞，以库丘林级的舰载武器我甚至做不出闪避动作就会被击中，怎么办！",
        "莉莉丝\n吉米，侦测到卫星轨道上出现高能反应",
        "吉米\n这可是地底啊，他们的武器不管怎么样都不可能击中的啊！",
        "莉莉丝\n吉米，部署在地面的干扰弹已经准备就绪，您无需担心",
        "吉米\n不，莉莉丝。我想我知道他们的秘密武器是什么了，没想到海盗背后的人居然愿意付出这样的代价。莉莉丝，工厂内还有任何武器吗？",
        "莉莉丝\n好消息是有一批防御装置由于太过陈旧未被售出，坏消息是我们的能源并不足以支撑这些装置的运作”",
        "吉米\n马上关闭所有外层防御装置，把能源集中到基地内部",
        "莉莉丝\n好的，吉米，但这需要时间",
        "吉米\n我会给你争取时间的，见鬼，为什么一群星际海盗会拥有跃迁门？"
    };

    public void Talk()
    {
        if (i < TalkInfo0.Length)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 255);
            TalkPreview.text = TalkInfo0[i];
            i++;
        }
        else if (i == TalkInfo0.Length && j < TalkInfo1.Length)
        {
            GetComponent<Image>().color = new Color(255 ,255, 255, 255);
            TalkPreview.text = TalkInfo1[j];
            j++;
        }
        else if (i == TalkInfo0.Length && j == TalkInfo1.Length && k < TalkInfo2.Length)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 255);
            TalkPreview.text = TalkInfo2[k];
            k++;
        }
        else if (i == TalkInfo0.Length && j == TalkInfo1.Length && k == TalkInfo2.Length && l<TalkInfo3.Length)
        {
            bg.SetActive(true);
            GetComponent<Image>().color = new Color(0, 0, 0, 255);
            TalkPreview.text = TalkInfo3[l];
            l++;
        }

        if (i == TalkInfo0.Length && j == TalkInfo1.Length && k == TalkInfo2.Length && l == TalkInfo3.Length)
        {
            SceneManager.LoadScene(1);
        }
    }
}
