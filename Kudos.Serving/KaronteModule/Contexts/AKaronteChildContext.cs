namespace Kudos.Serving.KaronteModule.Contexts
{
    public abstract class AKaronteChildContext
    {
        public readonly KaronteContext KaronteContext;

        internal AKaronteChildContext(ref KaronteContext kc)
        {
            KaronteContext = kc;
        }
    }
}
