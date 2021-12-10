using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private bool doublePoints;
    private bool safeMode;

    private bool powerupActive;

    private float powerupLenghtCounter;

    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;

    private float normalPointsPerSecond;
    private float spikeRate;

    private PlatformDestroyer[] spikeList;

    private GameManager theGameManager;

    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theGameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupActive)
        {
            powerupLenghtCounter -= Time.deltaTime;

            if (theGameManager.powerupReset)
            {
                powerupLenghtCounter = 0;
                theGameManager.powerupReset = false; 
            }

            if (doublePoints)
            {
                theScoreManager.pointPerSecond = normalPointsPerSecond * 2;
                theScoreManager.shouldDouble = true; 
            }

            if (safeMode )
            {
                thePlatformGenerator.randomSpikeThreshold = 0; 
            }

            if (powerupLenghtCounter <=0)
            {
                theScoreManager.pointPerSecond = normalPointsPerSecond;
                theScoreManager.shouldDouble = false;

                thePlatformGenerator.randomSpikeThreshold = spikeRate;

                powerupActive = false;
            }
        }
    }

    public void ActivatePowerup(bool points, bool safe, float time)
    {
        doublePoints = points;
        safeMode = safe;
        powerupLenghtCounter = time;

        normalPointsPerSecond = theScoreManager.pointPerSecond;
        spikeRate = thePlatformGenerator.randomSpikeThreshold;

        if (safeMode)
        {
            spikeList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains ("spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }
        powerupActive = true;
    }
}
