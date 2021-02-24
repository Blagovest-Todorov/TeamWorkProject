using System;
using System.Collections.Generic;
using System.Linq;

namespace Teams
{
    class Team
    {
        public Team()
        {
            Members = new List<string>();
        }

        public string Name { get; set; }
        public string Creator { get; set; }
        public List<string> Members { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            List<Team> teams = new List<Team>();

            for (int i = 0; i < n; i++)
            {
                string[] teamData = Console.ReadLine()
                    .Split('-');

                string creator = teamData[0];
                string teamName = teamData[1];

                Team existingTeam = GetTeamByName(teams, teamName);

                if (existingTeam != null)
                {
                    Console.WriteLine($"Team {teamName} was already created!");
                    continue;
                }

                if (CreatorExists(teams, creator))
                {
                    Console.WriteLine($"{creator} cannot create another team!");
                    continue;
                }

                Team team = new Team()
                {
                    Creator = creator, 
                    Name = teamName
                };

                teams.Add(team);
                Console.WriteLine($"Team {teamName} has been created by {creator}!");

            }

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "end of assignment")
                {
                    break;
                }
                string[] parts = line.Split("->");
                string user = parts[0];
                string teamName = parts[1];

                Team existingTeam = GetTeamByName(teams, teamName);
                if (existingTeam == null)
                {
                    Console.WriteLine($"Team {teamName} does not exist!");
                    continue;
                }

                if (IsMember(user, teams))
                {
                    Console.WriteLine($"Member {user} cannot join team {teamName}!");
                    continue;
                }

                existingTeam.Members.Add(user);
            }

            List<Team> sorted = teams
                .OrderByDescending(t => t.Members.Count)
                .ThenBy(t => t.Name)
                .ToList();

            foreach (Team team in sorted)
            {
                if (team.Members.Count == 0)
                {
                    break;
                }
                Console.WriteLine($"{team.Name}");
                Console.WriteLine($"- {team.Creator}");
                //Console.WriteLine($"--{team.Members}...");

                List<string> sortedMembers = team.Members
                    .OrderBy(m => m)
                    .ToList();

                foreach (string member in sortedMembers)
                {
                    Console.WriteLine($"-- {member}");
                }
            }

            List<Team> disbandedTeams = teams
                    .Where(t => t.Members.Count == 0)
                    .OrderBy(t => t.Name)
                    .ToList();

            Console.WriteLine($"Teams to disband:");

            foreach (var item in disbandedTeams)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static Team GetTeamByName(List<Team> teams, string teamName)
        {
            foreach (Team team in teams)
            {
                if (team.Name == teamName)
                {
                    return team;
                } 
            }

            return null; 
        }

        private static bool IsMember(string user, List<Team> teams)
        {
            foreach (Team team in teams)
            {
                if (team.Creator == user)
                {
                    return true;
                }

                foreach (var member in team.Members)
                {
                    if (user == member)
                    {
                        return true;
                    }
                }
            }           

            return false;
        }

        private static bool CreatorExists(List<Team> teams, string creator)
        {
            foreach (Team team in teams)
            {
                if (team.Creator == creator)
                {
                    return true;
                }
            }

            return false;
        }        
    }
}
