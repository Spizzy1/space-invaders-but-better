using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Utils;

public class upgradeScript : MonoBehaviour
{
    [SerializeField]
    GameObject rerollReferance;
    [SerializeField]
    List<Color> rarityColorList = new List<Color>();
    int rerolls;

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
        upgradeList.Add(new AttackSpeed(upgradeGeneric.rarity.common, "Increases attack speed")); //Sets all the rarities and tooltips of all upgrades
        upgradeList.Add(new HealthIncrease(upgradeGeneric.rarity.common, "Increases your HP by 3"));
        upgradeList.Add(new DamageIncrease(upgradeGeneric.rarity.common, "Flat damage increase"));
        upgradeList.Add(new BIGDamg(upgradeGeneric.rarity.MYTHIC, "Doubles your damage"));
        upgradeList.Add(new GlassCannon(upgradeGeneric.rarity.legendary, "Makes any hit instakill you... but doubles damage"));
        upgradeList.Add(new PointMultiLeg(upgradeGeneric.rarity.legendary, "Increases point gain"));
        upgradeList.Add(new MythicTest(upgradeGeneric.rarity.SANS, "BIG POINT"));
        upgradeList.Add(new SansTest(upgradeGeneric.rarity.SANS, "No cooldown"));
        upgradeList.Add(new unCommonDR(upgradeGeneric.rarity.uncommon, "Gives flat damage reduction"));
        upgradeList.Add(new halfDamage(upgradeGeneric.rarity.MYTHIC, "Halfs all damage"));
        upgradeList.Add(new rareDamageMult(upgradeGeneric.rarity.rare, "20 percnet more damage"));
        upgradeList.Add(new PierceOne(upgradeGeneric.rarity.rare, "Makes your bullets pierce one more enemy"));
        upgradeList.Add(new PierceInf(upgradeGeneric.rarity.SANS, "Makes your bullets pierce infinitely"));
        upgradeList.Add(new DoubleMovement(upgradeGeneric.rarity.rare, "Increases your movementspeed"));
        upgradeList.Add(new DoubleShot(upgradeGeneric.rarity.legendary, "Makes you shoot more shots but lowers attack speed"));
        upgradeList.Add(new ShotSpeedFifty(upgradeGeneric.rarity.uncommon, "Increases shot speed"));
        upgradeList.Add(new HolyMantle(upgradeGeneric.rarity.MYTHIC, "Grants you a rechargable shield"));
        upgradeList.Add(new JustKillsYou(upgradeGeneric.rarity.SANS, "Legit just ends your run lmao"));
        upgradeList.Add(new EnemyMoveDebuff(upgradeGeneric.rarity.uncommon, "Slows down enemy movement"));
        upgradeList.Add(new Clover(upgradeGeneric.rarity.MYTHIC, "Better items"));
        upgradeList.Add(new ShatterBullets(upgradeGeneric.rarity.legendary, "Your bullets shatter into weaker bullets"));
        upgradeList.Add(new OneHitHalf(upgradeGeneric.rarity.common, "First hit per wave has decreased damage"));
        upgradeList.Add(new Reroll(upgradeGeneric.rarity.legendary, "Lets you reroll items"));
        upgradeList.Add(new StoreDamage(upgradeGeneric.rarity.rare, "Hold X to enable a damage storing shield"));
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
        items.Add("reroll", 0);
        items.Add("damageStore", 0);
        #endregion
    }
    public void updateButtons(int[] weights, bool isReroll = false)
    {
        if (!isReroll && items["reroll"] > 0)
        {
            rerollReferance.SetActive(true);
            rerolls = items["reroll"];
            rerollReferance.GetComponent<Button>().onClick.RemoveAllListeners();
            rerollReferance.GetComponent<Button>().onClick.AddListener(() => { updateButtons(weights, true); checkInactive(); });
        }
        List<upgradeGeneric> listOfUpgrades = loadInList();
        foreach (Transform child in GameObject.Find("Buttons").transform)
        {
            float randomNr = 0;
            for (int i = 0; i < 1+items["soper luck"]; i++)
            {
                float tempNr = Random.Range(0f, 1f);
                if(tempNr > randomNr)
                {
                    randomNr = tempNr;
                }
            }
            WeightedList<upgradeGeneric.rarity> rarityList = new WeightedList<upgradeGeneric.rarity>();
            rarityList.Add(upgradeGeneric.rarity.common, weights[0]);
            rarityList.Add(upgradeGeneric.rarity.uncommon, weights[1]);
            rarityList.Add(upgradeGeneric.rarity.rare, weights[2]);
            rarityList.Add(upgradeGeneric.rarity.legendary, weights[3]);
            rarityList.Add(upgradeGeneric.rarity.MYTHIC, weights[4]);
            rarityList.Add(upgradeGeneric.rarity.SANS, weights[5]);
            upgradeGeneric.rarity chosenRarity = rarityList.RandomItem(randomNr);
            int chosenIndex = rarityList.RandomIndex(randomNr);

            List<upgradeGeneric> itemList = listOfUpgrades.Where(x => x.rarityType == chosenRarity).ToList();
            child.gameObject.GetComponent<Image>().color = rarityColorList[chosenIndex]; //changes color depending on rarity
            loadEffect(itemList, child.gameObject, ref listOfUpgrades);

        }
        void checkInactive()
        {
            rerolls--;
            if (rerolls <= 0)
            {
                rerollReferance.SetActive(false);
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
            float updateHP = base.player.GetComponent<Shoot>().health + 3;
            base.player.GetComponent<Shoot>().health += 3;

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
            base.player.GetComponent<Shoot>().glassCanon = true;
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
            base.player.GetComponent<Shoot>().cooldownGeneral = 0;
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
    class Reroll : upgradeGeneric
    {
        public Reroll(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["reroll"] += 1; }
    }
    class StoreDamage : upgradeGeneric
    {
        public StoreDamage(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { items["damageStore"] += 1; }
    }
    #endregion
}
