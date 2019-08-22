using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zony.Abp.WeiXin.Pay.Infrastructure
{
    public class SignatureGenerator : ISignatureGenerator
    {
        public Task<string> Generate(SortedDictionary<string, string> sortedDictionary)
        {
            throw new NotImplementedException();
        }
    }
}