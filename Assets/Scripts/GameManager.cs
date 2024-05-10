using ObjectPooling;
using SystemEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static PlayerUnit playerShip;
   
   public static bool turningLeft;
   public static bool turningRight;

   public GameObject choosePanel;
   public Canvas uIRoot;

   private void Awake()
   {
      SystemEventManager.Init();
      ObjectPoolManager.InitPools();
      WeaponManager.Init();
      playerShip = FindObjectOfType<PlayerUnit>();
   }

   private void Update()
   {
      if(Input.GetKeyDown(KeyCode.Space))
      {
         OnLevelUp();
      }
   }

   private void OnLevelUp()
   {
      Instantiate(choosePanel, uIRoot.transform);
   }
}