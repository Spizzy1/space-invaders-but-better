using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeScript : MonoBehaviour
{
    Sprite[] spriteList = new Sprite[10];
    // Start is called before the first frame update
    void Start()
    {
        AttackSpeed adadad = new AttackSpeed(upgradeGeneric.upgradeList.Speed, upgradeGeneric.rarity.common, "TEST TEST TEST");
        Debug.Log(adadad.tooltip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    abstract class upgradeGeneric
    {
        public enum upgradeList
        {
            HP,
            Speed,
            Damage
        }
        protected upgradeGeneric(upgradeList TYPE, rarity RARITY, string TOOLTIP)
        {
            this.type = TYPE;
            this.rarityType = RARITY;
            this.tooltip = TOOLTIP;
        }
        upgradeList type;
        public enum rarity
        {
            common, 
            rare,
            epic
        }
        rarity rarityType;
        public string tooltip;
        public abstract void Effect();
    }
    class AttackSpeed : upgradeGeneric
    {
        public AttackSpeed(upgradeList attackType, rarity common, string tooltip) : base(attackType, common, tooltip)
        {
        }
        public override void Effect()
        {
            GameObject.Find("player").GetComponent<shoot>().cooldownGeneral *= 0.9f;
        }
    }
}
