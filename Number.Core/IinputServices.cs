using Number.Db;
using System.Collections.Generic;

namespace Number.Core
{
    public interface IinputServices
    {
       List<int> GetFobanacci(Input input);
       List<string> InputValidation(int firstIndex, int secondIndex);
    }
}
