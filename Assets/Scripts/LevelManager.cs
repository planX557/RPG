using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string musicGroupName;


    private void Start()
    {
        AudioManager.instance.StartBGM(musicGroupName);
    }
}
