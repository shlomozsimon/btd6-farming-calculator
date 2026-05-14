using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is pulled strait from the btd6 josn file then simplfide by chatgpt beacuse that data was a bit over my head
//if you want i would love learn what all the origanl data was and how i could use it in my own code as i am still kind of new to unity and C#
//but the actual data was simplfide to remove usles data like sprite and imunitys as we wont be using them as wel as making it fit the data type i have defined
public class roundsetData : MonoBehaviour
{
    [System.Serializable]
    public class BloonGroup
    {
        // Type of bloon to spawn (Red, Blue, Green, Yellow, etc.)
        public string bloon;

        // Time when this group starts spawning (in milliseconds)
        public float start;

        // Time when this group finishes spawning (in milliseconds)
        public float end;

        // Total number of bloons in this group
        public int count;

        // Optional group name
        public string name = "";
    }

    // List of all groups in this round
    public List<BloonGroup> groups = new List<BloonGroup>()
    {
        new BloonGroup()
        {
            bloon = "Red",
            start = 0f,
            end = 1200f,
            count = 60
        },

        new BloonGroup()
        {
            bloon = "Blue",
            start = 1200f,
            end = 1920f,
            count = 45
        },

        new BloonGroup()
        {
            bloon = "Green",
            start = 1920f,
            end = 3120f,
            count = 45
        },

        new BloonGroup()
        {
            bloon = "Yellow",
            start = 3120f,
            end = 4320f,
            count = 35
        }
    };

    /// <summary>
    /// Returns the total number of bloons in this round.
    /// </summary>
    public int GetTotalBloons()
    {
        int total = 0;

        foreach (BloonGroup group in groups)
        {
            total += group.count;
        }

        return total;
    }

    /// <summary>
    /// Returns the total round duration in milliseconds.
    /// </summary>
    public float GetRoundDuration()
    {
        if (groups.Count == 0)
            return 0f;

        return groups[groups.Count - 1].end;
    }

    /// <summary>
    /// Prints round information to the Unity Console.
    /// </summary>
    public void PrintRoundInfo()
    {
        Debug.Log("Round Info");
        Debug.Log("Total Bloons: " + GetTotalBloons());
        Debug.Log("Round Duration: " + GetRoundDuration() + " ms");

        foreach (BloonGroup group in groups)
        {
            Debug.Log(
                "Bloon: " + group.bloon +
                " | Count: " + group.count +
                " | Start: " + group.start +
                " | End: " + group.end
            );
        }
    }

    // Automatically prints info when the game starts
    private void Start()
    {
        PrintRoundInfo();
    }
}