﻿using AmeisenBotX.Core.Character.Inventory.Objects;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AmeisenBotX.Core.Character.Comparators.Objects
{
    public class GearscoreFactory
    {
        public GearscoreFactory(Dictionary<string, double> statMultiplicators)
        {
            StatMultiplicators = statMultiplicators;
        }

        private Dictionary<string, double> StatMultiplicators { get; }

        public double Calculate(IWowItem item)
        {
            double score = 0;

            foreach (KeyValuePair<string, double> keyValuePair in StatMultiplicators)
            {
                if (item.Stats.TryGetValue(keyValuePair.Key, out string stat))
                {
                    if ((stat.Contains('.') || stat.Contains(',')) && double.TryParse(stat, NumberStyles.Any, CultureInfo.InvariantCulture, out double statDoubleValue))
                    {
                        score += statDoubleValue * keyValuePair.Value;
                    }
                    else if (int.TryParse(stat, out int statIntValue))
                    {
                        score += statIntValue * keyValuePair.Value;
                    }
                }
            }

            return score;
        }
    }
}