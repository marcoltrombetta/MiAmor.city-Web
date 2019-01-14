using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class EventCommentMap : MiAmorEntityTypeConfiguration<EventComment>
    {
        public EventCommentMap()
        {
            this.ToTable("EventComment");
            this.HasKey(a => a.Id);

        }
    }
}
