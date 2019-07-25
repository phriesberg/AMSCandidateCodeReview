using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm
{
    public class Finder
    {
        private readonly List<Thing> _p;

        public Finder(List<Thing> p)
        {
            _p = p;
        }

        public F Find(FT ft)
        {
            F matchedReturn = new F();

            List<F> matchList = new List<F>();

            foreach (Thing person in _p)
            {
                Thing thisMatch = new Thing(); 
                F newMatchedPair = new F();

                // get closest or farthest future birthday match for each person
                thisMatch = ft.Equals(FT.One)
                    ? _p.Where(potentialMatch => potentialMatch.BirthDate > person.BirthDate).OrderBy(potentialMatch => potentialMatch.BirthDate).FirstOrDefault()
                    : _p.Where(potentialMatch => potentialMatch.BirthDate > person.BirthDate).OrderByDescending(potentialMatch => potentialMatch.BirthDate).FirstOrDefault();

                // add that match to list
                if (thisMatch!=null)
                {
                    newMatchedPair = new F() { P1 = person, P2 = thisMatch, D = person.BirthDate - thisMatch.BirthDate };
                    matchList.Add(newMatchedPair);
                }
            }

            if (matchList.Count > 0)
            {
                // get farthest or closest difference
                matchedReturn = ft.Equals(FT.One)
                                ? matchedReturn = (from matchedPair in matchList where matchedPair.D == matchList.Max(i => i.D) select matchedPair).FirstOrDefault()
                                : matchedReturn = (from matchedPair in matchList where matchedPair.D == matchList.Min(i => i.D) select matchedPair).FirstOrDefault();
            }

            return matchedReturn;   
        }
    }
}