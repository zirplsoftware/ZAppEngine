using System;

namespace Zirpl.AppEngine.Stats.EloRating
{
    public class EloRatingEngine
    {
        public EloRatingEngine()
        {
            this.KValue = 120;
            this.PowerOfTenFactor = 400;
            this.ChangeDigits = 0;
        }

        public double KValue { get; set; }
        public double PowerOfTenFactor { get; set; }
        public int ChangeDigits { get; set; }

        public EloMatchResult Calculate(EloMatch match)
        {
            EloMatchResult result = new EloMatchResult();

            // http://en.wikipedia.org/wiki/Elo_rating_system

            // Ea = 1 / (1 + 10 ^ ((Rb - Ra) / Pf))     // Expected Score
            // Pa = K * (Sa - Ea)                       // Point change
            // R'a = Ra + Pa                            // New Rating

            // Eb = 1 / (1 + 10 ^ ((Ra - Rb) / Pf))     // Expected Score
            // Pb = K * (Sb - Eb)                       // Point change
            // R'b = Rb + Pb                            // New Rating

            double E1 = 1 / (1 + Math.Pow(10, (match.Team2CurrentRating - match.Team1CurrentRating) / this.PowerOfTenFactor));
            //double E2 = 1 / (1 + Math.Pow(10, (match.Team1CurrentRating - match.Team2CurrentRating) / this.PowerOfTenFactor));
            double E2 = (double)1 - E1;

            // Win gets 1, tie gets .5, loss gets 0
            double S1 = match.Team1MatchScore.Equals(match.Team2MatchScore)
                            ? .5
                            : (match.Team1MatchScore > match.Team2MatchScore ? 1 : 0);
            double S2 = match.Team1MatchScore.Equals(match.Team2MatchScore)
                            ? .5
                            : (match.Team2MatchScore > match.Team1MatchScore ? 1 : 0);

            double P1 = Math.Round(this.KValue*(S1 - E1), this.ChangeDigits);
            //double P2 = this.KValue*(S2 - E2);
            double P2 = (double) -1*P1;

            double R1New = match.Team1CurrentRating + P1;
            double R2New = match.Team2CurrentRating + P2;

            result.Team1NewRating = R1New;
            result.Team2NewRating = R2New;
            result.Team1MatchPoints = P1;
            result.Team2MatchPoints = P2;

            // HOW TO USE: (but this test actually fails)
            //    EloRating1 eloRating = new EloRating1(1500, 100, 0, 1);
            //    EloRatingEngine rating = new EloRatingEngine();
            //    rating.KValue = 120;
            //    rating.PowerOfTenFactor = 400;
            //    EloMatch match = new EloMatch()
            //                         {
            //                             Team1CurrentRating = 1500, 
            //                             Team2CurrentRating = 100, 
            //                             Team1MatchScore = 0, 
            //                             Team2MatchScore = 1
            //                         };
            //    EloMatchResult result = rating.Calculate(match);
            //    Assert.AreEqual(1560, result.Team1NewRating);
            //    Assert.AreEqual(1440, result.Team2NewRating);
            //    Assert.AreEqual(60, result.Team1MatchPoints);
            //    Assert.AreEqual(-60, result.Team2MatchPoints);

            return result;
        }
    }
}
