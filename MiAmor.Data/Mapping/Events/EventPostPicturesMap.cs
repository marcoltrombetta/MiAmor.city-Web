using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class EventPostPictureMap : MiAmorEntityTypeConfiguration<EventPostPicture>
    {
        public EventPostPictureMap()
        {
            this.ToTable("EventPostPicture");
            this.HasKey(a => a.Id);
        }
    }
}
