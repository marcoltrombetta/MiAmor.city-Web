using System.Data.Entity.ModelConfiguration;

namespace MiAmor.Data.Mapping
{
    public abstract class MiAmorEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected MiAmorEntityTypeConfiguration()
        {
            PostInitialize();
        }

        /// <summary>
        /// can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {
            
        }
    }
}