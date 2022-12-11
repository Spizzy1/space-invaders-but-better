using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    GameObject referance;
    public delegate void resetEnemies(int wave);
    public static event resetEnemies onReset;
    public float speed = 1;
    public int wave = 1;
    public Vector3 addVector;
    [SerializeField]
    GameObject wallNegative;
    [SerializeField]
    GameObject wallPositive;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        addVector = new Vector3(0.5f, 0, 0);
        GameObject.Find("Wave").GetComponent<WaveUI>().addWave(wave);
        yield return new WaitUntil(() => GameObject.Find("enemy manager").GetComponent<EnemyManager>() != null);
        GameObject.Find("enemy manager").GetComponent<EnemyManager>().prepareWave(1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void resetPos()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
    }
   public IEnumerator move()
    {
        List<GameObject> allChildrenObjects = new List<GameObject>();
        foreach (Transform child in this.gameObject.transform)
        {
            allChildrenObjects.Add(child.gameObject);
        }
        GameObject firstX = refreshFirst();
        GameObject lastX = refreshLast();
        GameObject lowest = refreshLowest();
        addVector = new Vector3(0.5f, 0, 0);
        bool canDo = true;
        bool allGone = false;
    stopWhile:
        while (canDo)
        {
            allChildrenObjects = allChildrenObjects.Where(x => x != null).ToList();
            if (allChildrenObjects.Count <= 0)
            {
                Debug.Log("aaaaaaaaaaaaaaaa");
                canDo = false;
                wave++;
                GameObject.Find("Wave").GetComponent<WaveUI>().addWave(wave);
                Debug.Log(wave);
                goto stopWhile;
            }
            if(firstX == null)
            {
                firstX = refreshFirst();
            }
            if(lastX == null)
            {
                lastX = refreshLast();
            }
            if(lowest == null)
            {
                lowest = refreshLast();
            }
            if(lastX != null && firstX != null)
            {
                bool goDown = false;
                this.gameObject.transform.position += addVector;
                if (lastX.transform.position.x + lastX.transform.localScale.x > wallPositive.transform.position.x || firstX.transform.position.x - firstX.transform.localScale.x < wallNegative.transform.position.x)
                {
                    goDown = true;
                }
                if (goDown)
                {
                    addVector *= -1;
                    this.gameObject.transform.position += new Vector3(0, -0.5f, 0);
                    if(lowest.transform.position.y - lowest.transform.localScale.y/2 < GameObject.Find("player").transform.position.y + GameObject.Find("player").transform.localScale.y/2)
                    {
                        SceneManager.LoadScene("GameOver");
                    }
                }
                //this.gameObject.transform.position = new Vector3(Mathf.Clamp(this.gameObject.transform.position.x, (wallNegative.transform.position.x + firstX.transform.localScale.x/2) + (gameObject.transform.position.x - firstX.transform.position.x), wallPositive.transform.position.x - (lastX.transform.position.x - this.gameObject.transform.position.x)), this.gameObject.transform.position.y, 0);

            }
            else
            {
                canDo = false;
            }
            yield return new WaitForSeconds((0.5f / speed)*(upgradeScript.items["EnemyMoveDebuff"] * 1.2f + 1));
        }
        resetPos();
        if(wave % Mathf.Clamp(5*(DataManage.difficulty-1),5, 10) == 0)
        {
            referance.SetActive(true);
            int[] weightArray = new int[] { 0, 0, 6000, 3000, 1500, 22 };
            referance.GetComponent<upgradeScript>().updateButtons(weightArray);
        }
        else if(wave % 2 == 0 && wave % Mathf.Clamp(5 * (DataManage.difficulty - 1), 5, 10) != 0)
        {
            referance.SetActive(true);
            int[] weightArray = new int[] { 55000, 25000, 12900, 6308, 2000, 22 };
            referance.GetComponent<upgradeScript>().updateButtons(weightArray);
        }
        else
        {
            GameObject.Find("enemy manager").GetComponent<EnemyManager>().prepareWave(wave);
        }
        DestroyBullets();

    }
    void DestroyBullets()
    {
        foreach(GameObject bullet in GameObject.FindGameObjectsWithTag("enemyBullet"))
        {
            Destroy(bullet);
        }
    }
    GameObject refreshFirst()
    {
        List<GameObject> allChildrenObjects = refresh();
        GameObject firstX = allChildrenObjects.Where(x => x.transform.position.x == allChildrenObjects.Min(x => x.transform.position.x)).ToList()[0];
        return firstX;
    }
    GameObject refreshLast()
    {
        List<GameObject> allChildrenObjects = refresh();
        GameObject lastX = allChildrenObjects.Where(x => x.transform.position.x == allChildrenObjects.Max(x => x.transform.position.x)).ToList()[0];
        return lastX;
    }
    GameObject refreshLowest()
    {
        List<GameObject> allChildrenObjects = refresh();
        GameObject lastY = allChildrenObjects.Where(x => x.transform.position.y == allChildrenObjects.Min(x => x.transform.position.y)).ToList()[0];
        return lastY;

    }
    List<GameObject> refresh()
    {
        List<GameObject> allChildrenObjects = new List<GameObject>();
        foreach (Transform child in this.gameObject.transform)
        {
            allChildrenObjects.Add(child.gameObject);
        }
        return allChildrenObjects;
    }
}
