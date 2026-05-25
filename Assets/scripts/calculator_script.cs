using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*coder shlomo simon (gamer101)
update log: 5/5/2026 started this project and created and shared the git repo as well as create a basic bit a caculations for banana farm (this part got deleted)
 5/10/2026 - restarted from the begging and was able to add starting values and a system for creating mk also decided to create update log
 5/11/2026 - added more MK/more complex MK as well a boat load of temporary variable that i will need to slowly replace one i define the related field
 5/12/2026 - finished adding all the MK and added a list of bloons and there chldren this will be used later for rounds also added a cash per pop caculator (most of the day was spent throwing my head at the wall trying to figure out why my math was wrong for the bad turns out i was just missing a single ddt)
 5/13/2026 - started working on the round set list this may take longer since i need some help with this part as well as learn more about this topic so i can be well versed in it and right and fix the code quickly
 5/17/2026 - imported all the game files from doombulble git repo used for help moding but i will be pulling data so i dont need to rewrite things like how long rounds last or what are in a round then we will add our caculations on top of it
 5/24/2026 - i decided that i will just manualy code everything instead because i am not knowadlagbale enought to mess with the game jsons files inorder to actualy to everything i need for what i actualy accomplished is add the first 10 standard round and set up a function for quicly filling out round data
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
    //creating a list to store all the diffrent types of bloons
    [Header("bloons")]
    public static List<BloonType> bloonstypeList = new List<BloonType>();
    //creating a list to store all the data of each round
    [Header("rounds")]
    [SerializeField]
    private List<Round> roundList = new List<Round>();

    // this array stores all the json files conataining each round
    [SerializeField]
    private TextAsset[] normalRoundSet;

    //gloable varaibles
    //created a vairable for bad for when we want to refrence it
    private BloonType bad;
    //created a vairable for bloon for when we want to refrence it
    private BloonType ceramic;
    //created a vairable for ceramic for when we want to refrence it
    private BloonType moab;
    //created a vairable for moab for when we want to refrence it
    private BloonType bfb;
    //created a vairable for bfb for when we want to refrence it
    private BloonType zomg;
    //created a vairable for zomg for when we want to refrence it
    private BloonType ddt;
    //created a vairable for ddt for when we want to refrence it
    private BloonType rainbow;
    //created a vairable for rainbow for when we want to refrence it
    private BloonType zebra;
    //created a vairable for zebra for when we want to refrence it
    private BloonType lead;
    //created a vairable for lead for when we want to refrence it
    private BloonType black;
    //created a vairable for black for when we want to refrence it
    private BloonType white;
    //created a vairable for white for when we want to refrence it
    private BloonType purple;
    //created a vairable for purple for when we want to refrence it
    private BloonType pink;
    //created a vairable for pink for when we want to refrence it
    private BloonType yellow;
    //created a vairable for bloyellowon for when we want to refrence it
    private BloonType green;
    //created a vairable for green for when we want to refrence it
    private BloonType blue;
    //created a vairable for blue for when we want to refrence it
    private BloonType red;


    private void Start() {
        // Create all Monkey Knowledge entries.
        InitializeMonkeyKnowledge();
        InitializeBloonType();
        InitializeStandardRounds();

        //invoc all the mk to test if its working
        testMK();

        //cacluate money for popping all the bloon down to nothing
        Debug.Log("bad bloon gives $" + CalculateCash(bad));
        Debug.Log("Ceramic: $" + CalculateCash(ceramic));
        Debug.Log("MOAB: $" + CalculateCash(moab));
        Debug.Log("BFB: $" + CalculateCash(bfb));
        Debug.Log("ZOMG: $" + CalculateCash(zomg));
        Debug.Log("DDT: $" + CalculateCash(ddt));

        // Show the final values to test some mk
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
    //this is a helper function for storing the bloon chiled and respect count of that child which is then stored as a list inside of bloonType
    [System.Serializable]
    public class BloonSpawn
    {
        public BloonType bloon;
        public int count;
    }

    //setting up the property of bloons
    // setting up the property of bloons
    [System.Serializable]
    public class BloonType
    {
        //stores the name of bloon 
        public string bloonName;

        //stores the speed of the bloon
        public float speed;

        // List of child bloons and how many of each are spawned
        public List<BloonSpawn> spawns = new List<BloonSpawn>();

        //stores bloon class/property
        public string bloonClass;
    }

    // Stores ONE spawn group inside a round
    [System.Serializable]
    public class RoundSpawn
    {
        // Which bloon spawns
        public BloonType bloon;

        // How many spawn
        public int count;
    }

    // Stores an entire round
    [System.Serializable]
    public class Round
    {
        // Round number
        public int roundNumber;

        // All spawn groups in the round
        public List<RoundSpawn> spawns = new List<RoundSpawn>();

        // Cash modifier
        public float cashMultiplier = 1.0f;

        // Time when the last bloon spawns (important for farm tics and penelty timer)
        public float endTime;
    }

    private void AddSpawn(Round round, BloonType bloon, int count)
    {
        round.spawns.Add(new RoundSpawn
        {
            bloon = bloon,
            count = count
        });
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

    //this is a self refrence function that calculates a bloon chash value using a rushin nesting doll style system
    public int CalculateCash(BloonType bloon)
    {
        //safty check for bloon
        if (bloon == null)
        {
            Debug.LogError("CalculateCash was called with a null bloon!");
            return 0;
        }

        int total = 1;

        //safty check for bloon child
        if (bloon.spawns == null)
            return total;

        //looping through all the bloon spawn
        foreach (BloonSpawn spawn in bloon.spawns)
        {
            //finale safty check
            if (spawn == null || spawn.bloon == null)
            {
                Debug.LogError("Null child found inside " + bloon.bloonName);
                continue;
            }

            //adding the amount spawned multipy by the child which then calls on itself until we whent through all the bloon children
            total += spawn.count * CalculateCash(spawn.bloon);
        }

        return total;
    }

    // this function take each of the bloons that are in the round and applies the caculation to each of them
    public int CalculateRoundCash(Round currentRound)
    {
        // Safety check
        if (currentRound == null)
        {
            Debug.LogError("Round was null!");
            return 0;
        }

        int totalCash = 0;

        // Loop through all spawn groups
        foreach (RoundSpawn spawn in currentRound.spawns)
        {
            // Safety checks
            if (spawn == null || spawn.bloon == null)
                continue;

            // Get value of ONE bloon
            int bloonCash = CalculateCash(spawn.bloon);

            // Multiply by amount spawned
            totalCash += bloonCash * spawn.count;
        }

        // Apply round multiplier
        totalCash = Mathf.RoundToInt(totalCash * currentRound.cashMultiplier);

        return totalCash;
    }

    //this initalizes the data for each round then defines the diffrent rushes in each round (note it is grouped by bloon layering)
    private void InitializeStandardRounds()
    {
        // ROUND 1
        Round round1 = new Round
        {
            roundNumber = 1,
            cashMultiplier = 1.0f,
            endTime = 17.51f,
        };

        AddSpawn(round1, red, 20);

        roundList.Add(round1);

        // ROUND 2
        Round round2 = new Round
        {
            roundNumber = 2,
            cashMultiplier = 1.0f,
            endTime = 19.00f,
        };

        AddSpawn(round2, red, 35);

        roundList.Add(round2);

        // ROUND 3
        Round round3 = new Round
        {
            roundNumber = 3,
            cashMultiplier = 1.0f,
            endTime = 16.71f,
        };

        AddSpawn(round3, red, 25);
        AddSpawn(round3, blue, 5);

        roundList.Add(round3);

        // ROUND 4
        Round round4 = new Round
        {
            roundNumber = 4,
            cashMultiplier = 1.0f,
            endTime = 17.31f,
        };

        AddSpawn(round4, red, 35);
        AddSpawn(round4, blue, 18);

        roundList.Add(round4);

        // ROUND 5
        Round round5 = new Round
        {
            roundNumber = 5,
            cashMultiplier = 1.0f,
            endTime = 16.50f,
        };

        AddSpawn(round5, blue, 27);
        AddSpawn(round5, red, 5);

        roundList.Add(round5);

        // ROUND 6
        Round round6 = new Round
        {
            roundNumber = 6,
            cashMultiplier = 1.0f,
            endTime = 18.70f,
        };

        AddSpawn(round6, red, 15);
        AddSpawn(round6, green, 4);
        AddSpawn(round6, blue, 15);

        roundList.Add(round6);

        // ROUND 7
        Round round7 = new Round
        {
            roundNumber = 7,
            cashMultiplier = 1.0f,
            endTime = 26.80f,
        };

        AddSpawn(round7, green, 5);
        AddSpawn(round7, blue, 20);
        AddSpawn(round7, red, 20);

        roundList.Add(round7);

        // ROUND 8
        Round round8 = new Round
        {
            roundNumber = 8,
            cashMultiplier = 1.0f,
            endTime = 28.87f,
        };

        AddSpawn(round8, blue, 20);
        AddSpawn(round8, green, 14);
        AddSpawn(round7, red, 10);

        roundList.Add(round8);

        // ROUND 9
        Round round9 = new Round
        {
            roundNumber = 9,
            cashMultiplier = 1.0f,
            endTime = 18.95f,
        };

        AddSpawn(round9, green, 30);

        roundList.Add(round9);

        // ROUND 10
        Round round10 = new Round
        {
            roundNumber = 10,
            cashMultiplier = 1.0f,
            endTime = 47.99f,
        };

        AddSpawn(round10, blue, 102);

        roundList.Add(round10);
    }

    // define each of the bloons and then pushing them to the bloontype list
    private void InitializeBloonType()
    {
        red = new BloonType
        {
            bloonName = "red",
            speed = 1.0f,
            spawns = new List<BloonSpawn>(),
            bloonClass = "bloon"
        };

        blue = new BloonType
        {
            bloonName = "blue",
            speed = 1.4f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = red, count = 1 }
            },
            bloonClass = "bloon"
        };

        green = new BloonType
        {
            bloonName = "green",
            speed = 1.8f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = blue, count = 1 }
            },
            bloonClass = "bloon"
        };

        yellow = new BloonType
        {
            bloonName = "yellow",
            speed = 3.2f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = green, count = 1 }
            },
            bloonClass = "bloon"
        };

        pink = new BloonType
        {
            bloonName = "pink",
            speed = 3.5f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = yellow, count = 1 }
            },
            bloonClass = "bloon"
        };

        black = new BloonType
        {
            bloonName = "black",
            speed = 1.8f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = pink, count = 2 }
            },
            bloonClass = "black"
        };

        purple = new BloonType
        {
            bloonName = "purple",
            speed = 3.0f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = pink, count = 2 }
            },
            bloonClass = "purple"
        };

        white = new BloonType
        {
            bloonName = "white",
            speed = 2.0f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = pink, count = 2 }
            },
            bloonClass = "white"
        };

        lead = new BloonType
        {
            bloonName = "lead",
            speed = 1.0f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = black, count = 2 }
            },
            bloonClass = "lead"
        };

        zebra = new BloonType
        {
            bloonName = "zebra",
            speed = 1.8f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = white, count = 1 },
                new BloonSpawn { bloon = black, count = 1 }
            },
            bloonClass = "zebra"
        };

        rainbow = new BloonType
        {
            bloonName = "rainbow",
            speed = 2.2f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = zebra, count = 2 }
            },
            bloonClass = "bloon"
        };

        ceramic = new BloonType
        {
            bloonName = "ceramic",
            speed = 2.5f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = rainbow, count = 2 }
            },
            bloonClass = "ceramic"
        };
        
        moab = new BloonType
        {
            bloonName = "moab",
            speed = 1.0f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = ceramic, count = 4 }
            },
            bloonClass = "moab"
        };
        
        bfb = new BloonType
        {
            bloonName = "bfb",
            speed = 0.25f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = moab, count = 4 }
            },
            bloonClass = "moab"
        };

        zomg = new BloonType
        {
            bloonName = "zomg",
            speed = 0.18f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = bfb, count = 4 }
            },
            bloonClass = "moab"
        };

        ddt = new BloonType
        {
            bloonName = "ddt",
            speed = 2.64f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = ceramic, count = 4 }
            },
            bloonClass = "moab"
        };

        bad = new BloonType
        {
            bloonName = "bad",
            speed = 0.18f,
            spawns = new List<BloonSpawn>
            {
                new BloonSpawn { bloon = zomg, count = 2 },
                new BloonSpawn { bloon = ddt, count = 3 }
            },
            bloonClass = "moab"
        };

        // Add all bloons to the master list
        bloonstypeList.Add(red);
        bloonstypeList.Add(blue);
        bloonstypeList.Add(green);
        bloonstypeList.Add(yellow);
        bloonstypeList.Add(pink);
        bloonstypeList.Add(black);
        bloonstypeList.Add(purple);
        bloonstypeList.Add(white);
        bloonstypeList.Add(lead);
        bloonstypeList.Add(zebra);
        bloonstypeList.Add(rainbow);
        bloonstypeList.Add(ceramic);
        bloonstypeList.Add(moab);
        bloonstypeList.Add(bfb);
        bloonstypeList.Add(zomg);
        bloonstypeList.Add(ddt);
        bloonstypeList.Add(bad);
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