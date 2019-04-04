using System;

namespace WebCloudSystem.Bll.Services.Utils {
    public class ParserService : IParserService
    {
        public int ParseUserId(string userId)
        {
            return Int32.Parse(userId);
        }
    }
}