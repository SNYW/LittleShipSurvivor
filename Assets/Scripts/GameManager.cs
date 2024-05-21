using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using SystemEvents;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
   public static PlayerUnit playerShip;
   public static GameManager instance;

   public float asteroidSpawnRadius;
   public float asteroidSpawnBaseCooldown;
   public float asteroidSpawnPerLevelSpeedup;
   public ObjectPool[] asteroidPools;
   private float CurrentAsteroidSpawnTime => asteroidSpawnBaseCooldown - asteroidSpawnPerLevelSpeedup * currentLevel;
   
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
      playerShip.Activate();
      instance = this;
      
      SystemEventManager.Subscribe(SystemEventManager.SystemEventType.LevelUp, OnLevelUp);
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.GameStart, this);
   }

   private void Start()
   {
      StartCoroutine(SpawnAsteroids());
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

   private IEnumerator SpawnAsteroids()
   {
      while (gameObject.activeSelf)
      {
         var pos = GetRandomPointInRadius(playerShip.transform.position, asteroidSpawnRadius);
         var newAsteroid = asteroidPools.Random().GetPooledObject().GetComponent<EnemyUnit>();
         newAsteroid.transform.position = pos;
         newAsteroid.gameObject.SetActive(true);
         newAsteroid.Activate();
         
         yield return new WaitForSeconds(CurrentAsteroidSpawnTime);
      }
   }

   public static Vector2 GetRandomPointInRadius(Vector3 center, float radius, bool onEdge = true)
   {
      float angle = Random.Range(0f, 2f * Mathf.PI);
      
      
      float distance = onEdge ? radius : Random.Range(0f, radius);
      
      float newX = center.x + distance * Mathf.Cos(angle);
      float newY = center.y + distance * Mathf.Sin(angle);
        
      return new Vector3(newX, newY);
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