using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class EventPostTagMap : MiAmorEntityTypeConfiguration<EventPostTag>
    {
        public EventPostTagMap()
        {
            this.ToTable("EventPostTag");
            this.HasKey(a => a.Id);

        }
    }
}
