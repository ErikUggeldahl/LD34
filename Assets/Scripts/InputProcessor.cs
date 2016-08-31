using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class InputProcessor : MonoBehaviour
{
    [SerializeField]
    WorldDriver driver;

    IList<string> noResults = new List<string> { "This doesn't seem right.", "What was I talking about?", "I've confused myself." };
    IList<ICommand> commands;

    void Start()
    {
        commands = new List<ICommand>
        {
            //new SimpleResponse(new List<string>{ @"^(?:)" }, new List<string>{ "" }),

            // Phrases
            new SimpleResponse(new List<string>{ @"^(?:hello|hi|greet|welcome)" }, new List<string>{ "Er.. yes. Hello to myself!" }),
            new SimpleResponse(new List<string>{ @"^(?:bye|good(?:-| )?bye|dismiss|leave)" }, new List<string>{ "Good-bye - to me!" }),
            new SimpleResponse(new List<string>{ @"^(?:yes|yep|ok|okay|uh(?:-| )?h?uh)" }, new List<string>{ "Yep." }),
            new SimpleResponse(new List<string>{ @"^(?:no|nope|nuh(?:-| )?uh)" }, new List<string>{ "Or maybe yes?!" }),

            // Unix
            new SimpleResponse(new List<string>{ @"chmod" }, new List<string>{ "I do not think I have sufficient priviledge!" }),
            new SimpleResponse(new List<string>{ @"mkdir" }, new List<string>{ "Out of what?" }),
            new SimpleResponse(new List<string>{ @"cp" }, new List<string>{ "I do not have the materials." }),
            new SimpleResponse(new List<string>{ @"grep -r" }, new List<string>{ "I would be looking forever!" }),
            new IncrementalResponse(new List<string> { @"^(?:ls|ll)" }, new List<string> { "Pretend like there are files being displayed here.", "Seriously, nothing to see here!" }),

            // Information
            new SimpleResponse(new List<string>{ @"^(?:where|where am i|pwd)" }, new List<string>{ "I cannot say for certain. I must be a long way from home." }),
            new SimpleResponse(new List<string>{ @"^(?:who$|who am i)" }, new List<string>{ "A lost soul." }),
            new SimpleWorldCommand(new List<string> { @"^(?:look|l$|see|gaze|peer|find|grep)" }, driver.Look),
            new SimpleWorldCommand(new List<string> { @"^(?:hint|think|help|man|guide|how|info)" }, driver.Hint),

            // Simple actions
            new SimpleResponse(new List<string>{ @"^(?:thing|do the thing$|do$)" }, new List<string>{ "How delightfully vague." }),
            new SimpleResponse(new List<string>{ @"^(?:eat|snack|munch)" }, new List<string>{ "I am all out of food. Woe!" }),
            new SimpleWorldCommand(new List<string> { @"^(?:drink|sip|chug|(?:use )?canteen)" }, driver.Drink),
            new SimpleResponse(new List<string>{ @"^(?:sleep|snooze|nap|rest|lie|lie down)" }, new List<string>{ "If I do, I may never get up again." }),
            new SimpleWorldCommand(new List<string>{ @"^(?:dance|jig|do a jig|move oddly)" }, driver.Dance),
            new SimpleWorldCommand(new List<string>{ @"^(?:die|rm -rf /|lose|give up|game\s?over|quit|end|suicide|commit suicide|perish)" }, driver.Die),
            new SimpleResponse(new List<string>{ @"^(?:poop|sh.t|crap)" }, new List<string>{ "You must be confusing this with another game." }),

            // Tools
            new SimpleWorldCommand(new List<string> { @"^(?:inventory|inv$|i$|items?|gear)" }, driver.Inventory),
            new SimpleResponse(new List<string>{ @"^(?:use )?(?:map|compass|navigate)" }, new List<string>{ "How silly of me to be out here without navigation." }),
            new SimpleResponse(new List<string>{ @"^(?:use )?(?:watch|time)" }, new List<string>{ "My watch broke long ago. Dreadful affair." }),
            new SimpleResponse(new List<string>{ @"^(?:use )?(?:rope|cord)" }, new List<string>{ "I have not rope. But I do have a whip." }),
            new SimpleResponse(new List<string>{ @"^(?:use )?(?:hat|cap)" }, new List<string>{ "I see no use except for my head." }),
            new SimpleWorldCommand(new List<string>{ @"^(?:use )?(?:whip|weapon)" }, driver.UseWhip),
            new SimpleResponse(new List<string>{ @"^(?:take off|remove)" }, new List<string>{ "Best to keep everything on me." }),

            // Violence
            new SimpleResponse(new List<string>{ @"^(?:kill|rm|rmdir)" }, new List<string>{ "I'm a pacifist. I cannot stand the thought." }),
            new SimpleResponse(new List<string>{ @"(?:punch|kick|fight|attack|push)" }, new List<string>{ "A fighter I am not." }),
            new SimpleResponse(new List<string>{ @"^(?:shoot|use gun)" }, new List<string>{ "Alas, I haven't my gun." }),

            // Emotions
            new SimpleResponse(new List<string>{ @"^(?:laugh|chuckle)" }, new List<string>{ "There's always time for a hearty chuckle." }),
            new SimpleResponse(new List<string>{ @"^(?:cry|weep)" }, new List<string>{ "Must stay... strong!" }),
            new SimpleResponse(new List<string>{ @"^(?:smile|grin)" }, new List<string>{ ":)" }),
            new SimpleResponse(new List<string>{ @"^(?:f.ck|damn|god.?damn)" }, new List<string>{ "This isn't a time to lose my cool." }),
            new SimpleWorldCommand(new List<string>{ @"^(?:talk|speak|converse|yell|scream|sing)" }, driver.Speak),
            new SimpleResponse(new List<string>{ @"^(?:scare|intimidate|startle)" }, new List<string>{ "I am not terribly scary. But I do have a whip." }),

            // Movement
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv)$" }, new List<string>{ "Where? I can't just wander aimlessly." }),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) here", "here", "cd ." }, new List<string>{ "Here? There I am already!" }),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) home", "cd ~" }, new List<string>{ "Banish thoughts of home. I must continue." }),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) forward" }, new List<string>{ "Forward. Hmm. My map doesn't indicate forward. What cardinal direction is that?" }),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) (?:back|backward)", "cd (?:..|-)" }, new List<string>{ "I musn't entertain the idea. There is only east." }),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) around" }, new List<string>{ "Around? There is only east." }),
            new SimpleWorldCommand(new List<string> { @"^(?:walk|go|move|journey|travel|cd|mv) (?:east|right)" }, driver.Walk),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) (?:west|left)" }, new List<string>{ "Hrmm. There's nothing there for me now." }),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) north" }, new List<string>{ "What is.. north? There is only east, and sometimes west." }),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) south" }, new List<string>{ "South? What is south? I only know of east, and of west." }),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) up", "fly.*" }, new List<string>{ "Ah. Yes. If only I were a bird. But alas. I am not." }),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) down", "dig.*" }, new List<string>{ "But I am no mole. I would prefer to stay above ground!" }),
            new SimpleResponse(new List<string>{ @"^(?:walk|go|move|journey|travel|cd|mv) .*" }, new List<string>{ "Huh? Where is that?" }),
            new SimpleResponse(new List<string>{ @"^(?:jump|leap)" }, new List<string>{ "I'm afraid my poor knees forbid it." }),
            new SimpleResponse(new List<string>{ @"^(?:crawl|kneel)" }, new List<string>{ "I'd rather not soil my knees and hands." }),
            new SimpleResponse(new List<string>{ @"^(?:run|sprint)" }, new List<string>{ "Sounds exhausting." }),
            new SimpleResponse(new List<string>{ @"^(?:stop|halt|stay)" }, new List<string>{ "I cannot be stopped." }),
            new SimpleResponse(new List<string>{ @"^(?:explore|wander|venture)" }, new List<string>{ "Ah, so many places to go. But I must stay focused." }),

            // Game
            new SimpleResponse(new List<string>{ @"win" }, new List<string>{ "If only it were all so simple." }),
            new SimpleResponse(new List<string>{ @"cheat" }, new List<string>{ "Cheaters never prosper." }),
            new SimpleResponse(new List<string>{ @"skip" }, new List<string>{ "Life does not work that way." }),
            new SimpleResponse(new List<string>{ @"continue" }, new List<string>{ "Where to?" }),

            // Key actions
            new SimpleWorldCommand(new List<string> { @"^(?:turn on|on|activate|boot)" }, driver.TurnOn)
        };
    }

    public void Process(string text)
    {
        text = Sanitize(text);

        foreach (var command in commands)
        {
            if (command.Matches(text))
            {
                command.Execute(text, driver);
                return;
            }
        }

        driver.DisplayResponse(RandomString.Get(noResults));
    }

    string Sanitize(string text)
    {
        return text.Trim().ToLower();
    }
}
