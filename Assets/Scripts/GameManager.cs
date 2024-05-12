using ObjectPooling;
using SystemEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static PlayerUnit playerShip;
   public static GameManager instance;
   
   public static bool turningLeft;
   public static bool turningRight;

   public GameObject choosePanel;
   public Canvas uIRoot;

   public int currentLevel;
   public int experienceForLevel;
   public int perLevelIncrease;
   private int _currentExp = 0;
   public float CurrentExpPercentage => (float)_currentExp / CurrentExpForLevel;
   public int CurrentExpForLevel => experienceForLevel + perLevelIncrease * currentLevel;

   private void Awake()
   {
      SystemEventManager.Init();
      ObjectPoolManager.InitPools();
      WeaponManager.Init();
      playerShip = FindObjectOfType<PlayerUnit>();
      instance = this;
      
      SystemEventManager.Subscribe(SystemEventManager.SystemEventType.LevelUp, OnLevelUp);
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.GameStart, this);
   }

   public void AddExperience(int experience)
   {
      _currentExp += experience;
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.ExperienceGained, experience);
      if (_currentExp < CurrentExpForLevel) return;
      
      _currentExp = 0;
      currentLevel++;
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.LevelUp, currentLevel);
   }


   private void Update()
   {
      if(Input.GetKeyDown(KeyCode.Space))
      {
         OnLevelUp(null);
      }
   }

   private void OnLevelUp(object o)
   {
      Instantiate(choosePanel, uIRoot.transform);
   }

   private void OnDisable()
   {
      SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.LevelUp, OnLevelUp);
   }
}