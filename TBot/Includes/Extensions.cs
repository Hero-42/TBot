using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Tbot.Model;

namespace Tbot.Includes {
	public static class Extensions {
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
			Random rnd = new();
			return source.OrderBy((item) => rnd.Next());
		}

		public static string FirstCharToUpper(this string input) {
			return input switch {
				null => throw new ArgumentNullException(nameof(input)),
				"" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
				_ => input.First().ToString().ToUpper() + input[1..]
			};
		}

		public static bool Has(this List<Celestial> celestials, Celestial celestial) {
			foreach (Celestial cel in celestials) {
				if (cel.HasCoords(celestial.Coordinate)) {
					return true;
				}
			}
			return false;
		}

		public static bool Has(this List<Celestial> celestials, Coordinate coords) {
			foreach (Celestial cel in celestials) {
				if (cel.HasCoords(coords)) {
					return true;
				}
			}
			return false;
		}

		public static IEnumerable<Celestial> Unique(this IEnumerable<Celestial> source) {
			return source.Distinct(new CelestialComparer()).ToList();
		}

		public class CelestialComparer : IEqualityComparer<Celestial> {
			public bool Equals(Celestial x, Celestial y) {
				return x.ID == y.ID;
			}

			public int GetHashCode([DisallowNull] Celestial obj) {
				return obj.ID;
			}
		}
	}
}
