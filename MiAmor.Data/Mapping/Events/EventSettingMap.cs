using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class EventSettingMap : MiAmorEntityTypeConfiguration<EventSettings>
    {
        public EventSettingMap()
        {
            this.ToTable("EventSettings");
            this.HasKey(a => a.Id);

        }
    }
}
