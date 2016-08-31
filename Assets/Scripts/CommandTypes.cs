using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class RandomString
{
    public static string Get(IList<string> strings)
    {
        var index = Random.Range(0, strings.Count);
        return strings[index];
    }
}

interface ICommand
{
    bool Matches(string text);
    void Execute(string text, WorldDriver driver);
}

abstract class SimpleRegex : ICommand
{
    protected IList<string> commands;

    public SimpleRegex(IList<string> commands)
    {
        this.commands = commands;
    }

    public bool Matches(string text)
    {
        foreach (var command in commands)
        {
            var match = Regex.Match(text, command);
            if (match.Success)
                return true;
        }

        return false;
    }

    public abstract void Execute(string text, WorldDriver driver);
}

class SimpleResponse : SimpleRegex
{
    protected IList<string> responses;

    public SimpleResponse(IList<string> commands, IList<string> responses) : base(commands)
    {
        this.responses = responses;
    }

    public override void Execute(string text, WorldDriver driver)
    {
        driver.DisplayResponse(RandomString.Get(responses));
    }
}

class IncrementalResponse : SimpleResponse
{
    int count = 0;
    string finalText;

    public IncrementalResponse(IList<string> commands, IList<string> responses, string finalText = "...") : base(commands, responses)
    {
        this.finalText = finalText;
    }

    public override void Execute(string text, WorldDriver driver)
    {
        var response = count < responses.Count ? responses[count] : finalText;
        count++;
        driver.DisplayResponse(response);
    }
}

class SimpleWorldCommand : SimpleRegex
{
    public delegate void WorldAction();

    WorldAction action;

    public SimpleWorldCommand(IList<string> commands, WorldAction action) : base(commands)
    {
        this.action = action;
    }

    public override void Execute(string text, WorldDriver driver)
    {
        driver.DisplayResponse(string.Empty);
        action();
    }
}
