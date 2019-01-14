using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;

namespace MiAmor.Services
{
    public interface ITokenService : IEntityService<Token>
    {
        Token GetById(int Id);
        string CreateNewTokenForCustomer(Customer Customer, int ExpirationDaysType);
        Token GetByStrToken(string StrToken);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class TokenService : EntityService<Token>, ITokenService
    {
        #region Fields

        private ICustomerService _CustomerService;
        private ITokenSettingsService _TokenSettingsService;
        private IEncryption _Encryption;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public TokenService(ICustomerService CustomerService, IContext context, ITokenSettingsService TokenSettingsService, IEncryption Encryption)
            : base(context)
        {
            _CustomerService = CustomerService;
            _Encryption = Encryption;
            _TokenSettingsService = TokenSettingsService;
            _context = context;
            _dbset = _context.Set<Token>();

        }

        #endregion

        #region Methods

        public Token GetByStrToken(string StrToken)
        {
            string TmpStrTokenToValidate = StrToken.Replace("Bearer ", "");
            Token Token = (from t in _dbset
                           where t.EncryptedToken == TmpStrTokenToValidate
                           select t).FirstOrDefault();
            return Token;
        }

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Token GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public string CreateNewTokenForCustomer(Customer Customer, int DaysToExpire)
        {
            
            Token ReturnToken = CreateAndGetToken(DaysToExpire);
            Customer.TokenId = ReturnToken.Id;
            _CustomerService.Update(Customer);
            return ReturnToken.EncryptedToken;
        }
        #endregion
        #region utilities
        private Token CreateAndGetToken(int ExpirationDaysType)
        {
            TokenSettings TS = _TokenSettingsService.GetById(1);
            int DaysToExpire = 1;
            if (ExpirationDaysType == 0)
                DaysToExpire = TS.ExpirationDaysNull;
            if (ExpirationDaysType == 1)
                DaysToExpire = TS.ExpirationDaysShort;
            if (ExpirationDaysType == 2)
                DaysToExpire = TS.ExpirationDaysLong;
            //Encryption Enc = new Encryption();
            DateTime DT = DateTime.Now;
            string StrEnc = DT.Day.ToString() + DT.Month.ToString() + DT.Year.ToString() + DT.Minute.ToString() + DT.Second.ToString() + DT.Millisecond.ToString();
            StrEnc = _Encryption.Encrypt(StrEnc);
            Token ReturnToken = new Token();
            ReturnToken.EncryptedToken = StrEnc.Trim();
            ReturnToken.ExpirationDate = DT.AddDays(DaysToExpire);
            ReturnToken.DateCreated = DT;
            ReturnToken.TokenType = 1;
            _dbset.Add(ReturnToken);
            _context.SaveChanges();
            ReturnToken.EncryptedToken = DT.Day.ToString("00") + StrEnc + DT.Month.ToString("00");
            return ReturnToken;

        }
        #endregion
    }

}
