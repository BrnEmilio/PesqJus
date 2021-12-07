using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codeyes.msc.each.Models
{
    public class Mapper
    {
        public Report Parse(Analytics analytics)
        {
            return new Report(analytics);
        }

        public async IAsyncEnumerable<List<Analytics>> GetList(IAsyncEnumerable<Analytics> asyncEnumerable)
        {
            yield return await AsyncEnumerable.ToListAsync<Analytics>(asyncEnumerable);
        }
    }
}
