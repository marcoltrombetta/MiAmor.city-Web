using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class TokenTypeMap : MiAmorEntityTypeConfiguration<TokenType>
    {
        public TokenTypeMap()
        {
            this.ToTable("TokenType");
            this.HasKey(c => c.Id);

        }
    }
}
