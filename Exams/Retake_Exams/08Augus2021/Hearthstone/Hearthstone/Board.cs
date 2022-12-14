using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board : IBoard
{
    private Dictionary<string, Card> deck = new Dictionary<string, Card>();

    public bool Contains(string name)
    {
        return deck.ContainsKey(name);
    }

    public int Count()
    {
        return deck.Count;
    }

    public void Draw(Card card)
    {
        if (deck.ContainsKey(card.Name))
        {
            throw new ArgumentException();
        }

        deck.Add(card.Name, card);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        return deck.Values.Where(card => card.Score >= start && card.Score <= end)
            .OrderByDescending(card => card.Level);
    }

    public void Heal(int health)
    {
        deck.Values.OrderBy(c => c.Health).FirstOrDefault().Health += health;
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        return deck.Values
            .Where(c => c.Name.StartsWith(prefix))
            .OrderBy(c => string.Join("", c.Name.Reverse()))
            .ThenBy(c => c.Level);
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        if (!deck.ContainsKey(attackerCardName) || !deck.ContainsKey(attackedCardName))
        {
            throw new ArgumentException();
        }

        var attacker = deck[attackerCardName];
        var defender = deck[attackedCardName];

        if (attacker.Level != defender.Level)
        {
            throw new ArgumentException();
        }

        if (defender.Health <= 0 || attacker.Health <= 0)
        {
            return;
        }

        defender.Health -= attacker.Damage;

        if (defender.Health <= 0)
        {
            attacker.Score += defender.Level;
        }
    }

    public void Remove(string name)
    {
        if (!deck.ContainsKey(name))
        {
            throw new ArgumentException();
        }

        deck.Remove(name);
    }

    public void RemoveDeath()
    {
        deck = deck
                .Where(kvp => kvp.Value.Health > 0)
                .ToDictionary(k => k.Key, v => v.Value);
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        return deck.Values.Where(card => card.Level == level).OrderByDescending(card => card.Score);
    }
}