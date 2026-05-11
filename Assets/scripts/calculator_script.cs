using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*coder shlomo simon (gamer101)
update log: 5/5/2026 started this project and created and shared the git repo as well as create a basic bit a caculations for banana farm (this part got deleted)
 5/10/2026 - restarted from the begging and was able to add starting values and a system for creating mk also decided to create update log
*/
public class CalculatorScript : MonoBehaviour
{
    //base value we start with when starting a game
    private int startingCash = 650;
    //sell back value
    private float sellBack = 0.7f;
    //determning weether you can deposit into banks or not
    private bool canDeposit = false;
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
            if (MK.isActive)
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
    }
}