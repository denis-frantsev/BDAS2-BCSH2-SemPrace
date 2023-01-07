using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
	public class KategorieBezZboziViewModel
	{
		[Display(Name = "Id kategorie")]
		public short IdKategorie { get; set; }
		[Display(Name = "Nazev")]
		public string Nazev { get; set; }
		[Display(Name = "Popis")]
		public string Popis { get; set; }

	}
}
