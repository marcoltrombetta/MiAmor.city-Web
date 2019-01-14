using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class EventPostMap : MiAmorEntityTypeConfiguration<EventPost>
    {
        public EventPostMap()
        {
            this.ToTable("EventPost");
            this.HasKey(a => a.Id);

        }
    }
}
