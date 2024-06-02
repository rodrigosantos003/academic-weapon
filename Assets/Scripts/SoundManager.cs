using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource WhooshSound;
    [SerializeField] private AudioSource SchoolBell;
    [SerializeField] private AudioSource DrinkingSound;
    [SerializeField] private AudioSource PaperSound;
    
    public void PlayWhooshSound()
    {
        WhooshSound.Play();
    }
    
    public void PlaySchoolBell()
    {
        SchoolBell.Play();
    }
    
    public void PlayDrinkingSound()
    {
        DrinkingSound.Play();
    }
    
    public void PlayPaperSound()
    {
        PaperSound.Play();
    }
}
