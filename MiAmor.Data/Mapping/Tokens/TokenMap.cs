using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class TokenMap : MiAmorEntityTypeConfiguration<Token>
    {
        public TokenMap()
        {
            this.ToTable("Token");
            this.HasKey(c => c.Id);




        }
    }
}
