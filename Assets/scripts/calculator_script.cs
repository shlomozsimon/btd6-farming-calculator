using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*coder shlomo simon (gamer101)
update log: 5/5/2026 started this project and created and shared the git repo as well as create a basic bit a caculations for banana farm (this part got deleted)
 5/10/2026 - restarted from the begging and was able to add starting values and a system for creating mk also decided to create update log
 5/11/2026 - added more MK/more complex MK as well a boat load of temporary variable that i will need to slowly replace one i define the related field
 5/12/2026 - finished adding all the MK
*/
public class CalculatorScript : MonoBehaviour
{
    // ---------------------------------------------------------------------
    // TEMPORARY VARIABLES
    // These are placeholder variables so your Monkey Knowledge rules compile
    // until the full tower systems are implemented.
    // ---------------------------------------------------------------------

    // Temporary variable for IMF Loan amount.
    // "Backroom Deals" increases the IMF loan by $1000.
    private int loan = 10000;

    // Temporary variable for IMF loan repayment percentage.
    // "Backroom Deals" reduces the repayment amount to 40%.
    private float repay = 1.0f;   // 100% repayment by default

    // Temporary variable for Monkey Town income bonus.
    // "Inland Revenue Streams" increases Monkey Town income by 10%.
    private float monkeyTownBonus = 0.0f;

    // Temporary variable for XXXL Trap RBE capacity.
    // "Big Traps" increases trap capacity by 30 RBE.
    private int trapRBE = 1000;

    // Temporary variable for Hero upgrade discount.
    // "Scholarships" reduces hero upgrade costs by 10%.
    private float heroUpgradeDiscount = 0.0f;

    // Temporary variable for Hero XP gain bonus.
    // "Self Taught Heroes" increases XP gain by 10%.
    private float heroXPGain = 0.0f;

    // Temporary variable for Hero placement discount.
    // "Hero Favors" reduces hero purchase cost by 10%.
    private float heroDiscount = 0.0f;

    // Temporary variable for starting Hero level.
    // "Empowered Heroes" allows heroes to start at level 3.
    private int heroLvl = 1;

    // Temporary variables for Merchantmen / Trade Empire farming.
    // "Trade Agreements" adds +$20 to each Merchantman.
    private int merchant = 0;

    // Temporary variable for Support Chinook crate value.
    // "Charged Chinooks" increases crate value by 25%.
    private float helliCrate = 1000f;

    // Temporary variable for Heart of Oak discount.
    // "Warm Oak" reduces the cost by $100.
    private int heartOfOakDiscount = 650;

    // Temporary variable for Heart of farm subsidy.
    // "farm subsidy" reduces first farm by $100
    private int cost = 1250;

    // Temporary variable for valuble bananas
    // "valuble bananas" increased by 5%.
    private float valubleBananas = 1.25f;

    // temporary variable for bigger banks
    // "bigger banks" increase max capasity of banks by $2500
    private int maxHold = 7000;

    public enum TowerType{
        farm,
        vilage,
        Merchantman,
        SupportChinook,
        Other
    }
    [SerializeField]
     TowerType towerType = TowerType.farm;

    public enum TowerCategory{
        Primary,
        Military,
        Magic,
        Support
    }

    // Current tower category being evaluated.
    [SerializeField]
    private TowerCategory tower = TowerCategory.Support;

    // Number of military towers already placed.
    // Used by "Military Conscription".
    [SerializeField]
    private int militaryTowerCount = 0;

    // Number of farms already placed.
    // Used by "farm subsidy".
    [SerializeField]
    private int farmTowerCount = 0;

    // Highest relevant upgrade tier for the tower currently being evaluated.
    // Used by "Come On Everybody!"
    [SerializeField]
    private int upgrade = 0;
    
   
   
    //end of temp variables
    private int startingCash = 650;
    //sell back value
    private float sellBack = 0.7f;
    //determning weether you can deposit into banks or not
    private bool canDeposit = false;
    //discount precentage
    [SerializeField]
    private float discount = 0.0f;
    //creating a list to store all of the MK we will be defining
    [Header("Monkey Knowledge")]
    [SerializeField]
    private List<MonkeyKnowledge> monkeyKnowledgeList = new List<MonkeyKnowledge>();

    private void Start() {
        // Create all Monkey Knowledge entries.
        InitializeMonkeyKnowledge();

        //invoc all the mk to test if its working
        testMK();

        // Show the final values after Monkey Knowledge has been applied.
        Debug.Log("Starting Cash: $" + startingCash);
        Debug.Log("Sell Back: " + (sellBack * 100f) + "%");
        Debug.Log("Can Deposit Into Banks: " + canDeposit);
        
    }

