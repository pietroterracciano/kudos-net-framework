namespace Kudos.Databases.ORMs.GefyraModule.Entities
{
    public abstract class AGefyraEntity
    {
        private readonly object _oLock;

        private string _s4SQLCommand, _s4SQLCommandAsPrefix;

        internal AGefyraEntity()
        {
            _oLock = new object();
        }

        protected internal object GetLock()
        {
            return _oLock;
        }

        public string Prepare4SQLCommandPrefix()
        {
            lock (_oLock)
            {
                if (_s4SQLCommandAsPrefix != null) return _s4SQLCommandAsPrefix;
                return _s4SQLCommandAsPrefix = OnPrepare4SQLCommandAsPrefix();
            }
        }

        public string Prepare4SQLCommand()
        {
            lock (_oLock)
            {
                if (_s4SQLCommand != null) return _s4SQLCommand;
                return _s4SQLCommand = OnPrepare4SQLCommand();
            }
        }

        protected internal abstract string OnPrepare4SQLCommand();
        protected internal abstract string OnPrepare4SQLCommandAsPrefix();
    }
}