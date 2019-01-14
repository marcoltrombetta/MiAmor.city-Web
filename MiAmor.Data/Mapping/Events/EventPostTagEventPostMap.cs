using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class EventPostTagEventPostMap : MiAmorEntityTypeConfiguration<EventPostTagEventPost>
    {
        public EventPostTagEventPostMap()
        {
            this.ToTable("EventPostTagEventPost");
            this.HasKey(a => a.Id);
        }
    }
}
