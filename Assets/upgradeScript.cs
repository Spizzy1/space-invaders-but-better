using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class upgradeScript : MonoBehaviour
{
    [SerializeField]
    List<Color> rarityColorList = new List<Color>();
    // Start is called before the first frame update
    void Start()
    {
        loadInList();

    }
    List<upgradeGeneric> loadInList()
    {
        GameObject.Find("Tooltip").GetComponent<TextMeshProUGUI>().text = " ";
        List<upgradeGeneric> upgradeList = new List<upgradeGeneric>();
        upgradeList.Add(new AttackSpeed(upgradeGeneric.rarity.common, "Decreases your attack cooldown by 10 percent")); //Sets all the rarities and tooltips of all upgrades
        upgradeList.Add(new HealthIncrease(upgradeGeneric.rarity.common, "Increases your HP by 3"));
        upgradeList.Add(new DamageIncrease(upgradeGeneric.rarity.common, "Increases your damage by 0.5"));
        upgradeList.Add(new BIGDamg(upgradeGeneric.rarity.legendary, "Doubles your damage!"));
        upgradeList.Add(new GlassCannon(upgradeGeneric.rarity.rare, "Makes any hit instakill you... but TRIPPLES damage"));
        upgradeList.Add(new PointMultiLeg(upgradeGeneric.rarity.legendary, "Subsequent points are DOUBLED!"));
        upgradeList.Add(new MythicTest(upgradeGeneric.rarity.MYTHIC, "BIG POINT"));
        upgradeList.Add(new SansTest(upgradeGeneric.rarity.SANS, "No cooldown"));
        upgradeList.Add(new unCommonDR(upgradeGeneric.rarity.uncommon, "Adds flat 0.2 damage reduction"));
        upgradeList.Add(new halfDamage(upgradeGeneric.rarity.legendary, "Halfs all incoming damage"));
        upgradeList.Add(new rareDamageMult(upgradeGeneric.rarity.rare, "Multiplies your damage by 20 percent"));
        upgradeList.Add(new PierceOne(upgradeGeneric.rarity.uncommon, "Makes your bullets pierce one more enemy"));
        upgradeList.Add(new PierceInf(upgradeGeneric.rarity.MYTHIC, "Makes your bullets pierce infinitely"));
        upgradeList.Add(new DoubleMovement(upgradeGeneric.rarity.legendary, "Doubles your movementspeed"));
        return upgradeList;
    }
    public void updateButtons(int[] weights)
    {
        List<upgradeGeneric> listOfUpgrades = loadInList();
        foreach (Transform child in GameObject.Find("Buttons").transform)
        {
            int randomNr = Random.Range(1, weights.Sum());
            Debug.Log(randomNr);
            if (randomNr <= weights[0]) //creates the item loot-table
            {
                List<upgradeGeneric> commonList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.common).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[0]; //changes color depending on rarity
                loadEffect(commonList, child.gameObject);
            }
            else if((randomNr > weights[0]) && (randomNr <= weights.Take(2).Sum()))
            {
                List<upgradeGeneric> unCommonList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.uncommon).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[1];
                loadEffect(unCommonList, child.gameObject);
            }
            else if(randomNr > weights.Take(2).Sum() && randomNr <= weights.Take(3).Sum())
            {
                List<upgradeGeneric> rareList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.rare).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[2];
                loadEffect(rareList, child.gameObject);
            }
            else if(randomNr > weights.Take(3).Sum() && randomNr <= weights.Take(4).Sum())
            {
                List<upgradeGeneric> legendaryList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.legendary).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[3];
                loadEffect(legendaryList, child.gameObject);
            }
            else if(randomNr > weights.Take(4).Sum() && randomNr <= weights.Take(5).Sum())
            {
                List<upgradeGeneric> MYTHICList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.MYTHIC).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[4];
                loadEffect(MYTHICList, child.gameObject);

            }
            else if(randomNr > weights.Take(5).Sum())
            {
                List<upgradeGeneric> SANSList = listOfUpgrades.Where(x => x.rarityType == upgradeGeneric.rarity.SANS).ToList();
                child.gameObject.GetComponent<Image>().color = rarityColorList[5];
                loadEffect(SANSList, child.gameObject);
            }

        }
    }
    void loadEffect(List<upgradeGeneric> filteredList, GameObject button)
    {
        int randomIndex = Random.Range(0, filteredList.Count);
        button.GetComponent<Button>().onClick.RemoveAllListeners();
        button.GetComponent<Button>().onClick.AddListener(start);
        button.GetComponent<Button>().onClick.AddListener(filteredList[randomIndex].Effect);
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
            base.player.GetComponent<shoot>().cooldownGeneral *= 0.9f;
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

            GameObject.Find("Lives").GetComponent<livesUI>().updateHP(updateHP);


        }
    }
    class DamageIncrease : upgradeGeneric
    {
        public DamageIncrease(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            base.player.GetComponent<shoot>().damage += 0.5f;
        }
    }
    class BIGDamg : upgradeGeneric
    {
        public BIGDamg(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            base.player.GetComponent<shoot>().damageMultiplier += 1;
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
            base.player.GetComponent<shoot>().damageMultiplier += 2;
        }
    }
    class PointMultiLeg : upgradeGeneric
    {
        public PointMultiLeg(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            GameObject.Find("Points").GetComponent<AddPoints>().pointMultiplier += 1;
        }
    }
    class MythicTest : upgradeGeneric
    {
        public MythicTest(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {

        }
        public override void Effect()
        {
            GameObject.Find("Points").GetComponent<AddPoints>().pointMultiplier += 10;
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
            player.GetComponent<shoot>().damageMultiplier += 0.2f;
        }
    }
    class halfDamage : upgradeGeneric
    {
        public halfDamage(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            player.GetComponent<shoot>().hurtMultiplier *= 0.5f;
        }
    }
    class unCommonDR : upgradeGeneric
    {
        public unCommonDR(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip)
        {
        }
        public override void Effect()
        {
            player.GetComponent<shoot>().damageReduction += 0.2f;
        }
    }
    class PierceOne : upgradeGeneric
    {
        public PierceOne(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip){}
        public override void Effect(){ player.GetComponent<shoot>().pierce += 1;}
    }
    class PierceInf : upgradeGeneric
    {
        public PierceInf(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { player.GetComponent<shoot>().pierce += 10000000; }
    }
    class DoubleMovement : upgradeGeneric
    {
        public DoubleMovement(rarity rarityConfig, string tooltip) : base(rarityConfig, tooltip) { }
        public override void Effect() { player.GetComponent<playermovement>().movespeed *= 2; }
    }
    #endregion
}
