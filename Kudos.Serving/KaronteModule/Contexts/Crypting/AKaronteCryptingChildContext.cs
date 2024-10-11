using System;
namespace Kudos.Serving.KaronteModule.Contexts.Crypting
{
	public abstract class AKaronteCryptingChildContext
	{
		public readonly KaronteCryptingContext KaronteCryptingContext;

		internal AKaronteCryptingChildContext(ref KaronteCryptingContext kcc)
		{
			KaronteCryptingContext = kcc;
        }
	}
}