    //setting up MK in this function
    public class MonkeyKnowledge
    {
        //stores the name of the mk
        public string MKName;
        
        //stores wether the mk is active or not
        public bool isActive;

        //store costom code that we can call apon inorder excute what ever the mk does note that action canot return a value
         public System.Action MKRule; 
    }

    private void testMK()
    {
        //looping through the list of mk and looking at each mk inside the list and if isActive is true then we run the code stored in it
        foreach(MonkeyKnowledge MK in monkeyKnowledgeList)
        {
            //we check to see if there is code for MKRule to run so we dont crash if its missing
            if (MK.isActive && MK.MKRule != null)
            {
                //unity runs on a dot matrix method so we get the MK wich is a veriable that is storing the mk we have pulled from the list then we get the MKRule that that Mk data is stroring then we invoke which just runs the code stroed in it
                MK.MKRule.Invoke();
            }
        }
    }

    //creating the mk with all of its atributes and then pushing it into a list
    private void InitializeMonkeyKnowledge()
    {
        //adding the extra starting cash MK
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "More cash",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => startingCash += 200,
        });
        //adding the extra sell back MK
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "Better Sell Deals",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => sellBack += 0.05f,
        });
        //adding the mk that allowes you to deposit into imfs
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "Bank Deposits",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => canDeposit = true,
        });
        //adding the mk that will increase boat prduction by $20
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "trade agreements",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            //note merchant varibal is temp until actualy define boat farming
            MKRule = () => merchant += 20,
        });
        //adding the mk that will increase creats by 25%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "charged chinooks",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            //note helliCrate varibal is temp until actualy helli farming
            MKRule = () => helliCrate *= 1.25f,
        });
        //adding the mk that will decrease hart of oak by $100
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "warm oak",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => heartOfOakDiscount -= 100,
        });
        /*adding the mk that will increase attack speed by 5% note it is disabled for now since i havent coded tower states which this function will eventualy refrence
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "speedy brewing",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => alc.attackSpeed *= 1.05f,
        });*/
        //adding the mk that will give a 5% discount to all military towers
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "advanced logistics",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => {
                if(tower == TowerCategory.Military){
                    discount = 0.05f;
                }
            },
        });
        //adding the mk that will increase creats by 25%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "milirary conscription",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => {
                if(tower == TowerCategory.Military && militaryTowerCount > 0){
                    discount = 0.33f;
                }
            },
        });
        //adding the mk that will discount by 5% if everthing is tier 3 or 4
         monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "come on everybody!",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => {
                if(tower == TowerCategory.Primary && (upgrade == 3 || upgrade == 4)){
                    discount = 0.05f;
                }
            },
        });
        //adding the mk that will discount village and farms by 2% and increase sell back by 2%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "flat pack buildings",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => {
                if(towerType == TowerType.farm || towerType == TowerType.vilage){
                    discount += 0.02f;
                    sellBack += 0.02f;
                }
            },
        });
        //adding the mk that will discount by an adintiale 2%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "inside trades",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => discount += 0.02f,
        });
        //adding the mk that will discount by an adintiale 2%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "inside trades",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => discount += 0.02f,
        });
        //adding the mk that will valuable banaas by an adintiale 5%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "inside trades",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => valubleBananas += 0.05f,
        });
        //adding the mk that will increase max capasity of banks by $2500
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "bigger banks",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => maxHold += 2500,
        });
        //adding the mk that increases imf loan by 1000 and a repay rate of 40%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "backroom deals",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => {
                loan += 1000;
                repay = 0.4f;
            }
        });
        //adding the mk that increases monkey town by 10%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "inland revenue streams",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => monkeyTownBonus += 0.1f,
        });
        //adding the mk that increases trap by 30 RBE
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "big traps",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => trapRBE += 30,
        });
        //adding the mk that decrease hero cost by 10%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "scholarships",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => heroUpgradeDiscount = 0.1f,
        });
        //adding the mk that increase hero xp gain by 10%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "self taught heroes",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => heroXPGain = 0.1f,
        });
        //adding the mk that increase hero xp gain by 10%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "self taught heroes",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => heroXPGain = 0.1f,
        });
        //adding the mk that decrease placment cost by 10%
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "hero favors",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => heroDiscount = 0.1f,
        });
        //adding the mk that lets heros start at level 3
        monkeyKnowledgeList.Add(new MonkeyKnowledge
        {
            MKName = "empowered heroes",

            isActive = true,

            //creating a blank function attached to the mk so if you call this mk you can excute this code
            MKRule = () => heroLvl = 3,
        });
    }
}