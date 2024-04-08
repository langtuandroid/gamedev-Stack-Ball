using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace multi_tower_attack_3d
{
    public class OnOffToggleSave : MonoBehaviour
    {
        // nichena function ne tame gametya muki ne use kari sakso
        // Normal UIButton upar thi j script ne call karvi, toggle button no levu. ane button na child ma 2 image lae levi on-off vari
        public Image[] soundOnOff; // [0] ma On vari image nakhvi ane [1] ma Off vari image nakhvi

        void Start()
        {
            AudioListener.volume = (PlayerPrefs.HasKey("soundsOn_towerAttack") && (PlayerPrefs.GetInt("soundsOn_towerAttack") != 1)) ? 0f : 1f;
            bool flag = !PlayerPrefs.HasKey("soundsOn_towerAttack") || (PlayerPrefs.GetInt("soundsOn_towerAttack") == 1);
            flag = !flag;
            soundOnOff[0].enabled = !flag;
            soundOnOff[1].enabled = flag;
        }

        public void SoundsClicked()
        {
            bool flag = !PlayerPrefs.HasKey("soundsOn_towerAttack") || (PlayerPrefs.GetInt("soundsOn_towerAttack") == 1);
            flag = !flag;
            PlayerPrefs.SetInt("soundsOn_towerAttack", !flag ? 0 : 1);
            AudioListener.volume = !flag ? 0f : 1f;
            Debug.Log("flag" + flag); // true means sound on chhe

            soundOnOff[0].enabled = flag;
            soundOnOff[1].enabled = !flag;
        }

    }
}
