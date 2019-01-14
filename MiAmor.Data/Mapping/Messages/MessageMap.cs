using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class MessageMap : MiAmorEntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            this.ToTable("Message");
            this.HasKey(a => a.Id);

        }
    }
}
