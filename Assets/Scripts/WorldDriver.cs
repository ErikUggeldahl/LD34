using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class WorldDriver : MonoBehaviour
{
    [SerializeField]
    GameObject man;

    [SerializeField]
    Animator worldAnim;

    [SerializeField]
    Animator manAnim;

    [SerializeField]
    Animator diaryAnim;

    [SerializeField]
    Text responseField;

    [SerializeField]
    Text diaryField;

    enum WorldState
    {
        Off = 0,
        On,
        PausedToThink,
        Snake,
        SnakeBite,
        Title,
        SandWalk,
        Drink,
        Mirage,
        DanceOver,
        Blankout,
        Scorpion,
        Dead,
        Recover,
        Underground
    }

    WorldState state;

    void Start()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (scene == "Ch1")
            state = WorldState.Off;
        else if (scene == "Ch2")
        {
            state = WorldState.Title;
            worldAnim.SetInteger("WorldState", 6);
            manAnim.SetTrigger("SnakeWalk");
        }
        else if (scene == "Ch3")
        {
            state = WorldState.SandWalk;
            worldAnim.SetInteger("WorldState", 7);
            manAnim.SetTrigger("SandWalk");
        }
        else if (scene == "Ch4")
        {
            state = WorldState.Dead;
        }
    }

    public void DisplayResponse(string response)
    {
        if ((state == WorldState.Off || state == WorldState.Blankout || state == WorldState.Dead) && response != string.Empty)
            responseField.text = "SYSTEM: Unable to process command. System must be activated first.";
        else
        {
            responseField.text = response;
            HideDiary();
        }
    }

    public void DisplaySystemResponse(string response)
    {
        responseField.text = "SYSTEM: " + response;
    }

    public void TurnOn()
    {
        if (state == WorldState.Off)
            worldAnim.SetInteger("WorldState", 1);
        else if (state == WorldState.Blankout)
            worldAnim.SetInteger("WorldState", 12);
        else if (state == WorldState.Dead)
        {
            manAnim.SetTrigger("Recover");
            worldAnim.SetInteger("WorldState", 14);
        }
        else
            DisplaySystemResponse("A-CRNM is already on.");
    }

    public void OnTurnOnDone()
    {
        state = WorldState.On;

        WriteToDiary("Dearest diary,\nI have found myself, by one coincidence or another, in a strange land far to the east.");
    }

    public void Look()
    {
        switch (state)
        {
            case WorldState.On: DisplayResponse("It is hard to say where I am exactly. A desert perhaps?"); break;
            case WorldState.PausedToThink: DisplayResponse("I am not far from where I started."); break;
            case WorldState.Snake: DisplayResponse("This snake blocks my progress!"); break;
            case WorldState.SnakeBite: DisplayResponse("I have found a new travelling companion."); break;
            case WorldState.Title: DisplayResponse("Fascinating."); break;
            case WorldState.SandWalk:
            case WorldState.Drink: DisplayResponse("A most dreadful sandstorm engulfs the area!"); break;
            case WorldState.Mirage: DisplayResponse("A phantom of my past bars the path. It waves rhythmically."); break;
            case WorldState.DanceOver: DisplayResponse("The coast seems clear."); break;
            case WorldState.Scorpion: DisplayResponse("Death looks me in the face."); break;
            case WorldState.Recover: DisplayResponse("I appear to be underground. And covered in blood. Hopefully not mine."); break;
            case WorldState.Underground: DisplayResponse("Fascinating. It is a large underground structure."); break;
            default: DisplayResponse(" "); break;
        }
    }

    public void Speak()
    {
        switch (state)
        {
            case WorldState.Snake: DisplayResponse("It does not seem one for conversation."); break;
            case WorldState.SnakeBite:
            case WorldState.Title: DisplayResponse("My new companion has its mouth full."); break;
            case WorldState.SandWalk: DisplayResponse("My mouth would fill with sand!"); break;
            case WorldState.Drink: DisplayResponse("I would choke on my water."); break;
            case WorldState.Mirage: DisplayResponse("Hello, my full hale self from hours ago!"); break;
            case WorldState.Scorpion: DisplayResponse("I dare not."); break;
            default: DisplayResponse("The isn't a soul around to hear me."); break;
        }
    }

    public void Inventory()
    {
        switch (state)
        {
            case WorldState.On:
            case WorldState.PausedToThink:
            case WorldState.Snake: DisplayResponse("I have but a a whip and my canteen of water."); break;
            case WorldState.SnakeBite:
            case WorldState.Title: DisplayResponse("A whip and my canteen of water. And a snake."); break;
            case WorldState.SandWalk: DisplayResponse("A whip, my canteen of water, a snake, and all the sand I can carry."); break;
            case WorldState.Drink:
            case WorldState.Mirage:
            case WorldState.DanceOver: DisplayResponse("A whip, my now empty canteen of water, a snake, and all the sand I can carry."); break;
            case WorldState.Scorpion: DisplayResponse("Does it matter?"); break;
            case WorldState.Recover:
            case WorldState.Underground: DisplayResponse("I have nary but the clothes on my back, if even!"); break;

            default: DisplayResponse(" "); break;
        }
    }

    public void Hint()
    {
        switch (state)
        {
            case WorldState.On: DisplayResponse("I must travel eastward. I am compelled."); break;
            case WorldState.PausedToThink: DisplayResponse("I should continue east."); break;
            case WorldState.Snake: DisplayResponse("Maybe I can use something to scare the creature."); break;
            case WorldState.SnakeBite: DisplayResponse("I must continue eastward. With my snake."); break;
            case WorldState.Title: DisplayResponse("I should just play it cool."); break;
            case WorldState.SandWalk: DisplayResponse("It behooves me to drink."); break;
            case WorldState.Drink: DisplayResponse("About time to carry on."); break;
            case WorldState.Mirage: DisplayResponse("It's almost as if it looks for a dance partner."); break;
            case WorldState.DanceOver: DisplayResponse("Trepidly forward I go."); break;
            case WorldState.Scorpion: DisplayResponse("This is the end. I should give up."); break;
            case WorldState.Recover: DisplayResponse("Where else but east."); break;
            case WorldState.Underground: DisplayResponse("What could help me now?"); break;
            default: DisplayResponse(" "); break;
        }
    }

    public void Walk()
    {
        if (state == WorldState.On)
        {
            worldAnim.SetInteger("WorldState", 2);
            manAnim.SetTrigger("Walk");
        }
        else if (state == WorldState.PausedToThink)
        {
            worldAnim.SetInteger("WorldState", 3);
            manAnim.SetTrigger("Walk");
        }
        else if (state == WorldState.Snake)
            DisplayResponse("I am quite worried of being bitten!");
        else if (state == WorldState.SnakeBite)
        {
            worldAnim.SetInteger("WorldState", 5);
            manAnim.SetTrigger("Walk");
            WriteToDiary("From that moment, we traveled together, East. Always... into the east.");
        }
        else if (state == WorldState.Title)
            DisplayResponse("Already on it.");
        else if (state == WorldState.SandWalk)
            DisplayResponse("I thirst!");
        else if (state == WorldState.Drink)
        {
            state = WorldState.Mirage;
            worldAnim.SetInteger("WorldState", 9);
            manAnim.SetTrigger("Idle");
            WriteToDiary("What trickery is this? I was beset by my own form, hale as could be.");
        }
        else if (state == WorldState.Mirage)
            DisplayResponse("I cannot pass. My phantom bars my path. It moves oddly.");
        else if (state == WorldState.DanceOver)
        {
            worldAnim.SetInteger("WorldState", 11);
            manAnim.SetTrigger("Walk");
            WriteToDiary("My ghost felled, with heavy feet I carried onward.");
        }
        else if (state == WorldState.Scorpion)
            DisplayResponse("Not a chance.");
        else if (state == WorldState.Recover)
        {
            state = WorldState.Underground;
            worldAnim.SetInteger("WorldState", 15);
            manAnim.SetTrigger("Walk");
        }
        else
            DisplayResponse(" ");
    }

    public void OnPauseToThink()
    {
        state = WorldState.PausedToThink;
        manAnim.SetTrigger("Idle");
        WriteToDiary("Only now do I stop to question the health of my mind. Indeed, upon my return I must consider company with lunatics. For now I steel myself to continue. To the east. Always to the east!");
    }

    public void OnSnake()
    {
        state = WorldState.Snake;
        manAnim.SetTrigger("Idle");
        WriteToDiary("It is not long before I encounter a serpent. Perhaps it is the Serpent himself, come to taunt me. From the shape of his skull and the twist of his coils, I recall with confidence his dangerous potential.");
    }

    public void UseWhip()
    {
        if (state == WorldState.Snake)
            manAnim.SetTrigger("Whip");
        else if (state == WorldState.Mirage)
            DisplayResponse("It will do no good against this foe.");
        else if (state == WorldState.Scorpion)
            DisplayResponse("One does not simply whip into submission death itself.");
        else if (state < WorldState.Dead)
            DisplayResponse("I should keep it at my side for when I really need it.");
        else
            DisplayResponse("I seem to have misplaced it. Woe, oh woe!");
    }

    public void OnWhip()
    {
        worldAnim.SetInteger("WorldState", 4);
    }

    public void OnSnakeBite()
    {
        state = WorldState.SnakeBite;
        manAnim.SetTrigger("SnakeBite");
        WriteToDiary("Only a bite to to the boot. No deterrent!");
    }

    public void OnFadeOut()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (scene == "Ch1")
            SceneManager.LoadScene("Ch2");
        else if (scene == "Ch2")
            SceneManager.LoadScene("Ch3");
    }

    public void OnTitleDiary()
    {
        WriteToDiary("Oh dear. What have I gotten myself into?");
    }

    public void OnSandWalkDone()
    {
        manAnim.SetTrigger("Idle");
        WriteToDiary("How horrible. I have become simply too thirsty to continue my journey. Perhaps this is the end. These are my last words.");
    }

    public void Drink()
    {
        if (state == WorldState.SandWalk)
        {
            state = WorldState.Drink;
            manAnim.SetTrigger("Drink");
            WriteToDiary("I reached for my canteen. What relief I had not drank from it earlier.");
        }
        else if (state < WorldState.SandWalk)
            DisplayResponse("Now is not the time for drinking.");
        else if (state == WorldState.Scorpion)
            DisplayResponse("I could use a stiff drink.");
        else
            DisplayResponse("I have drank myself dry. Tragedy has struck.");
    }

    public void Dance()
    {
        if (state == WorldState.Mirage)
        {
            worldAnim.SetInteger("WorldState", 10);
            manAnim.SetTrigger("Dance");
        }
        else if (state < WorldState.Mirage)
            DisplayResponse("Left feet. I have two of them.");
        else if (state == WorldState.Scorpion)
            DisplayResponse("I dare not even breathe.");
        else
            DisplayResponse("The time for dancing is behind me.");
    }

    public void OnDanceDone()
    {
        state = WorldState.DanceOver;
        manAnim.SetTrigger("Idle");
    }

    public void OnBlankout()
    {
        state = WorldState.Blankout;
        manAnim.Stop();
    }

    public void OnScorpion()
    {
        state = WorldState.Scorpion;
        WriteToDiary("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }

    public void Die()
    {
        if (state == WorldState.Scorpion)
        {
            worldAnim.SetInteger("WorldState", 13);
            WriteToDiary("Lo I encounter a beast more nefarious than the last. What can one man do against such wretched villainy? Perish.");
        }
        else if (state < WorldState.Scorpion)
            DisplayResponse("Morbid. I must not give up so easily. This journey is not over.");
        else
            DisplayResponse("Not now. Not when I have come this far.");
    }

    public void OnDeath()
    {
        SceneManager.LoadScene("Ch4");
    }

    public void OnRecover()
    {
        state = WorldState.Recover;
        WriteToDiary("My dear diary... I've really done it now. I appear to be underground. My clothes are in tatters. And whence all this blood?!");
    }

    public void OnGameFinished()
    {
        SceneManager.LoadScene("Ch1");
    }

    void WriteToDiary(string entry)
    {
        diaryField.text = entry;
        diaryAnim.SetTrigger("RevealDiary");
    }

    void HideDiary()
    {
        diaryField.text = string.Empty;
        diaryAnim.SetTrigger("HideDiary");
    }
}
