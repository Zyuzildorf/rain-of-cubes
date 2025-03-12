using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RootPoint : MonoBehaviour
{
   [SerializeField] private Spawner _spawner;
   [SerializeField] private ColorChanger _colorChander;

   private void Awake()
   {
      ObjectPool<Poollable> _poollables = _spawner.SpawnObjects();

      foreach (Poollable poollable in _poollables.)
      {
         
      }
      
   }
}
