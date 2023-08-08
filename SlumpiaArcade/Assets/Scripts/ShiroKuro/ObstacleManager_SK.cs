using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* Script for Managing every obstacle spawners and launchers */

public class ObstacleManager_SK : MonoBehaviour
{
    public Spawner_Kuro KuroSpawnerPrefab;                              // KuroSpawner's Prefab
    public Spawner_Shiro ShiroSpawnerPrefab;                            // ShiroSpawner's Prefab
    public BombLauncher ShiroBombLauncherPrefab;                        // ShiroBombLauncher's Prefab

    private Spawner_Kuro[] KuroSpawners;
    private Spawner_Shiro[] ShiroSpawners;
    private BombLauncher ShiroBombLauncher;                             // ShiroBombLauncher is unique

    [Header("Spawner Settings")]
    [SerializeField] private int KuroShootCount;                        // number of Kuro's obstacle going to spawn in field
    //[SerializeField] private int KuroShootSet;
    [SerializeField] private float KuroSpawnDelay;
    //[SerializeField] private float KuroSpawnDelaySet = 5f;
    private int KurospawnCount = 24;                                    // KuroSpawner's maximum count for instantiate
    private bool KurocanShoot;

    [SerializeField] private int ShiroShootCount;                       // number of Shiro's ball going to spawn in field
    [SerializeField] private float ShiroSpawnDelay;
    //[SerializeField] private float ShiroSpawnDelaySet = 40f;
    private int ShirospawnCount = 3;                                    // ShiroSpawner's maximum count for instantiate
    private bool ShirocanShoot;
    private bool ShirocanLaunch;
    [SerializeField] private float ShiroLaunchDelay;
    //[SerializeField] private float ShiroLaunchDelaySet = 7f;

    [Header("Spawn Interval Settings")]
    [SerializeField] private float newPatternInterval = 2f;             // new pattern appears each time passes 'newPatternInteral'
    private float firstSpawnDelay = 1f;
    private float lastUpdatedTime;

    // pool positions for Kuro's spawner located around the field
    private Vector2 poolPositionWest = new Vector2(-7, -4);
    private Vector2 poolPositionNorth = new Vector2(-4, 7);
    private Vector2 poolPositionEast = new Vector2(7, 4);
    private Vector2 poolPositionSouth = new Vector2(4, -7);

    // pool position for Shiro's spawnere located above the field
    private Vector2 poolPosition = new Vector2(-3.2f, 7);

    private int KuroSpawnIndex;
    private int[] KuroBusyIndexList;

    private int ShiroSpawnIndex;

    private bool ShiroTrigger = false;                                  // trigger for spawning Shiro's balls
    private bool KuroTrigger = false;                                   // trigger for spawning Kuro's obstacles

