using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    private Dictionary<int, Competitor> competitors = new Dictionary<int, Competitor>();
    private Dictionary<int, Competition> competitions = new Dictionary<int, Competition>();

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (this.competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        var competition = new Competition(name, id, participantsLimit);
        this.competitions.Add(id,competition);
    }

    public void AddCompetitor(int id, string name)
    {
        if (this.competitors.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        this.competitors.Add(id, new Competitor(id, name));
    }

    public void Compete(int competitorId, int competitionId)
    {
        if (!this.competitors.ContainsKey(competitorId) || !this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }
        var competitor = this.competitors[competitorId];

        competitor.TotalScore += this.competitions[competitionId].Score;
        this.competitions[competitionId].Competitors.Add(competitor);
    }

    public int CompetitionsCount()
    {
        return this.competitions.Count;
    }

    public int CompetitorsCount()
    {
        return this.competitors.Count;
    }

    // Performance tests
    public bool Contains(int competitionId, Competitor comp)
    {
        if (!this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }
        
        var res = this.competitions[competitionId].Competitors.Where(c => c.Id == comp.Id);

        if (res != null)
        {
           return true;
        }

        return false;
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        if (!this.competitors.ContainsKey(competitorId) || !this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        var competition = this.competitions[competitionId];
        var competitor = this.competitors[competitorId];

        competitor.TotalScore -= competition.Score;
        competition.Competitors.Remove(competitor);
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        var res = this.competitors.Values
            .Where(c => c.TotalScore > min && c.TotalScore <= max)
            .OrderBy(c => c.Id)
            .ToList();
        return res;
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        var competitor = this.competitors.Values.Where(c => c.Name == name);
        if (competitor.Count() == 0)
        {
            throw new ArgumentException();
        }

        return this.competitors.Values.Where(c => c.Name == name).OrderBy(c => c.Id);
    }

    public Competition GetCompetition(int id)
    {
        if (!this.competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        return this.competitions[id];
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        return this.competitors.Values
                .Where(c => c.Name.Length >= min && c.Name.Length <= max)
                .OrderBy(c => c.Id);
    }
}