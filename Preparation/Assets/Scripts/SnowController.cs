using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnowController : MonoBehaviour{
  
  //declares a variable named snowPS with datetype: ParticleSystem
  private ParticleSystem snowPS;
  
  
  
  
  //Attributes with range sliders for editing
  
  //Flake size
  [Range(0, 5)]
  public float SizeMin = 1.0f;
  [Range(0, 5)]
  public float SizeMax = 1.0f;
   [Range(0, 2)]
  public float SizeMod = 1.0f;
  
  //Noise Generator
  [Range(0, 500)]
  public float Amount = 5.0f;
  [Range(0, 2)]
  public float Freq = 1.0f;
 
  //Wind Speed/Direction
  [Range(0, 10)]
  public float Windspeed = 1.0f;
  [Range(-10, 10)]
  public float WindDirection = 0.0f;
  [Range(0, 1)]
  public float Swirl = 0.1f;
  
  // Steve H. was here
  
  
  
  void Start(){
    
    snowPS = GetComponent<ParticleSystem>();
  } 
  void Update(){
    
  
  //Emission module
    var emission = snowPS.emission;
        emission.rateOverTime = Amount;
    
    var flakeSize = snowPS.sizeOverLifetime.size;
         flakeSize = new ParticleSystem.MinMaxCurve(SizeMin, SizeMax);
  
  //Velocity over time module 
    var velocityOverLifetime = snowPS.velocityOverLifetime;
        velocityOverLifetime.speedModifierMultiplier = Windspeed;
        velocityOverLifetime.x = WindDirection;
  
  //Noise Module
    var noise = snowPS.noise;
        noise.sizeAmount = SizeMod;
        noise.frequency = Freq;
        noise.strengthX = Swirl;
  }
}