    // Start is called before the first frame update
    void Start()
    {
        KuroShootCount = PlayerPrefs.GetInt("KuroShootSet");
        KurocanShoot = true;

        ShiroShootCount = PlayerPrefs.GetInt("ShiroShootCount");
        ShirocanShoot = true;
        ShirocanLaunch = true;

        ShiroTrigger = false;
        KuroTrigger = false;

        // set spawning term
        KuroSpawnDelay = PlayerPrefs.GetFloat("KuroSpawnDelaySet");
        ShiroSpawnDelay = PlayerPrefs.GetFloat("ShiroSpawnDelaySet");
        ShiroLaunchDelay = PlayerPrefs.GetFloat("ShiroLaunchDelaySet");

        lastUpdatedTime = Time.time;

        KuroSpawners = new Spawner_Kuro[KurospawnCount];
        KuroBusyIndexList = new int[10];

        ShiroSpawners = new Spawner_Shiro[ShirospawnCount];

        for (int i = 0; i < KurospawnCount / 4; i++)
        {
            // instantiate Kuro's spawners west side of field
            KuroSpawners[i] = Instantiate(KuroSpawnerPrefab, poolPositionWest + new Vector2(0,1.6f * i), Quaternion.identity);
        }
        for (int i = KurospawnCount / 4; i < KurospawnCount * 2 / 4; i++)
        {
            // instantiate Kuro's spawners North side of field
            KuroSpawners[i] = Instantiate(KuroSpawnerPrefab, poolPositionNorth + new Vector2(1.6f * (i - (KurospawnCount / 4)), 0), Quaternion.identity);
        }
        for (int i = KurospawnCount * 2 / 4; i < KurospawnCount * 3 / 4; i++)
        {
            // instantiate Kuro's spawners east side of field
            KuroSpawners[i] = Instantiate(KuroSpawnerPrefab, poolPositionEast + new Vector2(0, -1.6f * (i - (KurospawnCount * 2 / 4))), Quaternion.identity);
        }
        for (int i = KurospawnCount * 3 / 4; i < KurospawnCount; i++)
        {
            // instantiate Kuro's spawners south side of field
            KuroSpawners[i] = Instantiate(KuroSpawnerPrefab, poolPositionSouth + new Vector2(-1.6f * (i - (KurospawnCount * 3 / 4)), 0), Quaternion.identity);
        }

        for(int j = 0; j < ShirospawnCount; j++)
        {
            // instantiate Shiro's spawners North side of field
            ShiroSpawners[j] = Instantiate(ShiroSpawnerPrefab, poolPosition + new Vector2(3.2f * j, 0), Quaternion.identity);
        }

        // instantiate Shiro's bomb launcher
        ShiroBombLauncher = Instantiate(ShiroBombLauncherPrefab, Vector2.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager_SK.instance.isGameOver)
        {
            // stop spawning obstacles when game is over
            return;
        }

        if(Time.time >= lastUpdatedTime + newPatternInterval)
        {
            if (!ShiroTrigger)
            {
                // start spawning Shiro's ball
                ShiroTrigger = true;
                lastUpdatedTime = Time.time;
            }
            else
            {
                if (!KuroTrigger)
                {
                    // start spawning Kuro's obstacle
                    KuroTrigger = true;
                    lastUpdatedTime = Time.time;
                }
                else
                {
                    if(Time.time > lastUpdatedTime + 2 * newPatternInterval)
                    {
                        if(KuroShootCount < PlayerPrefs.GetInt("KuroShootSet") + 3 - PlayerPrefs.GetInt("Difficulty"))
                        {
                            // increase number of Kuro's spawning obstacles
                            KuroShootCount++;
                            lastUpdatedTime = Time.time;
                        }
                        else
                        {
                            if (ShiroBombLauncher.normalBomb < 6)
                            {
                                // increase number of Shiro's launching normal bombs

                                //Debug.Log("Normal Bomb Added : " + ShiroBombLauncher.normalBomb);
                                ShiroBombLauncher.normalBomb++;
                                lastUpdatedTime = Time.time;
                            }
                            else if (ShiroBombLauncher.slowBomb < 3)
                            {
                                // increase number of Shiro's launching slow bombs

                                //Debug.Log("Slow Bomb Added : " + ShiroBombLauncher.slowBomb);
                                ShiroBombLauncher.slowBomb++;
                                lastUpdatedTime = Time.time;
                            }
                            else if (ShiroBombLauncher.homingBomb < 2)
                            {
                                // increase number of Shiro's launching homing(stun) bombs

                                //Debug.Log("Homing Bomb Added : " + ShiroBombLauncher.homingBomb);
                                ShiroBombLauncher.homingBomb++;
                                lastUpdatedTime = Time.time;
                            }
                            else
                            {
                                if (ShiroLaunchDelay > 4f)
                                {
                                    // decrease Shiro's bomb launching delay
                                    ShiroLaunchDelay--;
                                    lastUpdatedTime = Time.time;
                                }
                                else if (ShiroSpawnDelay > 5f)
                                {
                                    // decrease Shiro's ball spawning delay
                                    ShiroSpawnDelay--;
                                    lastUpdatedTime = Time.time;
                                }
                                else if (KuroSpawnDelay > 2f)
                                {
                                    // decrease Kuro's obstacle spawning delay
                                    KuroSpawnDelay--;
                                    lastUpdatedTime = Time.time;
                                }
                            }
                        }
                    }
                }
            }
        }

        // when obstacle spawners have been triggered and delay term passed, start corountine
        if (KuroTrigger && KurocanShoot && !GameManager_SK.instance.isGameOver)
        {
            StartCoroutine("SelectKuroSpawner");
        }

        if (ShiroTrigger && ShirocanShoot && !GameManager_SK.instance.isGameOver)
        {
            StartCoroutine("SelectShiroSpawner");
        }

        if (ShirocanLaunch && !GameManager_SK.instance.isGameOver)
        {
            StartCoroutine("LaunchShiroBomb");
        }
    }

    private IEnumerator SelectKuroSpawner()
    {
        /*
         Select which Kuro's spawner going to be activate and spawn obstacles
         */

        // prevent spawning obstacles right away
        KurocanShoot = false;

        // prevent selecting same(already selected) spawner by gathering selected spawner's index
        int[] busyIndexArray = KuroBusyIndexList.ToArray();
        Array.Clear(busyIndexArray, 0, busyIndexArray.Length);
        
        for(int i = 0; i < KuroShootCount; i++)
        {
            KuroSpawnIndex = UnityEngine.Random.Range(1, KurospawnCount + 1);
            if (i > 0)
            {
                foreach (int x in busyIndexArray)
                {
                    while (KuroSpawnIndex == x)
                    {
                        KuroSpawnIndex = UnityEngine.Random.Range(1, KurospawnCount + 1);
                    }
                }
            }
            
            //Debug.Log("Spawn Index :" + (KuroSpawnIndex - 1));

            busyIndexArray[i] = KuroSpawnIndex;
            // considering 'KuroSpawnIndex + 1' in 'busyIndexArray', because it's initial(cleared) value is zero(0)
            KuroSpawners[KuroSpawnIndex - 1].Shoot();
        }

        yield return new WaitForSeconds(KuroSpawnDelay);

        // after delay, let obstacle manager select spawners again
        KurocanShoot = true;
    }

    private IEnumerator SelectShiroSpawner()
    {
        /*
         Select which Shiro's spawner going to be activate and spawn obstacles
         */

        ShirocanShoot = false;

        ShiroSpawnIndex = UnityEngine.Random.Range(0, ShirospawnCount);

        for (int j = 0; j < ShirospawnCount; j++)
        {
            if (j != ShiroSpawnIndex)
            {
                // to be honest, always shoot 2 Shiro's ball regardless of 'ShiroShootCount'
                ShiroSpawners[j].Shoot();
            }
        }

        //Debug.Log("Shiro spawned except :" + ShiroSpawnIndex);

        yield return new WaitForSeconds(ShiroSpawnDelay);

        ShirocanShoot = true;
    }

    private IEnumerator LaunchShiroBomb()
    {
        /*
         Select which Shiro's bomb launcher going to be activate and spawn obstacles
         */

        ShirocanLaunch = false;

        yield return new WaitForSeconds(firstSpawnDelay);

        ShiroBombLauncher.Launch();

        //Debug.Log("Shiro Launched Bomb");

        yield return new WaitForSeconds(ShiroLaunchDelay);

        ShirocanLaunch = true;
    }
}
