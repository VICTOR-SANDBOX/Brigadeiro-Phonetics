using System.Collections.Generic;
using System.Linq;
using OpenUtau.Api;
using OpenUtau.Core.G2p;

namespace OpenUtau.Plugin.Builtin;

[Phonemizer("Portuguese CVVX Phonemizer", "PT-BR CVVX", "xiao", "PT")]
public class PortugueseCVVXPhonemizer : SyllableBasedPhonemizer
{
    private class PortugueseCVVXG2p : IG2p
    {
        private readonly HashSet<string> vowels;
        private readonly HashSet<string> symbols;
        private readonly string[] validSymbols;

        public PortugueseCVVXG2p(string[] vowels, string[] consonants, Dictionary<string, string> replacements)
        {
            this.vowels = new HashSet<string>(vowels);
            var allSymbols = vowels.Concat(consonants).Concat(replacements.Keys).Distinct();
            this.symbols = new HashSet<string>(allSymbols);
            this.validSymbols = allSymbols.OrderByDescending(s => s.Length).ToArray();
        }

        public bool IsValidSymbol(string symbol)
        {
            return symbols.Contains(symbol);
        }

        public bool IsVowel(string symbol)
        {
            return vowels.Contains(symbol);
        }

        public bool IsGlide(string symbol)
        {
            return false;
        }

        public string[] Query(string text)
        {
            var phonemes = Split(text);
            return phonemes ?? new string[0];
        }

        private string[] Split(string text)
        {
            List<string> result = new List<string>();
            string remaining = text;
            while (remaining.Length > 0)
            {
                bool found = false;
                foreach (string symbol in validSymbols)
                {
                    if (remaining.StartsWith(symbol))
                    {
                        result.Add(symbol);
                        remaining = remaining.Substring(symbol.Length);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    remaining = remaining.Substring(1);
                }
            }
            return result.ToArray();
        }

        public string[] UnpackHint(string hint, char separator = ' ')
        {
            return hint.Split(separator);
        }
    }

	private readonly string[] vowels = "a,e,i,o,u,@,7,1,0,Q,X,V".Split(",");
	private readonly string[] consonants = "p,b,t,d,k,g,f,v,s,z,x,j,m,n,nh,l,lh,r,rr,tch,dj".Split(",");
	private readonly Dictionary<string, string> dictionaryReplacements = new Dictionary<string, string>();

	public PortugueseCVVXPhonemizer()
	{
		var rawReplacements = new Dictionary<string, string[]>
		{
			{ "@", new[] { "a~", "A", "6", "am", "an", "6~", "ã" } },
			{ "7", new[] { "e~", "em", "en", "ẽ" } },
			{ "1", new[] { "i~", "im", "in", "I", "ĩ" } },
			{ "0", new[] { "o~", "om", "on", "õ" } },
			{ "Q", new[] { "u~", "um", "un", "U", "ũ" } },
			{ "X", new[] { "E", "3", "é", "e'" } },
			{ "V", new[] { "O", "9", "ó", "o'" } },
			{ "x", new[] { "S" } },
			{ "tch", new[] { "tS" } },
			{ "dj", new[] { "dZ" } },
			{ "j", new[] { "Z" } },
			{ "nh", new[] { "J" } },
			{ "lh", new[] { "L" } },
			{ "rr", new[] { "R", "X" } },
			{ "u", new[] { "w", "w~" } },
			{ "i", new[] { "j", "j~" } }
		};

		foreach (var kvp in rawReplacements)
		{
			foreach (string alias in kvp.Value)
			{
				if (!dictionaryReplacements.ContainsKey(alias))
				{
					dictionaryReplacements.Add(alias, kvp.Key);
				}
			}
		}
	}

	protected override string[] GetVowels() => vowels;
	protected override string[] GetConsonants() => consonants;
	protected override string GetDictionaryName() => "";
	protected override Dictionary<string, string> GetDictionaryPhonemesReplacement() => dictionaryReplacements;

	protected override IG2p LoadBaseDictionary()
	{
		return new PortugueseCVVXG2p(vowels, consonants, dictionaryReplacements);
	}

	protected override List<string> ProcessSyllable(Syllable syllable)
	{
		string prevV = syllable.prevV;
		string[] cc = syllable.cc;
		string v = syllable.v;
		List<string> list = new List<string>();

		if (syllable.IsStartingV)
		{
			list.Add($"- {v}");
		}
		else if (syllable.IsVV)
		{
			if (!CanMakeAliasExtension(syllable))
			{
                HandleVV(prevV, v, syllable.vowelTone, list);
			}
		}
		else if (syllable.IsStartingCVWithOneConsonant)
		{
            list.Add($"{cc.Last()}{v}");
		}
		else if (syllable.IsStartingCVWithMoreThanOneConsonant)
		{
			if (cc.Last() == "r" || cc.Last() == "l")
			{
				for (int i = 0; i < cc.Length - 1; i++)
				{
					list.Add($"{cc[i]}e");
				}
			}
            list.Add($"{cc.Last()}{v}");
		}
		else
		{
            HandleVC(prevV, cc, v, syllable.tone, list);
		}

		return list;
	}

    private void HandleVV(string prevV, string v, int tone, List<string> list)
    {
        if (prevV == "i" && v != "i")
        {
            if (HasOto($"y{v}", tone))
            {
                list.Add($"y{v}");
                return;
            }
        }

        string text = $"{prevV} {v}";
        if (HasOto(text, tone))
        {
            list.Add(text);
        }
        else if (v == "u" || v == "o")
        {
            list.Add($"{prevV} w");
        }
        else if (v == "i" || v == "e")
        {
            list.Add($"{prevV} y");
        }
        else
        {
            list.Add(v);
        }
    }

    private void HandleVC(string prevV, string[] cc, string v, int tone, List<string> list)
    {
        string c = cc[0];
        if (c == "dj")
        {
            c = "d";
        }
        if (c == "tch")
        {
            c = "t";
        }
        string vc = $"{prevV} {c}";
        if (HasOto(vc, tone))
        {
            list.Add(vc);
        }

        string cv = $"{cc.Last()}{v}";
        if (cc.Length > 1 && (cc.Last() == "r" || cc.Last() == "l"))
        {
            for (int i = 0; i < cc.Length - 1; i++)
            {
                string ce = $"{cc[i]}e";
                if (HasOto(ce, tone))
                {
                    list.Add(ce);
                }
            }
        }
        list.Add(cv);
    }

	protected override List<string> ProcessEnding(Ending ending)
	{
		List<string> list = new List<string>();

		if (ending.IsEndingV)
		{

            return list;
		}

        string v_dash = $"{ending.prevV} -";
        if (HasOto(v_dash, ending.tone))
        {
            list.Add(v_dash);
        }

		return list;
	}

	protected override double GetTransitionBasicLengthMs(string alias = "")
	{
		if (alias.StartsWith("r") || alias.EndsWith(" r"))
		{
			return base.GetTransitionBasicLengthMs() * 0.50;
		}

		return base.GetTransitionBasicLengthMs();
	}
}
