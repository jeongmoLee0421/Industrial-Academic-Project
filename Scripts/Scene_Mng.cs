using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Scene_Mng : MonoBehaviour
{
    public void MainMenu()      // 메인메뉴
    {
        SceneManager.LoadScene("Main_display");
    }
    public void CharacterMenu()     // 캐릭터 상태창
    {
        SceneManager.LoadScene("Character_Status");
    }
    public void VillageMenu()       // 마을메뉴
    {
        SceneManager.LoadScene("Village_Shop");
    }
    public void StageMenu()     // 스테이지 선택창
    {
        SceneManager.LoadScene("Stage_Select");
    }
    public void GearShopMenu()      // 장비상점
    {
        SceneManager.LoadScene("GearShop");
    }
    public void ConsumableShopMenu()        // 소모품상점
    {
        SceneManager.LoadScene("ConsumableShop");
    }
    public void VillageStage()       // Main씬 호출
    {
        if(GameManager.stage==1)
            SceneManager.LoadScene("Main");
    }
    public void ForestStage()
    {
        if(GameManager.stage==2)
            SceneManager.LoadScene("Forest_Map");
    }
    public void CastlStage()
    {
        if(GameManager.stage==3)
            SceneManager.LoadScene("Cave_Map");
    }
}
