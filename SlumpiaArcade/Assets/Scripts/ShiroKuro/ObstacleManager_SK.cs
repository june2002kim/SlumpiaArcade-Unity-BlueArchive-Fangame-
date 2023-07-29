using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleManager_SK : MonoBehaviour
{
    public Spawner_Kuro KuroSpawnerPrefab;
    public Spawner_Shiro ShiroSpawnerPrefab;
    public BombLauncher ShiroBombLauncherPrefab;

    private Spawner_Kuro[] KuroSpawners;
    private Spawner_Shiro[] ShiroSpawners;
    private BombLauncher ShiroBombLauncher;

    [Header("Spawner Settings")]
    [SerializeField] private int KuroShootCount;
    [SerializeField] private float KuroSpawnDelay = 5f;
    private int KurospawnCount = 24;
    private bool KurocanShoot;

    [SerializeField] private int ShiroShootCount;
    [SerializeField] private float ShiroSpawnDelay = 40f;
    private int ShirospawnCount = 3;
    private bool ShirocanShoot;
    private bool ShirocanLaunch;
    [SerializeField] private float ShiroLaunchDelay = 7f;

    [Header("Spawn Interval Settings")]
    [SerializeField] private float newPatternInterval = 5f;
    private float lastUpdatedTime;

    private Vector2 poolPositionWest = new Vector2(-7, -4);
    private Vector2 poolPositionNorth = new Vector2(-4, 7);
    private Vector2 poolPositionEast = new Vector2(7, 4);
    private Vector2 poolPositionSouth = new Vector2(4, -7);

    private Vector2 poolPosition = new Vector2(-3.2f, 7);

    private int KuroSpawnIndex;
    private int[] KuroBusyIndexList;

    private int ShiroSpawnIndex;

    private bool ShiroTrigger = false;
    private bool KuroTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        KuroShootCount = 3;
        KurocanShoot = true;

        ShiroShootCount = 2;
        ShirocanShoot = true;
        ShirocanLaunch = true;

        lastUpdatedTime = Time.time;

        KuroSpawners = new Spawner_Kuro[KurospawnCount];
        KuroBusyIndexList = new int[10];

        ShiroSpawners = new Spawner_Shiro[ShirospawnCount];

        for (int i = 0; i < KurospawnCount / 4; i++)
        {
            KuroSpawners[i] = Instantiate(KuroSpawnerPrefab, poolPositionWest + new Vector2(0,1.6f * i), Quaternion.identity);
        }
        for (int i = KurospawnCount / 4; i < KurospawnCount * 2 / 4; i++)
        {
            KuroSpawners[i] = Instantiate(KuroSpawnerPrefab, poolPositionNorth + new Vector2(1.6f * (i - (KurospawnCount / 4)), 0), Quaternion.identity);
        }
        for (int i = KurospawnCount * 2 / 4; i < KurospawnCount * 3 / 4; i++)
        {
            KuroSpawners[i] = Instantiate(KuroSpawnerPrefab, poolPositionEast + new Vector2(0, -1.6f * (i - (KurospawnCount * 2 / 4))), Quaternion.identity);
        }
        for (int i = KurospawnCount * 3 / 4; i < KurospawnCount; i++)
        {
            KuroSpawners[i] = Instantiate(KuroSpawnerPrefab, poolPositionSouth + new Vector2(-1.6f * (i - (KurospawnCount * 3 / 4)), 0), Quaternion.identity);
        }

        for(int j = 0; j < ShirospawnCount; j++)
        {
            ShiroSpawners[j] = Instantiate(ShiroSpawnerPrefab, poolPosition + new Vector2(3.2f * j, 0), Quaternion.identity);
        }

        ShiroBombLauncher = Instantiate(ShiroBombLauncherPrefab, Vector2.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= lastUpdatedTime + newPatternInterval)
        {
            if (!ShiroTrigger)
            {
                ShiroTrigger = true;
                lastUpdatedTime = Time.time;
            }
            else
            {
                if (!KuroTrigger)
                {
                    KuroTrigger = true;
                    lastUpdatedTime = Time.time;
                }
                else
                {
                    if(Time.time > lastUpdatedTime + 2 * newPatternInterval)
                    {
                        if(KuroShootCount < 6)
                        {
                            KuroShootCount++;
                            lastUpdatedTime = Time.time;
                        }
                        else
                        {
                            if (ShiroBombLauncherPrefab.normalBombCnt < 6f)
                            {
                                ShiroBombLauncherPrefab.normalBombCnt++;
                            }
                            else if (ShiroBombLauncherPrefab.slowBombCnt < 3f)
                            {
                                ShiroBombLauncherPrefab.slowBombCnt++;
                            }
                            if (ShiroBombLauncherPrefab.homingBombCnt < 2f)
                            {
                                ShiroBombLauncherPrefab.homingBombCnt++;
                            }
                            else
                            {
                                if (ShiroLaunchDelay > 4f)
                                {
                                    ShiroLaunchDelay--;
                                }
                                else if (ShiroSpawnDelay > 11f)
                                {
                                    ShiroSpawnDelay--;
                                }
                                else if (KuroSpawnDelay > 3f)
                                {
                                    KuroSpawnDelay--;
                                }
                            }
                        }
                    }
                }
            }
        }


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
        KurocanShoot = false;
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
            KuroSpawners[KuroSpawnIndex - 1].Shoot();
        }

        yield return new WaitForSeconds(KuroSpawnDelay);
        KurocanShoot = true;
    }

    private IEnumerator SelectShiroSpawner()
    {
        ShirocanShoot = false;

        ShiroSpawnIndex = UnityEngine.Random.Range(0, ShirospawnCount);

        for (int j = 0; j < ShirospawnCount; j++)
        {
            if (j != ShiroSpawnIndex)
            {
                ShiroSpawners[j].Shoot();
            }
        }

        //Debug.Log("Shiro spawned except :" + ShiroSpawnIndex);

        yield return new WaitForSeconds(ShiroSpawnDelay);
        ShirocanShoot = true;
    }

    private IEnumerator LaunchShiroBomb()
    {
        ShirocanLaunch = false;

        ShiroBombLauncher.Launch();
        //Debug.Log("Shiro Launched Bomb");

        yield return new WaitForSeconds(ShiroLaunchDelay);
        ShirocanLaunch = true;
    }
}
