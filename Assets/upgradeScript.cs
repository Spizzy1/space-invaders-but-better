using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class upgradeScript : MonoBehaviour
{
    [SerializeField]
    List<Color> rarityColorList = new List<Color>();

    public static Dictionary<string, int> items = new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        loadInList();
        loadItems();
        gameObject.SetActive(false);
    }
    List<upgradeGeneric> loadInList()
    {
        GameObject.Find("Tooltip").GetComponent<TextMeshProUGUI>().text = " ";
        List<upgradeGeneric> upgradeList = new List<upgradeGeneric>();
        #region classItemList
        upgradeList.Add(new AttackSpeed(upgradeGeneric.rarity.common, "Decreases your attack cooldown by 10 percent")); //Sets all the rarities and tooltips of all upgrades
        upgradeList.Add(new HealthIncrease(upgradeGeneric.rarity.common, "Increases your HP by 3"));
        upgradeList.Add(new DamageIncrease(upgradeGeneric.rarity.common, "Increases your damage by 0.5"));
        upgradeList.Add(new BIGDamg(upgradeGeneric.rarity.MYTHIC, "Doubles your damage"));
        upgradeList.Add(new GlassCannon(upgradeGeneric.rarity.legendary, "Makes any hit instakill you... but TRIPPLES damage"));
        upgradeList.Add(new PointMultiLeg(upgradeGeneric.rarity.legendary, "Subsequent points are increased"));
        upgradeList.Add(new MythicTest(upgradeGeneric.rarity.SANS, "BIG POINT"));
        upgradeList.Add(new SansTest(upgradeGeneric.rarity.SANS, "No cooldown"));
        upgradeList.Add(new unCommonDR(upgradeGeneric.rarity.uncommon, "Adds flat 0.2 damage reduction"));
        upgradeList.Add(new halfDamage(upgradeGeneric.rarity.MYTHIC, "Halfs all incoming damage"));
        upgradeList.Add(new rareDamageMult(upgradeGeneric.rarity.rare, "Multiplies your damage by 20 percent"));
        upgradeList.Add(new PierceOne(upgradeGeneric.rarity.rare, "Makes your bullets pierce one more enemy"));
        upgradeList.Add(new PierceInf(upgradeGeneric.rarity.SANS, "Makes your bullets pierce infinitely"));
        upgradeList.Add(new DoubleMovement(upgradeGeneric.rarity.rare, "Increases your movementspeed"));
        upgradeList.Add(new DoubleShot(upgradeGeneric.rarity.legendary, "Makes you shoot one extra shot... but severely lowers shooting speed"));
        upgradeList.Add(new ShotSpeedFifty(upgradeGeneric.rarity.uncommon, "Makes your shots 50 percent faster"));
        upgradeList.Add(new HolyMantle(upgradeGeneric.rarity.MYTHIC, "Gives you a rechargable shield, subsequent stacks lower cooldown"));
        upgradeList.Add(new JustKillsYou(upgradeGeneric.rarity.SANS, "Legit just ends your run lmao"));
        upgradeList.Add(new EnemyMoveDebuff(upgradeGeneric.rarity.uncommon, "Slows down enemy movement"));
        upgradeList.Add(new Clover(upgradeGeneric.rarity.MYTHIC, "Better items"));
        upgradeList.Add(new ShatterBullets(upgradeGeneric.rarity.legendary, "Makes your bullets shatter into weaker bullets on impact"));
        upgradeList.Add(new OneHitHalf(upgradeGeneric.rarity.common, "First hit per wave has decreased damage"));
        #endregion
        return upgradeList;
    }
    void loadItems()
    {
        #region itemKeyList
        items.Clear();
        items.Add("attackspeedIncrease", 0);
        items.Add("damageUP", 0);
        items.Add("doubleDamage", 0);
        items.Add("glassCannon", 0);
        items.Add("doublePoint", 0);
        items.Add("bigPoint", 0);
        items.Add("unCommonDR", 0);
        items.Add("halfDamage", 0);
        items.Add("smallDamageMulti", 0);
        items.Add("pierceOne", 0);
        items.Add("moveIncrease", 0);
        items.Add("doubleShoot", 0);
        items.Add("shotIncrease", 0);
        items.Add("pierceInf", 0);
        items.Add("HolyMantle", 0);
        items.Add("JustKillsYou", 0);
        items.Add("EnemyMoveDebuff", 0);
        items.Add("soper luck", 0);
        items.Add("shatterBullet", 0);
        items.Add("oneHithalf", 0);
        #endregion
    }
    public void updateButtons(int[] weights)
    {
        List<upgradeGeneric> listOfUpgrades = loadInList();
        foreach (Transform child in GameObject.Find("Buttons").transform)
        {
            int randomNr = 0;
            for (int i = 0; i < 1+items["soper luck"]; i++)
            {
                int tempNr = Random.Range(1, weights.Sum());
                if(tempNr > randomNr)
                {
                    randomNr = tempNr;
                }
            }
            Debug.Log(randomNr);
            if (randomNr <= weights[0]) //creates the item loot-table
            {
                List<upgradeGeneric> commonList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.common).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[0]; //changes color depending on rarity
                loadEffect(commonList, child.gameObject, ref listOfUpgrades);
            }
            else if((randomNr > weights[0]) && (randomNr <= weights.Take(2).Sum()))
            {
                List<upgradeGeneric> unCommonList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.uncommon).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[1];
                loadEffect(unCommonList, child.gameObject, ref listOfUpgrades);
            }
            else if(randomNr > weights.Take(2).Sum() && randomNr <= weights.Take(3).Sum())
            {
                List<upgradeGeneric> rareList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.rare).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[2];
                loadEffect(rareList, child.gameObject, ref listOfUpgrades);
            }
            else if(randomNr > weights.Take(3).Sum() && randomNr <= weights.Take(4).Sum())
            {
                List<upgradeGeneric> legendaryList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.legendary).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[3];
                loadEffect(legendaryList, child.gameObject, ref listOfUpgrades);
            }
            else if(randomNr > weights.Take(4).Sum() && randomNr <= weights.Take(5).Sum())
            {
                List<upgradeGeneric> MYTHICList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.MYTHIC).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[4];
                loadEffect(MYTHICList, child.gameObject, ref listOfUpgrades);

            }
            else if(randomNr > weights.Take(5).Sum())
            {
                List<upgradeGeneric> SANSList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.SANS).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[5];
                loadEffect(SANSList, child.gameObject, ref listOfUpgrades);
            }

        }
    }
    void loadEffect(List<upgradeGeneric> filteredList, GameObject button, ref List<upgradeGeneric> mainList)
    {
        int randomIndex = Random.Range(0, filteredList.Count);
        button.GetComponent<Button>().onClick.RemoveAllListeners();
        button.GetComponent<Button>().onClick.AddListener(start);
        button.GetComponent<Button>().onClick.AddListener(filteredList[randomIndex].Effect);
        mainList.Remove(filteredList[randomIndex]);
        Debug.Log(randomIndex);
        Debug.Log(filteredList);
        button.GetComponent<ButtonScript>().tooltip = filteredList[randomIndex].tooltip;
    }
    public void start()
    {
        GameObject.Find("enemy manager").GetComponent<EnemyManager>().prepareWave(GameObject.Find("EnemyGroup").GetComponent<EnemyMovement>().wave);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region upgrades
    abstract class upgradeGeneric
    {
        protected GameObject player = GameObject.Find("player");
        protected upgradeGeneric(rarity RARITY, string TOOLTIP)
        {
            this.rarityType = RARITY;
            this.tooltip = TOOLTIP;
            this.isUsed = false;
        }
        public enum rarity
        {
            common,
            uncommon,
            rare,
            legendary,
            MYTHIC,
            SANS
        }
        public bool isUsed;
        public rarity rarityType;
        public string tooltip;
        public abstract void Effect();
    }
    class AttackSpeed : upgradeGeneric
    {
        public AttackSpeed(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            items["attackspeedIncrease"] += 1;
        }
    }
    class HealthIncrease : upgradeGeneric
    {
        public HealthIncrease(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            float updateHP = base.player.GetComponent<shoot>().health + 3;
            base.player.GetComponent<shoot>().health += 3;

            GameObject.Find("Lives").GetComponent<livesUI>().updateHP(updateHP, items["glassCannon"] > 0);


        }
    }
    class DamageIncrease : upgradeGeneric
    {
        public DamageIncrease(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            items["damageUP"] += 1;
        }
    }
    class BIGDamg : upgradeGeneric
    {
        public BIGDamg(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            items["doubleDamage"] += 1;
        }
    }
    class GlassCannon : upgradeGeneric
    {
        public GlassCannon(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            base.player.GetComponent<shoot>().glassCanon = true;
            GameObject.Find("Lives").GetComponent<livesUI>().GlassHP();
            items["glassCannon"] += 1;
        }
    }
    class PointMultiLeg : upgradeGeneric
    {
        public PointMultiLeg(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            items["doublePoint"] += 1;
        }
    }
    class MythicTest : upgradeGeneric
    {
        public MythicTest(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {

        }
        public override void Effect()
        {
            items["bigPoint"] += 1;
        }
    }
    class SansTest : upgradeGeneric
    {
        public SansTest(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {

        }
        public override void Effect()
        {
            base.player.GetComponent<shoot>().cooldownGeneral = 0;
        }
    }
    class rareDamageMult : upgradeGeneric
    {
        public rareDamageMult(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            items["smallDamageMulti"] += 1;
        }
    }
    class halfDamage : upgradeGeneric
    {
        public halfDamage(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            items["pierceOne"] += 1;
        }
    }
    class unCommonDR : upgradeGeneric
    {
        public unCommonDR(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            items["unCommonDR"] += 1;
        }
    }
    class PierceOne : upgradeGeneric
    {
        public PierceOne(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip){}
        public override void Effect() { items["pierceOne"] += 1;}
    }
    class PierceInf : upgradeGeneric
    {
        public PierceInf(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["pierceInf"] += 1; }
    }
    class DoubleMovement : upgradeGeneric
    {
        public DoubleMovement(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["moveIncrease"] += 1; }
    }
    class DoubleShot : upgradeGeneric
    {
        public DoubleShot(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["doubleShoot"] += 1; }
    }
    class ShotSpeedFifty : upgradeGeneric
    {
        public ShotSpeedFifty(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["shotIncrease"] += 1; }
    }
    class HolyMantle : upgradeGeneric
    {
        public HolyMantle(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["HolyMantle"] += 1; }
    }
    class JustKillsYou : upgradeGeneric
    {
        public JustKillsYou(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { SceneManager.LoadScene("GameOver"); }
    }
    class EnemyMoveDebuff : upgradeGeneric
    {
        public EnemyMoveDebuff(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["EnemyMoveDebuff"] += 1; }
    }
    class Clover : upgradeGeneric
    {
        public Clover(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["soper luck"] += 1; }
    }
    class ShatterBullets : upgradeGeneric
    {
        public ShatterBullets(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["shatterBullet"] += 1; }
    }
    class OneHitHalf : upgradeGeneric
    {
        public OneHitHalf(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["oneHithalf"] += 1; }
    }
    #endregion
}
