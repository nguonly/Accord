/*
 * Criado por SharpDevelop.
 * Usuário: Caio
 * Data: 25/7/2009
 * Hora: 11:35
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;

namespace Accord.Music
{
	/// <summary>
	/// Description of TimeSignature.
	/// 
	/// </summary>
	/// <remarks>
	/// Currently only supports rational signatures.
	/// </remarks>
	public class TimeSignature
	{
		public int BeatCount { get; set;}
		public Duration BeatUnit { get; set; }
		
		public TimeSignature(int beats, Duration noteValue)
		{
			BeatCount = beats;
			BeatUnit = noteValue;
		}
		
		public TimeSignature(string value)
		{
			String[] s = value.Split('/');
			BeatCount = int.Parse(s[0]);
			BeatUnit = (Duration)Enum.Parse(typeof(Duration), s[1]);
		}
	}
}
